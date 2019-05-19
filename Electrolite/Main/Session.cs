/*
Copyright (c) 2019 Integrative Software LLC
Created: 5/2019
Author: Pablo Carbonell
*/

using Electrolite.Adapters;
using Electrolite.Common.Main;
using Electrolite.Tools;
using JKang.IpcServiceFramework;
using System;
using System.Diagnostics;
using System.Globalization;
using System.Threading;
using System.Threading.Tasks;

namespace Electrolite.Main
{
    sealed class Session : ISession
    {
        static readonly Numerator _numerator = new Numerator();

        public Uri Url { get; }
        public ElectroliteOptions StartupOptions { get; }
        readonly string _pipeName, _endpointName;
        readonly IIpcServiceHost _host;
        readonly CancellationTokenSource _source;

        bool _running;
        Process _browser;

        public Session(Uri url, ElectroliteOptions options)
        {
            Url = url;
            StartupOptions = options;
            int pipeId = _numerator.Numerate();
            _pipeName = "ElectrolitePipe" + pipeId.ToString(CultureInfo.InvariantCulture);
            _endpointName = "ElectroliteEndpoint" + pipeId.ToString(CultureInfo.InvariantCulture);
            _host = BrowserHostBuilder.Build(this, _endpointName, _pipeName);
            _source = new CancellationTokenSource();
        }

        public event EventHandler<ClosingEventArgs> OnClosing;
        public event EventHandler OnReady;

        bool _disposed;

        public void Dispose()
        {
            if (!_disposed)
            {
                _disposed = true;
                Stop();
                _source.Dispose();
            }
        }

        private void Stop()
        {
            if (_running)
            {
                _running = false;
                _source.Cancel();
                if (!_browser.HasExited)
                {
                    _browser.Close();
                }
            }
        }

        public void NotifyReady()
        {
            OnReady?.Invoke(this, new EventArgs());
        }

        public ClosingResponse NotifyClosing()
        {
            var args = new ClosingEventArgs();
            OnClosing?.Invoke(this, args);
            return args.Response;
        }

        public async Task RunAsync(CancellationToken token = default)
        {
            _running = true;
            var localToken = _source.Token;
            if (token != CancellationToken.None)
            {
                token.Register(() => Stop());
            }
            _browser = LaunchBrowserProcess();
            _browser.Exited += Browser_Exited;
            await _host.RunAsync(localToken);
            _running = false;
        }

        private void Browser_Exited(object sender, EventArgs e)
        {
            Stop();
        }

        private Process LaunchBrowserProcess()
        {
            var adapter = PlatformAdapterFactory.CreateAdapter();
            return adapter.LaunchBrowser(_pipeName);
        }
    }
}

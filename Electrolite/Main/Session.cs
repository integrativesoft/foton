/*
Copyright (c) 2019 Integrative Software LLC
Created: 5/2019
Author: Pablo Carbonell
*/

using Electrolite.Adapters;
using Electrolite.Common.Main;
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
            int pipeId = Process.GetCurrentProcess().Id;
            _pipeName = "ElectrolitePipe_" + pipeId.ToString(CultureInfo.InvariantCulture);
            Console.WriteLine($"Pipe: {_pipeName}");
            _endpointName = "ElectroliteEndpoint_" + pipeId.ToString(CultureInfo.InvariantCulture);
            _host = BrowserHostBuilder.Build(this, _endpointName, _pipeName);
            _source = new CancellationTokenSource();
        }

        public event EventHandler OnClosing;
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
                if (BrowserIsOpen())
                {
                    _browser.Close();
                }
            }
        }

        private bool BrowserIsOpen()
        {
            return _browser != null && !_browser.HasExited;
        }

        public void NotifyReady()
        {
            OnReady?.Invoke(this, new EventArgs());
        }

        public void NotifyClosing()
        {
            OnClosing?.Invoke(this, new EventArgs());
        }

        public async Task RunAsync(CancellationToken token = default)
        {
            _running = true;
            var localToken = AggregateTokens(token);
            LaunchBrowser();
            await _host.RunAsync(localToken);
            _running = false;
        }

        private CancellationToken AggregateTokens(CancellationToken token)
        {
            var localToken = _source.Token;
            if (token != CancellationToken.None)
            {
                token.Register(() => Stop());
            }
            return localToken;
        }

        private void LaunchBrowser()
        {
            var adapter = PlatformAdapterFactory.CreateAdapter();
            _browser = adapter.LaunchBrowser(_pipeName);
            _browser.Exited += Browser_Exited;
        }

        private void Browser_Exited(object sender, EventArgs e)
        {
            Stop();
        }
    }
}

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
using System.Globalization;
using System.Threading.Tasks;

namespace Electrolite.Main
{
    sealed class Session : ISession
    {
        static readonly Numerator _numerator = new Numerator();

        readonly Uri _url;
        public ElectroliteOptions StartupOptions { get; }
        readonly string _pipeName, _endpointName;
        readonly IIpcServiceHost _host;

        public Session(Uri url, ElectroliteOptions options)
        {
            _url = url;
            StartupOptions = options;
            int pipeId = _numerator.Numerate();
            _pipeName = "ElectrolitePipe" + pipeId.ToString(CultureInfo.InvariantCulture);
            _endpointName = "ElectroliteEndpoint" + pipeId.ToString(CultureInfo.InvariantCulture);
            _host = BrowserHostBuilder.Build(this, _endpointName, _pipeName);
        }

        public event EventHandler<ClosingEventArgs> OnClosing;
        public event EventHandler OnReady;

        bool _disposed;

        public void Dispose()
        {
            if (!_disposed)
            {
                _disposed = true;

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

        public void Open()
        {
            LaunchBrowserProcess();
            _host.RunAsync();
        }

        public void RunBlocking()
        {
            LaunchBrowserProcess();
            _host.Run();
        }

        public async Task RunBlockingAsync()
        {
            LaunchBrowserProcess();
            await _host.RunAsync();
        }

        private void LaunchBrowserProcess()
        {
            var adapter = PlatformAdapterFactory.CreateAdapter();
            adapter.LaunchBrowser(_endpointName);
        }
    }
}

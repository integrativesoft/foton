/*
Copyright (c) 2019 Integrative Software LLC
Created: 5/2019
Author: Pablo Carbonell
*/

using Electrolite.Adapters;
using Electrolite.Common.Ipc;
using Electrolite.Common.Main;
using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;

namespace Electrolite.Main
{
    sealed class Session : ISession
    {
        public Uri Url { get; }
        public ElectroliteOptions StartupOptions { get; }
        readonly IpcPipeDuplex<IBrowserHost, IBrowserWindow> _duplex;
        readonly CancellationTokenSource _source;
        readonly string _serverPipe;

        bool _running;
        Process _browser;

        public Session(Uri url, ElectroliteOptions options)
        {
            Url = url;
            StartupOptions = options;
            int processId = Process.GetCurrentProcess().Id;
            _serverPipe = PlatformCommon.NameHostPipe(processId);
            _duplex = new IpcPipeDuplex<IBrowserHost, IBrowserWindow>(new IpcDuplexParameters<IBrowserHost>
            {
                ClientPipe = PlatformCommon.NameBrowserPipe(processId),
                ServerEndpoint = PlatformCommon.NameHostEndpoint(processId),
                ServerPipe = _serverPipe,
                ServerFactory = ((provider) => new BrowserHost(this))
            });
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
            await _duplex.RunAsync(localToken);
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
            _browser = adapter.LaunchBrowser(_serverPipe);
            _browser.Exited += Browser_Exited;
        }

        private void Browser_Exited(object sender, EventArgs e)
        {
            Stop();
        }
    }
}

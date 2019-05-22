/*
Copyright (c) 2019 Integrative Software LLC
Created: 5/2019
Author: Pablo Carbonell
*/

using Electrolite.Adapters;
using Electrolite.Common.Ipc;
using Electrolite.Common.Main;
using Nito.AsyncEx;
using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;

namespace Electrolite.Main
{
    sealed class IpcSession : ISession
    {
        public Uri Url { get; }
        public ElectroliteOptions StartupOptions { get; }
        readonly IPlatformAdapter _adapter;
        readonly IpcPipeDuplex<IBrowserHost, IBrowserWindow> _duplex;
        readonly CancellationTokenSource _source;

        bool _running;
        Process _browser;
        TaskCompletionSource<bool> _startWaiter;
        TaskCompletionSource<bool> _stopWaiter;

        public IpcSession(IPlatformAdapter adapter, Uri url, ElectroliteOptions options)
        {
            _adapter = adapter;
            Url = url;
            StartupOptions = options;
            int processId = Process.GetCurrentProcess().Id;
            _duplex = new IpcPipeDuplex<IBrowserHost, IBrowserWindow>(new IpcDuplexParameters<IBrowserHost>
            {
                ClientPipe = ElectroliteCommon.ElectroliteBrowser(processId),
                ServerEndpoint = ElectroliteCommon.ElectroliteHostEndpoint(processId),
                ServerPipe = ElectroliteCommon.ElectroliteHost(processId),
                ServerFactory = ((provider) => new BrowserHost(this))
            });
            _source = new CancellationTokenSource();
            _startWaiter = new TaskCompletionSource<bool>();
        }

        public event EventHandler OnClosing;
        public event EventHandler OnReady;
        public event EventHandler<BackgroundErrorEventArgs> BackgroundError;

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
                _startWaiter = new TaskCompletionSource<bool>();
                _stopWaiter.SetResult(true);
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

        public void Run()
        {
            AsyncContext.Run(async () => await RunAsync());
        }

        public void RunBackground(CancellationToken token = default)
        {
            Task.Run(async () => await RunAsyncCatch(token));
        }

        private async Task RunAsyncCatch(CancellationToken token = default)
        {
            try
            {
                await RunAsync(token);
            }
            catch (Exception e)
            {
                BackgroundError?.Invoke(this, new BackgroundErrorEventArgs
                {
                    Exception = e
                });
            }
        }

        public async Task RunAsync(CancellationToken token = default)
        {
            _running = true;
            _stopWaiter = new TaskCompletionSource<bool>();
            _startWaiter.SetResult(true);
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
            int id = Process.GetCurrentProcess().Id;
            _browser = _adapter.LaunchBrowser(id, SplashImagePath);
            _browser.Exited += Browser_Exited;
            BrowserProcessId = _browser.Id;
        }

        public int BrowserProcessId { get; private set; }
        public string SplashImagePath { get; set; }

        private void Browser_Exited(object sender, EventArgs e)
        {
            Stop();
        }

        public async Task ModifySettings(ElectroliteOptions options)
        {
            await _duplex.Client.OrderAsync(x => x.ModifyOptions(options));
        }

        public async Task WaitForShutdown()
        {
            if (!_running)
            {
                await _startWaiter.Task;
            }
            await _stopWaiter.Task;
        }
    }
}

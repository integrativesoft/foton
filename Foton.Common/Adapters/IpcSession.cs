/*
Copyright (c) 2019 Integrative Software LLC
Created: 5/2019
Author: Pablo Carbonell
*/

using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using Foton.Common.Ipc;
using Foton.Common.Main;
using Nito.AsyncEx;

namespace Foton.Common.Adapters
{
    public sealed class IpcSession : ISession
    {
        public Uri Url { get; }
        public ElectroliteOptions StartupOptions { get; }
        private readonly IIpcPlatformAdapter _adapter;
        private readonly IpcPipeDuplex<IBrowserHost, IBrowserWindow> _duplex;
        private readonly CancellationTokenSource _source;

        private bool _running;
        private Process _browser;
        private TaskCompletionSource<bool> _startWaiter;
        private TaskCompletionSource<bool> _stopWaiter;

        public IpcSession(IIpcPlatformAdapter adapter, Uri url, ElectroliteOptions options)
        {
            _adapter = adapter;
            Url = url;
            StartupOptions = options;
            var processId = Process.GetCurrentProcess().Id;
            _duplex = new IpcPipeDuplex<IBrowserHost, IBrowserWindow>(new IpcDuplexParameters<IBrowserHost>
            {
                ClientPipe = ElectroliteCommon.ElectroliteBrowser(processId),
                ServerEndpoint = ElectroliteCommon.ElectroliteHostEndpoint(processId),
                ServerPipe = ElectroliteCommon.ElectroliteHost(processId),
                ServerFactory = provider => new BrowserHost(this)
            });
            _source = new CancellationTokenSource();
            _startWaiter = new TaskCompletionSource<bool>();
        }

        public event EventHandler OnClosing;
        public event EventHandler OnReady;
        public event EventHandler<BackgroundErrorEventArgs> BackgroundError;

        private bool _disposed;

        public void Dispose()
        {
            if (_disposed) return;
            _disposed = true;
            Stop();
            _source.Dispose();
        }

        private void Stop()
        {
            if (!_running) return;
            _running = false;
            _source.Cancel();
            if (BrowserIsOpen())
            {
                _browser.Close();
            }
            _startWaiter = new TaskCompletionSource<bool>();
            _stopWaiter.SetResult(true);
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
            Task.Run(async () => await RunAsyncCatch(token), token);
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
            var id = Process.GetCurrentProcess().Id;
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

/*
Copyright (c) 2019 Integrative Software LLC
Created: 5/2019
Author: Pablo Carbonell
*/

using Electrolite.Core.Ipc;
using Electrolite.Core.Main;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Electrolite.Core.Adapters
{
    public interface ISession : IDisposable
    {
        event EventHandler OnClosing;
        event EventHandler OnReady;
        event EventHandler<BackgroundErrorEventArgs> BackgroundError;
        string SplashImagePath { get; set; }
        void Run();
        Task RunAsync(CancellationToken token = default);
        void RunBackground(CancellationToken token = default);
        Task ModifySettings(ElectroliteOptions options); // TODO: use independent properties
        int BrowserProcessId { get; }
        Task WaitForShutdown();
    }
}

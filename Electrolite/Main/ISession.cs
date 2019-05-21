/*
Copyright (c) 2019 Integrative Software LLC
Created: 5/2019
Author: Pablo Carbonell
*/

using Electrolite.Common.Main;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Electrolite.Main
{
    public interface ISession : IDisposable
    {
        event EventHandler OnClosing;
        event EventHandler OnReady;
        string SplashImagePath { get; set; }
        void Run();
        Task RunAsync(CancellationToken token = default);
        void RunBackground(CancellationToken token = default);
        Task ModifySettings(ElectroliteOptions options);
        int BrowserProcessId { get; }
        Task WaitForShutdown();
    }
}

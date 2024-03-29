﻿/*
Copyright (c) 2019 Integrative Software LLC
Created: 5/2019
Author: Pablo Carbonell
*/

using System;
using System.Threading;
using System.Threading.Tasks;

namespace Foton.Common.Main
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
        Task ModifySettings(FotonOptions options);
        int BrowserProcessId { get; }
        Task WaitForShutdown();
    }
}

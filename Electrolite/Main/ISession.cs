/*
Copyright (c) 2019 Integrative Software LLC
Created: 5/2019
Author: Pablo Carbonell
*/

using System;
using System.Threading;
using System.Threading.Tasks;

namespace Electrolite.Main
{
    public interface ISession : IDisposable
    {
        event EventHandler OnClosing;
        event EventHandler OnReady;
        Task RunAsync(CancellationToken token = default);
    }
}

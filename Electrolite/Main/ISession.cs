/*
Copyright (c) 2019 Integrative Software LLC
Created: 5/2019
Author: Pablo Carbonell
*/

using System;
using System.Threading.Tasks;

namespace Electrolite.Main
{
    public interface ISession : IDisposable
    {
        event EventHandler<ClosingEventArgs> OnClosing;
        event EventHandler OnReady;
        void RunBlocking();
        Task RunBlockingAsync();
        void Open();
    }
}

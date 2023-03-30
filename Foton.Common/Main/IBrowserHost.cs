/*
Copyright (c) 2019 Integrative Software LLC
Created: 5/2019
Author: Pablo Carbonell
*/

using Foton.Common.Ipc;

namespace Foton.Common.Main
{
    public interface IBrowserHost
    {
        string Ping();
        Order<StartupParameters> GetStartupOptions();
        Order NotifyClosing();
        Order NotifyReady();
    }
}

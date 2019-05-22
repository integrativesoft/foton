/*
Copyright (c) 2019 Integrative Software LLC
Created: 5/2019
Author: Pablo Carbonell
*/

using System;

namespace Electrolite.Core.Ipc
{
    public sealed class IpcDuplexParameters<TServer>
        where TServer : class
    {
        public string ServerEndpoint { get; set; }
        public string ServerPipe { get; set; }
        public string ClientPipe { get; set; }
        public Func<IServiceProvider, TServer> ServerFactory { get; set; }
    }
}

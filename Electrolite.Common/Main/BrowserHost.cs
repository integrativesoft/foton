/*
Copyright (c) 2019 Integrative Software LLC
Created: 5/2019
Author: Pablo Carbonell
*/

using Electrolite.Core.Adapters;
using Electrolite.Core.Ipc;
using Electrolite.Core.Main;

namespace Electrolite.Main
{
    sealed class BrowserHost : IBrowserHost
    {
        readonly IpcSession _parent;

        public BrowserHost(IpcSession parent)
        {
            _parent = parent;
        }

        public Order<StartupParameters> GetStartupOptions()
        {
            return IpcExtensions.WrapOrder(() => new StartupParameters
            {
                Url = _parent.Url,
                Options = _parent.StartupOptions
            });
        }

        public Order NotifyReady()
        {
            return IpcExtensions.WrapOrder(() => _parent.NotifyReady());
        }

        public Order NotifyClosing()
        {
            return IpcExtensions.WrapOrder(() => _parent.NotifyClosing());
        }

        public string Ping() => "Pong";
    }
}

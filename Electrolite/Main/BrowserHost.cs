/*
Copyright (c) 2019 Integrative Software LLC
Created: 5/2019
Author: Pablo Carbonell
*/

using Electrolite.Common.Ipc;
using Electrolite.Common.Main;

namespace Electrolite.Main
{
    sealed class BrowserHost : IBrowserHost
    {
        readonly Session _parent;

        public BrowserHost(Session parent)
        {
            _parent = parent;
        }

        public Order<ElectroliteOptions> GetStartupOptions()
        {
            return IpcExtensions.WrapOrder(() => _parent.StartupOptions);
        }

        public Order NotifyReady()
        {
            return IpcExtensions.WrapOrder(() => _parent.NotifyReady());
        }

        public Order<ClosingResponse> OnClosing(ClosingReason readon)
        {
            return IpcExtensions.WrapOrder(() => _parent.NotifyClosing());
        }
    }
}

/*
Copyright (c) 2019 Integrative Software LLC
Created: 5/2019
Author: Pablo Carbonell
*/

using Electrolite.Common.Ipc;

namespace Electrolite.Common.Main
{
    public interface IBrowserHost
    {
        Order<ElectroliteOptions> GetStartupOptions();
        Order<ClosingResponse> OnClosing(ClosingReason readon);
        Order NotifyReady();
    }
}

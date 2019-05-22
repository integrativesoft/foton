/*
Copyright (c) 2019 Integrative Software LLC
Created: 5/2019
Author: Pablo Carbonell
*/

using Electrolite.Common.Main;
using Electrolite.Main;
using System;
using System.Diagnostics;

namespace Electrolite.Adapters
{
    interface IPlatformAdapter
    {
        Process LaunchBrowser(int parentProcessId, string splashPath);
        ISession CreateSession(Uri url, ElectroliteOptions options);
    }
}

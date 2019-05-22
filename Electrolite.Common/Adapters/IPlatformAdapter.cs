/*
Copyright (c) 2019 Integrative Software LLC
Created: 5/2019
Author: Pablo Carbonell
*/

using Electrolite.Core.Main;
using System;
using System.Diagnostics;

namespace Electrolite.Core.Adapters
{
    public interface IPlatformAdapter
    {
        bool IsSupported { get; }
        ISession CreateSession(Uri url, ElectroliteOptions options);
    }

    public interface IIpcPlatformAdapter : IPlatformAdapter
    {
        Process LaunchBrowser(int parentProcessId, string splashPath);
    }
}

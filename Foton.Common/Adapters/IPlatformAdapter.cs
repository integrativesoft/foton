/*
Copyright (c) 2019 Integrative Software LLC
Created: 5/2019
Author: Pablo Carbonell
*/

using System;
using System.Diagnostics;
using Foton.Common.Main;

namespace Foton.Common.Adapters
{
    public interface IPlatformAdapter
    {
        ISession CreateSession(Uri url, FotonOptions options);
    }

    public interface IIpcPlatformAdapter : IPlatformAdapter
    {
        Process LaunchBrowser(int parentProcessId, string splashPath);
    }
}

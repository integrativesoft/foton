/*
Copyright (c) 2019 Integrative Software LLC
Created: 5/2019
Author: Pablo Carbonell
*/

using System.Diagnostics;

namespace Electrolite.Adapters
{
    interface IPlatformAdapter
    {
        Process LaunchBrowser(int parentProcessId);
    }
}

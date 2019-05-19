/*
Copyright (c) 2019 Integrative Software LLC
Created: 5/2019
Author: Pablo Carbonell
*/

using System.Diagnostics;

namespace Electrolite.Adapters
{
    class WindowsAdapter : IPlatformAdapter
    {
        const string Filename = "Electrolite.Windows.exe";

        public Process LaunchBrowser(string pipeName)
        {
            return PlatformCommon.LaunchBrowser(pipeName, Filename);
        }
    }
}

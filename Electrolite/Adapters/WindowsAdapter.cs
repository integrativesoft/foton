/*
Copyright (c) 2019 Integrative Software LLC
Created: 5/2019
Author: Pablo Carbonell
*/

using System.Diagnostics;
using System.IO;

namespace Electrolite.Adapters
{
    class WindowsAdapter : IPlatformAdapter
    {
#if DEBUG
        const string BasePath = @"..\..\..\..\Electrolite.Windows\bin\x64\Debug";
#else
        const string BasePath = "Windows";
#endif
        const string Filename = "Electrolite.Windows.exe";

        public Process LaunchBrowser(int parentProcessId)
        {
            string path = Path.Combine(BasePath, Filename);
            PlatformCommon.VerifyFileExists(path);
            return PlatformCommon.LaunchBrowser(parentProcessId.ToString(), path);
        }
    }
}

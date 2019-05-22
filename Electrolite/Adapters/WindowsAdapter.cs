/*
Copyright (c) 2019 Integrative Software LLC
Created: 5/2019
Author: Pablo Carbonell
*/

using System;
using System.Diagnostics;
using System.IO;
using Electrolite.Common.Main;
using Electrolite.Main;

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

        public Process LaunchBrowser(int parentProcessId, string splashPath)
        {
            string path = Path.Combine(BasePath, Filename);
            PlatformCommon.VerifyFileExists(path);
            var args = BuildArgs(parentProcessId, splashPath);
            return PlatformCommon.LaunchBrowser(args, path);
        }

        private static string BuildArgs(int parentProcessId, string splashPath)
        {
            string args = parentProcessId.ToString();
            if (!string.IsNullOrEmpty(splashPath))
            {
                args = args + " " + Uri.EscapeDataString(splashPath);
            }
            return args;
        }

        public ISession CreateSession(Uri url, ElectroliteOptions options)
        {
            return new IpcSession(this, url, options);
        }
    }
}

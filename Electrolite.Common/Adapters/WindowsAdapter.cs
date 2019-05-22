/*
Copyright (c) 2019 Integrative Software LLC
Created: 5/2019
Author: Pablo Carbonell
*/

using Electrolite.Core.Adapters;
using Electrolite.Core.Main;
using System;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;

namespace Electrolite.Windows.Main
{
    public sealed class WindowsAdapter : IIpcPlatformAdapter
    {
#if DEBUG
        const string BasePath = @"..\..\..\..\Electrolite.Windows\bin\x64\Debug";
#else
        const string BasePath = "Windows";
#endif
        const string Filename = "Electrolite.Windows.exe";

        public bool IsSupported => RuntimeInformation.IsOSPlatform(OSPlatform.Windows);

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

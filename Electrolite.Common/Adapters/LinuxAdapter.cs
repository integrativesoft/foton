/*
Copyright (c) 2019 Integrative Software LLC
Created: 5/2019
Author: Pablo Carbonell
*/

using System;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;
using Electrolite.Common.Main;

namespace Electrolite.Common.Adapters
{
    internal sealed class LinuxAdapter : IIpcPlatformAdapter
    {
#if DEBUG
        private const string BasePath = @"..\..\..\..\Electrolite.Linux\bin\x64\Debug";
#else
        const string BasePath = "Linux";
#endif
        private const string Filename = "Electrolite.Linux";

        public ISession CreateSession(Uri url, ElectroliteOptions options)
        {
            return new IpcSession(this, url, options);
        }

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
    }
}
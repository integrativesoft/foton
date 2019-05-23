/*
Copyright (c) 2019 Integrative Software LLC
Created: 5/2019
Author: Pablo Carbonell
*/

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using Electrolite.Common.Main;

namespace Electrolite.Common.Adapters
{
    internal sealed class LinuxAdapter : IIpcPlatformAdapter
    {
#if DEBUG
        private const string BasePath = "../Electrolite.Linux/bin/Debug";
#else
        const string BasePath = "Linux";
#endif
        private const string Filename = "Electrolite.Linux.dll";

        public ISession CreateSession(Uri url, ElectroliteOptions options)
        {
            return new IpcSession(this, url, options);
        }

        public Process LaunchBrowser(int parentProcessId, string splashPath)
        {
            var dllPath = Path.Combine(BasePath, Filename);
            PlatformCommon.VerifyFileExists(dllPath);
            var command = BuildCommand(dllPath, parentProcessId, splashPath);
            return RunProcess(command);
        }
        
        private static string BuildCommand(string dllPath, int parentProcessId, string splashPath)
        {
            var list = new List<string>
            {
                "dotnet",
                dllPath,
                parentProcessId.ToString()
            };
            if (!string.IsNullOrEmpty(splashPath))
            {
                list.Add(Uri.EscapeDataString(splashPath));
            }
            return string.Join(" ", list);
        }

        private static Process RunProcess(string command)
        {
            command = command.Replace("\"","\"\"");
            var proc = new Process
            {
                StartInfo = new ProcessStartInfo
                {
                    FileName = "/bin/bash",
                    Arguments = "-c \"" + command + "\"",
                    UseShellExecute = false
                }
            };
            proc.Start();
            return proc;
        }
    }
}
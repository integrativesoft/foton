/*
Copyright (c) 2019 Integrative Software LLC
Created: 5/2019
Author: Pablo Carbonell
*/

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using Foton.Common.Main;

namespace Foton.Common.Adapters
{
    internal sealed class LinuxAdapter : IIpcPlatformAdapter
    {
#if DEBUG
        private const string BasePath = "../Foton.Linux/bin/Debug";
#else
        const string BasePath = "Linux";
#endif
        private const string Filename = "Foton.Linux.dll";

        public ISession CreateSession(Uri url, FotonOptions options)
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
                },
                EnableRaisingEvents = true
            };
            proc.Start();
            return proc;
        }
    }
}
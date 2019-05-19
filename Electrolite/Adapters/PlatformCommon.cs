/*
Copyright (c) 2019 Integrative Software LLC
Created: 5/2019
Author: Pablo Carbonell
*/

using System.Diagnostics;
using System.IO;
using System.Reflection;

namespace Electrolite.Adapters
{
    class PlatformCommon
    {
        public static Process LaunchBrowser(string endpointName, string fileName)
        {
            var process = new Process
            {
                StartInfo = new ProcessStartInfo
                {
                    CreateNoWindow = false,
                    UseShellExecute = true,
                    FileName = GetFilePath(fileName),
                    Arguments = endpointName
                },
                EnableRaisingEvents = true
            };
            process.Start();
            return process;
        }

        private static string GetFilePath(string fileName)
        {
            var assembly = Assembly.GetAssembly(typeof(PlatformCommon));
            var path = assembly.CodeBase;
            var folder = Path.GetDirectoryName(path);
            return Path.Combine(folder, fileName);
        }
    }
}

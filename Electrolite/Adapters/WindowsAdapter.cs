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
    class WindowsAdapter : IPlatformAdapter
    {
        const string Filename = "Electrolite.Windows.exe";

        public void LaunchBrowser(string endpointName)
        {
            var process = new Process
            {
                StartInfo = new ProcessStartInfo
                {
                    CreateNoWindow = false,
                    UseShellExecute = true,
                    FileName = GetFilename(),
                    Arguments = endpointName
                }
            };
            process.Start();
        }

        private static string GetFilename()
        {
            var assembly = Assembly.GetAssembly(typeof(WindowsAdapter));
            var path = assembly.CodeBase;
            var folder = Path.GetDirectoryName(path);
            return Path.Combine(folder, Filename);
        }
    }
}

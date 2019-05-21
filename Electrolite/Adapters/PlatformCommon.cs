/*
Copyright (c) 2019 Integrative Software LLC
Created: 5/2019
Author: Pablo Carbonell
*/

using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Reflection;

namespace Electrolite.Adapters
{
    class PlatformCommon
    {
        public static Process LaunchBrowser(string endpointName, string path)
        {
            var process = new Process
            {
                StartInfo = new ProcessStartInfo
                {
                    CreateNoWindow = false,
                    UseShellExecute = true,
                    FileName = path,
                    Arguments = endpointName
                },
                EnableRaisingEvents = true
            };
            try
            {
                process.Start();
            }
            catch (System.Exception e)
            {
                System.Console.Write(e.Message);
            }
            return process;
        }

        public static string GetAssemblyPath()
        {
            var assembly = Assembly.GetAssembly(typeof(PlatformCommon));
            var path = assembly.CodeBase;
            return Path.GetDirectoryName(path);
        }

        public static void VerifyFileExists(string path)
        {
            if (!File.Exists(path))
            {
                string message = $"File not found: {path}";
                throw new System.InvalidProgramException(message);
            }
        }

        public static string NameHostEndpoint(int id)
            => NameElement("ElectroliteEndpoint_{0}", id);

        public static string NameBrowserEndpoint(int id)
            => NameElement("ElectroliteEndpoint_{0}", id);

        public static string NameBrowserPipe(int id)
            => NameElement("ElectroliteClient_{0}", id);

        public static string NameHostPipe(int id)
            => NameElement("ElectroliteServer_{0}", id);

        private static string NameElement(string template, int id)
        {
            return string.Format(template, id.ToString(CultureInfo.InvariantCulture));
        }
    }
}

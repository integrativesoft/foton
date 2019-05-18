/*
Copyright (c) 2019 Integrative Software LLC
Created: 5/2019
Author: Pablo Carbonell
*/

using Electrolite.Common.Main;
using System;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Threading.Tasks;

namespace Electrolite.Main
{
    public static class ElectroliteApp
    {
        public static Task Open(Uri url)
        {
            return GetElectrolite().Show(url);
        }

        public static Task Open(Uri url, ElectroliteOptions options)
        {
            return GetElectrolite().Show(url, options);
        }

        private static IElectrolite GetElectrolite()
        {
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                return LoadWindows();
            }
            else
            {
                return LoadUnix();
            }
        }

        private static IElectrolite LoadWindows()
        {
            var dll = LoadAssembly("Electrolite.Windows.dll");
            var type = dll.GetType("Electrolite.Windows.Main.Electrolite");
            return Activator.CreateInstance(type) as IElectrolite;
        }

        private static IElectrolite LoadUnix()
        {
            throw new NotImplementedException();
        }

        private static Assembly LoadAssembly(string fileName)
        {
            string folder = CurrentFolder();
            string path = Path.Combine(folder, fileName);
            return Assembly.LoadFile(path);
        }

        private static string CurrentFolder()
        {
            string fullPath = Assembly.GetAssembly(typeof(ElectroliteApp)).Location;
            return Path.GetDirectoryName(fullPath);
        }

    }
}

/*
Copyright (c) 2019 Integrative Software LLC
Created: 5/2019
Author: Pablo Carbonell
*/

using Electrolite.Common.Main;
using System;

namespace Electrolite.Main
{
    public static class ElectroliteApp
    {
        public static ISession CreateSession(Uri url)
        {
            return CreateSession(url, ElectroliteOptions.GetDefaults());
        }

        public static ISession CreateSession(Uri url, ElectroliteOptions options)
        {
            return new Session(url, options);
        }

        /*public static Task<ISession> Open(Uri url)
        {
            return Open(url, ElectroliteOptions.GetDefaults());
        }

        public static Task<ISession> Open(Uri url, ElectroliteOptions options)
        {
            
        }

        private static void StartListening()
        {

        }

        private static IBrowserWindow GetElectrolite()
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

        private static IBrowserWindow LoadWindows()
        {
            var dll = LoadAssembly("Electrolite.Windows.dll");
            var type = dll.GetType("Electrolite.Windows.Main.Electrolite");
            return Activator.CreateInstance(type) as IBrowserWindow;
        }

        private static IBrowserWindow LoadUnix()
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
        }*/

    }
}

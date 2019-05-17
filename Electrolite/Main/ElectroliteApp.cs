/*
Copyright (c) 2019 Integrative Software LLC
Created: 5/2019
Author: Pablo Carbonell
*/

using Electrolite.Common.Main;
using System;
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
            return null;
        }

        private static IElectrolite LoadUnix()
        {
            throw new NotImplementedException();
        }
    }
}

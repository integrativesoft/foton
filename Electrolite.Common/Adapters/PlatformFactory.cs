/*
Copyright (c) 2019 Integrative Software LLC
Created: 5/2019
Author: Pablo Carbonell
*/

using System;
using System.Runtime.InteropServices;

namespace Electrolite.Common.Adapters
{
    public static class PlatformFactory
    {
        public static IPlatformAdapter CreateAdapter()
        {
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                return new WindowsAdapter();
            }
            else if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
            {
                return new LinuxAdapter();
            }
            else
            {
                throw new NotImplementedException();
            }
        }
    }
}

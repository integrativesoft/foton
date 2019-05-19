/*
Copyright (c) 2019 Integrative Software LLC
Created: 5/2019
Author: Pablo Carbonell
*/

using System;
using System.Runtime.InteropServices;

namespace Electrolite.Adapters
{
    static class PlatformAdapterFactory
    {
        public static IPlatformAdapter CreateAdapter()
        {
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                return new WindowsAdapter();
            }
            else
            {
                throw new NotImplementedException();
            }
        }
    }
}

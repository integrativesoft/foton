/*
Copyright (c) 2019 Integrative Software LLC
Created: 5/2019
Author: Pablo Carbonell
*/

using Electrolite.Adapters;
using Electrolite.Common.Main;
using System;

namespace Electrolite.Main
{
    public static class ElectroliteApp
    {
        public static ISession CreateSession(Uri url)
            => CreateSession(url, new ElectroliteOptions());

        public static ISession CreateSession(Uri url, ElectroliteOptions options)
        {
            var adapter = PlatformAdapterFactory.CreateAdapter();
            return adapter.CreateSession(url, options);
        }
    }
}

/*
Copyright (c) 2019 Integrative Software LLC
Created: 5/2019
Author: Pablo Carbonell
*/

using Electrolite.Core.Adapters;
using System;

namespace Electrolite.Core.Main
{
    public static class ElectroliteApp
    {
        public static ISession CreateSession(Uri url)
            => CreateSession(url, new ElectroliteOptions());

        public static ISession CreateSession(Uri url, ElectroliteOptions options)
        {
            var adapter = PlatformFactory.CreateAdapter();
            return adapter.CreateSession(url, options);
        }
    }
}

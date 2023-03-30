/*
Copyright (c) 2019 Integrative Software LLC
Created: 5/2019
Author: Pablo Carbonell
*/

using System;
using Foton.Common.Adapters;
using Foton.Common.Main;

namespace Foton.Core.Main
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

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
    public static class FotonApp
    {
        public static ISession CreateSession(Uri url)
            => CreateSession(url, new FotonOptions());

        public static ISession CreateSession(Uri url, FotonOptions options)
        {
            var adapter = PlatformFactory.CreateAdapter();
            return adapter.CreateSession(url, options);
        }
    }
}

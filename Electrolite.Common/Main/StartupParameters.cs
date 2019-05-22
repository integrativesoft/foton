/*
Copyright (c) 2019 Integrative Software LLC
Created: 5/2019
Author: Pablo Carbonell
*/

using System;

namespace Electrolite.Core.Main
{
    public sealed class StartupParameters
    {
        public Uri Url { get; set; }
        public ElectroliteOptions Options { get; set; }
    }
}

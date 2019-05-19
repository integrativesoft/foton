/*
Copyright (c) 2019 Integrative Software LLC
Created: 5/2019
Author: Pablo Carbonell
*/

using Electrolite.Common.Main;
using System;

namespace Electrolite.Main
{
    public class ClosingEventArgs : EventArgs
    {
        public ClosingResponse Response { get; set; }
    }
}

/*
Copyright (c) 2019 Integrative Software LLC
Created: 5/2019
Author: Pablo Carbonell
*/

using System;

namespace Foton.Common.Main
{
    public class BackgroundErrorEventArgs : EventArgs
    {
        public Exception Exception { get; set; }
    }
}

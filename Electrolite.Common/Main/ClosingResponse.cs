/*
Copyright (c) 2019 Integrative Software LLC
Created: 5/2019
Author: Pablo Carbonell
*/

namespace Electrolite.Common.Main
{
    public enum ClosingReason
    {
        UserClosing,
        SystemShutdown
    }

    public sealed class ClosingResponse
    {
        public bool AllowClose { get; set; }
        public string Message { get; set; }
    }
}

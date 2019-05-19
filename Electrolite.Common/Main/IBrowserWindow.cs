/*
Copyright (c) 2019 Integrative Software LLC
Created: 5/2019
Author: Pablo Carbonell
*/

using Electrolite.Common.Ipc;
using System;

namespace Electrolite.Common.Main
{
    public enum PlatformType
    {
        WindowsAny,
        UnixAny
    }

    public interface IBrowserWindow : IDisposable
    {
        PlatformType PlatformType { get; }
        Order Show(Uri url, ElectroliteOptions options);
    }
}

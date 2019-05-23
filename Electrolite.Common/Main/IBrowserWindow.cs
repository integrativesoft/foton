/*
Copyright (c) 2019 Integrative Software LLC
Created: 5/2019
Author: Pablo Carbonell
*/

using Electrolite.Core.Ipc;

namespace Electrolite.Core.Main
{
    public enum PlatformType
    {
        Windows,
        Linux,
        MacOS
    }

    public interface IBrowserWindow
    {
        PlatformType PlatformType { get; }
        Order ModifyOptions(ElectroliteOptions options);
    }
}

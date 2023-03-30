/*
Copyright (c) 2019 Integrative Software LLC
Created: 5/2019
Author: Pablo Carbonell
*/

using Foton.Common.Ipc;

namespace Foton.Common.Main
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

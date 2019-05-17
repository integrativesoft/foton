/*
Copyright (c) 2019 Integrative Software LLC
Created: 5/2019
Author: Pablo Carbonell
*/

using System;
using System.Threading.Tasks;

namespace Electrolite.Common.Main
{
    public enum PlatformType
    {
        WindowsAny,
        UnixAny
    }

    public interface IElectrolite : IDisposable
    {
        PlatformType PlatformType { get; }
        Task Show(Uri url);
        Task Show(Uri url, ElectroliteOptions options);
        event EventHandler<ClosingEventArgs> Closing;
    }
}

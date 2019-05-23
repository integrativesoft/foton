/*
Copyright (c) 2019 Integrative Software LLC
Created: 5/2019
Author: Pablo Carbonell
*/

using Electrolite.Core.Ipc;
using Electrolite.Core.Main;

namespace Electrolite.Linux.Main
{
    class BrowserHost : IBrowserWindow
    {
        public static MainForm Form { get; set; }

        public PlatformType PlatformType => PlatformType.Linux;

        public Order ModifyOptions(ElectroliteOptions options)
        {
            return IpcExtensions.WrapOrder(() =>
            {
                Form.ApplySetttings(options);
            });
        }
    }
}

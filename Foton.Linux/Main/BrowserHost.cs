/*
Copyright (c) 2019 Integrative Software LLC
Created: 5/2019
Author: Pablo Carbonell
*/

using Foton.Common.Ipc;
using Foton.Common.Main;

namespace Foton.Linux.Main
{
    internal class BrowserHost : IBrowserWindow
    {
        public static MainForm Form { get; set; }

        public PlatformType PlatformType => PlatformType.Linux;

        public Order ModifyOptions(ElectroliteOptions options)
        {
            return IpcExtensions.WrapOrder(() =>
            {
                Form.ApplySettings(options);
            });
        }
    }
}

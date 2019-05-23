/*
Copyright (c) 2019 Integrative Software LLC
Created: 5/2019
Author: Pablo Carbonell
*/

using Electrolite.Core.Ipc;
using Electrolite.Core.Main;

namespace Electrolite.Windows.Main
{
    class BrowserHost : IBrowserWindow
    {
        public static MainForm Form { get; set; }

        public PlatformType PlatformType => PlatformType.Windows;

        public Order ModifyOptions(ElectroliteOptions options)
        {
            return IpcExtensions.WrapOrder(() =>
            {
                ModifyOptionsSafe(options);
            });
        }

        private void ModifyOptionsSafe(ElectroliteOptions options)
        {
            if (Form.InvokeRequired)
            {
                Form.Invoke(new ModifyOptionsDelegate(ModifyOptionsSafe), new object[] { options });
            }
            else
            {
                Form.ApplySetttings(options);
            }
        }

        private delegate void ModifyOptionsDelegate(ElectroliteOptions options);
    }
}

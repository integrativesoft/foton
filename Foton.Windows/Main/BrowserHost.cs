/*
Copyright (c) 2019 Integrative Software LLC
Created: 5/2019
Author: Pablo Carbonell
*/

using Foton.Core.Ipc;
using Foton.Core.Main;

namespace Foton.Windows.Main
{
    class BrowserHost : IBrowserWindow
    {
        public static MainForm Form { get; set; }

        public PlatformType PlatformType => PlatformType.Windows;

        public Order ModifyOptions(FotonOptions options)
        {
            return IpcExtensions.WrapOrder(() =>
            {
                ModifyOptionsSafe(options);
            });
        }

        private void ModifyOptionsSafe(FotonOptions options)
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

        private delegate void ModifyOptionsDelegate(FotonOptions options);
    }
}

/*
Copyright (c) 2019 Integrative Software LLC
Created: 5/2019
Author: Pablo Carbonell
*/

using Electrolite.Common.Ipc;
using Electrolite.Common.Main;

namespace Electrolite.Windows.Main
{
    class BrowserHost : IBrowserWindow
    {
        readonly MainForm _form;

        public BrowserHost(MainForm form)
        {
            _form = form;
        }

        public PlatformType PlatformType => PlatformType.WindowsAny;

        public Order ModifyOptions(ElectroliteOptions options)
        {
            return IpcExtensions.WrapOrder(() =>
            {
                ModifyOptionsSafe(options);
            });
        }

        private void ModifyOptionsSafe(ElectroliteOptions options)
        {
            if (_form.InvokeRequired)
            {
                _form.Invoke(new ModifyOptionsDelegate(ModifyOptionsSafe), new object[] { options });
            }
            else
            {
                _form.ApplySetttings(options);
            }
        }

        private delegate void ModifyOptionsDelegate(ElectroliteOptions options);
    }
}

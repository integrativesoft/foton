/*
Copyright (c) 2019 Integrative Software LLC
Created: 5/2019
Author: Pablo Carbonell
*/

using Electrolite.Common.Adapters;
using Eto.Forms;
using Eto.GtkSharp.Forms;

namespace Electrolite.Linux.Main
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var parameters = ProcessArguments.Parse(args);
            MainInternal(parameters);
        }

        private static void MainInternal(ProcessArguments args)
        {
            LoadStyles();
            var duplex = PlatformCommon.CreateClientDuplex(args.ParentProcessId, () => new BrowserHost());
            var splash = ShowSplash(args.SplashPath);

            var form = new MainForm(duplex, splash);
            BrowserHost.Form = form;
            new Application().Run(form);
        }

        private static void LoadStyles()
        {
            Eto.Style.Add<FormHandler>("splash", handler =>
            {
                handler.ShowBorder = false;
                handler.ShowInTaskbar = false;
                handler.Resizable = false;
                handler.Maximizable = false;
                handler.Minimizable = false;
            });
        }

        private static Form ShowSplash(string path)
        {
            if (string.IsNullOrEmpty(path))
            {
                return null;
            }
            var splash = new SplashForm(path);
            splash.Show();
            splash.BringToFront();
            return splash;
        }

    }
}

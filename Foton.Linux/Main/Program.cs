/*
Copyright (c) 2019 Integrative Software LLC
Created: 5/2019
Author: Pablo Carbonell
*/

using Foton.Common.Adapters;
using Eto.Forms;
using Eto.GtkSharp.Forms;

namespace Foton.Linux.Main
{
    internal static class Program
    {
        private static void Main(string[] args)
        {
            var parameters = ProcessArguments.Parse(args);
            MainInternal(parameters);
        }

        private static void MainInternal(ProcessArguments args)
        {
            LoadStyles();
            var app = new Application();
            app.Initialized += (sender, e) => AppInitialized(app, args);
            app.Run();
        }

        private static void AppInitialized(Application app, ProcessArguments args)
        {
            var splash = ShowSplash(args.SplashPath);
            var duplex = PlatformCommon.CreateClientDuplex(args.ParentProcessId, () => new BrowserHost());
            var form = new MainForm(duplex, splash);
            form.Show();
            SettingsApplier.CenterForm(form);
            form.Visible = false;
            BrowserHost.Form = form;
            form.Closed += (sender, eventArgs) => app.Quit();
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
                handler.WindowStyle = WindowStyle.None;
            });
        }
        
        private static SplashForm ShowSplash(string path)
        {
            if (string.IsNullOrEmpty(path))
            {
                return null;
            }
            var splash = new SplashForm(path);
            splash.Show();
            SettingsApplier.CenterForm(splash);
            splash.BringToFront();
            return splash;
        }

    }
}

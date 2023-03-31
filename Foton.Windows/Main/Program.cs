/*
Copyright (c) 2019 Integrative Software LLC
Created: 5/2019
Author: Pablo Carbonell
*/

using CefSharp;
using CefSharp.WinForms;
using Foton.Common.Adapters;
using Foton.Core.Adapters;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace Foton.Windows.Main
{
    static class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            var parameters = ProcessArguments.Parse(args);
            MainInternal(parameters);
        }

        private static void MainInternal(ProcessArguments args)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.ApplicationExit += Application_ApplicationExit;

            var duplex = PlatformCommon.CreateClientDuplex(args.ParentProcessId, () => new BrowserHost());
            var splash = ShowSplash(args.SplashPath);

            InitializeCef();
            var form = new MainForm(duplex, splash);
            BrowserHost.Form = form;
            Application.Run(form);
        }

        private static void InitializeCef()
        {
            Cef.EnableHighDPISupport();
            var settings = new CefSettings();
            Cef.Initialize(settings);
        }

        private static void Application_ApplicationExit(object sender, EventArgs e)
        {
            Cef.Shutdown();
        }

        private static Form ShowSplash(string path)
        {
            if (string.IsNullOrEmpty(path))
            {
                return null;
            }
            var image = Image.FromFile(path);
            var splash = new SplashForm(image);
            splash.Show();
            splash.BringToFront();
            return splash;
        }
    }
}

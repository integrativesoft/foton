/*
Copyright (c) 2019 Integrative Software LLC
Created: 5/2019
Author: Pablo Carbonell
*/

using CefSharp;
using CefSharp.WinForms;
using Electrolite.Core.Ipc;
using Electrolite.Core.Main;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace Electrolite.Windows.Main
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.ApplicationExit += Application_ApplicationExit;

            int parentId = GetParentId(args);
            var duplex = CreateDuplex(parentId);
            var splash = ShowSplash(args);

            InitializeCef();
            var form = new MainForm(duplex, splash);
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

        internal static int GetParentId(string[] args)
        {
            if (args.Length >= 1 && int.TryParse(args[0], out int parentId))
            {
                return parentId;
            }
            throw new ArgumentException("Endpoint not specified or invalid.");
        }

        private static IpcPipeDuplex<IBrowserWindow, Core.Main.IBrowserHost> CreateDuplex(int parentId)
        {
            return new IpcPipeDuplex<IBrowserWindow, Core.Main.IBrowserHost>(new IpcDuplexParameters<IBrowserWindow>
            {
                ClientPipe = ElectroliteCommon.ElectroliteHost(parentId),
                ServerEndpoint = ElectroliteCommon.ElectroliteBrowserEndpoint(parentId),
                ServerPipe = ElectroliteCommon.ElectroliteBrowser(parentId),
                ServerFactory = (service => new BrowserHost())
            });
        }

        private static Form ShowSplash(string[] args)
        {
            if (args.Length < 2)
            {
                return null;
            }
            string path = Uri.UnescapeDataString(args[1]);
            var image = Image.FromFile(path);
            var splash = new SplashForm(image);
            splash.Show();
            splash.BringToFront();
            return splash;
        }
    }
}

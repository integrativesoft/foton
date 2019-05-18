/*
Copyright (c) 2019 Integrative Software LLC
Created: 5/2019
Author: Pablo Carbonell
*/

using CefSharp;
using CefSharp.WinForms;
using System;
using System.Windows.Forms;

namespace Electrolite.Windows.Main
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            Cef.EnableHighDPISupport();
            var settings = new CefSettings();
            Cef.Initialize(settings);
            //var form = new MainForm();
            //Application.Run(form);
            Application.ApplicationExit += Application_ApplicationExit;
        }

        private static void Application_ApplicationExit(object sender, EventArgs e)
        {
            Cef.Shutdown();
        }
    }
}

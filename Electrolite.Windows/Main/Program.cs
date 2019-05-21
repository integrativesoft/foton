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
        static void Main(string[] args)
        {
            InitializeApplicationContext();
            int parentId = GetParentId(args);
            var form = new MainForm(parentId);
            Application.Run(form);
        }

        private static void InitializeApplicationContext()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Cef.EnableHighDPISupport();
            var settings = new CefSettings();
            Cef.Initialize(settings);
            Application.ApplicationExit += Application_ApplicationExit;
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
    }
}

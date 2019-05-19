/*
Copyright (c) 2019 Integrative Software LLC
Created: 5/2019
Author: Pablo Carbonell
*/

using CefSharp;
using CefSharp.WinForms;
using System;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Electrolite.Windows.Main
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static async Task Main(string[] args)
        {
            InitializeApplicationContext();
            MessageBox.Show("running!");
            string pipe = GetPipename(args);
            var form = new MainForm(pipe);
            await form.Prepare();
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

        internal static string GetPipename(string[] args)
        {
            if (args.Length == 0)
            {
                throw new ArgumentException("Endpoint not specified.");
            }
            return args[0];
        }
    }
}

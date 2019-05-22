/*
Copyright (c) 2019 Integrative Software LLC
Created: 5/2019
Author: Pablo Carbonell
*/

using CefSharp.WinForms;
using Electrolite.Core.Ipc;
using Electrolite.Core.Main;
using System;
using System.Windows.Forms;

namespace Electrolite.Windows.Main
{
    sealed class MainForm : Form
    {
        readonly IpcPipeDuplex<IBrowserWindow, IBrowserHost> _duplex;
        readonly ChromiumWebBrowser _browser;
        readonly SettingsApplier _painter;
        readonly StartupParameters _startup;
        readonly Transparenter _transparenter;
        readonly Form _splash;

        public MainForm(IpcPipeDuplex<IBrowserWindow, IBrowserHost> duplex, Form splash)
        {
            _duplex = duplex;
            _splash = splash;
            _transparenter = new Transparenter(this);
            _painter = new SettingsApplier(this);
            _startup = _duplex.Client.Order(x => x.GetStartupOptions());
            ApplySetttings(_startup.Options);
            _painter.CenterForm();
            _browser = new ChromiumWebBrowser(_startup.Url.AbsoluteUri)
            {
                Dock = DockStyle.Fill,
            };
            _browser.LoadingStateChanged += Browser_LoadingStateChanged;
            Controls.Add(_browser);
            ResizeBegin += (s, e) => SuspendLayout();
            ResizeEnd += (s, e) => ResumeLayout(true);
            FormClosed += MainForm_FormClosed;
            _transparenter.MakeTransparent();
            _duplex.RunBackground();
        }

        private void MainForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            Hide();
            try
            {
                _duplex.Client.Order(x => x.NotifyClosing());
            }
            catch
            {
            }
            Shutdown();
        }

        public static void Shutdown()
        {
            CefSharp.Cef.Shutdown();
            Application.ExitThread();
            Environment.Exit(0);
            Application.Exit();
        }

        private void Browser_LoadingStateChanged(object sender, CefSharp.LoadingStateChangedEventArgs e)
        {
            if (!e.IsLoading)
            {
                _splash?.Hide();
                _duplex.Client.Order(x => x.NotifyReady());
                _transparenter.MakeOpaque();
                _splash?.Close();
            }
        }

        public void ApplySetttings(ElectroliteOptions options)
        {
            _painter.Apply(options);
        }
    }
}

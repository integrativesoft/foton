/*
Copyright (c) 2019 Integrative Software LLC
Created: 5/2019
Author: Pablo Carbonell
*/

using CefSharp.WinForms;
using Electrolite.Common.Ipc;
using Electrolite.Common.Main;
using JKang.IpcServiceFramework;
using System;
using System.Windows.Forms;

namespace Electrolite.Windows.Main
{
    sealed class MainForm : Form
    {
        readonly IpcServiceClient<IBrowserHost> _client;
        readonly ChromiumWebBrowser _browser;
        readonly SettingsApplier _painter;
        readonly StartupParameters _startup;
        readonly Transparenter _transparenter;

        public MainForm(string pipeName)
        {
            _transparenter = new Transparenter(this);
            _client = BuildClient(pipeName);
            _painter = new SettingsApplier(this);
            _startup = _client.Order(x => x.GetStartupOptions());
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
            new BrowserServer(this).RunInBackground();
        }

        private void MainForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            _client.Order(x => x.NotifyClosing());
            Shutdown();
        }

        public static void Shutdown()
        {
            CefSharp.Cef.Shutdown();
            Application.ExitThread();
            Environment.Exit(0);
            Application.Exit();
        }

        private static IpcServiceClient<IBrowserHost> BuildClient(string pipeName)
        {
            return new IpcServiceClientBuilder<IBrowserHost>()
                .UseNamedPipe(pipeName)
                .Build();
        }

        private void Browser_LoadingStateChanged(object sender, CefSharp.LoadingStateChangedEventArgs e)
        {
            if (!e.IsLoading)
            {
                _client.Order(x => x.NotifyReady());
                _transparenter.MakeOpaque();
            }
        }

        public void ApplySetttings(ElectroliteOptions options)
        {
            _painter.Apply(options);
        }

    }
}

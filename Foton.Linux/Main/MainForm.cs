/*
Copyright (c) 2019 Integrative Software LLC
Created: 5/2019
Author: Pablo Carbonell
*/

using Eto.Forms;
using System;
using Foton.Common.Ipc;
using Foton.Common.Main;
using Eto.Drawing;

namespace Foton.Linux.Main
{
    internal sealed class MainForm : Form
    {
        private readonly IpcPipeDuplex<IBrowserWindow, IBrowserHost> _duplex;
        private readonly SplashForm _splash;
        private readonly SettingsApplier _painter;

        public MainForm(IpcPipeDuplex<IBrowserWindow, IBrowserHost> duplex, SplashForm splash)
        {
            _duplex = duplex;
            _splash = splash;
            Visible = false;
            _painter = new SettingsApplier(this);
            var startup = _duplex.Client.Order(x => x.GetStartupOptions());
            ApplySettings(startup.Options);
            var browser = new WebView
            {
                Url = new Uri(startup.Url.AbsoluteUri)
            };
            browser.DocumentLoaded += Browser_LoadingStateChanged;
            Content = browser;
            Closed += MainForm_FormClosed;
            _duplex.RunBackground();
        }

        private void MainForm_FormClosed(object sender, EventArgs e)
        {
            _duplex.Client.Order(x => x.NotifyClosing());
            Environment.Exit(0);
        }

        private void Browser_LoadingStateChanged(object sender, WebViewLoadedEventArgs e)
        {
            _duplex.Client.Order(x => x.NotifyReady());
            Visible = true;
            _splash?.Close();
        }

        public void ApplySettings(FotonOptions options)
        {
            _painter.Apply(options);
        }
    }
}

/*
Copyright (c) 2019 Integrative Software LLC
Created: 5/2019
Author: Pablo Carbonell
*/

using Eto.Forms;
using System;
using Electrolite.Common.Ipc;
using Electrolite.Common.Main;

namespace Electrolite.Linux.Main
{
    internal class MainForm : Dialog
    {
        private readonly IpcPipeDuplex<IBrowserWindow, IBrowserHost> _duplex;
        private readonly Form _splash;
        private readonly StartupParameters _startup;
        private readonly WebView _browser;

        public MainForm(IpcPipeDuplex<IBrowserWindow, IBrowserHost> duplex, Form splash)
        {
            _duplex = duplex;
            _splash = splash;
            //_transparenter = new Transparenter(this);
            //_painter = new SettingsApplier(this);
            _startup = _duplex.Client.Order(x => x.GetStartupOptions());
            //ApplySetttings(_startup.Options);
            //_painter.CenterForm();
            _browser = new WebView
            {
                Url = new Uri(_startup.Url.AbsoluteUri)
            };
            _browser.DocumentLoaded += Browser_LoadingStateChanged;
            Content = _browser;
            //ResizeBegin += (s, e) => SuspendLayout();
            //ResizeEnd += (s, e) => ResumeLayout(true);
            Closed += MainForm_FormClosed;
            //_transparenter.MakeTransparent();
            _duplex.RunBackground();
        }

        private void MainForm_FormClosed(object sender, EventArgs e)
        {
            try
            {
                _duplex.Client.Order(x => x.NotifyClosing());
            }
            catch
            {
            }            
        }

        private void Browser_LoadingStateChanged(object sender, WebViewLoadedEventArgs e)
        {
            //_splash?.Hide();
            _duplex.Client.Order(x => x.NotifyReady());
            //_transparenter.MakeOpaque();
            _splash?.Close();
        }

        public void ApplySetttings(ElectroliteOptions options)
        {
            //_painter.Apply(options);
        }
    }
}

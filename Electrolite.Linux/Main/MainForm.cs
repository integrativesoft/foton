/*
Copyright (c) 2019 Integrative Software LLC
Created: 5/2019
Author: Pablo Carbonell
*/

using Electrolite.Core.Ipc;
using Electrolite.Core.Main;
using Eto.Forms;
using System;

namespace Electrolite.Linux.Main
{
    class MainForm : Dialog
    {
        readonly IpcPipeDuplex<IBrowserWindow, IBrowserHost> _duplex;
        readonly Form _splash;
        readonly StartupParameters _startup;
        readonly WebView _browser;

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

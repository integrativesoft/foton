/*
Copyright (c) 2019 Integrative Software LLC
Created: 5/2019
Author: Pablo Carbonell
*/

using CefSharp.WinForms;
using Electrolite.Common.Ipc;
using Electrolite.Common.Main;
using JKang.IpcServiceFramework;
using Nito.AsyncEx;
using System.Windows.Forms;

namespace Electrolite.Windows.Main
{
    sealed class MainForm : Form
    {
        readonly IpcServiceClient<IBrowserHost> _client;
        readonly ChromiumWebBrowser _browser;
        StartupParameters _startup;

        public MainForm(string pipeName)
        {
            _client = BuildClient(pipeName);
            ResizeBegin += (s, e) => SuspendLayout();
            ResizeEnd += (s, e) => ResumeLayout(true);
            AsyncContext.Run(async () => {
                _startup = await _client.OrderAsync(x => x.GetStartupOptions());
            });
            ApplySettings(_startup.Options);
            _browser = new ChromiumWebBrowser(_startup.Url.AbsoluteUri)
            {
                Dock = DockStyle.Fill
            };
            Controls.Add(_browser);
        }

        private static IpcServiceClient<IBrowserHost> BuildClient(string pipeName)
        {
            return new IpcServiceClientBuilder<IBrowserHost>()
                .UseNamedPipe(pipeName)
                .Build();
        }

        private void ApplySettings(ElectroliteOptions options)
        {
            StartPosition = FormStartPosition.CenterScreen;
            Text = options.Title;
            MinimizeBox = options.MinButton;
            MaximizeBox = options.MaxButton;
            Height = options.Height;
            Width = options.Width;
            ShowInTaskbar = options.ShownInTaskbar;
            if (!string.IsNullOrEmpty(options.IconPath))
            {
                Icon = new System.Drawing.Icon(options.IconPath);
            }
        }
    }
}

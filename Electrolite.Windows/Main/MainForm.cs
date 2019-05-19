/*
Copyright (c) 2019 Integrative Software LLC
Created: 5/2019
Author: Pablo Carbonell
*/

using CefSharp.WinForms;
using Electrolite.Common.Ipc;
using Electrolite.Common.Main;
using JKang.IpcServiceFramework;
using System.Threading.Tasks;
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
            _browser = new ChromiumWebBrowser("about:blank")
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

        public async Task Prepare()
        {
            _startup = await _client.OrderAsync(x => x.GetStartupOptions());
            ApplySettings(_startup.Options);
        }

        private void ApplySettings(ElectroliteOptions options)
        {
            Text = options.Title;
            MinimizeBox = options.MinButton;
            MaximizeBox = options.MaxButton;
            if (!string.IsNullOrEmpty(options.IconPath))
            {
                Icon = new System.Drawing.Icon(options.IconPath);
            }
        }
    }
}

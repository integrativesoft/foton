/*
Copyright (c) 2019 Integrative Software LLC
Created: 5/2019
Author: Pablo Carbonell
*/

using CefSharp.WinForms;
using Electrolite.Common.Main;
using System.Windows.Forms;

namespace Electrolite.Windows.Main
{
    sealed class MainForm : Form
    {
        readonly ElectroliteOptions _options;
        readonly ChromiumWebBrowser _browser;

        public MainForm(ElectroliteOptions options)
        {
            _options = options;
            Text = _options.Title;
            MinimizeBox = options.MinButton;
            MaximizeBox = options.MaxButton;
            if (!string.IsNullOrEmpty(options.IconPath))
            {
                Icon = new System.Drawing.Icon(options.IconPath);
            }
            ResizeBegin += (s, e) => SuspendLayout();
            ResizeEnd += (s, e) => ResumeLayout(true);
            _browser = new ChromiumWebBrowser("http://html5test.com")
            {
                Dock = DockStyle.Fill
            };
            Controls.Add(_browser);
        }
    }
}

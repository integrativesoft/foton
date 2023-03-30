/*
Copyright (c) 2019 Integrative Software LLC
Created: 5/2019
Author: Pablo Carbonell
*/

using System;
using Foton.Common.Main;
using Eto.Drawing;
using Eto.Forms;
using Eto.GtkSharp.Forms;

namespace Foton.Linux.Main
{
    internal sealed class SettingsApplier
    {
        private readonly MainForm _form;
        private readonly FormHandler _handler;

        public SettingsApplier(MainForm form)
        {
            _form = form;
            _handler = (FormHandler) form.Handler;
        }

        public void Apply(FotonOptions options)
        {
            if (options.Size != null)
            {
                _form.Size = new Size(options.Size.Width, options.Size.Height);
            }
            
            if (options.MinSize != null)
            {
                _form.MinimumSize = new Size(options.MinSize.Width, options.MinSize.Height);                
            }

            _form.Title = options.Title;
            _form.Minimizable = options.MinButton;
            _form.Maximizable = options.MaxButton;
            _form.ShowInTaskbar = options.ShownInTaskbar;
            
            if (!string.IsNullOrEmpty(options.IconPath))
            {
                _form.Icon = new Icon(options.IconPath);
            }
        }

        public static void CenterForm(Form form)
        {
            var screen = form.Screen;
            var x = (int)(screen.WorkingArea.Width - form.Size.Width) / 2;
            var y = (int) (screen.WorkingArea.Height - form.Size.Height) / 2;
            form.Location = new Point(x + 500, y);
        }
    }
}
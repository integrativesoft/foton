/*
Copyright (c) 2019 Integrative Software LLC
Created: 5/2019
Author: Pablo Carbonell
*/

using Foton.Core.Main;
using System;
using System.Drawing;
using System.Reflection;
using System.Windows.Forms;

namespace Foton.Windows.Main
{
    sealed class SettingsApplier
    {
        readonly MainForm _form;

        public SettingsApplier(MainForm form)
        {
            _form = form;
        }

        public void Apply(ElectroliteOptions options)
        {
            _form.Text = options.Title;
            _form.MinimizeBox = options.MinButton;
            _form.MaximizeBox = options.MaxButton;
            _form.Size = GetSize(options.Size);
            if (options.MinSize != null)
            {
                _form.MinimumSize = ConvertSize(options.MinSize);
            }
            if (options.MaxSize != null)
            {
                _form.MaximumSize = ConvertSize(options.MaxSize);
            }
            _form.ShowInTaskbar = options.ShownInTaskbar;
            _form.ShowIcon = options.ShowIcon;
            if (string.IsNullOrEmpty(options.IconPath))
            {
                _form.Icon = LoadIcon("Foton.Windows.Main.favicon.ico");
            }
            else
            {
                _form.Icon = new Icon(options.IconPath);
            }
        }

        private Icon LoadIcon(string resource)
        {
            var assembly = Assembly.GetAssembly(typeof(SettingsApplier));
            return assembly.LoadIcon(resource);
        }

        private Size GetSize(ElectroliteSize size)
        {
            if (size == null)
            {
                return GetDefaultSize();
            }
            else
            {
                return ConvertSize(size);
            }
        }

        private static Size ConvertSize(ElectroliteSize size)
        {
            return new Size
            {
                Width = size.Width,
                Height = size.Height
            };
        }

        private Size GetDefaultSize()
        {
            var screen = Screen.FromControl(_form);
            return new Size
            {
                Height = (int)Math.Round(screen.WorkingArea.Height * 0.8),
                Width = (int)Math.Round(screen.WorkingArea.Width * 0.8)
            };
        }

        public void CenterForm()
        {
            var screen = Screen.FromControl(_form);
            var area = screen.WorkingArea;
            _form.Left = (area.Width - _form.Width) / 2;
            _form.Top = (area.Height - _form.Height) / 2;
        }
    }
}

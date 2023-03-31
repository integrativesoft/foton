/*
Copyright (c) 2019 Integrative Software LLC
Created: 5/2019
Author: Pablo Carbonell
*/

using System.Drawing;
using System.Windows.Forms;

namespace Foton.Windows.Main
{
    sealed class Transparenter
    {
        readonly Color TransparentColor = Color.LimeGreen;
        readonly Form _parent;
        Panel _panel;
        Color _defaultColor;

        public Transparenter(Form parent)
        {
            _parent = parent;
        }

        public void MakeTransparent()
        {
            _parent.SuspendLayout();
            _panel = new Panel
            {
                BackColor = TransparentColor,
                Dock = DockStyle.Fill
            };
            _defaultColor = _parent.BackColor;
            _parent.AllowTransparency = true;
            _parent.BackColor = TransparentColor;
            _parent.TransparencyKey = TransparentColor;
            _parent.FormBorderStyle = FormBorderStyle.None;
            _parent.Controls.Add(_panel);
            _panel.BringToFront();
            _parent.ResumeLayout();
        }

        public void MakeOpaque()
        {
            _parent.SuspendLayout();
            _parent.FormBorderStyle = FormBorderStyle.Sizable;
            _panel.Visible = false;
            _parent.Controls.Remove(_panel);
            _parent.BackColor = _defaultColor;
            _parent.AllowTransparency = false;
            _parent.ResumeLayout();
        }
    }
}

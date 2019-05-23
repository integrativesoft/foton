/*
Copyright (c) 2019 Integrative Software LLC
Created: 5/2019
Author: Pablo Carbonell
*/

using Eto.Drawing;
using Eto.Forms;
using Eto.GtkSharp.Forms;

namespace Electrolite.Linux.Main
{
    internal class SplashForm : Form
    {
        public SplashForm(string path)
        {
            Style = "splash";
            Content = new ImageView
            {
                Image = new Bitmap(path)
            };
        }
    }
}

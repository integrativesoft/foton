/*
Copyright (c) 2019 Integrative Software LLC
Created: 5/2019
Author: Pablo Carbonell
*/

using System.Drawing;
using System.Windows.Forms;

namespace Foton.Windows.Main
{
    sealed class SplashForm : Form
    {
        public SplashForm(Image image)
        {
            FormBorderStyle = FormBorderStyle.None;
            Width = image.Width;
            Height = image.Height;
            BackgroundImage = image;
            StartPosition = FormStartPosition.CenterScreen;
            ShowInTaskbar = false;
        }
    }
}

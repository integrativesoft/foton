/*
Copyright (c) 2019 Integrative Software LLC
Created: 5/2019
Author: Pablo Carbonell
*/

using System.Windows;
using System.Windows.Controls;

namespace Electrolite.Windows.Main
{
    sealed class MainForm : Window
    {
        public MainForm()
        {
            InitializeComponent();
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
        }

        private void InitializeComponent()
        {
            var panel = new StackPanel();
            panel.Children.Add(new Button
            {
                Content = "hello"
            });
            Content = panel;
        }
    }
}

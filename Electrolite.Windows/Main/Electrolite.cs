/*
Copyright (c) 2019 Integrative Software LLC
Created: 5/2019
Author: Pablo Carbonell
*/

using Electrolite.Common.Main;
using System;
using System.Threading.Tasks;

namespace Electrolite.Windows.Main
{
    public sealed class Electrolite : IElectrolite
    {
        public event EventHandler<ClosingEventArgs> Closing;
        readonly MainForm _form;

        public Electrolite()
        {
            CefPlatformResolver.InitializeCefSharp();
            _form = new MainForm();
        }

        bool _disposed;

        public PlatformType PlatformType => PlatformType.WindowsAny;

        public void Dispose()
        {
            if (!_disposed)
            {
                _disposed = true;
                _form.Close();
            }
        }

        public Task Show(Uri url)
        {
            return Show(url, ElectroliteOptions.GetDefaults());
        }

        public Task Show(Uri url, ElectroliteOptions options)
        {
            _form.Height = options.Height;
            _form.Width = options.Width;
            _form.Title = options.Title;
            return Task.CompletedTask;
        }
    }
}

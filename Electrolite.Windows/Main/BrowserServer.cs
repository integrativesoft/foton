/*
Copyright (c) 2019 Integrative Software LLC
Created: 5/2019
Author: Pablo Carbonell
*/

using Electrolite.Common.Main;
using JKang.IpcServiceFramework;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace Electrolite.Windows.Main
{
    class BrowserServer
    {
        readonly MainForm _form;

        public BrowserServer(MainForm form)
        {
            _form = form;
        }

        public void RunInBackground()
        {
            Task.Run(() =>
            {
                Main();
            });
        }

        private void Main()
        {
            var services = ConfigureServices(new ServiceCollection());
            int pid = Process.GetCurrentProcess().Id;
            string endpoint = ElectroliteCommon.GetClientEndpointName(pid);
            string pipe = ElectroliteCommon.GetClientPipeName(pid);
            new IpcServiceHostBuilder(services.BuildServiceProvider())
                .AddNamedPipeEndpoint<IBrowserWindow>(name: endpoint, pipeName: pipe)
                .Build()
                .Run();
        }

        private IServiceCollection ConfigureServices(IServiceCollection services)
        {
            return services
                .AddIpc(builder =>
                {
                    builder
                        .AddNamedPipe(options =>
                        {
                            options.ThreadCount = 2;
                        })
                        .AddService<IBrowserWindow, BrowserHost>(CreateBrowserHost);
                });
        }

        private BrowserHost CreateBrowserHost(IServiceProvider service)
        {
            return new BrowserHost(_form);
        }
    }
}

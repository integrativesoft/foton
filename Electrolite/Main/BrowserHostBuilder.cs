/*
Copyright (c) 2019 Integrative Software LLC
Created: 5/2019
Author: Pablo Carbonell
*/

using Electrolite.Common.Main;
using JKang.IpcServiceFramework;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Electrolite.Main
{
    sealed class BrowserHostBuilder
    {
        readonly Session _session;
        readonly string _pipeName, _endpointName;

        public static IIpcServiceHost Build(Session session, string endpointName, string pipeName)
        {
            return new BrowserHostBuilder(session, endpointName, pipeName).Build();
        }

        private BrowserHostBuilder(Session session, string endpointName, string pipeName)
        {
            _session = session;
            _pipeName = pipeName;
            _endpointName = endpointName;
        }

        public IIpcServiceHost Build()
        {
            var services = ConfigureServices(new ServiceCollection());
            return new IpcServiceHostBuilder(services.BuildServiceProvider())
                .AddNamedPipeEndpoint<IBrowserHost>(name: _endpointName, pipeName: _pipeName)
                .Build();
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
                        .AddService(CreateBrowserHost);
                });
        }

        private IBrowserHost CreateBrowserHost(IServiceProvider arg)
        {
            return new BrowserHost(_session);
        }
    }
}

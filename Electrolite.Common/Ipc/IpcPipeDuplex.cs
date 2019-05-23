/*
Copyright (c) 2019 Integrative Software LLC
Created: 5/2019
Author: Pablo Carbonell
*/

using System;
using System.Threading;
using System.Threading.Tasks;
using Electrolite.Common.Main;
using JKang.IpcServiceFramework;
using Microsoft.Extensions.DependencyInjection;

namespace Electrolite.Common.Ipc
{
    public sealed class IpcPipeDuplex<TServer, TClient>
        where TServer : class
        where TClient : class
    {
        private readonly IpcDuplexParameters<TServer> _parameters;
        public IIpcServiceHost Server { get; }
        public IpcServiceClient<TClient> Client { get; }

        public event EventHandler<BackgroundErrorEventArgs> BackgroundError;

        public IpcPipeDuplex(IpcDuplexParameters<TServer> parameters)
        {
            _parameters = parameters;
            Server = BuildHost();
            Client = BuildClient();
        }

        public async Task RunAsync(CancellationToken token = default)
        {
            await Server.RunAsync(token);
        }

        public void RunBackground(CancellationToken token = default)
        {
            Task.Run(async () =>
            {
                await RunCatchAsync(token);
            });
        }

        private async Task RunCatchAsync(CancellationToken token)
        {
            try
            {
                await Server.RunAsync(token);
            }
            catch (Exception e)
            {
                BackgroundError?.Invoke(this, new BackgroundErrorEventArgs
                {
                    Exception = e
                });
            }
        }

        private IIpcServiceHost BuildHost()
        {
            var services = ConfigureServices(new ServiceCollection());
            return new IpcServiceHostBuilder(services.BuildServiceProvider())
                .AddNamedPipeEndpoint<TServer>(name: _parameters.ServerEndpoint, pipeName: _parameters.ServerPipe)
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
                        .AddService(_parameters.ServerFactory);
                });
        }

        private IpcServiceClient<TClient> BuildClient()
        {
            return new IpcServiceClientBuilder<TClient>()
                .UseNamedPipe(_parameters.ClientPipe)
                .Build();
        }
    }
}

/*
Copyright (c) 2019 Integrative Software LLC
Created: 5/2019
Author: Pablo Carbonell
*/

using JKang.IpcServiceFramework;
using Microsoft.Extensions.DependencyInjection;
using System.Threading;
using System.Threading.Tasks;

namespace Electrolite.Common.Ipc
{
    public sealed class IpcPipeDuplex<TServer, TClient>
        where TServer : class
        where TClient : class
    {
        readonly IpcDuplexParameters<TServer> _parameters;
        public IIpcServiceHost Server { get; }
        public IpcServiceClient<TClient> Client { get; }

        public IpcPipeDuplex(IpcDuplexParameters<TServer> parameters)
        {
            _parameters = parameters;
            Server = BuildHost();
            Client = BuildClient();
        }

        public async Task RunAsync()
        {
            await Server.RunAsync();
        }

        public async Task RunAsync(CancellationToken token)
        {
            await Server.RunAsync(token);
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

/*
Copyright (c) 2019 Integrative Software LLC
Created: 5/2019
Author: Pablo Carbonell
*/

using Electrolite.Common.Ipc;
using Electrolite.Common.Main;
using JKang.IpcServiceFramework;
using System.Threading.Tasks;

namespace Electrolite.Windows.Main
{
    class BrowserClient
    {
        readonly IpcServiceClient<IBrowserHost> _client;

        public BrowserClient(string pipename)
        {
            _client = new IpcServiceClientBuilder<IBrowserHost>()
                .UseNamedPipe(pipename)
                .Build();
        }

        public async Task<StartupParameters> GetStartupParameters()
        {
            return await _client.OrderAsync(x => x.GetStartupOptions());
        }
    }
}

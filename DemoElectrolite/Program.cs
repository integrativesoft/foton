/*
Copyright (c) 2019 Integrative Software LLC
Created: 5/2019
Author: Pablo Carbonell
*/

using Electrolite.Common.Main;
using Electrolite.Main;
using System;
using System.Threading.Tasks;

namespace DemoElectrolite
{
    public static class Program
    {
        const string MyURL = "https://www.google.com";

        public static async Task Main()
        {
            Console.WriteLine("Starting...");
            var options = new ElectroliteOptions();
            var url = new Uri(MyURL);
            using (var session = ElectroliteApp.CreateSession(url, options))
            {
                session.RunBackground();
                await session.WaitForShutdown();
            }
        }
    }
}

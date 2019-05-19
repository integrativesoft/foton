/*
Copyright (c) 2019 Integrative Software LLC
Created: 5/2019
Author: Pablo Carbonell
*/

using Electrolite.Main;
using System;
using System.Threading.Tasks;

namespace DemoElectrolite
{
    public static class Program
    {
        const string MyURL = "http://html5test.com";

        public static async Task Main()
        {
            Console.WriteLine("Starting...");
            var url = new Uri(MyURL);
            using (var session = ElectroliteApp.CreateSession(url))
            {
                await session.RunAsync();
            }
        }
    }
}

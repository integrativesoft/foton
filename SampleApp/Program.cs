/*
Copyright (c) 2019 Integrative Software LLC
Created: 5/2019
Author: Pablo Carbonell
*/

using Electrolite.Core.Main;
using System;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;
using Electrolite.Common.Main;

namespace SampleApp
{
    public static class Program
    {
        private const string MyUrl = "http://html5test.com";

        public static async Task Main()
        {
            Console.WriteLine("Current directory:");
            Console.WriteLine(Directory.GetCurrentDirectory());
            var options = new ElectroliteOptions
            {
                Title = "Sample Electrolite app"
            };
            var url = new Uri(MyUrl);
            using (var session = ElectroliteApp.CreateSession(url, options))
            {
                session.SplashImagePath = GetIncludedImageFullPath("SampleSplash.jpg");
                session.BackgroundError += ((sender, args) =>
                {
                    Console.WriteLine(args.Exception.ToString());
                });
                session.RunBackground();
                await session.WaitForShutdown();
            }
        }

        private static string GetIncludedImageFullPath(string name)
        {
            var exePath = Assembly.GetEntryAssembly().Location;
            var folder = Path.GetDirectoryName(exePath);
            return Path.Combine(folder, name);
        }
    }
}

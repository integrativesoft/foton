/*
Copyright (c) 2019 Integrative Software LLC
Created: 5/2019
Author: Pablo Carbonell
*/

using Electrolite.Common.Main;
using Electrolite.Main;
using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;

namespace DemoElectrolite
{
    public static class Program
    {
        const string MyURL = "http://html5test.com";

        public static async Task Main()
        {
            var watch = new Stopwatch();
            watch.Start();
            Console.WriteLine("Starting...");
            var options = new ElectroliteOptions();
            var url = new Uri(MyURL);
            using (var session = ElectroliteApp.CreateSession(url, options))
            {
                session.SplashImagePath = GetImagePath("SampleSplash.jpg");
                session.OnReady += ((sender, args) =>
                {
                    watch.Stop();
                    Console.WriteLine($"Elapsed: {watch.Elapsed}");
                });
                session.BackgroundError += ((sender, args) =>
                {
                    Console.WriteLine(args.Exception.ToString());
                });
                session.RunBackground();
                await session.WaitForShutdown();
            }
        }

        private static string GetImagePath(string name)
        {
            var exePath = Assembly.GetEntryAssembly().Location;
            var folder = Path.GetDirectoryName(exePath);
            return Path.Combine(folder, name);
        }
    }
}

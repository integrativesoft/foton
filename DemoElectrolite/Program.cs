/*
Copyright (c) 2019 Integrative Software LLC
Created: 5/2019
Author: Pablo Carbonell
*/

using Electrolite.Main;
using System;

namespace DemoElectrolite
{
    public static class Program
    {
        const string MyURL = "http://html5test.com";

        public static void Main()
        {
            var url = new Uri(MyURL);
            using (var session = ElectroliteApp.CreateSession(url))
            {
                session.RunBlocking();
            }
        }
    }
}

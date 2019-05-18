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
        public static void Main(string[] args)
        {
            ElectroliteApp.Open(new Uri("https://www.google.com"));
        }
    }
}

/*
Copyright (c) 2019 Integrative Software LLC
Created: 5/2019
Author: Pablo Carbonell
*/

namespace Electrolite.Common.Main
{
    public class ElectroliteOptions
    {
        public int Width { get; set; }
        public int Height { get; set; }
        public string Title { get; set; }

        public static ElectroliteOptions GetDefaults()
        {
            return new ElectroliteOptions
            {
                Height = 800,
                Width = 1000,
                Title = "Electrolite App"
            };
        }
    }
}

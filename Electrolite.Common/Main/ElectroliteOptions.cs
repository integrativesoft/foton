/*
Copyright (c) 2019 Integrative Software LLC
Created: 5/2019
Author: Pablo Carbonell
*/

using System.Runtime.Serialization;

namespace Electrolite.Common.Main
{
    [DataContract]
    public class ElectroliteOptions
    {
        [DataMember]
        public int Width { get; set; }

        [DataMember]
        public int Height { get; set; }

        [DataMember]
        public string Title { get; set; }

        [DataMember]
        public string IconPath { get; set; }

        [DataMember]
        public bool MinButton { get; set; }

        [DataMember]
        public bool MaxButton { get; set; }

        [DataMember]
        public bool ShownInTaskbar { get; set; }

        public static ElectroliteOptions GetDefaults()
        {
            return new ElectroliteOptions
            {
                Height = 800,
                Width = 1000,
                Title = "Electrolite App",
                ShownInTaskbar = true
            };
        }
    }
}

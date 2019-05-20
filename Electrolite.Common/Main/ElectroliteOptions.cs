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
        public ElectroliteSize Size { get; set; }

        [DataMember]
        public ElectroliteSize MinSize { get; set; }

        [DataMember]
        public ElectroliteSize MaxSize { get; set; }

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
                Title = "Electrolite App",
                ShownInTaskbar = true
            };
        }
    }
}

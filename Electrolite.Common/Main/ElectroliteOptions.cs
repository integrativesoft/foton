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
        public ElectroliteSize MinSize { get; set; } = new ElectroliteSize(400, 300);

        [DataMember]
        public ElectroliteSize MaxSize { get; set; }

        [DataMember]
        public string Title { get; set; } = "Electrolite";

        [DataMember]
        public bool MinButton { get; set; } = true;

        [DataMember]
        public bool MaxButton { get; set; } = true;

        [DataMember]
        public bool ShownInTaskbar { get; set; } = true;

        [DataMember]
        public bool ShowIcon { get; set; } = true;

        [DataMember]
        public string IconPath { get; set; }
    }
}

/*
Copyright (c) 2019 Integrative Software LLC
Created: 5/2019
Author: Pablo Carbonell
*/

using System.Runtime.Serialization;

namespace Foton.Common.Main
{
    [DataContract]
    public class FotonOptions : IFotonOptions
    {
        [DataMember]
        public FotonSize Size { get; set; }

        [DataMember]
        public FotonSize MinSize { get; set; } = new FotonSize(1000, 700);

        [DataMember]
        public string Title { get; set; } = "Foton";

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

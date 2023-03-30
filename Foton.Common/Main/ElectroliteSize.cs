/*
Copyright (c) 2019 Integrative Software LLC
Created: 5/2019
Author: Pablo Carbonell
*/

using System.Runtime.Serialization;

namespace Foton.Common.Main
{
    [DataContract]
    public sealed class ElectroliteSize
    {
        [DataMember]
        public int Width { get; set; }

        [DataMember]
        public int Height { get; set; }

        public ElectroliteSize()
        {
        }

        public ElectroliteSize(int width, int height)
        {
            Width = width;
            Height = height;
        }
    }
}

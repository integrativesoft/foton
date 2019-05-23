/*
Copyright (c) 2019 Integrative Software LLC
Created: 5/2019
Author: Pablo Carbonell
*/

namespace Electrolite.Common.Main
{
    public interface IElectroliteOptions
    {
        ElectroliteSize Size { get; set; }
        ElectroliteSize MinSize { get; set; }
        string Title { get; set; }
        bool MinButton { get; set; }
        bool MaxButton { get; set; }
        bool ShownInTaskbar { get; set; }
        bool ShowIcon { get; set; }
        string IconPath { get; set; }
    }
}

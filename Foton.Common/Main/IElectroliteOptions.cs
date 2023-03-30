/*
Copyright (c) 2019 Integrative Software LLC
Created: 5/2019
Author: Pablo Carbonell
*/

namespace Foton.Common.Main
{
    public interface IFotonOptions
    {
        FotonSize Size { get; set; }
        FotonSize MinSize { get; set; }
        string Title { get; set; }
        bool MinButton { get; set; }
        bool MaxButton { get; set; }
        bool ShownInTaskbar { get; set; }
        bool ShowIcon { get; set; }
        string IconPath { get; set; }
    }
}

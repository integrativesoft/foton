/*
Copyright (c) 2019 Integrative Software LLC
Created: 5/2019
Author: Pablo Carbonell
*/

using System.Drawing;
using System.Globalization;
using System.Reflection;

namespace Electrolite.Common.Main
{
    public static class ElectroliteCommon
    {
        public static string GetClientEndpointName(int processId)
        {
            return "ElectroliteClientEndpoint_" + processId.ToString(CultureInfo.InvariantCulture);
        }

        public static string GetClientPipeName(int processId)
        {
            return "ElectroliteClientPipe_" + processId.ToString(CultureInfo.InvariantCulture);
        }

        public static Icon LoadIcon(this Assembly assembly, string resourceName)
        {
            using (var stream = assembly.GetManifestResourceStream(resourceName))
            {
                return new Icon(stream);
            }
        }
    }
}

/*
Copyright (c) 2019 Integrative Software LLC
Created: 5/2019
Author: Pablo Carbonell
*/

using System.Drawing;
using System.Globalization;
using System.IO;
using System.Reflection;

namespace Foton.Common.Main
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

        public static Image LoadImage(this Assembly assembly, string resourceName)
        {
            using (var stream = assembly.GetManifestResourceStream(resourceName))
            {
                return Image.FromStream(stream);
            }
        }

        public static string ElectroliteHostEndpoint(int id)
            => NameElement("ElectroliteHostEndpoint_{0}", id);

        public static string ElectroliteBrowserEndpoint(int id)
            => NameElement("ElectroliteEndpoint_{0}", id);

        public static string ElectroliteBrowser(int id)
            => NameElement("ElectroliteBrowser_{0}", id);

        public static string ElectroliteHost(int id)
            => NameElement("ElectroliteHost_{0}", id);

        private static string NameElement(string template, int id)
        {
            return string.Format(template, id.ToString(CultureInfo.InvariantCulture));
        }

        public static Image ImageFromBytes(byte[] byteArrayIn)
        {
            using (var ms = new MemoryStream(byteArrayIn))
            {
                return Image.FromStream(ms);
            }
        }
    }
}

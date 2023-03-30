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
    public static class FotonCommon
    {
        public static string GetClientEndpointName(int processId)
        {
            return "FotonClientEndpoint_" + processId.ToString(CultureInfo.InvariantCulture);
        }

        public static string GetClientPipeName(int processId)
        {
            return "FotonClientPipe_" + processId.ToString(CultureInfo.InvariantCulture);
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

        public static string FotonHostEndpoint(int id)
            => NameElement("FotonHostEndpoint_{0}", id);

        public static string FotonBrowserEndpoint(int id)
            => NameElement("FotonEndpoint_{0}", id);

        public static string FotonBrowser(int id)
            => NameElement("FotonBrowser_{0}", id);

        public static string FotonHost(int id)
            => NameElement("FotonHost_{0}", id);

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

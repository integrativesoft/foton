/*
Copyright (c) 2019 Integrative Software LLC
Created: 5/2019
Author: Pablo Carbonell
*/

using System;

namespace Foton.Common.Adapters
{
    public sealed class ProcessArguments
    {
        public int ParentProcessId { get; set; }
        public string SplashPath { get; set; }

        public static bool TryParse(string[] args, out ProcessArguments arguments)
        {
            if (args.Length >= 1 && int.TryParse(args[0], out int procesId))
            {
                arguments = new ProcessArguments
                {
                    ParentProcessId = procesId,
                    SplashPath = GetSplashFilename(args)
                };
                return true;
            }
            else
            {
                arguments = null;
                return false;
            }
        }

        private static string GetSplashFilename(string[] args)
        {
            if (args.Length < 2)
            {
                return "";
            }
            else
            {
                return Uri.UnescapeDataString(args[1]);
            }
        }

        public static ProcessArguments Parse(string[] args)
        {
            if (!TryParse(args, out var result))
            {
                throw new ArgumentException("Invalid parameters calling browser process.");
            }
            return result;
        }
    }
}

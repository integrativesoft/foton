﻿/*
Copyright (c) 2019 Integrative Software LLC
Created: 5/2019
Author: Pablo Carbonell
*/

using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using Electrolite.Common.Ipc;
using Electrolite.Common.Main;

namespace Electrolite.Common.Adapters
{
    public static class PlatformCommon
    {
        public static Process LaunchBrowser(string endpointName, string path)
        {
            var process = new Process
            {
                StartInfo = new ProcessStartInfo
                {
                    CreateNoWindow = false,
                    UseShellExecute = true,
                    FileName = path,
                    Arguments = endpointName
                },
                EnableRaisingEvents = true
            };
            try
            {
                process.Start();
            }
            catch (System.Exception e)
            {
                System.Console.Write(e.Message);
            }
            return process;
        }

        public static string GetAssemblyPath()
        {
            var assembly = Assembly.GetAssembly(typeof(PlatformCommon));
            var path = assembly.CodeBase;
            return Path.GetDirectoryName(path);
        }

        public static void VerifyFileExists(string path)
        {
            if (!File.Exists(path))
            {
                string message = $"File not found: {path}";
                throw new System.InvalidProgramException(message);
            }
        }

        public static IpcPipeDuplex<IBrowserWindow, IBrowserHost> CreateClientDuplex(int parentId, Func<IBrowserWindow> factory)
        {
            return new IpcPipeDuplex<IBrowserWindow, IBrowserHost>(new IpcDuplexParameters<IBrowserWindow>
            {
                ClientPipe = ElectroliteCommon.ElectroliteHost(parentId),
                ServerEndpoint = ElectroliteCommon.ElectroliteBrowserEndpoint(parentId),
                ServerPipe = ElectroliteCommon.ElectroliteBrowser(parentId),
                ServerFactory = (service => factory())
            });
        }
    }
}
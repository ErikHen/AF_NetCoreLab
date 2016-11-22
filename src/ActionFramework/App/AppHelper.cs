﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Loader;
using Microsoft.DotNet.ProjectModel;

namespace ActionFramework.App
{
    public static class AppHelper
    {
        public static List<App> GetInstalledApps()
        {
            var apps = new List<App>();
            var appsPath = GetInstalledAppsDirectory();
            foreach (var appDirectory in Directory.GetDirectories(appsPath))
            {
                var pathSegements = appDirectory.Split('\\');
                var appName = pathSegements[pathSegements.GetUpperBound(0)];
                var filePath = appDirectory + "\\" + appName + ".dll";
                if (File.Exists(filePath))
                {
                    var appAssembly = AssemblyLoadContext.Default.LoadFromAssemblyPath(filePath);
                    var appType = appAssembly.GetType(appName + "." + appName);
                    var appInstance = Activator.CreateInstance(appType) as App;
                    apps.Add(appInstance);
                }
            }

            return apps;
        }

        private static string GetInstalledAppsDirectory()
        {
            //get path to the running application's directory
            var applicationPath = ProjectRootResolver.ResolveRootDirectory(Directory.GetCurrentDirectory());

            const string installedAppsDirectoryName = "InstalledApps";
            var installedAppsPath = applicationPath + "\\" + installedAppsDirectoryName; //todo: check if this works on different OS

            if (!Directory.Exists(installedAppsPath))
            {
                Directory.CreateDirectory(installedAppsPath);
            }

            return installedAppsPath;
        }

        public static string GetAppDirectory(string appName)
        {
            //System.Threading.Timer
            return GetInstalledAppsDirectory() + "\\" + appName;
        }

    }
}
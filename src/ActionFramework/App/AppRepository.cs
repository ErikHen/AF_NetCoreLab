using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Loader;
using Microsoft.DotNet.ProjectModel;
using System.Linq;

namespace ActionFramework.App
{
    public static class AppRepository
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

        public static App GetApp(string appName)
        {
            var filePath = GetInstalledAppsDirectory() + "\\" + appName + "\\" + appName + ".dll";
            if (!File.Exists(filePath))
            {
                return null;
            }

            var appAssembly = AssemblyLoadContext.Default.LoadFromAssemblyPath(filePath);
            var appType = appAssembly.GetType(appName + "." + appName);
            var appInstance = Activator.CreateInstance(appType) as App;
            return appInstance;
        }

        public static bool RunAction(string appName, string actionName)
        {
            var app = GetApp(appName);
            var success = false;
            if (app != null)
            {
                var action = app.Actions.FirstOrDefault(a => a.ActionName == actionName);
                success = app.RunAction(action);
            }

            return success;
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
            var appDirectory = GetInstalledAppsDirectory() + "\\" + appName;
            if (!Directory.Exists(appDirectory))
            {
                return null;
            }

            return GetInstalledAppsDirectory() + "\\" + appName;
        }

    }
}

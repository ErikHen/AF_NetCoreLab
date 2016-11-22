using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using ActionFramework.Model;
using ActionFramework.App;

namespace ActionFramework.Agent.API
{
    [Route("api/[controller]/[action]")]
    public class TestController : Controller
    {
        [HttpGet]
        public List<ActionFramework.App.App> Get()
        {
            var apps = AppHelper.GetInstalledApps();
            //var asdf = new List<ActionFramework.App.App>();
            //foreach (var app in apps)
            //{
            //    asdf.Add(app.Description);
            //    //foreach
            //}
            return apps;
        }


        [HttpGet]
        public SystemInformation SystemInformation()
        {
            return new SystemInformation()
            {
                OsDescription = System.Runtime.InteropServices.RuntimeInformation.OSDescription
            };
        }
    }
}

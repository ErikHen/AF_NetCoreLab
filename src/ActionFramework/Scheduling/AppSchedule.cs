using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using ActionFramework.App;
using Newtonsoft.Json;

namespace ActionFramework.Scheduling
{
    public class AppSchedule
    {
        private const string FileName = "AppSchedule.json";
        public string AppName { get; set; }
        public List<ActionSchedule> ActionSchedules { get; set; }

        //todo
        //public DateTime LastUpdated { get; set; }
        //public string LastUpdatedBy { get; set; } 

        public AppSchedule()
        {
            ActionSchedules = new List<ActionSchedule>();
        }

        public bool Save()
        {
            var filePath = AppHelper.GetAppDirectory(AppName) + "\\" + FileName;
            var json = JsonConvert.SerializeObject(this, Formatting.Indented);
            File.WriteAllText(filePath, json);
            return true;
        }
    }
}

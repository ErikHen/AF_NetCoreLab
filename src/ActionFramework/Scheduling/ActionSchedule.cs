using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ActionFramework.Scheduling
{
    public class ActionSchedule
    {
        public string AppName { get; set; }
        public string ActionName { get; set; }
        public int Interval { get; set; }
        public IntervalUnit Unit { get; set; }

        /// <summary>
        /// Next scheduled run of this action. Use UTC date/time.
        /// </summary>
        public DateTime NextRun { get; set; }
        /// <summary>
        /// Action will not run after this date.
        /// </summary>
        public DateTime StopDateTime { get; set; }

        /// <summary>
        /// When the action schedule was last updated. Use UTC date/time.
        /// </summary>
       // public DateTime LastUpdated { get; set; }
        //todo: public string LastUpdatedBy { get; set; } 

        public double IntervalAsSeconds()
        {
            double seconds = Interval;
            if (Unit == IntervalUnit.Minute)
            {
                seconds = seconds * 60;
            }
            if (Unit == IntervalUnit.Hour)
            {
                seconds = seconds * 3600;
            }

            return seconds;
        }
           

        public ActionSchedule(string appName, string actionName)
        {
            AppName = appName;
            ActionName = actionName;
        }
    }
}
    

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ActionFramework.Scheduling
{
    public class ActionSchedule
    {
        public string ActionName { get; set; }
        public int Interval { get; set; }
        public IntervalUnit Unit { get; set; }
        /// <summary>
        /// Next scheduled run of this action. Use UTC date/time.
        /// </summary>
        public DateTime NextRun { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ActionFramework.Agent.App;
using ActionFramework.Scheduling;

namespace ActionFramework.Agent.Scheduling
{
    public class ActionTimer
    {
        public ActionTimer(ActionSchedule actionSchedule)
        {
            var state = new TimerState(actionSchedule);

            var dueTime = TimeSpan.FromSeconds(1); //one second
            if (actionSchedule.NextRun > DateTime.UtcNow)
            {
                dueTime = actionSchedule.NextRun - DateTime.UtcNow;
            }

            // Create a timer 
            var timer = new Timer(RunScheduledAction, state, dueTime, TimeSpan.FromSeconds(actionSchedule.IntervalAsSeconds()));

            // Keep a handle to the timer, so it can be disposed.
            state.Timer = timer;
        }

        static void RunScheduledAction(object state)
        {
            var s = (TimerState)state;
            s.Counter++;
            Console.WriteLine("{0} Running scheduled action {1}.", DateTime.UtcNow.TimeOfDay, s.Counter);
           
            if (s.ActionSchedule.StopDateTime != DateTime.MinValue && s.ActionSchedule.StopDateTime <= DateTime.UtcNow)
            {
                Console.WriteLine("disposing of timer...");
                s.Timer.Dispose();
                s.Timer = null;
            }
            else
            {
                AppRepository.RunAction(s.ActionSchedule.AppName, s.ActionSchedule.ActionName);

                //update schedule with next run date
                s.ActionSchedule.NextRun = DateTime.UtcNow.AddSeconds(s.ActionSchedule.IntervalAsSeconds());
                ActionScheduleRepository.SaveActionSchedule(s.ActionSchedule);

                //todo: update timer with dueTime infinity?
                //s.Timer.Change()
            }

            
        }

        

        
    }



    public class TimerState
    {
        public int Counter { get; set; } //will this be used?
        public bool IsRunning { get; set; }
        public Timer Timer { get; set; }
        public ActionSchedule ActionSchedule { get; set; }

        public TimerState(ActionSchedule actionSchedule)
        {
            Counter = 0;
            IsRunning = false;
            ActionSchedule = actionSchedule;
        }
    }
}

using Microsoft.AspNetCore.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Server.Kestrel.Internal.Networking;

namespace ActionFramework.Agent
{
    public class Program
    {
        //private static Timer _timer;
        private static int _counter = 0;
        public static void Main(string[] args)
        {
            // Console.WriteLine("enter timer");
            // var _timer = new System.Timer.Timer // Timer(TestCallBack, _counter, 1000, 2000);
            Console.WriteLine("Setting up action timers");
            var scheduler = new Scheduling.Scheduler();
            scheduler.ScheduleAllActions();

            var host = new WebHostBuilder()
                .UseKestrel()
                .UseUrls("http://localhost:7406") //lägg i config?
                .UseStartup<Startup>()
                .Build();

            host.Run();

            
        }


        //private static void TestCallBack(object counter)
        //{
        //    var counter2 = (int)counter;
        //    Console.WriteLine("Timercallback " + counter2 + " " + _counter + " " + DateTime.Now);
        //    _counter++;
        //    if (_counter > 10)
        //    {
        //        _timer.Dispose();
        //    }
        //    //_counter = counter2;
        //    //timer.Change(Timeout.Infinite, Timeout.Infinite); //stops the timer
        //    //Thread.Sleep(3000); //doing some long operation
        //    //timer.Change(0, 1000 * 10);  //restarts the timer
        //}
    }
}

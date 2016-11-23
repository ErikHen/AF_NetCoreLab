using System;
using ActionFramework.Log;

namespace ActionFramework.App
{
    public abstract class Action
    {
        //public abstract Guid ActionId { get; }
        public string ActionName => GetType().Name;
        public abstract string Description { get; }
        /// <summary>
        /// Should never be called directly. Use "RunAction" method in app instead.
        /// </summary>
        /// <returns></returns>
        public abstract bool Execute(out string actionMessage);

      
       
    }
}

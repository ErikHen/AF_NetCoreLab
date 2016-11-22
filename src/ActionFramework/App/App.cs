using System;
using System.Collections.Generic;

namespace ActionFramework.App
{
    public abstract class App
    {
        public abstract Guid AppId { get; }
        public abstract string Description { get; }
        public abstract List<Action> Actions { get; }

        public string Name => GetType().Name;
    }
}

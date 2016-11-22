using System;

namespace ActionFramework.App
{
    public abstract class Action
    {
        public abstract Guid ActionId { get; }
        public abstract string Name { get; }
        public abstract string Description { get; }
        public abstract string Execute();
    }
}

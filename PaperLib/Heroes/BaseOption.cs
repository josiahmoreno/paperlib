using Enemies;
using MenuData;
using System;
using System.Collections.Generic;

namespace Heroes
{
    public abstract class BaseOption : IOption
    {
      

        public virtual string Name { get; }
        public virtual TargetType TargetType { get; }
        public abstract Guid? Guid { get; }

        public bool Equals(IOption other)
        {

            return other != null && GetType() == other.GetType()
                && Guid == other.Guid && Name == other.Name &&
                TargetType == other.TargetType;
        }

        public virtual void Execute(Battle.Battle battle, object activeHero, Enemy[] targets, Action<IEnumerable<Tuple<Enemy, bool>>> p)
        {
            throw new NotImplementedException();
        }
    }
}
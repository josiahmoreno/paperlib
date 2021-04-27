using Enemies;
using System;
using System.Collections.Generic;
using Attributes;
using Battle;

namespace MenuData
{
    public interface IOption
    {
        Guid? Guid { get; }
        string Name { get;  }

        TargetType TargetType { get; }
        void Execute(Battle.Battle battle, object activeHero, Enemy[] targets, Action<IEnumerable<Tuple<Enemy, bool>>> p);
       
    }

    public enum TargetType
    {
        Single,
        All
    }
}
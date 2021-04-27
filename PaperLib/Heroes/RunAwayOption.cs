using System;
using System.Collections.Generic;
using Enemies;
using MenuData;

namespace Heroes
{
    public class RunAwayOption : IOption
    {
        public Guid? Guid => System.Guid.Parse("2744296b-ed8e-4502-b215-20cf57c9d962");
        public string Name { get; }
        public TargetType TargetType { get; }
        public void Execute(Battle.Battle battle, object activeHero, Enemy[] targets, Action<IEnumerable<Tuple<Enemy, bool>>> p)
        {
            throw new NotImplementedException();
        }
    }
}
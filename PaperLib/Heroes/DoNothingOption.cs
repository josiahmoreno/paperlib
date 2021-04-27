using System;
using System.Collections.Generic;
using Enemies;
using MenuData;

namespace Heroes
{
    public class DoNothingOption : IOption
    {
        public Guid? Guid => System.Guid.Parse("0b5a66b7-0bed-4aaa-8211-50d868b19ed3");
        public string Name { get; }
        public TargetType TargetType { get; }
        public void Execute(Battle.Battle battle, object activeHero, Enemy[] targets, Action<IEnumerable<Tuple<Enemy, bool>>> p)
        {
            //throw new NotImplementedException();
        }
    }
}
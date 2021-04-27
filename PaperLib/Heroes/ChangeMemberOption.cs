using System;
using System.Collections.Generic;
using System.Linq;
using Battle;
using Enemies;
using MenuData;

namespace Heroes
{
    public class ChangeMemberOption : IOption
    {
        public ChangeMemberOption(Hero[] partners)
        {
            this.PartnerOptions = partners.Select(hero => new PartnerOption(hero.ToString(),hero)).ToArray();
        }

        public string Name { get; }

        public Guid? Guid => System.Guid.Parse("8da95dad-6310-4680-bd30-01dffde99f00");

        public TargetType TargetType { get; }
        public IOption[] PartnerOptions { get; }

        public void Execute(Battle.Battle battle, object activeHero, Enemy[] targets, Action<IEnumerable<Tuple<Enemy, bool>>> p)
        {
            //battle.Heroes[1] = battle.Pa;
            throw new NullReferenceException();
        }
    }
}
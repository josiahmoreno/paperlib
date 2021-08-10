using System;
using System.Linq;
using Battle;
using Enemies;
using Items;
using System.Collections.Generic;
using Heroes;

namespace MenuData
{
    internal class ItemOption: BaseOption
    {
        private IItem item;

        public ItemOption(IItem item)
        {
            this.item = item;
        }

        public override Guid? Guid { get; }
        public string Name => item.Name;

        public override TargetType TargetType => item.TargetType;
        public HashSet<Attributes.Attributes> PossibleEnemyTypes { get; set; }

        public override void Execute(Battle.Battle battle, object activeHero, Enemy[] targets, Action<IEnumerable<Tuple<Enemy, bool>>> p)
        {
            Console.WriteLine($"ItemOption a- {Name} targets={targets.Length}");
            if (item.Damage != null)
            {
                
                targets.ToList().ForEach(target => target.TakeDamage((int) item.Damage));
            }
            Console.WriteLine($"Item Option b -{Name}");
            p?.Invoke(null);
            Console.WriteLine($"Item Option c -{Name}");
        }

        public bool Equals(IOption other)
        {
            return base.Equals(other) && other is ItemOption itemOption && itemOption.item == item;

        }

        public override string ToString()
        {
            return $"ItemOption {{{Name} {TargetType}}} ";
        }
    }
}
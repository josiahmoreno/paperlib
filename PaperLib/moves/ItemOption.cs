using System;
using System.Linq;
using Battle;
using Enemies;
using Items;
using System.Collections.Generic;

namespace MenuData
{
    internal class ItemOption: IOption
    {
        private IItem item;

        public ItemOption(IItem item)
        {
            this.item = item;
        }

        public Guid? Guid { get; }
        public string Name => item.Name;

        public TargetType TargetType => item.TargetType;
        public HashSet<Attributes.Attributes> PossibleEnemyTypes { get; set; }

        public void Execute(Battle.Battle battle, object activeHero, Enemy[] targets, Action<IEnumerable<Tuple<Enemy, bool>>> p)
        {
            Console.WriteLine($"ItemOption a- {GetType().Name}");
            if (item.Damage != null)
            {
                
                targets.ToList().ForEach(target => target.TakeDamage((int) item.Damage));
            }
            Console.WriteLine($"Item Option b -{GetType().Name}");
            p?.Invoke(null);
            Console.WriteLine($"Item Option c -{GetType().Name}");
        }
    }
}
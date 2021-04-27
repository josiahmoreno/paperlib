using Battle;
using Enemies;
using MenuData;
using System;
using System.Collections.Generic;

namespace MenuData
{
    public class Option: IOption
    {
        public Guid? Guid => null;
        public string Name { get => store.FetchName(this); }
        public TargetType TargetType { get; private set; }
        public HashSet<Attributes.Attributes> PossibleEnemyTypes { get; set; }

        public Battle.ITextBubbleSystem TextSystem;
        public IActionMenuStore store;
        public Option(IActionMenuStore store, Battle.ITextBubbleSystem system, TargetType targetType)
        {
            this.store = store;
            this.TextSystem = system;
            this.TargetType = TargetType;
        }
        public Option(string v)
        {
            
        }

    

        public virtual void Execute(Battle.Battle battle,object activeHero, Enemy[] targets, System.Action<IEnumerable<Tuple<Enemy, bool>>> p)
        {
            throw new System.NotImplementedException();
        }
    }
}
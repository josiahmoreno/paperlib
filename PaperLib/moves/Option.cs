using Battle;
using Enemies;
using MenuData;
using System;
using System.Collections.Generic;

namespace MenuData
{
    [Obsolete]
    public class Option: IOption, IEquatable<IOption>
    {
   
        public Guid? Guid => null;
        private string _name;
        public string Name { get => _name ?? store.FetchName(this); }
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
            this._name = v;
        }

    

        public virtual void Execute(Battle.Battle battle,object activeHero, Enemy[] targets, System.Action<IEnumerable<Tuple<Enemy, bool>>> p)
        {
            throw new System.NotImplementedException();
        }


        public bool Equals(IOption other)
        {
            return  other != null && Guid == other.Guid;
        }
    }
}
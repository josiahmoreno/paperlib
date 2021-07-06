using System;
using System.Linq;
using Battle;

namespace MenuData
{
    internal class ActionMenuData: IActionMenuData
    {
        private readonly IActionMenuStore store;

        public string Name { get; internal set; }

        public IOption[] Options { get; internal set; }

        public ActionMenuData(string v,params IOption[] options)
        {
            this.store = store;
            this.Name = v;
            this.Options = options;
        }

        protected string GetAttackName(Attacks.IAttack item)
        {
            return store.FetchName(item.Identifier);
        }

        public bool Equals(IActionMenuData other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            var result =  Name == other.Name && Enumerable.SequenceEqual(Options, other.Options);
            return result;
        }

       

     
    }
}
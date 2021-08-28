using Attacks;
using Enemies;
using Heroes;
using System;

namespace PaperLib.Sequence
{
    public class DamageTarget : IDamageTarget
    {
        public IAttack Attack { get; set; }

        public IEntity hero { get; set; }

        public IEntity target { get; set; }

        private bool? quicktimeResult = null;
        public Func<bool> successfulQuicktime { get; set; }

        public DamageTarget(IAttack attack, IEntity hero, IEntity target, Func<bool> successfulQuicktime)
        {
            Attack = attack;
            this.hero = hero;
            this.target = target;
            this.successfulQuicktime = successfulQuicktime;
        }

        public bool GetQuicktimeResult()
        {
            if (quicktimeResult == null)
            {
                quicktimeResult = successfulQuicktime();
            }

            return  quicktimeResult ?? throw  new Exception();
        }
    }
}
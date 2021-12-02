using System;
using System.Collections.Generic;
using Attacks;
using Battle;
using Enemies;
using Heroes;
using PaperLib.Sequence;

namespace MenuData
{
    internal class HammerOption : AttackOption
    {
        private ISequence sequence;

        public HammerOption(IActionMenuStore store, IAttack hammer, ISequence sequence) : base(store,"Hammer", hammer, TargetType.Single,true)
        {
            this.sequence = sequence;   
        }

        public override void Execute(Battle.Battle battle, object activeHero, Enemy[] targets, Action<IEnumerable<Tuple<Enemy, bool>>> p)
        {
            Console.WriteLine($"{GetType().Name} - execute {activeHero}, {string.Join(",",(object[])targets) }");

            var mario = activeHero as Hero;
            if (mario.Sequenceable == null)
            {
                throw new NullReferenceException($"{activeHero}'s sequenceable can't be null");
            }
            foreach(var tar in targets)
            {
                if(tar.Sequenceable == null)
                {
                    throw new NullReferenceException($"{tar}'s sequenceable can't be null");
                }
            }
            var jump = Attack;
            var damageTarget = new DamageTarget(Attack, mario, targets[0], () => {
                return Battle.Battle.ActionCommandCenter.FetchSequence().Sucessful;
            });
            
            void onComplete(object sender, EventArgs args)
            {
                sequence.OnComplete -= onComplete;
                var list = new List<Tuple<Enemy, bool>>();
                list.Add(new Tuple<Enemy, bool>(damageTarget.target as Enemy, damageTarget.GetQuicktimeResult()));
                p(list);
            }
            sequence.OnComplete += onComplete;
            sequence.Execute(mario.Sequenceable, targets[0].Sequenceable, damageTarget);
        }
    }
}
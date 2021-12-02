using Attacks;
using Battle;
using Enemies;
using Heroes;
using PaperLib.Sequence;
using System;
using System.Collections.Generic;

namespace MenuData
{
    internal class JumpOption : AttackOption
    {
        public JumpOption(IActionMenuStore store,string name, IAttack item, TargetType targetType) : base(store,name, item, targetType, true)
        {
        }

        public override void Execute(Battle.Battle battle, object activeHero, Enemy[] targets, Action<IEnumerable<Tuple<Enemy, bool>>> p)
        {
            Console.WriteLine($"{GetType().Name} - execute {activeHero}");
            
            var mario = activeHero as Hero;
            if (mario.Sequenceable == null)
            {
                throw new NullReferenceException("sequenceable can't be null");
            }
            var jump = Attack;
            var damageTarget = new DamageTarget(Attack, mario, targets[0], () => {
                return Battle.Battle.ActionCommandCenter.FetchSequence().Sucessful;
            });
            var jumpSequence = new JumpSequence(Battle.Battle.Logger);
            void onComplete(object sender,EventArgs args)
            {
                jumpSequence.OnComplete -= onComplete;
                var list = new List<Tuple<Enemy, bool>>();
                list.Add(new Tuple<Enemy, bool>(damageTarget.target as Enemy, damageTarget.GetQuicktimeResult()));
                p(list);
            }
            jumpSequence.OnComplete += onComplete;
            jumpSequence.Execute(mario.Sequenceable, targets[0].Sequenceable, damageTarget); 
        }
    }
}
﻿using Attacks;
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
        public JumpOption(IActionMenuStore store,string name, IAttack item, TargetType targetType) : base(store,name, item, targetType)
        {
        }

        public override void Execute(Battle.Battle battle, object activeHero, Enemy[] targets, Action<IEnumerable<Tuple<Enemy, bool>>> p)
        {
            var mario = activeHero as Hero;
            var jump = Attack;
            var damageTarget = new DamageTarget(Attack, mario, targets[0], () => {
                return false;
            });
            var jumpSequence = new JumpSequence(null, damageTarget);
            void onComplete(object sender,EventArgs args)
            {
                jumpSequence.OnComplete -= onComplete;
                var list = new List<Tuple<Enemy, bool>>();
                list.Add(new Tuple<Enemy, bool>(damageTarget.target as Enemy, damageTarget.successfulQuicktime()));
                p(list);
            }
            jumpSequence.OnComplete += onComplete;
            jumpSequence.Execute(mario.Sequenceable, targets[0].Sequenceable); ;
        }
    }
}
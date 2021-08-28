using System;
using System.Collections.Generic;
using Battle;
using Enemies;
using Heroes;
using PaperLib.Sequence;

namespace Battle
{
    public  class RegularAttackWrapper : IEnemyAttack
    {
        public RegularAttackWrapper(Attacks.Attacks goomaKingKick, int damage)
        {
            this.Power = damage;
        }

        public int Power { get; }

        public Attacks.Attacks Identifier => throw new NotImplementedException();

        public bool CanHitFlying()
        {
            throw new NotImplementedException();
        }

        public bool Equals(IEnemyAttack other)
        {
            return other != null && GetType() == other.GetType() &&  Power == other.Power;
        }

        public void Execute(object active, Hero hero, IBattleAnimationSequence battleAnimationSequence,  Action p)
        {
            var enemy = active as Enemy;
            //hero.TakeDamage(this, battleAnimationSequence.Sucessful);
            //p?.Invoke();
            
           
            var damageTarget = new DamageTarget(this, enemy,hero, () => {
                return Battle.ActionCommandCenter.FetchSequence().Sucessful;
            });
            var jumpSequence = new JumpSequence(Battle.Logger, damageTarget);
            void onComplete(object sender, EventArgs args)
            {
                jumpSequence.OnComplete -= onComplete;
                p();
            }
            jumpSequence.OnComplete += onComplete;
            jumpSequence.Execute(enemy.Sequenceable, hero.Sequenceable); ;
        }

        public bool IsGroundOnly()
        {
            throw new NotImplementedException();
        }
    }
}
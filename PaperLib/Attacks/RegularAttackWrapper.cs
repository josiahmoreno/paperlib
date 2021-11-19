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
            this.Identifier = goomaKingKick;
            this.Power = damage;
        }

        public int Power { get; }

        public Attacks.Attacks Identifier { get; }

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
            Console.WriteLine($"RegularAttackWrapper - {active} is attacking {hero} with {Identifier}");
            //hero.TakeDamage(this, battleAnimationSequence.Sucessful);
            //p?.Invoke();
            
           
            var damageTarget = new DamageTarget(this, enemy,hero, () => {
                return Battle.ActionCommandCenter.FetchSequence().Sucessful;
            });
            var jumpSequence = new JumpSequence(Battle.Logger);
            void onComplete(object sender, EventArgs args)
            {
                jumpSequence.OnComplete -= onComplete;
                p();
            }
            jumpSequence.OnComplete += onComplete;
            jumpSequence.Execute(enemy.Sequenceable, hero.Sequenceable, damageTarget); ;
        }

        public bool IsGroundOnly()
        {
            throw new NotImplementedException();
        }
    }
}
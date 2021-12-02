using System;
using Battle;
using Heroes;
using Enemies;

namespace Attacks
{
    internal class HealAttack : IEnemyAttack
    {
        private Attacks fuzzieSuck;
        private int damage;
        private int heal;

        public HealAttack(Attacks fuzzieSuck, int v1, int v2)
        {
            this.fuzzieSuck = fuzzieSuck;
            this.damage = v1;
            this.heal = v2;
        }

        public int Power => damage;

        public Attacks Identifier => Attacks.HealAttack;

        public void Execute(object active, Hero hero, IBattleAnimationSequence battleAnimationSequence, Action p)
        {
            var success = battleAnimationSequence.Sucessful;
            if (!success)
            {
                (active as Enemy).Health.Heal(heal);
            }
            hero.TakeDamage(this, null, success);
            p?.Invoke();
        }

        public bool Equals(IEnemyAttack other)
        {
            return other != null && GetType() == other.GetType() && other is HealAttack he && Power == other.Power && heal == he.heal;
        }

        public bool IsGroundOnly()
        {
           return false;
        }

        public bool CanHitFlying()
        {
           return true;
        }
    }
}
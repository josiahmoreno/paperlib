using System;
using Battle;
using Heroes;
using Enemies;

namespace Attacks
{
    internal class HealAttack : IEnemyAttack
    {
        private EnemyAttack fuzzieSuck;
        private int damage;
        private int heal;

        public HealAttack(EnemyAttack fuzzieSuck, int v1, int v2)
        {
            this.fuzzieSuck = fuzzieSuck;
            this.damage = v1;
            this.heal = v2;
        }

        public int Damage => damage;

        public void Execute(object active, Hero hero, IBattleAnimationSequence battleAnimationSequence, Action p)
        {

            if (battleAnimationSequence.Sucessful)
            {
                (active as Enemy).Health.Heal(heal);
            }
            hero.TakeDamage(this, battleAnimationSequence.Sucessful);
            p?.Invoke();
        }
    }
}
using System;
using Battle;
using Heroes;

namespace Battle
{
    public  class RegularAttack : IEnemyAttack
    {
        public RegularAttack(EnemyAttack goomaKingKick, int damage)
        {
            this.Damage = damage;
        }

        public int Damage { get; }

        public void Execute(object active, Hero hero, IBattleAnimationSequence battleAnimationSequence,  Action p)
        {

            hero.TakeDamage(this, battleAnimationSequence.Sucessful);
            p?.Invoke();
        }
    }
}
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

        public bool Equals(IEnemyAttack other)
        {
            return other != null && GetType() == other.GetType() &&  Damage == other.Damage;
        }

        public void Execute(object active, Hero hero, IBattleAnimationSequence battleAnimationSequence,  Action p)
        {

            hero.TakeDamage(this, battleAnimationSequence.Sucessful);
            p?.Invoke();
        }
    }
}
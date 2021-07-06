using System;
using Heroes;

namespace Battle
{
    public interface IEnemyAttack : IEquatable<IEnemyAttack>
    {
        int Damage { get; }
        void Execute(object active, Hero hero,IBattleAnimationSequence battleAnimationSequence ,Action p);
    }
}
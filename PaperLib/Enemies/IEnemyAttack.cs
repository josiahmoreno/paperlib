using System;
using Attacks;
using Heroes;

namespace Battle
{
    public interface IEnemyAttack : IAttack, IEquatable<IEnemyAttack>
    {
       
        void Execute(object active, Hero hero,IBattleAnimationSequence battleAnimationSequence ,Action p);
    }
}
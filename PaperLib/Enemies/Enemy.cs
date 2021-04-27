using Attacks;
using Attributes;
using Battle;
using Heroes;
using System;
using System.Collections.Generic;
using Tests;

namespace Enemies
{
    public interface Enemy
    {
        IHealth Health { get; set; }
        bool IsFlying { get; }
        bool IsSpiked { get; }
        IAttribute[] Attrs { get; }

        EnemyType EnemyType { get; }
        bool TakeDamage(IProtection protection,IAttack hammer,bool ActionCommandSuccessful);

        void Kill();

        bool IsAlive();

        bool IsDead { get; }

        event EventHandler OnKilled;


        IEnemyAttack GetRandomMove();
        bool TakeDamage(int damage);

        List<IEnemyAttack> Sequence { get; }

        object PostDamagePhase(bool item2);

        GameText FetchTattleData();
    }
}
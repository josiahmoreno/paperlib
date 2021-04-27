using System;
using Battle;
using Heroes;

namespace Battle
{
    public class ScriptAttack : IEnemyAttack
    {
        private object goomnutJump;

        
        public ScriptAttack(EnemyAttack goomnutJump, int damage = 2)
        {
            this.goomnutJump = goomnutJump;
        }

        public int Damage => 2;

        public void Execute(object active, Hero hero, IBattleAnimationSequence battleAnimationSequence, Action p)
        {
            hero.Health.TakeDamage(Damage);
            p.Invoke();
        }
    }

    public enum EnemyAttack
    {
        GoomnutJump,
        GoomaKingKick,
        PowerJump,
        JrTroopaPowerJump,
        JrTroopaJump,
        MagikoopTestAttack1,
        FuzzieSuck,
        GoombaBonk
    }
}
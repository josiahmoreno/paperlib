using System;
using Battle;
using Heroes;

namespace Battle
{
    public class ScriptAttack : IEnemyAttack
    {
        private object goomnutJump;

        
        public ScriptAttack(Attacks.Attacks goomnutJump, int damage = 2)
        {
            this.goomnutJump = goomnutJump;
        }

        public int Power => 2;

        public Attacks.Attacks Identifier => throw new NotImplementedException();

        public bool CanHitFlying()
        {
            throw new NotImplementedException();
        }

        public bool Equals(IEnemyAttack other)
        {
            return other != null && GetType() == other.GetType() && Power == other.Power;
        }
        public void Execute(object active, Hero hero, IBattleAnimationSequence battleAnimationSequence, Action p)
        {
            hero.Health.TakeDamage(Power);
            p.Invoke();
        }

        public bool IsGroundOnly()
        {
            throw new NotImplementedException();
        }
    }

  
}
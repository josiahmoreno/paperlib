using Attributes;
using Battle;
using Enemies;

namespace Enemies
{
    public class RedGoomba : Goomba
    {
 

        public RedGoomba(int currentHealth) : base(new HealthImpl(currentHealth, 6))
        {
            this.Moves.Add(new RegularAttack(EnemyAttack.GoombaBonk, 1));
        }
    }
}
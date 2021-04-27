using Attributes;
using Battle;

namespace Enemies
{
    public class NewBlueGoomba: NewGoomba
    {
        public NewBlueGoomba(ITattleStore tattleStore) : base(new HealthImpl(6),tattleStore)
        {
        }

        public NewBlueGoomba(IHealth health, ITattleStore tattleStore) : base(health, tattleStore)
        {
        }
        
  
        public NewBlueGoomba(int currentHealth, ITattleStore tattleStore) : base(new HealthImpl(currentHealth, 6),tattleStore)
        {
            this.Moves.Add(new RegularAttack(EnemyAttack.GoombaBonk, 1));
        }
        
    }
}
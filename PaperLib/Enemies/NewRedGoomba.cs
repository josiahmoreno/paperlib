using Attributes;
using Battle;

namespace Enemies
{
    public class NewRedGoomba: NewBaseEnemy
    {
        public NewRedGoomba(ITattleStore tattleStore) : base(new HealthImpl(6),tattleStore)
        {
        }

        public NewRedGoomba(IHealth health, ITattleStore tattleStore) : base(health, tattleStore)
        {
        }
        
  
        public NewRedGoomba(int currentHealth, ITattleStore tattleStore) : base(new HealthImpl(currentHealth, 6),tattleStore)
        {
            this.Moves.Add(new RegularAttackWrapper(Attacks.Attacks.GoombaBonk, 1));
        }

        public override string Identifier { get; set; } = "RedGoomba";
    }
}
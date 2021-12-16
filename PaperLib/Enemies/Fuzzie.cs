using Attacks;
using Attributes;
using Battle;
using Enemies;

namespace Enemies
{
    public class Fuzzie : NewBaseEnemy
    {


        public Fuzzie(ITattleStore tattleStore) : base(new HealthImpl(3),tattleStore)
        {
            this.Moves.Add(new HealAttack(Attacks.Attacks.FuzzieSuck, 1,1));
           
        }

        public override string Identifier { get; set; } = "Fuzzie";
    }
}
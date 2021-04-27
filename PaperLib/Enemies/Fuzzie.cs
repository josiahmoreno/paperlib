using Attacks;
using Attributes;
using Battle;
using Enemies;

namespace Enemies
{
    public class Fuzzie : NewGoomba
    {


        public Fuzzie(ITattleStore tattleStore) : base(new HealthImpl(3),tattleStore)
        {
            this.Moves.Add(new HealAttack(EnemyAttack.FuzzieSuck, 1,1));
           
        }
    }
}
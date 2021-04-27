using Attributes;
using Battle;
using Tests;

namespace Enemies
{
    public class Magikoopa : Goomba
    {


        public Magikoopa() : base(new HealthImpl(8))
        {
            this.Moves.Add(new RegularAttack(EnemyAttack.MagikoopTestAttack1, 3));
            this.Attrs = new IAttribute[] {new Flying() };
        }
    }
}
using Attributes;
using Battle;
using Tests;

namespace Enemies
{
    public class Magikoopa : Goomba
    {


        public Magikoopa() : base(new HealthImpl(8))
        {
            this.Moves.Add(new RegularAttackWrapper(Attacks.Attacks.MagikoopTestAttack1, 3));
            this.Attrs = new IAttribute[] {new Flying() };
        }
    }
}
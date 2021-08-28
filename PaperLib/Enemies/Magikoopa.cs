using System.Collections.Generic;
using Attributes;
using Battle;
using Tests;

namespace Enemies
{
    public class Magikoopa : NewBaseEnemy
    {


        public Magikoopa() : base(new HealthImpl(8),new TattleStore())
        {
            this.Attrs = new IAttribute[] {new Flying() };
        }

        public override List<IEnemyAttack> Moves => new List<IEnemyAttack>()
        {
          
            new RegularAttackWrapper(Attacks.Attacks.MagikoopTestAttack1, 3)
        };

        public override string Identifier { get; set; } = "Magikoopa";
    }
}
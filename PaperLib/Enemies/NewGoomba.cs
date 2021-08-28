using Attributes;
using Battle;
using Enemies;
using System.Collections.Generic;

namespace Enemies
{
    public class NewGoomba : NewBaseEnemy
    {
        public override string Identifier { get; set; } = "Goomba";

        //Hop (1), Power Hop (2; HP = 1)

        public override List<IEnemyAttack> Moves => new List<IEnemyAttack>()
        {
          
            new RegularAttackWrapper(Attacks.Attacks.GoombaBonk ,1)
        };

        public NewGoomba(ITattleStore store): base(store)
        {

        }
    }
}
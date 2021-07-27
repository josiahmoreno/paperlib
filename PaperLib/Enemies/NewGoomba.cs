using Attacks;
using Battle;
using Enemies;
using System.Collections.Generic;

namespace PaperLib.Enemies
{
    public class NewGoomba : NewBaseEnemy
    {
        public override string Identifier { get; set; } = "Goomba";

        //Hop (1), Power Hop (2; HP = 1)

        public override List<IEnemyAttack> Moves => new List<IEnemyAttack>()
        {
            new RegularAttack(EnemyAttack.GoombaBonk,1)
        };

        public NewGoomba(ITattleStore store): base(store)
        {

        }
    }
}
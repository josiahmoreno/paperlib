using Attributes;
using Battle;
using Enemies;
using System.Collections.Generic;
using System.Linq;
using Tests;

namespace Enemies
{
    public class GoombaKing : NewBaseEnemy
    {
        
       
        
        public GoombaKing(List<IEnemyAttack> moves) : base(new HealthImpl(10),new TattleStore())
        {
            //this.Moves = moves;
            moves.ForEach(move => {
                if(move is ScriptAttack scriptAttack)
                {
                    sequence.Add(move);
                } else
                {
                    Moves.Add(move);
                }
                
                });
        }

        public override string Identifier { get; set; } = "GoombaKing";

        private List<IEnemyAttack> sequence = new List<IEnemyAttack>();
        public override IEnemyAttack GetRandomMove()
        {
            if (sequence?.Count > 0)
            {
                var attack = sequence.First();
                sequence.RemoveAt(0);
                return attack;
            }
            else
            {
                var random = new System.Random();
                var index = random.Next(Moves.Count);
                return  Moves[index];
               
            }
        }

    }
}
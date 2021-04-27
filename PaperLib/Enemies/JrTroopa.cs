using Attributes;
using Battle;
using System.Collections.Generic;
using System.Linq;

namespace Enemies
{
    public class JrTroopa : Goomba
    {

        public override List<IEnemyAttack> Sequence { get; }= new List<IEnemyAttack>();


        public JrTroopa(List<IEnemyAttack> moves) : base(new HealthImpl(5))
        {
            //this.Moves = moves;
            moves.ForEach(move => {
                if (move is ScriptAttack scriptAttack)
                {
                    Sequence.Add(move);
                }
                else
                {
                    Moves.Add(move);
                }

            });
        }

        public override IEnemyAttack GetRandomMove()
        {
            if (Sequence?.Count > 0)
            {
                var attack = Sequence.First();
                Sequence.RemoveAt(0);
                return attack;
            }
            else
            {
                var random = new System.Random();
                var index = random.Next(Moves.Count);
                return Moves[index];

            }
        }
    }
}
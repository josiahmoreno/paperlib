using Battle;
using Enemies;

namespace PaperLib.Enemies
{
    public class NewGoomba : NewBaseEnemy
    {
        public override string Identifier { get; set; } = "Goomba";

        public NewGoomba(ITattleStore store): base(store)
        {

        }
    }
}
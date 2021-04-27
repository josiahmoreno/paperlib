using System;
using System.Collections.Generic;
using System.Text;
using Battle;
using Enemies;
using Tests;

namespace Moves
{
    class Tattle : MoveOption
    {
        private TextBubbleSystem textBubbleSystem;
        public Tattle( ITextBubbleSystem system) : base(MovesList.Tattle, system, MenuData.TargetType.Single)
        {
        }

        public Tattle() : base(MovesList.Tattle, MenuData.TargetType.Single)
        {
        }

        public override void Execute(Battle.Battle battle,object activeHero, Enemy[] targets, Action<IEnumerable<Tuple<Enemy, bool>>> action)
        {
            Console.WriteLine($"{activeHero.GetType().Name} uses {GetType().Name} on {targets[0]}");
            battle.OnTextCompleted(action);
            if (targets[0].FetchTattleData() != null)
            {
                battle.ShowText(targets[0].FetchTattleData());
            }
            else
            {
                battle.ShowText(battle.TattleStore.FetchGameText(targets[0]));
            }
            


        }

        //public override void Execute(object activeHero, Enemy[] targets, Action action)
        //{
        //    Console.WriteLine($"{activeHero.GetType().Name} uses {GetType().Name} on {targets[0]}");
        //    TextSystem.OnTextCompleted(action);
        //    TextSystem.ShowText(new GameText("SampleText1", "2","3","4"));
            

        //}
    }
}

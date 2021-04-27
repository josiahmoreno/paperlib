using Battle;
using Enemies;
using Heroes;
using Items;
using MenuData;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Attacks;

namespace Tests
{
    public class boss_battle_magikoopa
    {
        private Mario Mario;
        private Goombario Goombario;

        private Magikoopa Magikoopa;

        private Battle.Battle battle;
        //https://youtu.be/en4NPWApoYY?list=PLgU0IdjAiGw4ibWVo_RPVsdxv0GvFGCtS
       
        [SetUp]
        public void Setup()
        {

            var bubbleSystem = new TextBubbleSystem();
            Mario = new Mario(
                new Inventory(),
                new List<IJumps> { new Attacks.Jump(), new PowerJump() }.ToArray(),
                new Attacks.Hammer(), new Attacks.HammerThrow());
            Goombario = new Goombario();
            //var scriptAttack = new ScriptAttack(EnemyAttack.JrTroopaPowerJump);
            //JrTroopa = new JrTroopa(new List<IEnemyAttack> { new RegularAttack(EnemyAttack.JrTroopaJump, 1) });
            Magikoopa = new Magikoopa();
            var enemies = new List<Enemy>()
            {
                Magikoopa
            };
            battle = new Battle.Battle(new List<Hero> { Mario, Goombario }, enemies, bubbleSystem);
         
           
            //battle.Start();

        }



        [Test]
        public void magikoopa_1()
        {
            Assert.IsTrue(!battle.IsStarted(), $"battle state = {battle.State.ToString()}");
            Assert.IsTrue(!battle.ActionMenu.Showing);
            Assert.IsTrue(!battle.OptionsListMenu.Showing);
            Assert.IsTrue(battle.OptionsListMenu.Active == null);
            Assert.IsTrue(battle.TurnSystem.Active == Mario);
            //0:10
            battle.Start();
            Assert.IsTrue(!battle.TextBubbleSystem.Showing);
            //menu is shown with jump selected
            Assert.IsTrue(battle.ActionMenu.ActiveAction.Name == "Jump");
            Assert.IsTrue(battle.TargetSystem.Actives == null);
            battle.Execute();
            Assert.IsTrue(battle.OptionsListMenu.Showing);
            //0:11 jump option is chosen, and ablities are shown. Jump and power jump are shown
            Assert.IsTrue(battle.ActiveOptionName == "Jump");

            Assert.IsTrue(battle.TargetSystem.Actives == null);
            battle.Execute();
            //0:12 targeting system is shown with magikoop selected
            Assert.IsTrue(battle.TargetSystem.Actives[0] == Magikoopa);
            //0:14 cancel is pressed
            battle.Cancel();
            //battle.Cancel();
            Assert.IsTrue(battle.OptionsListMenu.Showing);
            Assert.IsTrue(battle.ActiveOptionName == "Jump");
            Assert.IsTrue(battle.TargetSystem.Actives == null);
            //0:14 ablities are reshown. Jump and power jump are shown
            //0:14 cancel is pressed
            battle.Cancel();
            //0:14 menu are reshown. Jump is selected
            Assert.IsTrue(!battle.OptionsListMenu.Showing);
            Assert.IsTrue(battle.ActionMenu.Showing);
            Assert.IsTrue(battle.ActionMenu.ActiveAction.Name == "Jump");
            //0:16 partner swap
            battle.TurnSystem.Swap();
            //0:16 goombario is active
            Assert.IsTrue(battle.TurnSystem.Active == Goombario);
            //0:16 ablities is highlighted
            Assert.IsTrue(battle.ActionMenu.Showing);
            Assert.IsTrue(battle.ActionMenu.ActiveAction.Name == "Abilities");
            //0:17 press a 
            Assert.IsTrue(!battle.OptionsListMenu.Showing);
            battle.Execute();
            Assert.IsTrue(battle.OptionsListMenu.Showing);
            //0:17 ablities are shown. headbonk and tattle are shown
            Assert.IsTrue(battle.ActiveOptionName == "Headbonk");
            battle.MoveTargetDown();
            Assert.IsTrue(battle.ActiveOptionName == "Tattle");
            battle.Execute();
            Assert.IsTrue(!battle.OptionsListMenu.Showing);
            Assert.IsTrue(!battle.ActionMenu.Showing);
            Assert.IsTrue(battle.TargetSystem.Showing);
            Assert.IsTrue(battle.TargetSystem.Actives[0] == Magikoopa, $"{battle.TargetSystem.Actives[0]}");
            //0:20 text bubble showing
            battle.Execute();
            Assert.IsTrue(!battle.OptionsListMenu.Showing);
            Assert.IsTrue(!battle.ActionMenu.Showing);
            Assert.IsTrue(!battle.TargetSystem.Showing);
            Assert.IsTrue(battle.TextBubbleSystem.Showing);
            //0:23 press a
            battle.Execute();
            //0:25 press a
            battle.Execute();
            //0:29 press a
            battle.Execute();
            //0:31 now mario is active
            Assert.IsTrue(!battle.TextBubbleSystem.Showing);
            //0:31 jump action is selected
            Assert.IsTrue(battle.TurnSystem.Active == Mario);
            //0:32 press a
            Assert.IsTrue(battle.ActionMenu.ActiveAction.Name == "Jump");
            Assert.IsTrue(battle.ActionMenu.Showing);
            battle.Execute();
            Assert.IsTrue(!battle.ActionMenu.Showing);
            Assert.IsTrue(battle.ActiveOptionName == "Jump");
            //0:32 jump option is chosen, and ablities are shown. Jump and power jump are shown
            //down press
            battle.MoveTargetDown();
            //0:32 power jump is highlighted
            Assert.IsTrue(battle.ActiveOptionName == "Power Jump");
            //0:32 press a
            battle.Execute();
            //0:32 targeting system is shown with magikoop selected
            Assert.IsTrue(battle.TargetSystem.Actives[0] == Magikoopa, $"{battle.TargetSystem.Actives[0]}");
            //0:33 press a
            battle.ActionCommandCenter.AddSuccessfulPress();
            battle.ActionCommandCenter.AddSuccessfulPress();
            Assert.IsTrue(Magikoopa.IsFlying);
            battle.Execute();
            //0:34 a is prssedat correct time for bonus! Nice
            Assert.IsTrue(Magikoopa.Health.CurrentValue == 4, $"Magikoop hp = {Magikoopa.Health.CurrentValue}");
            //0:35 damage is dealt
            Assert.IsTrue(!Magikoopa.IsFlying);
            //0:36 after damage event added, magikoopa falls off broomstick, she is now on the ground
            //0:38 magikoopa's turn. she shoots mario with a shape blast
            Assert.IsTrue(Mario.Health.CurrentValue == 8);
            //0:40 press a, mario blocks
            //0:40 mario takes 2 damage
            //0:42 menu is shown with jump selected
            //0:42 move down
            battle.MoveTargetDown();
            Assert.IsTrue(battle.ActionMenu.Showing);
            Assert.IsTrue(battle.ActionMenu.ActiveAction.Name == "Hammer");
            //0:42 hammer action is selected
            //0:43 press a to go to hammer options
            battle.Execute();

            Assert.IsTrue(battle.OptionsListMenu.Showing);
            Assert.IsTrue(battle.OptionsListMenu.Items.Count() == 2);
            Assert.IsTrue(battle.GetActiveOptionName() == "Hammer", $"ActiveOptionName == {battle.GetActiveOptionName()}");
            //0:44 move down to hammer throw
            battle.MoveTargetDown();
            Assert.IsTrue(battle.GetActiveOptionName() == "Hammer Throw", $"ActiveOptionName == {battle.GetActiveOptionName()}");
            //0:46 press a
            battle.Execute();
            //0:46 target system showing on a magikoopa
            Assert.IsTrue(!battle.ActionMenu.Showing);
            Assert.IsTrue(!battle.OptionsListMenu.Showing);
            Assert.IsTrue(battle.TargetSystem.Showing);
            Assert.IsTrue(battle.TargetSystem.Actives[0] == Magikoopa);
            Assert.IsTrue(Magikoopa.Health.CurrentValue == 4, $"Magi HP = {Magikoopa.Health.CurrentValue}");
            //magikoopa is hit, she is now at 2
            battle.ActionCommandCenter.AddSuccessfulPress();
            battle.Execute();
            Assert.IsTrue(Magikoopa.Health.CurrentValue == 2, $"Magi HP = {Magikoopa.Health.CurrentValue}");
            //0:53 now its goombario's turn
            Assert.IsTrue(battle.TurnSystem.Active == Goombario);
            Assert.IsTrue(battle.ActionMenu.Showing);
            Assert.IsTrue(battle.ActionMenu.ActiveAction.Name == "Abilities");
            battle.Execute();
            //0:54 press a, abblities options with headbonk selected
            Assert.IsTrue(!battle.ActionMenu.Showing);
            Assert.IsTrue(battle.OptionsListMenu.Showing);
            Assert.IsTrue(!battle.TargetSystem.Showing);
            Assert.IsTrue(battle.ActiveOptionName == "Headbonk");
            //0:55 press a, headbonl selected, now targeting shows
            battle.Execute();
            Assert.IsTrue(battle.TargetSystem.Showing);
            Assert.IsTrue(battle.TargetSystem.Actives[0] == Magikoopa);
            //0:55 press a, goombario is about to hit magijoopa
            battle.ActionCommandCenter.AddSuccessfulPress();
            battle.Execute();
            //0:56 press a, a is pressed at the right time of action command so goombario gets second bounce
            //0:59 magikoopa is dead
            Assert.IsTrue(Magikoopa.Health.CurrentValue == 0, $"Magi HP = {Magikoopa.Health.CurrentValue}");
            Assert.IsTrue(battle.IsEnded());
        }








    }
}

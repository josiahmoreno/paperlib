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

namespace Tests
{
    public class boss_battle_jr_troopa
    {
        private Mario Mario;
        private Goompa Goompa;

        private JrTroopa JrTroopa;

        private Battle.Battle battle;
        //https://www.youtube.com/watch?v=eta_XFZBsXE&list=PLgU0IdjAiGw4ibWVo_RPVsdxv0GvFGCtS&index=1
        //battle sequence starts
        //
        //ui is shown for a second as camera pans out
        //mario turns left to listen to goompa
        //text shows
        //1st text, press a
        //2nd, press a
        //3rd, press a
        //0:20 4th, press a
        //mario turns back around 
        //0:22 actions are shown (flag,items gray, jump, hammer) default is jump
        //move down to hammer 
        //press a
        //show targeting
        //press a to confirm jr troopa
        //0:25 mario hammers jr troopa for one   -1
        //zooms into goompa for him to talk
        //one chat bubble
        //1st text, press a
        //0:29
        //then jr troopa attacks
        //0:32
        //mario takes one, mario hp = 9
        //0:34
        //shows actions of mario,jump is selected
        //press a, confirm jump
        //showing targeting on jr.troopa
        //press a, confirming target
        //0:36
        //mario jumps on jr troopa for one -2
        //0:39
        //jrs troops turn, he jumps on mario, mario hp = 8 
        //shows actions of mario,jump is selected
        //press a, confirm jump
        //showing targeting on jr.troopa
        //press a, confirming target
        //0:43
        //mario jumps on jr troopa for one -3
        //camera resets
        //0:46
        //jr troop starts talking,//text shows
        //0:48
        //1st text press a to continue text
        //0:49 
        //jr troopa jumps on mario, mario hp = 7
        //shows actions of mario, jump is selected
        //move down actions, hammer is selected
        //0:52 press a, confirm hammer
        //showing targeting on jr.troopa
        //press a, confirming target
        //0:54 mario hammers jr troopa for one -4
        //0:56 goompa tells mario he almost has it!
        //one chat bubble
        //0:59 press a, chat continues
        //then jr troopa talks, zooms in
        //1st chat bubble
        //2nd chat bubble
        //1:04 3rd chat bubble, press a
        //he uses a power jump! hits mario for 2
        //1:07
        //mario is at 5/10

        //move up actions, jump is selected
        //press a, confirm jump
        //showing targeting on jr.troopa
        //press a, confirming target
        //1:12
        //mario jumpd on jr troopa for one -5
        //goompa talks, shows text
        //1:17
        //1st text, press a
        //2nd text, press a
        //3rd text, press a
        //4rd text, press a
        //battle done
        [SetUp]
        public void Setup()
        {

            var bubbleSystem = new TextBubbleSystem();
            Mario = new Mario(
                new Inventory(),
                new List<IJumps> { new Attacks.Jump() }.ToArray(),
                new Attacks.Hammer());
            Goompa = new Goompa();
            var scriptAttack = new ScriptAttack(EnemyAttack.JrTroopaPowerJump);
            JrTroopa = new JrTroopa(new List<IEnemyAttack> { new RegularAttack(EnemyAttack.JrTroopaJump, 1) });

            var enemies = new List<Enemy>()
            {
                JrTroopa,
            };
            battle = new Battle.Battle(new List<Hero> { Mario, Goompa }, enemies, bubbleSystem);
            battle.AddEventOnStarting(new TextBubbleEvent((battleEvent, battle) =>
            {

                battle.ShowText(new GameText("1", "2", "3", "4"));
                //battleEvent.Completed = true;
                 battle.OnTextCompleted((_) => battleEvent.Complete());

            }, (battle) => battle.State == BattleState.STARTING));
            battle.AddEventOnStart(new BattleEvent((battleEvent, battle) =>
            {

                battle.ShowText(new GameText("Nice Job"));
                battleEvent.Completed = true;
                battle.OnTextCompleted((_) => battle.EndTurn());




            }, (battle) => battle.Enemies.First(enemy => enemy is JrTroopa).Health.CurrentValue == 4));

            battle.AddEventOnStart(new BattleEvent((battleEvent, battle) =>
            {

                battle.ShowText(new GameText("Mario is lame!"));
                battleEvent.Completed = true;
                battle.OnTextCompleted((_) => battle.EndTurn());
                // what i return a turn end enum, then battle events haave to end turns!

            }, (battle) => battle.Enemies.First(enemy => enemy is JrTroopa).Health.CurrentValue == 3));

            battle.AddEventOnStart(new BattleEvent((battleEvent, battle) =>
            {

                battle.ShowText(new GameText("Goompa: You are almost there mario!"));
                battleEvent.Completed = true;
                battle.OnTextCompleted((_) =>
                {
                    battle.EndTurn();
                });
                // what i return a turn end enum, then battle events haave to end turns!

            }, (battle) => battle.Enemies.First(enemy => enemy is JrTroopa).Health.CurrentValue == 2));

            battle.AddEventOnStart(new BattleEvent((battleEvent, battle) =>
            {

                battle.TextBubbleSystem.ShowText(new GameText("Goompa: You are almost there mario!"));
                battleEvent.Completed = true;

                battle.OnTextCompleted((_) =>
                {
                    battle.ShowText(new GameText("JrTroopa: All right, you asked for it", "Full power!!"));
                    battle.Enemies.First(o => o == JrTroopa).Sequence.Add(scriptAttack);
                    battle.OnTextCompleted(__ =>
                    {
                        battle.EndTurn();
                    });
                });
                // what i return a turn end enum, then battle events haave to end turns!

            }, (battle) => battle.Enemies.First(enemy => enemy is JrTroopa).Health.CurrentValue == 1));
            battle.AddEventOnStart(new BattleEvent((battleEvent, battle) =>
            {

                battle.ShowText(new GameText("Goompa: You got Star points", "You get em when u win", "Every 100 you level up","Git Hard"));
                battle.Enemies.First(o => o == JrTroopa).Sequence.Add(scriptAttack);
                battle.Enemies.First(o => o == JrTroopa).Sequence.Add(scriptAttack);
                battle.OnTextCompleted((_) =>
                {
                    battle.EndTurn();
                });
                // what i return a turn end enum, then battle events haave to end turns!

            }, (battle) => battle.Enemies.First(enemy => enemy is JrTroopa).Health.CurrentValue == 0));
            //battle.Start();

        }



        [Test]
        public void jr_troopa_1()
        {
            Assert.IsTrue(!battle.IsStarted(), $"battle state = {battle.State.ToString()}");
            Assert.IsTrue(!battle.ActionMenu.Showing);
            Assert.IsTrue(!battle.OptionsListMenu.Showing);
            Assert.IsTrue(battle.OptionsListMenu.Active == null);
            Assert.IsTrue(battle.TurnSystem.Active == Mario);
            //0:28
            battle.Start();
            Assert.IsTrue(battle.TextBubbleSystem.Showing);
            Assert.IsTrue(!battle.ActionMenu.Showing);
            battle.TextBubbleSystem.Continue();
            battle.TextBubbleSystem.Continue();
            battle.TextBubbleSystem.Continue();
            Assert.IsTrue(@battle.TextBubbleSystem.Showing);
            //0:20 4th, press a
            battle.TextBubbleSystem.Continue();
            Assert.IsTrue(!battle.TextBubbleSystem.Showing);
            Assert.IsTrue(battle.ActionMenu.Showing);
            battle.ActionMenu.MoveTargetDown();
            Assert.IsTrue(battle.ActionMenu.ActiveAction.Name == "Hammer");
            battle.ExecuteFromActionMenu();
            Assert.IsTrue(battle.TargetSystem.Showing);
            Assert.IsTrue(battle.TargetSystem.Actives[0] == JrTroopa);
            battle.ConfirmTarget();
            //0:25 mario hammers jr troopa for one   -1
            Assert.IsTrue(JrTroopa.Health.CurrentValue == 4);
            Assert.IsTrue(battle.TextBubbleSystem.Showing);
            //zooms into goompa for him to talk
            //one chat bubble
            battle.TextBubbleSystem.Continue();
            Assert.IsTrue(!battle.TextBubbleSystem.Showing);
            //0:29
            //then jr troopa attacks
            Assert.IsTrue(battle.TurnSystem.LastActive == "JrTroopa", $"{battle.TurnSystem.Active}");

            //0:32
            //mario takes one, mario hp = 9
            Assert.IsTrue(Mario.Health.CurrentValue == 9);
            Assert.IsTrue(battle.ActionMenu.Showing);
            battle.ActionMenu.MoveTargetUp();
            Assert.IsTrue(battle.ActionMenu.ActiveAction.Name == "Jump");
            battle.ExecuteFromActionMenu();
            Assert.IsTrue(battle.TargetSystem.Showing);
            Assert.IsTrue(battle.TargetSystem.Actives[0] == JrTroopa);
            //0:36
            //mario jumps on jr troopa for one -2
            battle.ConfirmTarget();
            Assert.IsTrue(JrTroopa.Health.CurrentValue == 3);
            Assert.IsTrue(battle.TextBubbleSystem.Showing, $"");
            Assert.IsTrue(Mario.Health.CurrentValue == 9);
            Assert.IsTrue(battle.TurnSystem.LastActive == "JrTroopa", $"{battle.TurnSystem.Active}");
            Assert.IsTrue(battle.TextBubbleSystem.Showing, $"");
            //camera resets
            //jr troop starts talking,//text shows
            //1st text
            battle.TextBubbleSystem.Continue();
            Assert.IsTrue(!battle.TextBubbleSystem.Showing, $"");
            //0:39
            //jrs troops turn, he jumps on mario, mario hp = 8 
            Assert.IsTrue(Mario.Health.CurrentValue == 8, $"mario health {Mario.Health.CurrentValue}");

            //shows actions of mario,jump is selected
            Assert.IsTrue(battle.ActionMenu.Showing);
            Assert.IsTrue(battle.ActionMenu.ActiveAction.Name == "Jump");
            //press a, confirm jump
            battle.ExecuteFromActionMenu();
            //showing targeting on jr.troopa
            Assert.IsTrue(battle.TargetSystem.Actives[0] == JrTroopa);
            //press a, confirming target
            battle.ConfirmTarget();
            //0:43
            //mario jumps on jr troopa for one -3
            Assert.IsTrue(JrTroopa.Health.CurrentValue == 2);
            //camera resets
            //0:46
            //jr troop starts talking,//text shows
            Assert.IsTrue(battle.TextBubbleSystem.Showing);
            //0:48
            //1st text press a to continue text
            battle.TextBubbleSystem.Continue();
            //0:49 
            //jr troopa jumps on mario, mario hp = 7
            Assert.IsTrue(Mario.Health.CurrentValue == 7, $"mario health {Mario.Health.CurrentValue}");
            //shows actions of mario, jump is selected
            Assert.IsTrue(battle.ActionMenu.Showing);
            Assert.IsTrue(battle.ActionMenu.ActiveAction.Name == "Jump");
            //move down actions, hammer is selected
            battle.ActionMenu.MoveTargetDown();
            //0:52 press a, confirm hammer
            battle.ExecuteFromActionMenu();
            //showing targeting on jr.troopa
            //press a, confirming target
            battle.ConfirmTarget();
            //0:54 mario hammers jr troopa for one -4
            Assert.IsTrue(JrTroopa.Health.CurrentValue == 1);
            //0:56 goompa tells mario he almost has it!
            //one chat bubble
            Assert.IsTrue(battle.TextBubbleSystem.Showing);
            //0:59 press a, chat continues
            battle.TextBubbleSystem.Continue();
            //then jr troopa talks, zooms in
            //1st chat bubble
            Assert.IsTrue(battle.TextBubbleSystem.Showing);
            battle.TextBubbleSystem.Continue();
            //1:04 2nd chat bubble
            battle.TextBubbleSystem.Continue();
            // 3rd chat bubble, press a
            //he uses a power jump! hits mario for 2
            //1:07
            //mario is at 5/10
            Assert.IsTrue(Mario.Health.CurrentValue == 5, $"mario health {Mario.Health.CurrentValue}");
            //move up actions, jump is selected
            battle.ActionMenu.MoveTargetUp();
            Assert.IsTrue(battle.ActionMenu.ActiveAction.Name == "Jump");
            //press a, confirm jump
            battle.ExecuteFromActionMenu();
            //showing targeting on jr.troopa
            //press a, confirming target
            battle.ConfirmTarget();
            //1:12
            //mario jumpd on jr troopa for one -5
            //goompa talks, shows text
            Assert.IsTrue(battle.TextBubbleSystem.Showing);
            Assert.IsTrue(!battle.IsEnded());
            //1:17
            //1st text, press a
            battle.TextBubbleSystem.Continue();
            //2nd text, press a
            battle.TextBubbleSystem.Continue();
            //3rd text, press a
            battle.TextBubbleSystem.Continue();
            //4rd text, press a
            battle.TextBubbleSystem.Continue();
            //battle done
            Assert.IsTrue(battle.IsEnded());
        }








    }
}

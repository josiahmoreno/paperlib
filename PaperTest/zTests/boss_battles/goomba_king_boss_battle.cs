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
using PaperLib.Enemies;

namespace Tests
{
    public class goomba_king_boss_battle
    {
        private Mario mario;
        private Goombario goombario;

        private GoombaKing GoombaKing;

        private Battle.Battle battle;
        //https://www.youtube.com/watch?v=WxzJ956y7Bg&list=PLgU0IdjAiGw4ibWVo_RPVsdxv0GvFGCtS&index=3
        //battle sequence starts
        //red goomba talks 0:12 
        //press A 0:14
        //then blue goomba talks
        //press A 0:16.5
        //then goomba king talks
        //press A 0:19
        //then goombario talks 0:19.5
        //press A 0:23
        //press A 0:26
        //camera reset
        //show actions 0:28
        //default action is jump
        //move up 
        //action is now items
        //prezz z 0:31.5
        //active hero is now goombario
        //press a (default action is abilities)
        //show menu
        //move down 0:33:
        //press a (tattle is pressed)
        //show targeting
        //active target is goomba king
        //show text
        //press a(text is continued) 0:40
        //press a(text is continued) 0:41
        //press a(text is continued) 0:44
        //press a(last) 0:44
        //action is complete so it switches to next available hero
        //mario swaps to active
        //action is now items
        //press A
        //show menu (items belonging to mario)
        //move down
        //fire flower is highlighted
        //press A
        //show targeting
        //all enemies selected
        //press a, play fire flower animation
        //in order goomba king is git for 3, then red goomba, then blue goomba (7)
        //red goomba burn death animation plays
        //blue goomba burn death animation plays
        //red goomba dies
        //blue goomba dies
        //goomba kings turn
        //he jumps
        //then the enviroment hits mario with three goomnuts 1:04 
        //mario gets hit with 2 damage (mario 8)
        //marios turn
        ////////shows actions of mario
        //move down
        //move down
        //active action is hammer 1:08
        //show targeting
        //active target is goomba king
        //move left
        //active target is goomnuttree
        //press a 1:12
        //mario hammers goomnut tree
        //goomba king takes 3 damage 1:14 (goombaking = 4)
        //now marios turn is complete
        //switches to goombario
        //show actions of goombario
        //active action is 'abilities\
        //press a
        //show options of abilites: headbonk and tattle
        //press a
        //show targeting
        //active target is goomba king
        //press a
        //target confirmed as goomba king
        //goombario jumps on goomba king
        //goomba king takes 1 damage 1:20
        //health is now three
        //no more turns for heroes
        //goomba kings turn
        //goomba kicks mario
        //mario takes 1 damage 1:23
        //mario's turn
        //show actions of mario
        //hammer is active action
        //move up 1:26
        //jump is now highlighted action
        //show menu of jump (jump and super jump showing)
        //move down 1:27.5 
        //power jump is now highlighted
        //show targeting
        //goomba king is highlighted
        //mario is super jumping
        //goomba king takes 3 damage 1:29
        //goomba king dies
        //goomba king xp releases 1:32
        //show xp
        //winning animation 
        //level up choice
        //move left
        //hp showed
        //move right
        //fp showed
        //move right
        //bp showed 1:40
        //move left
        //fp showed
        //move left
        //hp showed
        //press a
        //hp chosen
        //complete 1:42



        [SetUp]
        public void Setup()
        {

            var bubbleSystem = new TextBubbleSystem();
            mario = new Mario(
                new Inventory(new Item("Mushroom"), new Item("Fire Flower", 3, TargetType.All), new Item("Mushroom")),
                new List<IJumps> { new Attacks.Jump(), new PowerJump() }.ToArray(),
                new Attacks.Hammer());
            goombario = new Goombario(bubbleSystem);
            GoombaKing = new GoombaKing(new List<IEnemyAttack> { new ScriptAttack(EnemyAttack.GoomnutJump), new GoombaKingKick() });
            var goomNutTree = new GoomnutTree();
            var enemyFactory = new EnemyFactory();
            var enemies = new List<Enemy>()
            {
                goomNutTree,
                GoombaKing,
                new RedGoomba(2),
                enemyFactory.FetchEnemy<NewBlueGoomba>(2),
            };
            battle = new Battle.Battle(new List<Hero> { mario, goombario }, enemies, bubbleSystem);
            battle.Start();
        }

        [Test]
        public void turn_check()
        {
            Assert.IsTrue(battle.TurnSystem.Active == mario);
            Assert.IsTrue(battle.ActionMenu.Showing);

        }

        [Test]
        public void goomba_king_tattle_fire_flower()
        {

            Assert.IsTrue(battle.TurnSystem.Active == mario);
            //0:28
            Assert.IsTrue(battle.ActionMenu.Showing);

            Assert.IsTrue(battle.ActionMenu.ActiveAction != null);
            Assert.IsTrue(battle.ActionMenu.ActiveAction == mario.Actions[2] && battle.ActionMenu.ActiveAction.Name == "Jump");
            battle.ActionMenu.MoveTargetUp();
            Assert.IsTrue(battle.ActionMenu.ActiveAction == mario.Actions[1] && battle.ActionMenu.ActiveAction.Name == "Items");
            battle.TurnSystem.Swap();
            Assert.IsTrue(battle.TurnSystem.Active == goombario);
            Assert.IsTrue(battle.ActionMenu.ActiveAction == goombario.Actions[1] && battle.ActionMenu.ActiveAction.Name == "Abilities");
            battle.ShowOptionsMenu();
            Assert.IsTrue(battle.OptionsListMenu.Showing);
            Assert.IsFalse(battle.ActionMenu.Showing);
            Assert.IsTrue(battle.OptionsListMenu.Items.Length == 2);
            battle.OptionsListMenu.MoveTargetDown();
            //press a (tattle is pressed)
            Assert.IsTrue(battle.GetActiveOptionName() == "Tattle", "was not tattle");
            battle.ShowTargeting(battle.OptionsListMenu.Active);
            Assert.IsFalse(battle.OptionsListMenu.Showing, $"OptionsListMenu showing = {battle.OptionsListMenu.Showing}");
            Assert.IsTrue(battle.TargetSystem.Actives[0] == GoombaKing, $"target is not goombaking, target = {battle.TargetSystem.Actives[0]}");
            battle.ConfirmTarget();
            Assert.IsTrue(battle.TextBubbleSystem.Showing);
            //press a(text is continued) 0:40
            battle.TextBubbleSystem.Continue();
            Assert.IsTrue(battle.TextBubbleSystem.Showing);
            battle.TextBubbleSystem.Continue();
            Assert.IsTrue(battle.TextBubbleSystem.Showing);
            battle.TextBubbleSystem.Continue();
            Assert.IsTrue(battle.TextBubbleSystem.Showing);
            battle.TextBubbleSystem.Continue();
            Assert.IsFalse(battle.TextBubbleSystem.Showing);
            //this is failing somethinh anout goombario?
            Assert.IsTrue(battle.TurnSystem.Active == mario, $"{battle.TurnSystem.Active}");
            Assert.IsTrue(battle.ActionMenu.ActiveAction == mario.Actions[1] && battle.ActionMenu.ActiveAction.Name == "Items", $"{battle.ActionMenu.ActiveAction.Name}");
            battle.ShowOptionsMenu();
            Assert.IsTrue(battle.OptionsListMenu.Items.Length == 3);
            battle.OptionsListMenu.MoveTargetDown();

            Assert.IsTrue(battle.GetActiveOptionName() == "Fire Flower", $"was not Fire Flower, {battle.GetActiveOptionName()}");
            //excuting option should show targeting!
            battle.ShowTargeting(battle.OptionsListMenu.Active);
            battle.ConfirmTarget();
            int goombaKingHealth = battle.Enemies.First(enemy => enemy == GoombaKing).Health.CurrentValue;
            Assert.IsTrue(condition: goombaKingHealth == 7, message: $"was not GoombaKing, {goombaKingHealth}");
            Enemy redGoomba = battle.Enemies.First(enemy => enemy is RedGoomba);
            Assert.IsTrue(condition: redGoomba.IsDead, message: $"redGoomba is not dead, {redGoomba}");
            Enemy blueGoomba = battle.Enemies.First(enemy => enemy is NewBlueGoomba);
            Assert.IsTrue(condition: blueGoomba.IsDead, message: $"blueGoomba is not dead, {blueGoomba}");
            Assert.IsTrue(battle.TurnSystem.LastActive == GoombaKing.GetType().Name, $"{battle.TurnSystem.Active}");
            //then the enviroment hits mario with three goomnuts 1:04
            Assert.IsTrue(mario.Health.CurrentValue == 8, $"{mario.Health.CurrentValue}");
            Assert.IsTrue(battle.TurnSystem.Active == mario, $"{battle.TurnSystem.Active}");
            Assert.IsTrue(battle.ActionMenu.Showing);
            battle.ActionMenu.MoveTargetDown();
            battle.ActionMenu.MoveTargetDown();
            Assert.IsTrue(battle.ActionMenu.ActiveAction.Name == "Hammer", $"action = {battle.ActionMenu.ActiveAction.Name}");
            Assert.IsTrue(battle.ActionMenu.Showing);
            battle.ExecuteFromActionMenu();
            Assert.IsTrue(!battle.ActionMenu.Showing && !battle.OptionsListMenu.Showing, $"{battle.OptionsListMenu.Showing}");
            Assert.IsTrue(battle.TargetSystem.Actives[0] == GoombaKing, $"{battle.OptionsListMenu.Showing}");
            battle.TargetSystem.MoveTargetLeft();
            Assert.IsTrue(battle.TargetSystem.Actives[0] is GoomnutTree, $"{battle.OptionsListMenu.Showing}");
            battle.ConfirmTarget();
            //goomba king takes 3 damage 1:14 (goombaking = 4)
            Assert.IsTrue(condition: GetGoombaKingHealth() == 4, message: $"was not GoombaKing, {goombaKingHealth}");
            Assert.IsTrue(battle.TurnSystem.Active == goombario);
            Assert.IsTrue(battle.ActionMenu.ActiveAction == goombario.Actions[1] && battle.ActionMenu.ActiveAction.Name == "Abilities");
            battle.ExecuteFromActionMenu();
            Assert.IsTrue(battle.GetActiveOptionName() == "Headbonk", $"was not Headbonk {battle.GetActiveOptionName()}");
            battle.ExecuteFromOptionMenu();
            Assert.IsTrue(battle.TargetSystem.Actives[0] == GoombaKing, $"{battle.OptionsListMenu.Showing}");
            battle.ConfirmTarget();
            Assert.IsTrue(condition: GetGoombaKingHealth() == 3, message: $"was not 3, {goombaKingHealth}");
            Assert.IsTrue(battle.ActionMenu.ActiveAction.Name == "Hammer", $"action = {battle.ActionMenu.ActiveAction.Name}");
            battle.ActionMenu.MoveTargetUp();
            Assert.IsTrue(battle.ActionMenu.ActiveAction == mario.Actions[2] && battle.ActionMenu.ActiveAction.Name == "Jump");
            battle.ExecuteFromActionMenu();
            battle.OptionsListMenu.MoveTargetDown();
            Assert.IsTrue(battle.GetActiveOptionName() == "Power Jump", $"was not Power Jump, {battle.GetActiveOptionName()}");
            battle.ExecuteFromOptionMenu();
            Assert.IsTrue(battle.TargetSystem.Showing);
            Assert.IsTrue(battle.TargetSystem.Actives[0] == GoombaKing, $"{battle.OptionsListMenu.Showing}");
            battle.ConfirmTarget();
            Assert.IsTrue(condition: GetGoombaKingHealth() == 0, message: $"was not GoombaKing, {goombaKingHealth}");
            Assert.IsTrue(condition: battle.IsEnded(), message: $"was not GoombaKing, {goombaKingHealth}");
        }
        
        [Test]
        public void goomba_king_goomnut()
        {

            Assert.IsTrue(battle.TurnSystem.Active == mario);
            //0:28
            Assert.IsTrue(battle.ActionMenu.Showing);

            Assert.IsTrue(battle.ActionMenu.ActiveAction != null);
            Assert.IsTrue(battle.ActionMenu.ActiveAction == mario.Actions[2] && battle.ActionMenu.ActiveAction.Name == "Jump");
            battle.ActionMenu.MoveTargetUp();
            Assert.IsTrue(battle.ActionMenu.ActiveAction == mario.Actions[1] && battle.ActionMenu.ActiveAction.Name == "Items");
            battle.TurnSystem.Swap();
            Assert.IsTrue(battle.TurnSystem.Active == goombario);
            Assert.IsTrue(battle.ActionMenu.ActiveAction == goombario.Actions[1] && battle.ActionMenu.ActiveAction.Name == "Abilities");
            battle.ShowOptionsMenu();
            Assert.IsTrue(battle.OptionsListMenu.Showing);
            Assert.IsFalse(battle.ActionMenu.Showing);
            Assert.IsTrue(battle.OptionsListMenu.Items.Length == 2);
            battle.OptionsListMenu.MoveTargetDown();
            //press a (tattle is pressed)
            Assert.IsTrue(battle.GetActiveOptionName() == "Tattle", "was not tattle");
            battle.ShowTargeting(battle.OptionsListMenu.Active);
            Assert.IsFalse(battle.OptionsListMenu.Showing, $"OptionsListMenu showing = {battle.OptionsListMenu.Showing}");
            Assert.IsTrue(battle.TargetSystem.Actives[0] == GoombaKing, $"target is not goombaking, target = {battle.TargetSystem.Actives[0]}");
            battle.ConfirmTarget();
            Assert.IsTrue(battle.TextBubbleSystem.Showing);
            //press a(text is continued) 0:40
            battle.TextBubbleSystem.Continue();
            Assert.IsTrue(battle.TextBubbleSystem.Showing);
            battle.TextBubbleSystem.Continue();
            Assert.IsTrue(battle.TextBubbleSystem.Showing);
            battle.TextBubbleSystem.Continue();
            Assert.IsTrue(battle.TextBubbleSystem.Showing);
            battle.TextBubbleSystem.Continue();
            Assert.IsFalse(battle.TextBubbleSystem.Showing);
            //this is failing somethinh anout goombario?
            Assert.IsTrue(battle.TurnSystem.Active == mario, $"{battle.TurnSystem.Active}");
            Assert.IsTrue(battle.ActionMenu.ActiveAction == mario.Actions[1] && battle.ActionMenu.ActiveAction.Name == "Items", $"{battle.ActionMenu.ActiveAction.Name}");
            battle.MoveTargetDown();
            
            battle.Execute();
            Assert.IsTrue(battle.OptionsListMenu.Items.Length == 2);
          
            Assert.IsTrue(battle.GetActiveOptionName() == "Jump", $"was not Fire Flower, {battle.GetActiveOptionName()}");
            //excuting option should show targeting!
            battle.Execute();
            battle.TargetSystem.MoveTargetLeft();
            battle.ConfirmTarget();
            int goombaKingHealth = battle.Enemies.First(enemy => enemy == GoombaKing).Health.CurrentValue;
            Assert.IsTrue(condition: goombaKingHealth == 7, message: $"goombaking health is not 7, {goombaKingHealth}");
            Enemy redGoomba = battle.Enemies.First(enemy => enemy is RedGoomba);
            Assert.IsTrue(condition: redGoomba.IsDead, message: $"redGoomba is not dead, {redGoomba}");
            Enemy blueGoomba = battle.Enemies.First(enemy => enemy is NewBlueGoomba);
            Assert.IsTrue(condition: blueGoomba.IsDead, message: $"blueGoomba is not dead, {blueGoomba}");
            
        }

        private int GetGoombaKingHealth()
        {
            return battle.Enemies.First(enemy => enemy == GoombaKing).Health.CurrentValue;
        }






    }
}

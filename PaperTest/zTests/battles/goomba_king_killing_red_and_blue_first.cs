using System.Collections.Generic;
using Attacks;
using Battle;
using Enemies;
using Heroes;
using Items;
using MenuData;
using NUnit.Framework;
using PaperLib.Enemies;
using Battler = Battle.Battle;

namespace Tests.battles
{
    [TestFixture]
    public class goomba_king_killing_red_and_blue_first
    {
        private Mario mario;
        private Goombario goombario;
        private GoombaKing GoombaKing;
        private Battler battle;
        private RedGoomba redGoomba;
        private NewBlueGoomba blueGoomba;

        [SetUp]
        public void Setup()
        {

            var bubbleSystem = new TextBubbleSystem();
            this.mario = new Mario(
                new Inventory(new Item("Mushroom"), new Item("Fire Flower", 3, TargetType.All), new Item("Mushroom")),
                new List<IJumps> { new Attacks.Jump(), new PowerJump() }.ToArray(),
                new Attacks.Hammer());
            this.goombario = new Goombario(bubbleSystem);
            this.GoombaKing = new GoombaKing(new List<IEnemyAttack> { new ScriptAttack(EnemyAttack.GoomnutJump), new GoombaKingKick() });
            var goomNutTree = new GoomnutTree();
            this.redGoomba = new RedGoomba(2);
            var enemyFactory = new EnemyFactory();
            this.blueGoomba = enemyFactory.FetchEnemy<NewBlueGoomba>(2);
            var enemies = new List<Enemy>()
            {
                goomNutTree,
                GoombaKing,
                redGoomba,
                blueGoomba,
            };
            battle = new Battle.Battle(new List<Hero> { mario, goombario }, enemies, bubbleSystem);
            battle.Start();
         
        }

        [Test]
        public void killRed()
        {
            battle.Execute();
            battle.Execute();
            Assert.True(battle.TargetSystem.Actives[0] == GoombaKing);
            battle.TargetSystem.MoveTargetRight();
            Assert.True(battle.TargetSystem.Actives[0] == redGoomba);
            battle.Execute();
            Assert.True(redGoomba.Health.CurrentValue == 1);
            battle.Execute();
            battle.Execute();
            battle.TargetSystem.MoveTargetRight();
            Assert.True(battle.TargetSystem.Actives[0] == redGoomba, $"{battle.TargetSystem.Actives[0].GetType().Name}");
            battle.Execute();
            Assert.True(redGoomba.IsDead, "redGoomba.Is not Dead");
            Assert.True(redGoomba.Health.CurrentValue == 0, "redGoomba.Is not Dead");
            battle.Execute();
            battle.Execute();
            Assert.True(battle.TargetSystem.Actives[0] == GoombaKing, $"{battle.TargetSystem.Actives[0].GetType().Name}");
            battle.TargetSystem.MoveTargetRight();
            Assert.True( battle.TargetSystem.Actives[0] == blueGoomba, $"{battle.TargetSystem.Actives[0].GetType().Name}");
        }
    }
}
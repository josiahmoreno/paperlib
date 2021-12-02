using System;
using System.Collections.Generic;
using System.Management.Instrumentation;
using Attacks;
using Attributes;
using Enemies;
using Heroes;
using NUnit.Framework;
using PaperLib.Enemies;
using Tests.battlesequence;
using Battler = Battle.Battle;

namespace Tests.target_system
{
    [TestFixture]
    public class target_movement_test
    {
        private Battler Battle;
        [SetUp]
        public void setup()
        {
           
            var mario = new Mario(jumps: new List<IJumps> { new Jump(), new PowerJump() });
            mario.Sequenceable = new TestSequenceable();
            var goombario = new Goombario();
            goombario.Sequenceable = new TestSequenceable();
          
            var fac = new EnemyFactory();
            var goomba1 = fac.FetchEnemy<NewGoomba>();
            goomba1.Sequenceable = new TestSequenceable();
            var goomba2 = fac.FetchEnemy<NewGoomba>();
            goomba2.Sequenceable = new TestSequenceable();
            var goomba3 = fac.FetchEnemy<NewGoomba>();
            goomba3.Sequenceable = new TestSequenceable();
            this.Battle = new Battler(new List<Hero>() { mario, goombario }, new List<Enemy>() { goomba1, goomba2, goomba3 });
            Battle.Start();
        }

        [Test]
        public void moveLeftTargetSystemOutOfBounds()
        {
            
            Assert.True(Battle.ActionMenu.Showing);
            Battle.Execute();
            Assert.True(!Battle.ActionMenu.Showing);
            Assert.True(Battle.OptionsListMenu.Showing);
            Battle.Execute();
            Assert.True(Battle.TargetSystem.Showing);
            Console.WriteLine($"enemies = {Battle.Enemies.Count}, {Battle.TargetSystem.SelectedIndex}");
            Battle.TargetSystem.MoveTargetLeft();
            
        }
        [Test]
        public void actives_null_after_confirm()
        {
            Console.WriteLine($"enemies = {Battle.Enemies.Count}, {Battle.TargetSystem.SelectedIndex}");
            Assert.True(Battle.ActionMenu.Showing);
            Battle.Execute();
            Assert.True(!Battle.ActionMenu.Showing);
            Assert.True(Battle.OptionsListMenu.Showing);
            Console.WriteLine($"enemies = {Battle.Enemies.Count}, {Battle.TargetSystem.SelectedIndex}");
            Battle.Execute();
            Assert.True(Battle.TargetSystem.Showing);
            Console.WriteLine($"enemies = {Battle.Enemies.Count}, {Battle.TargetSystem.SelectedIndex}");
            Assert.IsTrue(Battle.TargetSystem.Actives.Length > 0);
            Battle.Execute();
            Assert.True(!Battle.TargetSystem.Showing);
            Assert.IsNull(Battle.TargetSystem.Actives);


        }

        [Test]
        public void targetNoLongerActiveAfterExecute()
        {
            Assert.True(Battle.ActionMenu.Showing);
            Battle.Execute();
            Assert.True(!Battle.ActionMenu.Showing);
            Assert.True(Battle.OptionsListMenu.Showing);
            Battle.Execute();
            Assert.True(Battle.TargetSystem.Showing);
            Console.WriteLine($"enemies = {Battle.Enemies.Count}, {Battle.TargetSystem.SelectedIndex}");
            Battle.TargetSystem.MoveTargetRight();
            Battle.TargetSystem.MoveTargetRight();
            Battle.TargetSystem.MoveTargetRight();
        }

        [Test]
        public void HammerTest()
        {
           
            var goomba1 = new Goomba();
            var goomba2 = new Goomba(new Flying());
            var goomba3 = new Goomba(new Flying());
            var battler = new Battler( new List<Hero>() { new Mario() }, new List<Enemy>() { goomba1, goomba2, goomba3 });
            battler.Start();
            battler.TargetSystem.Show(battler.ActionMenu.Items[3].Options[0]);
            var targetSystem = battler.TargetSystem;
            targetSystem.MoveTargetRight();
            Assert.True(targetSystem.Actives[0] == goomba1);
            // Use the Assert class to test conditions
        }
    }
}
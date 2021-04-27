using Enemies;
using Heroes;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Attacks;
using PaperLib.Enemies;

namespace Tests
{
    public class boots_power_jumps_blue_goomba
    {
        private Battle.Battle battle;

        [SetUp]
        public void Setup()
        {
            var enemyFactory = new EnemyFactory();
            battle = new Battle.Battle();
            battle.Heroes.Add(new Mario(new List<IJumps>() { new PowerJump() }));
            battle.Enemies.Add(enemyFactory.FetchEnemy<NewBlueGoomba>());
        }



        [Test]
        public void boots_power_jumps_on_blue_goomba()
        {
            battle.Start();
            Hero mario = battle.Heroes.Find(hero => hero.IsUnique && hero.Identity == Heroes.Heroes.Mario);
            var blueGoomba = battle.Enemies.First();
            var powerJump = mario.Jumps.Find(jump => jump.Identifier == Attacks.Attacks.PowerJump);
            Assert.IsTrue(powerJump != null, "power jump is null");
            mario.JumpOn(blueGoomba, powerJump);
            var isAlive = blueGoomba.IsAlive();
            Assert.IsTrue(isAlive, $"health = {blueGoomba.Health.CurrentValue}");
            Assert.IsTrue(blueGoomba.Health.CurrentValue == 3, $"health = {blueGoomba.Health.CurrentValue}");
        }



    }
}

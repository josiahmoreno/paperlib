using Enemies;
using Heroes;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PaperLib.Enemies;

namespace Tests
{
    public class Mario_attacks_blue_goomba
    {
        private Battle.Battle battle;

        [SetUp]
        public void Setup()
        {
            battle = new Battle.Battle();
            battle.Heroes.Add(new Mario());
            var enemyFactory = new EnemyFactory();
            ;
            battle.Enemies.Add(enemyFactory.FetchEnemy<NewBlueGoomba>());
        }

        [Test]
        public void StartBattle()
        {
            Assert.Pass();
        }

        [Test]
        public void Mario_jumps_on_goomba()
        {
            battle.Start();
            Hero mario = battle.Heroes.Find(hero => hero.IsUnique && hero.Identity == Heroes.Heroes.Mario);
            var blueGoomba = battle.Enemies.First();
            mario.JumpOn(blueGoomba);
            var isAlive = blueGoomba.IsAlive();
            Assert.IsTrue(isAlive, $"health = {blueGoomba.Health.CurrentValue}");
            Assert.IsTrue(blueGoomba.Health.CurrentValue == 5);
            
        }


        [Test]
        public void Mario_hammers_on_goomba()
        {
            battle.Start();
            Hero mario = battle.Heroes.Find(hero => hero.IsUnique && hero.Identity == Heroes.Heroes.Mario);
            var blueGoomba = battle.Enemies.First();
            mario.Hammers(blueGoomba);
            var isAlive = blueGoomba.IsAlive();
            Assert.IsTrue(isAlive, $"health = {blueGoomba.Health.CurrentValue}");
            Assert.IsTrue(blueGoomba.Health.CurrentValue == 5);
        }

    }
}

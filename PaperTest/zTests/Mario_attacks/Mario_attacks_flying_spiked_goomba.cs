using NUnit.Framework;
using System.Linq;
using Battle;
using Heroes;
using Enemies;
using Attributes;
using System.Collections.Generic;

namespace Tests
{
    public class Mario_attacks_flying_spiked_goomba
    {
        private Battle.Battle battle;

        [SetUp]
        public void Setup()
        {
            battle = new Battle.Battle(new List<Hero> { new Mario() }, new List<Enemy> { new Goomba(new Flying(), new Spike()) });
        }

        [Test]
        public void StartBattle()
        {
            Assert.Pass();
        }

        [Test]
        public void Mario_jumps_on_flying_spiked_goomba()
        {

            battle.Start();
            IMario mario = battle.Heroes.Find(hero => hero.IsUnique && hero.Identity == Heroes.Heroes.Mario) as IMario;
            
            mario.JumpOn(battle.Enemies.First());
            var isAlive = battle.Enemies.First().IsAlive();
            Assert.IsTrue(isAlive);
        }
        [Test]
        public void is_goomba_flying_and_spiked()
        {
            battle.Start();
            // var mario = battle.Heroes.Find(hero => hero.IsUnique && hero.Idenity == Heroes.Mario);

            // mario.JumpOn(battle.Enemies.First());
            var goomba = battle.Enemies.First();
            bool isSpikedAndFlying = goomba.IsFlying && goomba.IsSpiked;
            Assert.IsTrue(isSpikedAndFlying);
        }

        [Test]
        public void Mario_hammers_on_flying_and_spiked_goomba()
        {
            battle.Start();
            IMario mario = battle.Heroes.Find(hero => hero.IsUnique && hero.Identity == Heroes.Heroes.Mario) as IMario;
            mario.Hammers(battle.Enemies.First());
            var isAlive = battle.Enemies.First().IsAlive();
            Assert.IsTrue(isAlive);
        }



    }
}


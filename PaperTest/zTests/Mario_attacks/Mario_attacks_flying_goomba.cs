using NUnit.Framework;
using System.Linq;
using Battle;
using Heroes;
using Enemies;
using Attributes;
using System.Collections.Generic;

namespace Tests
{
    public class Mario_attacks_flying_goomba
    {
        private Battle.Battle battle;

        [SetUp]
        public void Setup()
        {
            battle = new Battle.Battle(new List<Hero> { new Mario() }, new List<Enemy> { new Goomba(new Flying()) });
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
            IMario mario = battle.Heroes.Find(hero => hero.IsUnique && hero.Identity == Heroes.Heroes.Mario) as IMario;
            
            mario.JumpOn(battle.Enemies.First());
            var isDead = battle.Enemies.First().Health.CurrentValue == 0;
            Assert.IsTrue(isDead);
        }
        [Test]
        public void is_goomba_flying()
        {
            battle.Start();
           // var mario = battle.Heroes.Find(hero => hero.IsUnique && hero.Idenity == Heroes.Mario);

           // mario.JumpOn(battle.Enemies.First());
            bool isFlying = battle.Enemies.First().IsFlying ;
            Assert.IsTrue(isFlying);
        }

        [Test]
        public void Mario_hammers_on_goomba()
        {
            battle.Start();
            IMario mario = battle.Heroes.Find(hero => hero.IsUnique && hero.Identity == Heroes.Heroes.Mario) as IMario;
            mario.Hammers(battle.Enemies.First());
            var isDead = battle.Enemies.First().Health.CurrentValue == 0;
            Assert.IsFalse(isDead);
        }

    }
}


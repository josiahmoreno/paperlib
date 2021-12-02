using NUnit.Framework;
using System.Linq;
using Battle;
using Heroes;
using Enemies;
using System.Collections.Generic;

namespace Tests
{
    public class Mario_attacks_goomba
    {
        private Battle.Battle battle;

        [SetUp]
        public void Setup()
        {
            battle = new Battle.Battle(new List<Hero> { new Mario() }, new List<Enemy> { new Goomba()});
        }

      

        [Test]
        public void Mario_jumps_on_goomba()
        {
            battle.Start();
            IMario mario = battle.Heroes.Find(hero => hero.IsUnique && hero.Identity == Heroes.Heroes.Mario) as IMario;
            
            mario.JumpOn(battle.Enemies.First());
            var isDead = battle.Enemies.First().IsDead;
            Assert.IsTrue(isDead);
        }
        [Test]
        public void check_goomba_health()
        {
            battle.Start();
           // var mario = battle.Heroes.Find(hero => hero.IsUnique && hero.Idenity == Heroes.Mario);

           // mario.JumpOn(battle.Enemies.First());
            var isAlive = battle.Enemies.First().IsAlive();
            Assert.IsTrue(isAlive);
        }

        [Test]
        public void Mario_hammers_on_goomba()
        {
            battle.Start();
            IMario mario = battle.Heroes.Find(hero => hero.IsUnique && hero.Identity == Heroes.Heroes.Mario) as IMario;

            mario.Hammers(battle.Enemies.First());
            var isDead = battle.Enemies.First().IsDead;
            Assert.IsTrue(isDead);
        }

    }
}


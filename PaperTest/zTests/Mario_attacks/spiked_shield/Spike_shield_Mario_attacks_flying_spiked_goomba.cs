using NUnit.Framework;
using System.Linq;
using Battle;
using Heroes;
using Enemies;
using Attributes;
using System.Collections.Generic;

namespace Tests
{
    public class Spike_shield_Mario_attacks_flying_spiked_goomba
    {
        private Battle.Battle battle;

        [SetUp]
        public void Setup()
        {
            battle = new Battle.Battle(new List<Hero> { new Mario() }, new List<Enemy> { new Goomba(new Flying() ,new Spike()) });
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



    }
}


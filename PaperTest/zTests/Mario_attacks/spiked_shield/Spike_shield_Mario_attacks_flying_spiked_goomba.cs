using NUnit.Framework;
using System.Linq;
using Battle;
using Heroes;
using Enemies;
using Attributes;

namespace Tests
{
    public class Spike_shield_Mario_attacks_flying_spiked_goomba
    {
        private Battle.Battle battle;

        [SetUp]
        public void Setup()
        {
            battle = new Battle.Battle();
            battle.Heroes.Add(new Mario(new SpikeShield()));
            battle.Enemies.Add(new Goomba(new Flying(),new Spike()));
        }


        [Test]
        public void Mario_jumps_on_flying_spiked_goomba()
        {
            battle.Start();
            Hero mario = battle.Heroes.Find(hero => hero.IsUnique && hero.Identity == Heroes.Heroes.Mario);
            
            mario.JumpOn(battle.Enemies.First());
            var isDead = battle.Enemies.First().IsDead;
            Assert.IsTrue(isDead);
        }



    }
}


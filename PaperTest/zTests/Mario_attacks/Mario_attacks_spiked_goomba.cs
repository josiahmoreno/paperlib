using NUnit.Framework;
using System.Linq;
using Battle;
using Heroes;
using Enemies;
using Attributes;

namespace Tests
{
    public class Mario_attacks_spikes_goomba
    {
        private Battle.Battle battle;

        [SetUp]
        public void Setup()
        {
            battle = new Battle.Battle();
            battle.Heroes.Add(new Mario());
            battle.Enemies.Add(new Goomba(new Spike()));
        }

     

        [Test]
        public void Mario_jumps_on_spiked_goomba()
        {
            battle.Start();
            Hero mario = battle.Heroes.Find(hero => hero.IsUnique && hero.Identity == Heroes.Heroes.Mario);
            
            mario.JumpOn(battle.Enemies.First());
            var isAlive = battle.Enemies.First().IsAlive();
            Assert.IsTrue(isAlive, $"health = {  battle.Enemies.First().Health.CurrentValue }");
        }
        [Test]
        public void is_goomba_spiked()
        {
            battle.Start();
           // var mario = battle.Heroes.Find(hero => hero.IsUnique && hero.Idenity == Heroes.Mario);

           // mario.JumpOn(battle.Enemies.First());
            bool isSpiked = battle.Enemies.First().IsSpiked ;
            Assert.IsTrue(isSpiked, $"attributes = { battle.Enemies.First().Attrs.ToString()}");
        }

        [Test]
        public void Mario_hammers_on_spiked_goomba()
        {
            battle.Start();
            Hero mario = battle.Heroes.Find(hero => hero.IsUnique && hero.Identity == Heroes.Heroes.Mario);
            mario.Hammers(battle.Enemies.First());
          
            Assert.IsTrue(battle.Enemies.First().IsDead);
        }

    }
}


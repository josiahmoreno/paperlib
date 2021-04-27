using Enemies;
using Heroes;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tests
{
    public class Turns
    {
        private Mario mario;

        //private Battle.Battle battle;

        [SetUp]
        public void Setup()
        {
            mario =  new Mario();
            //battle = new Battle.Battle();
            //battle.Heroes.Add(new Mario());
            //battle.Enemies.Add(new BlueGoomba());
        }

      
        [Test]
        public void HasBattleEnded()
        {
            var encounter = new Encounter(new Goomba(), new SpikedGoomba());
            var battle = new Battle.Battle(new List<Hero> { mario }, encounter);
            battle.End();
            Assert.IsTrue(battle.IsEnded());
            Assert.IsFalse(battle.IsStarted());
        }

        [Test]
        public void HasBattleEndedAfterAllEnemiesKilled()
        {
            var goomba = new Goomba();
            var encounter = new Encounter(goomba, new SpikedGoomba());
            var battle = new Battle.Battle(new List<Hero> { mario }, encounter);
            battle.Start();
            Assert.IsTrue(battle.IsStarted());
            //Can you kill a goomba if the battle has started??
            battle.Enemies.ForEach(enemy => enemy.Kill());
            Assert.IsTrue(battle.IsEnded());
            Assert.IsFalse(battle.IsStarted());
        }



    }
}

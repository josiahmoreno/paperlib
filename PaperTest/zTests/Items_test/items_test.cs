using Enemies;
using Heroes;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Attributes;
using Battle;
using PaperLib.Enemies;

namespace Tests
{
    public class items_test
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
        public void StartBattle()
        {
          
            var encounter = new Encounter(new Goomba(),new SpikedGoomba());
            var battle = new Battle.Battle(new List<Hero> { mario}, encounter);
            battle.Start();
            Assert.IsTrue(battle.IsStarted());
            Assert.IsFalse(battle.IsEnded());
        }

      
        


    }

    

    
}

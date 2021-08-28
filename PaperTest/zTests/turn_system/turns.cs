using NUnit.Framework;
using PaperLib;
using PaperLib.Enemies;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tests;

namespace PaperTest
{
    [TestFixture]
    public class turns: ILogger
    {
        public void Log(string v)
        {
            Console.WriteLine(v);
        }

        [Test]
        public void turn_end()
        {
            ITurnSystem system = new Battle.DefaultTurnSystem(this);
            
            var heroes = new List<Heroes.Hero>();
            var mario = new Heroes.Mario();
            heroes.Add(mario);
            var enemies = new List<Enemies.Enemy>();
            var factory = new EnemyFactory();
            var goomba = factory.FetchEnemy<NewGoomba>();
            bool hasSwapped = false;
            system.OnSwapped += (obj,arg) =>
            {
                Console.WriteLine("test on swapped");
                hasSwapped = true;
            };
            enemies.Add(goomba);
            system.Load(heroes,enemies);
            Assert.AreEqual(mario, system.Active);
            system.End();
            Assert.AreEqual(goomba, system.Active);
            system.End();
            Assert.AreEqual(mario, system.Active);
            system.End();
            Assert.AreEqual(goomba, system.Active);
            system.End();
            Assert.IsFalse(hasSwapped);
        }
    }
}

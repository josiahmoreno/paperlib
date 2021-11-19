using Battle;
using Heroes;
using NUnit.Framework;
using System.Collections.Generic;
using Enemies;
using MenuData;
using PaperLib.Enemies;
using System;

namespace Tests.menu_tests
{
    [TestFixture]
    public class menu_tests
    {

        [Test]
        public void sequences_equal()
        {
            var mario = new Mario();
            var mario2 = new Mario();
            Assert.IsTrue(Mario.Comparer.Equals(mario, mario2));
        }

        [Test]
        public void goomvario_sequences_equal()
        {
            var mario = new Goompa();
            var mario2 = new Goompa();
            Assert.AreEqual(mario, mario2);
        }

        [Test]
        public void jr_troopa_equal()
        {
            
            //
            var jrTroopaOne = new Enemies.JrTroopa(new List<IEnemyAttack>
                { new RegularAttackWrapper(Attacks.Attacks.JrTroopaJump, 1) });
            var jrTroopaTwo = new Enemies.JrTroopa(new List<IEnemyAttack>
                { new RegularAttackWrapper(Attacks.Attacks.JrTroopaJump, 1) });
            Assert.IsTrue(NewBaseEnemy.Comparer.Equals(jrTroopaOne, jrTroopaTwo));
        }


        [Test]
        public void battleMenuShowing()
        {
            Battle.Battle.Logger = new TestLogger();
            Battle.Battle battle = new Battle.Battle();
            battle.Load(new List<Hero>() { new Mario() }, new List<Enemy>() { new EnemyFactory().FetchEnemy<NewGoomba>() });
            battle.Start();
            IActionMenu menu = battle.ActionMenu;
            battle.Execute();
            Assert.IsFalse(menu.Showing);
        }

    }
}
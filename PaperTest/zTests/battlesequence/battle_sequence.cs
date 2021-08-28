using Enemies;
using MenuData;
using NUnit.Framework;
using PaperLib.Sequence;
using System.Collections.Generic;

namespace Tests.battlesequence
{
    [TestFixture]
    public class battle_sequence
    {
        //
        // private IOption move;
        // private object activeHero;
        // private Enemy[] target;
        // private Battle.Battle battle;
        
        [Test]
        public void mario_jumps_on_goomba_sequence()
        {

            // move.Execute(battle, activeHero, target, (justDamaged) =>
            // {
            //     
            // });
            var mario = new TestSequenceable();
            var target = mario.CopyPosition();
            var goomba = new TestTarget(20,0,0);
            var runUp = new RunToAnimation(goomba);
            var jump = new JumpAnimation();
            var damage = new DamageStep();
            var runBack = new RunToAnimation(target);
            var battleSequence = new BattleSequence(null,new List<ISequenceStep>{ runUp , jump, damage, runBack }, mario, runBack);
            battleSequence.Execute();
            Assert.IsTrue(battleSequence.IsComplete);

        }
    }
}
using Attacks;
using Enemies;
using Heroes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaperLib.Sequence
{
    public class JumpSequence 
    {
        private ILogger Logger;
        private IDamageTarget damageTarget;

        //public IAttack Attack { get; }

        //private readonly Hero hero;
        //private readonly Enemy enemyTarget;
        //private readonly Func<bool> successfulQuicktime;

        public JumpSequence(ILogger logger, IDamageTarget damageTarget)
        {
            this.Logger = logger;
            this.damageTarget = damageTarget;
        }
        public void Execute(ISequenceable sequenceable, IMovementTarget movementTarget)
        {
            
            var mario = sequenceable ?? throw new NullReferenceException("sequenceable is null");
            var marioStart = mario.CopyPosition();
            Logger?.Log($"{GetType().Name} - starting at {mario.Position} and moving to {movementTarget}, then back to {marioStart}");
            var jumpStart = movementTarget;
            var jumpEnd = movementTarget.Copy();
            int offset = 100;
            if(marioStart.Position.Item1 > jumpEnd.Position.Item1)
            {
                offset = -100;
            }
            jumpStart.Position = new Tuple<float, float, float>(jumpStart.Position.Item1 - offset, jumpStart.Position.Item2, jumpStart.Position.Item3);
            var jumpStart2 = jumpStart.Copy();
            var runUp = new RunToAnimation(Logger, jumpStart);
            var jumpOnto = new JumpAnimation(Logger, jumpEnd);
            jumpOnto.QuickTime = damageTarget.successfulQuicktime;
            var damage = new DamageStep(Logger, damageTarget);
            var jumpOff = new JumpAnimation(Logger, jumpStart2);
            var runBack = new RunToAnimation(Logger, marioStart);
            var battleSequence = new BattleSequence(Logger,new List<ISequenceStep> { runUp, jumpOnto, damage, jumpOff,runBack }, mario, runBack);
            battleSequence.OnComplete += BattleSequence_OnComplete;
            battleSequence.Execute();

        }

        private void BattleSequence_OnComplete(object sender, EventArgs e)
        {
            (sender as BattleSequence).OnComplete -= BattleSequence_OnComplete;
            OnComplete?.Invoke(this, EventArgs.Empty);
        }

        public EventHandler OnComplete;
    }
}

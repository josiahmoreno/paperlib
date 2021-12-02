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
    public class HammerSequence : ISequence
    {
        private ILogger Logger;
        public HammerSequence(ILogger logger)
        {
            this.Logger = logger;

        }
        public void Execute(ISequenceable sequenceable, IPositionable movementTarget, IDamageTarget damageTarget)
        {
            //set up positions
            if(movementTarget == null)
            {
                throw new NullReferenceException("movementTarget is null");
            }
            var mario = sequenceable ?? throw new NullReferenceException("sequenceable is null");
            var startingMarioPosition = mario.CopyPosition();
            Logger?.Log($"{GetType().Name} - starting at {mario} and moving to {movementTarget}, then back to {startingMarioPosition}");
            var firstLaunchPosition = movementTarget.CopyPosition();
            var goombaPosition = movementTarget.CopyPosition();
            Logger?.Log($"{GetType().Name} - startingMarioPosition {{{startingMarioPosition.x}}}/goombaPosition {{{goombaPosition.x}}}");
            if (startingMarioPosition.x < goombaPosition.x)
            {

                firstLaunchPosition.x = firstLaunchPosition.x - 40;
            }

            var landingPosition = firstLaunchPosition.CopyPosition();

            var runUp = new RunToAnimation(Logger, firstLaunchPosition);
            ISequenceStep wait = new HammerWindUpAnimation(Logger);
            var damage = new DamageStep(Logger, damageTarget);
            var runBack = new RunToAnimation(Logger, startingMarioPosition);
            var battleSequence = new BattleSequence(Logger, new List<ISequenceStep> { runUp, wait, damage, runBack }, mario, runBack);
            battleSequence.OnComplete += BattleSequence_OnComplete;
            battleSequence.Execute();
        }

        private void BattleSequence_OnComplete(object sender, EventArgs e)
        {
            (sender as BattleSequence).OnComplete -= BattleSequence_OnComplete;
            OnComplete?.Invoke(this, EventArgs.Empty);
        }

        public event EventHandler OnComplete;
    }
}

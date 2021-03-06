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
    public class JumpSequence : ISequence
    {
        private ILogger Logger;
        public JumpSequence(ILogger logger)
        {
            this.Logger = logger;

        }
        public void Execute(ISequenceable sequenceable, IPositionable movementTarget, IDamageTarget damageTarget)
        {
            //set up positions
            var mario = sequenceable ?? throw new NullReferenceException("sequenceable is null");
            var startingMarioPosition = mario.CopyPosition();
            Logger?.Log($"{GetType().Name} - starting at {mario} and moving to {movementTarget}, then back to {startingMarioPosition}");
            var firstLaunchPosition = movementTarget.CopyPosition();
            var goombaPosition = movementTarget.CopyPosition();
            int offset = startingMarioPosition.x > goombaPosition.x ? -100 : 100;
            firstLaunchPosition.x -= offset;

            var landingPosition = firstLaunchPosition.CopyPosition();

            var runUp = new RunToAnimation(Logger, firstLaunchPosition);
            var jumpOnto = new JumpAnimation(Logger, goombaPosition);
            var damage = new DamageStep(Logger, damageTarget);
            var jumpOff = new JumpAnimation(Logger, landingPosition);
            var runBack = new RunToAnimation(Logger, startingMarioPosition);
            Logger?.Log($"{GetType().Name} - running back to {startingMarioPosition}");

            var battleSequence = new BattleSequence(Logger, new List<ISequenceStep> { runUp, jumpOnto, damage, jumpOff, runBack }, mario, runBack);
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

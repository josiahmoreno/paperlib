using PaperLib.Sequence;
using System;
using System.Threading;

namespace PaperLib.Sequence
{
    internal class JumpAnimation: ISequenceStep
    {
        private IPositionable movementTarget;
        private ILogger Logger;
        public JumpAnimation(ILogger logger, IPositionable movementTarget)
        {
            this.movementTarget = movementTarget;
            Logger = logger;
        }

        public event EventHandler OnComplete;
        public void Start(ISequenceable hero)
        {

            Logger?.Log($"{this} start");
            //Thread.Sleep(2000);
            hero.Jump(movementTarget, ()=>
            {
                OnComplete?.Invoke(this, EventArgs.Empty);
            });

          
        }
    }
}
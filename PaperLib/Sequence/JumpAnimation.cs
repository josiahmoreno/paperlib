using PaperLib.Sequence;
using System;
using System.Threading;

namespace PaperLib.Sequence
{
    internal class JumpAnimation: ISequenceStep
    {
        private IMovementTarget movementTarget;
        private ILogger Logger;
        public JumpAnimation(ILogger logger, IMovementTarget movementTarget)
        {
            this.movementTarget = movementTarget;
            Logger = logger;
        }

        public event EventHandler OnComplete;
        private bool IsCompleted;

        public Func<bool> QuickTime { get; internal set; }

        public void Start(ISequenceable hero)
        {

            Logger?.Log($"{this.ToString()} start");
            //Thread.Sleep(2000);
            hero.Jump(movementTarget, ()=>
            {
                OnComplete?.Invoke(this, EventArgs.Empty);
            });

          
        }
    }
}
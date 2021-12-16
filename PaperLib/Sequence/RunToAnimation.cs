using PaperLib.Sequence;
using System;

namespace PaperLib.Sequence
{
    internal class RunToAnimation : ISequenceStep
    {
        public ILogger Logger;
        private bool IsCompleted;
        public RunToAnimation(ILogger logger, IPositionable goomba)
        {
            Logger = logger;
            Goomba = goomba;
        }

        public IPositionable Goomba { get; }

        public event EventHandler OnComplete;
        private Guid Guid = Guid.NewGuid();
        public void Start(ISequenceable hero)
        {
            if (IsCompleted)
            {
                throw new Exception("can't rerun a completed animation");
            }
            Logger?.Log($"{this} {Guid.ToString().Substring(0,4)} moving to {{{Goomba}}}");
         
           
            hero.OnMoveComplete = () =>
            {
                Logger?.Log($"{this} oncomplete {{{Goomba}}}, Complete == null {OnComplete == null}");
                IsCompleted = true;
                //hero.Animator.Complete(this);
                OnComplete(this, EventArgs.Empty);
            };
            hero.MoveTo(Goomba);
            
            //throw new System.NotImplementedException();
        }

        public override string ToString()
        {
            return $"{GetType().Name}, {Guid.ToString().Substring(0, 4)}, {Goomba}";
        }
    }
}
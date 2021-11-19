using PaperLib.Sequence;
using System;

namespace PaperLib.Sequence
{
    internal class RunToAnimation : ISequenceStep
    {
       
        public RunToAnimation(IPositionable goomba)
        {
            Goomba = goomba;
        }

        public IPositionable Goomba { get; }

        public event EventHandler OnComplete;

        public void Start(ISequenceable hero)
        {
            Console.WriteLine($"{this} start {{{Goomba}}}");
           
            hero.OnMoveComplete = () => { OnComplete(this, EventArgs.Empty); };
            hero.MoveTo(Goomba);
            
            //throw new System.NotImplementedException();
        }

        public override string ToString()
        {
            return $"{GetType().Name} - {Goomba}";
        }
    }
}
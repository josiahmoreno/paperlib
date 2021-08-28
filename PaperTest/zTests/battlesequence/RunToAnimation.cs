using PaperLib.Sequence;
using System;

namespace PaperLib.Sequence
{
    internal class RunToAnimation : ISequenceStep
    {
       
        public RunToAnimation(IMovementTarget goomba)
        {
            Goomba = goomba;
        }

        public IMovementTarget Goomba { get; }

        public event EventHandler OnComplete;

        public void Start(ISequenceable hero)
        {
            Console.WriteLine($"{this} start {{{Goomba}}}");
            Tuple<float, float, float> a = Goomba.Position;
            hero.OnMoveComplete = () => { OnComplete(this, EventArgs.Empty); };
            hero.MoveTo(Goomba);
            
            //throw new System.NotImplementedException();
        }

        public override string ToString()
        {
            return $"{GetType().Name} - {Goomba.Position}";
        }
    }
}
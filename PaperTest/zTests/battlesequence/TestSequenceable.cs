using PaperLib.Sequence;
using System;
using System.Threading;

namespace Tests.battlesequence
{
    internal class TestSequenceable: ISequenceable
    {

        public TestSequenceable()
        {
        }

        ////public Tuple<float, float, float> Position { get; set; } = new Tuple<float, float, float>(0f,0f,0f);
        //public IMovementTarget CopyPosition()
        //{
        //    return MovementTarget.CopyPosition(); ;
        //}

        public Action OnMoveComplete { get; set ; }
        public Animator Animator { get; set; } = new TestAnimator();

        //public IPositionable MovementTarget { get; set; } = new TestTarget(0, 0, 0);
        public float x { get; set; }
        public float y { get; set; }
        public float z { get ; set; }

        public IPositionable CopyPosition()
        {
            return new TestTarget(x, y, z);
            Console.WriteLine("no position to copy");
            //throw new NotImplementedException();
        }

        public void Jump(IPositionable p, Action p1)
        {
            p1();
        }

        public void MoveTo(IPositionable p)
        {
            OnMoveComplete();
            //throw new System.NotImplementedException();
        }

        public void Wait(SendOrPostCallback sendOrPostCallback, object v)
        {
            sendOrPostCallback.Invoke(null);
        }
    }

    internal class TestAnimator : Animator
    {
        public void Start(ISequenceStep runToAnimation)
        {
            
        }

        public void Complete(ISequenceStep runToAnimation)
        {
            
        }
    }
}
using PaperLib.Sequence;
using System;

namespace Tests.battlesequence
{
    internal class TestSequenceable: ISequenceable
    {
        public TestSequenceable()
        {
        }

        public Tuple<float, float, float> Position { get; set; } = new Tuple<float, float, float>(0f,0f,0f);
        public IMovementTarget Copy()
        {
           return new TestTarget(Position);
        }

        public Action OnMoveComplete { get; set ; }

    
       

        public void Jump(IMovementTarget p, Action p1)
        {
            p1();
        }

        public void MoveTo(IMovementTarget p)
        {
            OnMoveComplete();
            //throw new System.NotImplementedException();
        }

        public void StartAnimation()
        {
            throw new System.NotImplementedException();
        }

        public IMovementTarget CopyPosition()
        {
            return new TestTarget(Position);
        }

       
    }
}
using PaperLib.Sequence;
using System;

namespace Tests.battlesequence
{
    internal class TestSequenceable: ISequenceable
    {
        public TestSequenceable()
        {
        }

        public Tuple<float, float, float> Position { get; private set; } = new Tuple<float, float, float>(0f,0f,0f);
        public Action OnMoveComplete { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public void Jump()
        {
            throw new System.NotImplementedException();
        }

        public void MoveTo()
        {
            throw new System.NotImplementedException();
        }

        public void MoveTo(IMovementTarget p)
        {
            
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
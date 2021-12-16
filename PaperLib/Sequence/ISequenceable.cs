using System;
using System.Threading;

namespace PaperLib.Sequence
{
    public interface ISequenceable : IPositionable
    {
        void Jump(IPositionable p, Action p1);
        void MoveTo(IPositionable p);
        void Wait(SendOrPostCallback sendOrPostCallback, object v);

        Action OnMoveComplete { get; set; }
        Animator Animator { get; set; }

        //IMovementTarget MovementTarget { get; set; }
       
    }
}
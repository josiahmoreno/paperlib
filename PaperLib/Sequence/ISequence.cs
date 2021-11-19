using System;

namespace PaperLib.Sequence
{
    public interface ISequence
    {
        event EventHandler OnComplete;
        void Execute(ISequenceable sequenceable, IPositionable movementTarget, IDamageTarget damageTarget);
    }
}
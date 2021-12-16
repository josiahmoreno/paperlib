using System;

namespace PaperLib.Sequence
{
    public interface ISequenceStep
    {
        event EventHandler OnComplete;

        void Start(ISequenceable hero);
    }
}
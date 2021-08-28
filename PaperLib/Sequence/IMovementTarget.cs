using System;

namespace PaperLib.Sequence
{
    public interface IMovementTarget
    {
        Tuple<float,float,float> Position { get; set; }

        IMovementTarget Copy();
    }
}
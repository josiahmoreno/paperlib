using System;

namespace PaperLib.Sequence
{
    public interface IQuicktime
    {
        Func<bool> Getter { get; }
    }
}
using System;

namespace PaperLib.Sequence
{
    public interface IPositionable
    {
        
        float x { get; set; }
        float y { get; set; }
        float z { get; set; }

        IPositionable CopyPosition();
    }
}
namespace PaperLib.Sequence
{
    public interface Animator
    {
        void Start(ISequenceStep runToAnimation);
        void Complete(ISequenceStep runToAnimation);
    }
}
using PaperLib.Sequence;
using System;
using System.Threading;

namespace PaperLib.Sequence
{
    internal class JumpAnimation: ISequenceStep
    {
        private ILogger Logger;
        public JumpAnimation()
        {
        }

        public event EventHandler OnComplete;

        public void Start(ISequenceable hero)
        {
            Console.WriteLine(this);
            //Thread.Sleep(2000);
           
            OnComplete?.Invoke(this,EventArgs.Empty);
        }
    }
}
using System;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;

namespace PaperLib.Sequence
{
    internal class HammerWindUpAnimation : ISequenceStep
    {
        private ILogger logger;

        public HammerWindUpAnimation(ILogger logger)
        {
            this.logger = logger;
        }

        public event EventHandler OnComplete;


        public void Start(ISequenceable hero)
        {


           
            hero.Wait(new SendOrPostCallback(state =>
            {
                logger.Log($"{this} timer ended, - {Thread.CurrentThread.ManagedThreadId} completingggggg...");
                OnComplete(this, EventArgs.Empty);
            }), new object());


        }
    }
}
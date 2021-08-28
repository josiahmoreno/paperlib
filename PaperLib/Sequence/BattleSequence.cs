using System;
using System.Collections.Generic;
using Heroes;
using MenuData;

namespace PaperLib.Sequence
{
    public class BattleSequence
    {
        private readonly ILogger logger;
        private readonly ISequenceable hero;
        private List<ISequenceStep> PreAnimations;
        private ISequenceStep lastAnimation;

        public BattleSequence(ILogger logger, List<ISequenceStep> preAnimations, ISequenceable hero, ISequenceStep lastAnimation)
        {
            this.logger = logger;
            this.hero = hero;
            this.lastAnimation = lastAnimation;
            PreAnimations = preAnimations;
        }

        public bool IsComplete { get; private set; }

  

        public void Execute()
        {
            var enumator = PreAnimations[0];
            logger?.Log($"{GetType().Name} - Execute, step count = {PreAnimations.Count}");
            for(int i = 0; i< PreAnimations.Count; i++)
            {
                var animation = PreAnimations[i];
                if (i != PreAnimations.Count - 1)
                {
                    var capturedIndex = i;
                    bool captComplete = false;
                    void onCom(object sender, EventArgs args)
                    {
                        var camp = capturedIndex;
                        var captttCombool = captComplete;
                        var preee = PreAnimations;
                        if (captComplete)
                        {
                            //return;
                            throw new Exception($"can't complete again, capturedIndex={capturedIndex} {PreAnimations.Count}");
                        }
                        captComplete = true;
                        logger?.Log($"{GetType().Name} - just completed1, capturedIndex={capturedIndex} {PreAnimations.Count}");
                        logger?.Log($"{GetType().Name} - just completed2, {PreAnimations[capturedIndex]}");
                        //animation.OnComplete -= onCom;
                        PreAnimations[capturedIndex].OnComplete -= onCom;
                        
                        PreAnimations[capturedIndex + 1].Start(hero);
                    }
                    //PreAnimations[capturedIndex + 1].OnComplete += onCom;
                    animation.OnComplete += onCom;
                    logger?.Log($"{GetType().Name} - assigning oncomplete to {animation}");
                } else
                {
                    void onFinish(object sender, EventArgs args)
                    {
                        animation.OnComplete -= onFinish;
                        IsComplete = true;
                        OnComplete?.Invoke(this,EventArgs.Empty);
                    }
                    logger?.Log($"{GetType().Name} - assigning onfinish to to last step, {animation}");
                    animation.OnComplete += onFinish;
                }

            }
            enumator.Start(hero);
             

            //throw new NotImplementedException();
        }

        public event EventHandler OnComplete;
    }

    public interface ISequenceStep
    {
        event EventHandler OnComplete;

        void Start(ISequenceable hero);
    }

    public interface ISequenceable : IMovementTarget
    {
        void StartAnimation();
        void Jump(IMovementTarget p, Action p1);
        void MoveTo(IMovementTarget p);
        Action OnMoveComplete { get; set; }
        IMovementTarget CopyPosition();
    }
}
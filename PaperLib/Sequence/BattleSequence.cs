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
                        //logger?.Log($"{GetType().Name} - just completed1, capturedIndex={capturedIndex} {PreAnimations.Count}");
                        //logger?.Log($"{GetType().Name} - just completed2, {PreAnimations[capturedIndex]}");
                      
                        PreAnimations[capturedIndex].OnComplete -= onCom;
                        hero.Animator.Complete(PreAnimations[capturedIndex]);
                        hero.Animator.Start(PreAnimations[capturedIndex +1 ]);
                        PreAnimations[capturedIndex + 1].Start(hero);
                    }
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
            hero.Animator.Start(enumator);
        }

        public event EventHandler OnComplete;
    }
}
using System.Collections.Generic;
using Heroes;
using MenuData;

namespace PaperLib.Sequence
{
    public class BattleSequence
    {

        private List<IBattleAnimation> PreAnimations;
        public void Execute(IOption option, ISequenceable hero)
        {
            PreAnimations.ForEach(a=> a.Start(hero));
        }
    }

    public interface IBattleAnimation
    {
        void Start(ISequenceable hero);
    }

    public interface ISequenceable
    {
        void StartAnimation();
        void MoveTo();
        void Jump();
    }
}
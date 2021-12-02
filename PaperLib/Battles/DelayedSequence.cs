namespace Battle
{
    internal class DelayedSequence : IBattleAnimationSequence
    {
        private IBattleAnimationSequence battleAnimationSequence;
        private IActionCommandCenter actionCommandCenter;


        public DelayedSequence(IActionCommandCenter actionCommandCenter)
        {
            this.actionCommandCenter = actionCommandCenter;
        }

        public bool Sucessful => actionCommandCenter.FetchSequence().Sucessful;
    }
}
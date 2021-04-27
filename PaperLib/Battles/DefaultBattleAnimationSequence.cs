namespace Battle
{
    internal class DefaultBattleAnimationSequence : IBattleAnimationSequence
    {
        private bool last;

        public DefaultBattleAnimationSequence(bool last)
        {
            this.last = last;
        }

        public bool Sucessful => last;
    }
}
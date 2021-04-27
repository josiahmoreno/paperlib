namespace Battle
{
    public interface IActionCommandCenter
    {
        IBattleAnimationSequence FetchSequence();
        void AddSuccessfulPress();
    }
}
namespace Attacks
{
    public interface IAttack
    {
        int Power { get; }

        bool IsGroundOnly();
        bool CanHitFlying();

        Attacks Identifier { get; }
    }
}
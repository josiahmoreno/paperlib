namespace Attacks
{
    public interface IAttack
    {
        int Power { get; }

        bool IsGroundOnly();
        bool IsJump();

        Attacks Identifier { get; }
    }
}
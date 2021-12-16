using Attacks;

namespace Heroes
{
    internal class ShellToss : IAttack
    {
        public int Power => 1;

        public Attacks.Attacks Identifier => Attacks.Attacks.ShellToss;

        public bool IsGroundOnly()
        {
            return true;
        }

        public bool CanHitFlying()
        {
           return false;
        }
    }
}
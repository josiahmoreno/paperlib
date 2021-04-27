namespace Attacks
{
    public class HammerThrow: IAttack
    {
        public HammerThrow()
        {
        }

        public int Power => 1;

        public Attacks Identifier => Attacks.HammerThrow;

        public bool IsGroundOnly()
        {
            return false;
        }

        public bool IsJump()
        {
            return false;
        }
    }
}
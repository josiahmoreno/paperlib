namespace Attacks
{
    public class Hammer: IAttack
    {
        public Hammer()
        {
        }

        public int Power => 1;

        public Attacks Identifier => Attacks.BaseHammer;

        public bool IsGroundOnly()
        {
            return true ;
        }

        public bool IsJump()
        {
           return false;
        }
    }
}
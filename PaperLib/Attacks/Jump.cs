using Heroes;

namespace Attacks
{
    public class Jump: IJumps
    {
        public Jump()
        {
        }
        public virtual int PowerModifier { get;  } 
        public int Power { get => (1 + PowerModifier);  } 

        public virtual Attacks Identifier { get; } = Attacks.BaseJump;

        public bool IsGroundOnly()
        {
            return false ;
        }

        public bool IsJump()
        {
           return true;
        }
    }
}
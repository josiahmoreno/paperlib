using Heroes;
using System.Collections.Generic;

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

        public bool CanHitFlying()
        {
           return true;
        }

        public class JumpEq : EqualityComparer<IJumps>
        {
            public override bool Equals(IJumps b1, IJumps b2)
            {
                if (object.ReferenceEquals(b1, b2))
                    return true;

                if (b1 is null || b2 is null)
                    return false;

                return b1.PowerModifier == b2.PowerModifier &&
                    b1.Power == b2.Power &&
                    b1.Identifier == b2.Identifier &&
                    b1.IsGroundOnly() == b2.IsGroundOnly() && 
                    b1.CanHitFlying() == b2.CanHitFlying();
            }

            public override int GetHashCode(IJumps box) =>box.GetHashCode();
        }

    }
}
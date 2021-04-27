using System;
using System.Collections.Generic;
using System.Text;

namespace Attacks
{
    class Headbonk : IAttack
    {
        public int Power => 1;

        public Attacks Identifier => Attacks.Headbonk;

        public bool IsGroundOnly()
        {
            return false;
        }

        public bool IsJump()
        {
            return true;
        }
    }
}

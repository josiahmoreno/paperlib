using System;
using Attacks;
using Tests;

namespace Attributes
{
    public class Flying : IAttribute
    {
        public Attributes attribute
        {
            get => Attributes.Flying;
        }

        public Flying()
        {
        }

        public bool CanAttack(IProtection protection, IAttack attack)
        {
            return !attack.IsGroundOnly();
        }

        public bool Matches(Attributes spiked)
        {
            return attribute == spiked; ;
        }
    }
}
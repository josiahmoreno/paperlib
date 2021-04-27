using Attacks;
using Attributes;
using NUnitTestProject1.Attributes.Protections;
using Tests;

namespace Attributes
{
    public class Spike : IAttribute
    {

        public Attributes attribute
        {
            get => Attributes.Spiked;
        }

        public bool CanAttack(IProtection protection, IAttack attack)
        {
            return !attack.IsJump() || ( protection?.Value == (Protections.SpikeShield));
        }

        public bool Matches(Attributes spiked)
        {
            return attribute == spiked; ;
        }
    }
}
using Attributes;
using NUnitTestProject1.Attributes.Protections;

namespace Tests
{
    public class SpikeShield: IProtection
    {
        
        public SpikeShield()
        {
        }

     

        public Protections Value { get => Protections.SpikeShield; }
    }
}
using Attributes;
using NUnitTestProject1.Attributes.Protections;

namespace Tests
{
    public interface IProtection
    {
        Protections Value { get; }
    }
}
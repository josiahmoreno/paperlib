using Attacks;
using System;
using Tests;

namespace Attributes
{
    public interface IAttribute : IEquatable<IAttribute>
    {
        Attributes attribute { get; }
        bool CanAttack(IProtection protection , IAttack attack);
        bool Matches(Attributes spiked);
    }
}
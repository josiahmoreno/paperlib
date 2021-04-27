using Attacks;
using Tests;

namespace Attributes
{
    public interface IAttribute
    {
        Attributes attribute { get; }
        bool CanAttack(IProtection protection , IAttack attack);
        bool Matches(Attributes spiked);
    }
}
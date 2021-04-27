using Attacks;
using Battle;

namespace MenuData
{
    public interface IActionMenuStore
    {
        string FetchName(Attacks.Attacks identifier);
        string FetchName(IOption active);
    }
}
using Attacks;
using Battle;

namespace Items
{
    public interface IItem
    {
        string Name { get; }
        int? Damage { get; }
        MenuData.TargetType TargetType { get; }
    }
}
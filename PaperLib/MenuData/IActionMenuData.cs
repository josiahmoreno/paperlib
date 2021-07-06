using System;

namespace MenuData
{
    public interface IActionMenuData : IEquatable<IActionMenuData>
    {
        string Name { get; }
        IOption[] Options { get; }
    }
}
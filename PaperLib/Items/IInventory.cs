using System;

namespace Items
{
    public interface IInventory: IEquatable<IInventory>
    {
        IItem[] Items { get; }
    }
}
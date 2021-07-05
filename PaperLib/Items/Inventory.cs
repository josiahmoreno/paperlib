
using System.Collections.Generic;

namespace Items
{
    public class Inventory : IInventory
    {
        public IItem[] Items { get; }
        public Inventory(params IItem[] items)
        {
            this.Items = items;
        }

        

        public bool Equals(IInventory other)
        {
            return other != null && EqualityComparer<IItem[]>.Default.Equals(Items, other.Items);
        }

      
    }
}
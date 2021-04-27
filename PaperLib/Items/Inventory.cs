
namespace Items
{
    public class Inventory : IInventory
    {
        public Inventory(params IItem[] items)
        {
            this.Items = items;
        }

        public IItem[] Items { get; }
    }
}

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
            throw new System.NotImplementedException();
        }


    }
}
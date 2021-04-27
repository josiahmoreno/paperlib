using Battle;
using Items;
using System.Linq;

namespace MenuData
{
    internal class ItemsMenuData : ActionMenuData
    {
       

        public ItemsMenuData(IInventory iventorySystem): base("Items")
        {
            Options = iventorySystem.Items.ToList().Select((item) =>
            {
                return new ItemOption(item);
            }).ToArray();
        }
    }
}
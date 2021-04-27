using Attacks;
using Battle;
using System.Linq;

namespace MenuData
{
    internal class HammerMenuData : ActionMenuData
    {
       

        public HammerMenuData(IActionMenuStore store,IAttack[] IAttack) : base("Hammer")
        {
            Options = IAttack.ToList().Select((item) =>
            {
                return new MenuData.HammerOption(store,item);
            }).ToArray();
        }
    }
}
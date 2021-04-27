using Attacks;
using System;
using System.Linq;

namespace MenuData
{
    internal class JumpMenuData : ActionMenuData
    {


        public JumpMenuData(IActionMenuStore store,IAttack[] IAttack) : base("Jump")
        {
            Options = IAttack.ToList().Select((item) =>
            {
                return new MenuData.JumpOption(store,item.GetType().Name,item,TargetType.Single);
            }).ToArray();
        }

        
    }
}
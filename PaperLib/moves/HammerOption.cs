using System.Collections.Generic;
using Attacks;
using Battle;

namespace MenuData
{
    internal class HammerOption : AttackOption
    {

        public HammerOption(IActionMenuStore store) : this(store, new Hammer() )
        {
        }
        
        
        public HammerOption(IActionMenuStore store, IAttack hammer) : base(store,"Hammer", hammer, TargetType.Single)
        {
            
        }
    }
}
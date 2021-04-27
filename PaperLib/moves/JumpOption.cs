using Attacks;
using Battle;

namespace MenuData
{
    internal class JumpOption : AttackOption
    {
        public JumpOption(IActionMenuStore store,string name, IAttack item, TargetType targetType) : base(store,name, item, targetType)
        {
        }
    }
}
using Attacks;
using Enemies;
using Heroes;
using System;

namespace PaperLib.Sequence
{
    public interface IDamageTarget
    {
        IAttack Attack { get; }

          IEntity hero { get; }
         IEntity target { get; }
        Func<bool> successfulQuicktime { get; }
    }
}
using System;
using Enemies;
using System.Collections.Generic;
using MenuData;
using Attacks;

namespace Moves
{
    internal class HeadbonkOption: AttackOption
    {

        public HeadbonkOption() : base(new DefaultActionMenuStore(),"Headbonk", new Headbonk(), TargetType.Single)
        {
        }

        public HeadbonkOption(IActionMenuStore store,IAttack hammer) : base(store,"Headbonk", hammer, TargetType.Single)
        {
        }
    

    //public Headbonk() : base(Moves.Att.Headbonk, MenuData.TargetType.Single)
    //{
    //}

    //public override void Execute(Battle.Battle battle, object activeHero, Enemy[] targets, Action<IEnumerable<Tuple<Enemy, bool>>> p)
    //{

    //    p.Invoke(new List<Tuple<Enemy, bool>>() { new Tuple<Enemy, bool>(targets[0], targets[0].TakeDamage(1)) });
    //}
    }
}
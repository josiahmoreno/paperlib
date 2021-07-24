﻿using System;
using Attacks;
using Battle;
using Heroes;
using Enemies;
using System.Linq;
using System.Collections.Generic;

namespace MenuData
{
    internal class AttackOption: BaseOption
    {
        public IAttack Attack { get; set; }
        private IActionMenuStore store;
        public AttackOption(string name,IAttack item, TargetType targetType): this(new DefaultActionMenuStore(),name,item,targetType)
        {
           
        }
        public AttackOption(IActionMenuStore store,string name,IAttack item, TargetType targetType)
        {
            // this.Name = name;
            this.store = store;
            this.Attack = item;
            this.TargetType = targetType;
        }

        //public string Name { get; private set; }

        public override Guid? Guid { get; }
        public override string Name { get => store.FetchName(this); }
        public TargetType TargetType { get; private set; }
        public HashSet<Attributes.Attributes> PossibleEnemyTypes { get; set; } = null;

        public override void Execute(Battle.Battle battle, object activeHero, Enemy[] targets, Action<IEnumerable<Tuple<Enemy,bool>>> p)
        {
            
            var mario = activeHero as Hero;
            var battleAnimationSequnce = battle.WaitForBattleAnimationSequence();
            bool succ = battleAnimationSequnce.Sucessful;
            var results = targets.Select(target => {

                bool attWasSuc = mario.Attacks(Attack, target, succ);
                return new Tuple<Enemy,bool>(target, attWasSuc);
                }).ToList();
            p.Invoke(results);
        }

        public bool Equals(IOption other)
        {
            return base.Equals(other) && other is AttackOption attackOption && attackOption.Attack == Attack;
        }
    }
}
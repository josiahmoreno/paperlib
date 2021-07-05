﻿
using Battle;
using Enemies;
using System.Collections.Generic;
using Attacks;
using System;

namespace Heroes
{
    public interface Hero: IEquatable<Hero>
    {
        MenuData.IActionMenuData[] Actions { get; }
        bool IsUnique { get;  }
        Heroes? Identity { get; set; }

        Attributes.IHealth Health { get; }
        //List<IJumps> Jumps { get; }
        //Hero[] Partners { get; }
        //void JumpOn(Enemy enemy,  IJumps jumps = null);
        //void Hammers(Enemy enemy);

        void TakeDamage(IEnemyAttack enemyAttack, bool successfulActionCommand = false);
        bool Attacks(IAttack attack, Enemy target, bool succ);
    }

    public interface IMario: Hero
    {
        List<IJumps> Jumps { get; }
        Hero[] Partners { get; }

        void JumpOn(Enemy enemy, IJumps jumps = null);
        void Hammers(Enemy enemy);
    }
}
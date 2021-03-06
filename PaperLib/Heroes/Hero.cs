
using Battle;
using Enemies;
using System.Collections.Generic;
using Attacks;
using System;
using Items;
using PaperLib.Sequence;

namespace Heroes
{
    public interface Hero: IEntity
    {
        MenuData.IActionMenuData[] Actions { get; }
        bool IsUnique { get;  }
        Heroes? Identity { get; set; }

        Attributes.IHealth Health { get; }
       

        //List<IJumps> Jumps { get; }
        //Hero[] Partners { get; }
        //void JumpOn(Enemy enemy,  IJumps jumps = null);
        //void Hammers(Enemy enemy);

     
    }

    public interface IMario: Hero
    {
        List<IJumps> Jumps { get; }
        Hero[] Partners { get; }

        void JumpOn(Enemy enemy, IJumps jumps = null);
        void Hammers(Enemy enemy);
        
        IInventory iventory { get; set; }
    }
}
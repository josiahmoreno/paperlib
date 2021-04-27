using System;
using Attacks;
using MenuData;
using Moves;
using System.Collections.Generic;

namespace MenuData
{
    internal class DefaultActionMenuStore : IActionMenuStore
    {
        Dictionary<Attacks.Attacks, string> dictionary = new Dictionary<Attacks.Attacks, string>()
        {
            { Attacks.Attacks.PowerJump, "Power Jump" },
            { Attacks.Attacks.BaseJump, "Jump" },
            { Attacks.Attacks.Headbonk, "Headbonk" },
            { Attacks.Attacks.BaseHammer, "Hammer" },
             { Attacks.Attacks.HammerThrow, "Hammer Throw" }
        };

        Dictionary<Moves.MovesList, string> movesDictionary = new Dictionary<Moves.MovesList, string>()
        {
            { Moves.MovesList.Tattle, "Tattle" },
            { Moves.MovesList.Headbonk, "Headbonk" }


        };
        
        Dictionary<Guid, string> store = new Dictionary<Guid, string>()
        {
            {Guid.Parse("8da95dad-6310-4680-bd30-01dffde99f00"), "Change Member" },
            { Guid.Parse("0b5a66b7-0bed-4aaa-8211-50d868b19ed3"), "Do Nothing" },
            { Guid.Parse("2744296b-ed8e-4502-b215-20cf57c9d962"), "Run Away" },
            {Guid.Parse("c22a8302-5573-4743-aa32-ae5c4237862f"),"Power Shell"}


        };
        public string FetchName(Attacks.Attacks identifier)
        {
            return dictionary[identifier];
        }

        public string FetchName(Moves.MovesList identifier)
        {
            return movesDictionary[identifier];
        }

        public string FetchName(IOption active)
        {
           if(active is AttackOption attackOption)
            {
                return FetchName(attackOption.Attack.Identifier);
            }
           if(active is MoveOption moveOption)
            {
                return FetchName(moveOption.Move);
            }
            if (active is ItemOption itemOption)
            {
                return itemOption.Name;
            }

            if (active.Guid == null || !active.Guid.HasValue || !store.ContainsKey(active.Guid.Value))
            {
                return null;
            }
            return store[active.Guid.Value];
        }
    }
}
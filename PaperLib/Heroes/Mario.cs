using Enemies;
using Attacks;
using Tests;
using System.Collections.Generic;
using Attributes;
using Battle;
using System;
using System.Linq;
using Items;
using MenuData;
using PaperLib.Sequence;

namespace Heroes
{
    public class Mario : IMario
    {
        private IProtection protection;

        public ISequenceable Sequenceable { get; set; }
        public Mario(Hero[] partners, List<IJumps> jumps):this( partners,new Inventory(), jumps.ToArray(),new Hammer())
        {
            
        }
        public Mario(List<IJumps> jumps) : this( new Hero[0],new Inventory(), jumps.ToArray(),new Hammer())
        {
            this.Jumps = jumps;
        }

        public IInventory iventory { get; set; }

        public Mario() : this( new Hero[0], new Inventory(), new IAttack[] { new Jump()},new Hammer())
        {

        }
        
        public Mario( Inventory iventory, IAttack[] jumps, params IAttack[] hammers): this(new Hero[0],iventory,jumps,hammers)
        {
        }
        public Mario(Hero[] partners, Inventory iventory, IAttack[] jumps, params IAttack[] hammers)
        {
            this.iventory = iventory;
            Actions = new MenuData.IActionMenuData[4];
            Actions[0] = new MenuData.ActionMenuData("Strategies",new ChangeMemberOption(partners), new DoNothingOption(), new RunAwayOption());
            Actions[1] = new MenuData.ItemsMenuData(iventory);
            Actions[2] = new MenuData.JumpMenuData(new DefaultActionMenuStore(),jumps);
            if(jumps.Length > 0)
            {

            this.Jumps = jumps.ToList().Cast<IJumps>().ToList();
            }
            Actions[3] = new MenuData.HammerMenuData(new DefaultActionMenuStore(),hammers);
        }

        public bool Attack(IAttack attack, IEntity target, bool ActionCommandSuccessful)
        {
            return target.TakeDamage(attack,this.protection,  ActionCommandSuccessful);
        }

        public Mario(IProtection protection) : this( new Hero[0],new Inventory(), new IAttack[0])
        {
            this.protection = protection;
        }

        public bool IsUnique { get => Identity != null; }
        public virtual Heroes? Identity { get; set; } = Heroes.Mario;

        //jumps should be selected from the list of jumps which is from the list of actions
        public List<IJumps> Jumps { get; internal set; } = new List<IJumps>();
        public Hero[] Partners { get; }

        //new flag, new items, new jumps, new hammers

        public MenuData.IActionMenuData[] Actions { get; set; }

        public IHealth Health { get; private set; } = new HealthImpl(10);

        public void Hammers(Enemy enemy)
        {

            enemy.TakeDamage( new Hammer(), protection, false);
        }

        public void JumpOn(Enemy enemy, IJumps jumps = null)
        {
            if (jumps == null)
            {
                jumps = new Jump();

            }

            //  bool TakeDamage(IAttack enemyAttack, IProtection protection,bool successfulActionCommand = false);
            enemy.TakeDamage(jumps, protection, false);

            // throw new System.NotImplementedException();
        }

        public override string ToString()
        {
            return $"{GetType().Name} : hp = {Health.CurrentValue}, actions {Actions.Length}";
        }

        public bool TakeDamage(IAttack enemyAttack, IProtection protection, bool successfulActionCommand)
        {
            if (successfulActionCommand)
            {
                Health.TakeDamage(enemyAttack.Power -1 );
            }
            else
            {
                Health.TakeDamage(enemyAttack.Power);

            } 
            return true;
        }

       

        public static MarioEqualityComparer Comparer = new MarioEqualityComparer();

       // public override bool Equals(object obj)
       //  {
       //      if( obj is Mario mario)
       //      {
       //          if(!EqualityComparer<IProtection>.Default.Equals(protection, mario.protection))
       //          {
       //              return false;
       //          }
       //          if(!EqualityComparer<IInventory>.Default.Equals(iventory, mario.iventory))
       //          {
       //              return false;
       //          }
       //          if(IsUnique != mario.IsUnique)
       //          {
       //
       //          }
       //          if(Identity != mario.Identity)
       //          {
       //
       //          }
       //          if(!Enumerable.SequenceEqual(Jumps, mario.Jumps))
       //          {
       //              return false;
       //          }
       //          if(!Enumerable.SequenceEqual(Actions, mario.Actions))
       //          {
       //              return false;
       //          }
       //          if(!EqualityComparer<IHealth>.Default.Equals(Health, mario.Health))
       //          {
       //              return false;
       //          }
       //          return true;
       //      } else
       //      {
       //          return false;
       //      }
       //             
       //  }

     

 
    }

    public class MarioEqualityComparer : IEqualityComparer<IMario>
    {
        public bool Equals(IMario x, IMario y)
        {
            
             
                    // if(!EqualityComparer<IProtection>.Default.Equals(x.protection, y.protection))
                    // {
                    //     return false;
                    // }
                    if(!EqualityComparer<IInventory>.Default.Equals(x.iventory, y.iventory))
                    {
                        return false;
                    }
                    if(x.IsUnique != y.IsUnique)
                    {
       
                    }
                    if(x.Identity != y.Identity)
                    {
       
                    }
                    if(!Enumerable.SequenceEqual(x.Jumps, y.Jumps))
                    {
                        return false;
                    }
                    if(!Enumerable.SequenceEqual(x.Actions, y.Actions))
                    {
                        return false;
                    }
                    if(!EqualityComparer<IHealth>.Default.Equals(x.Health, y.Health))
                    {
                        return false;
                    }
                    return true;
                
        }

        public int GetHashCode(IMario obj)
        {
            return obj.GetHashCode();
        }
    }
}
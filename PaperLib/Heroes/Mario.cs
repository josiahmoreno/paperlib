using Enemies;
using Attacks;
using Tests;
using System.Collections.Generic;
using Attributes;
using Battle;
using System;
using Items;
using MenuData;

namespace Heroes
{
    public class Mario : Hero
    {
        private IProtection protection;


        public Mario(Hero[] partners, List<IJumps> jumps):this( partners,new Inventory(), jumps.ToArray(),new Hammer())
        {
            
        }
        public Mario(List<IJumps> jumps) : this( new Hero[0],new Inventory(), jumps.ToArray(),new Hammer())
        {
            this.Jumps = jumps;
        }

        private IInventory iventory;

        public Mario() : this( new Hero[0], new Inventory(), new IAttack[0],new Hammer())
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
            Actions[3] = new MenuData.HammerMenuData(new DefaultActionMenuStore(),hammers);
        }

        public bool Attacks(IAttack attack, Enemy target, bool ActionCommandSuccessful)
        {
            return target.TakeDamage(this.protection, attack, ActionCommandSuccessful);
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
            enemy.TakeDamage(protection, new Hammer(), false);
        }

        public void JumpOn(Enemy enemy, IJumps jumps = null)
        {
            if (jumps == null)
            {
                jumps = new Jump();

            }

            enemy.TakeDamage(protection, jumps, false);

            // throw new System.NotImplementedException();
        }

        public override string ToString()
        {
            return $"{GetType().Name} : hp = {Health.CurrentValue}, actions {Actions.Length}";
        }

        public void TakeDamage(IEnemyAttack enemyAttack, bool successfulActionCommand)
        {
            if (successfulActionCommand)
            {
                Health.TakeDamage(enemyAttack.Damage -1 );
            }
            else
            {
                Health.TakeDamage(enemyAttack.Damage);

            }
        }
    }
}
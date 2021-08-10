﻿using Attacks;
using Attributes;
using Battle;
using Heroes;
using System;
using System.Collections.Generic;
using System.Linq;
using Tests;

namespace Enemies
{
    public abstract class NewBaseEnemy : Enemy
    {
        public IAttribute[] Attrs { get; internal set; }


        public virtual List<IEnemyAttack> Moves { get; } = new System.Collections.Generic.List<Battle.IEnemyAttack>();
        public NewBaseEnemy(ITattleStore tattleStore)
        {
            this.Health = new HealthImpl(1);
            this._tattleStore = tattleStore;
        }

        private ITattleStore _tattleStore;
        public NewBaseEnemy(IHealth health,ITattleStore tattleStore)
        {
            this.Health = health;
            this._tattleStore = tattleStore;
        }
        public NewBaseEnemy(ITattleStore tattleStore,params IAttribute[] flying)
        {
            this.Health = new HealthImpl(1);
            this.Attrs = flying;
            this._tattleStore = tattleStore;
        }

        event EventHandler Enemy.OnKilled
        {
            add
            {
                Health.OnZero += value;
            }

            remove
            {
                Health.OnZero -= value;
            }
        }

        public bool IsFlying => Attrs != null && Array.Exists(Attrs, attr => attr.Matches(global::Attributes.Attributes.Flying));

        public virtual IHealth Health { get; set; } 

        public bool IsSpiked => Attrs != null && Array.Exists(Attrs, attr => attr.Matches(global::Attributes.Attributes.Spiked));

      




        public bool TakeDamage(IProtection protection,IAttack attack, bool ActionCommandSuccessful)
        {
            //can be attacked by the 'hammer' (attack)
            bool successful = false;
            if (Attrs == null)
            {
                if (ActionCommandSuccessful)
                {
                    successful = true;
                    this.Health.TakeDamage(attack.Power +1 );
                } else
                {
                    successful = true;
                    this.Health.TakeDamage(attack.Power);
                }
              
            }
            
            else if(Array.TrueForAll(Attrs, attr => attr.CanAttack(protection, attack)))
            {
                if (ActionCommandSuccessful)
                {
                    successful = true;
                    this.Health.TakeDamage(attack.Power + 1);
                }
                else
                {
                    successful = true;
                    this.Health.TakeDamage(attack.Power);
                }
            }
            return successful;

        }

        public bool IsAlive()
        {
           return Health.CurrentValue != 0;
        }

        public bool IsDead
        {
            get
            {
                return Health.CurrentValue == 0;
            }
        }

        public virtual EnemyType EnemyType => EnemyType.Mob;

        public virtual List<IEnemyAttack> Sequence { get;  } = null;
        
        
        public abstract string Identifier { get; set; }

        //private string _identifier;
        //public string Identifier { get => _identifier; set => _identifier = value; }


        public override string ToString()
        {
            return $"{this.GetType().Name}, hp =  {Health.CurrentValue.ToString()}";
        }

        public void Kill()
        {
            this.Health.TakeDamage(Health.CurrentValue);
        }

        public virtual IEnemyAttack GetRandomMove()
        {
            if (Sequence?.Count > 0)
            {
                var attack = Sequence.First();
                Sequence.RemoveAt(0);
                return attack;
            }
            else
            {
                if(Moves.Count == 0)
                {
                    throw new Exception("Enemy has no moves!");
                }
                var random = new System.Random();
                var index = random.Next(Moves.Count);
                return Moves[index];

            }
        }

        public bool TakeDamage(int damage)
        {
            Console.WriteLine($"{GetType().Name} takes {damage} damage");
            Health.TakeDamage(damage);
            return true;
        }

        //Should i remove flying everytime?
        public object PostDamagePhase(bool item2)
        {
            if (item2 && Attrs!= null)
            {
                var list = Attrs.ToList();
                list.RemoveAll((attr) => attr.Matches(Attributes.Attributes.Flying));
                Attrs = list.ToArray();
                //Attrs.ToList().Where((attr)=> attr.Matches(Attributes.Attributes.Flying));
            }
            return null;
        }

        public GameText FetchTattleData()
        {
            return _tattleStore.FetchGameText(this);
        }





        public static BaseEnemyEqualityComparer Comparer = new BaseEnemyEqualityComparer();

        //public override bool Equals(object obj)
        //{
        //    return Equals(obj as NewBaseEnemy);
        //}

        //public bool Equals(Enemy ot)
        //{

        //    if(ot != null && ot is NewBaseEnemy other)
        //    {
        //        if (!EqualityComparer<IAttribute[]>.Default.Equals(Attrs, other.Attrs))
        //        {
        //            return false;
        //        }
        //        if(!Enumerable.SequenceEqual(Moves, other.Moves))
        //        {
        //            return false;
        //        }
        //        //if(!EqualityComparer<ITattleStore>.Default.Equals(_tattleStore, other._tattleStore))
        //       // {
        //         //   return false;
        //       // }
        //        if(IsFlying != other.IsFlying)
        //        {
        //            return false;
        //        }
        //        if(!EqualityComparer<IHealth>.Default.Equals(Health, other.Health))
        //        {
        //            return false;
        //        }
        //        if(IsSpiked != other.IsSpiked)
        //        {
        //            return false;
        //        }
        //        if(IsDead != other.IsDead)
        //        {
        //            return false;
        //        }
        //        if(Sequence != null && !Enumerable.SequenceEqual(Sequence, other.Sequence))
        //        {
        //            return false;
        //        }
        //        if(Identifier != other.Identifier)
        //        {
        //            return false;
        //        }


        //            return true;
        //    }
        //    return false;

        //}
    }
}
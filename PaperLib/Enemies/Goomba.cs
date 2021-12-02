using Attacks;
using Attributes;
using Battle;
using Heroes;
using PaperLib.Sequence;
using System;
using System.Collections.Generic;
using System.Linq;
using Tests;

namespace Enemies
{
    public class Goomba : Enemy
    {
        public IAttribute[] Attrs { get; internal set; }


        public List<IEnemyAttack> Moves { get; } = new System.Collections.Generic.List<Battle.IEnemyAttack>();
        public Goomba()
        {
            this.Health = new HealthImpl(1);
        }

        public Goomba(IHealth health)
        {
            this.Health = health;
        }
        public Goomba(params IAttribute[] flying)
        {
            this.Health = new HealthImpl(1);
            this.Attrs = flying;
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

      


        

        public bool TakeDamage(IAttack attack, IProtection protection, bool ActionCommandSuccessful)
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
            Console.WriteLine($"Ouch! {this.GetType().Name} is now at{this.Health.CurrentValue}");
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

        private string _identifier;
        public string Identifier { get => _identifier; set => _identifier = value; }

        public IPositionable MovementTarget { get; set; }

        public ISequenceable Sequenceable { get; set; }

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
            return null;
            //throw new NotImplementedException();
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as Goomba);
        }

        public bool Equals(Enemy othe)
        {
            return othe != null && othe is Goomba other && 
                   EqualityComparer<IAttribute[]>.Default.Equals(Attrs, other.Attrs) &&
                   EqualityComparer<List<IEnemyAttack>>.Default.Equals(Moves, other.Moves) &&
                   IsFlying == other.IsFlying &&
                   EqualityComparer<IHealth>.Default.Equals(Health, other.Health) &&
                   IsSpiked == other.IsSpiked &&
                   IsDead == other.IsDead &&
                   EnemyType == other.EnemyType &&
                   EqualityComparer<List<IEnemyAttack>>.Default.Equals(Sequence, other.Sequence) &&
                   _identifier == other._identifier &&
                   Identifier == other.Identifier;
        }

        public bool Attack(IAttack attack, IEntity target, bool succ)
        {
            throw new NotImplementedException();
        }
    }
}
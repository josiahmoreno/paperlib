using Attacks;
using Attributes;
using Battle;
using Enemies;
using MenuData;
using System.Collections.Generic;
using Tests;

namespace Heroes
{
    public  class BaseHero : Hero
    {
        private IProtection protection;
        public IActionMenuData[] Actions { get; set; }
        public bool IsUnique { get => Identity != null; }
        public virtual Heroes? Identity { get; set; } = Heroes.Mario;
        public IHealth Health { get; private set; } = new HealthImpl(10);

        public bool Attacks(IAttack attack, Enemy target, bool ActionCommandSuccessful)
        {
            return target.TakeDamage(this.protection, attack, ActionCommandSuccessful);
        }


        public bool Equals(Hero y)
        {
            return Equals(y as object);
        }

        public override bool Equals(object obj)
        {
            if (obj is BaseHero baseHero)
            {
                if (!EqualityComparer<IProtection>.Default.Equals(protection, baseHero.protection))
                {
                    return false;
                }

                if (IsUnique != baseHero.IsUnique)
                {
                    return false;
                }
                if (Identity != baseHero.Identity)
                {
                    return false;
                }

                if (!EqualityComparer<IHealth>.Default.Equals(Health, baseHero.Health))
                {
                    return false;
                }

                return true;
            }

            return false;
        }

        public int GetHashCode(Hero obj)
        {
            return GetHashCode();
        }

        public override int GetHashCode()
        {
            int hashCode = -390069450;
            hashCode = hashCode * -1521134295 + EqualityComparer<IProtection>.Default.GetHashCode(protection);
            hashCode = hashCode * -1521134295 + EqualityComparer<IActionMenuData[]>.Default.GetHashCode(Actions);
            hashCode = hashCode * -1521134295 + IsUnique.GetHashCode();
            hashCode = hashCode * -1521134295 + Identity.GetHashCode();
            hashCode = hashCode * -1521134295 + EqualityComparer<IHealth>.Default.GetHashCode(Health);
            return hashCode;
        }

        public void TakeDamage(IEnemyAttack enemyAttack, bool successfulActionCommand)
        {
            if (successfulActionCommand)
            {
                Health.TakeDamage(enemyAttack.Damage - 1);
            }
            else
            {
                Health.TakeDamage(enemyAttack.Damage);

            }
        }
    }
}
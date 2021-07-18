using Attributes;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Enemies
{
    public class BaseEnemyEqualityComparer: IEqualityComparer<Enemy>
    {
        public bool Equals(Enemy enemy, Enemy ot)
        {

            if (ot != null && ot is Enemy other)
            {
                if (!EqualityComparer<IAttribute[]>.Default.Equals(enemy.Attrs, other.Attrs))
                {
                    return false;
                }
                if (!Enumerable.SequenceEqual(enemy.Moves, other.Moves))
                {
                    return false;
                }
                //if(!EqualityComparer<ITattleStore>.Default.Equals(_tattleStore, other._tattleStore))
                // {
                //   return false;
                // }
                if (enemy.IsFlying != other.IsFlying)
                {
                    return false;
                }
                if (!EqualityComparer<IHealth>.Default.Equals(enemy.Health, other.Health))
                {
                    return false;
                }
                if (enemy.IsSpiked != other.IsSpiked)
                {
                    return false;
                }
                if (enemy.IsDead != other.IsDead)
                {
                    return false;
                }
                if (enemy.Sequence != null && !Enumerable.SequenceEqual(enemy.Sequence, other.Sequence))
                {
                    return false;
                }
                if (enemy.Identifier != other.Identifier)
                {
                    return false;
                }


                return true;
            }
            return false;

        }

        public int GetHashCode(Enemy obj)
        {
           return obj.GetHashCode();
        }
    }
}
using System;
using System.Linq;
using Attacks;
using Attributes;
using Battle;
using Enemies;
using Tests;

namespace Enemies
{
    public class GoomnutTree: EnvironmentTarget
    {
        public GoomnutTree() : base(new HealthImpl(1))
        {
        }



        public override EnemyType EnemyType => EnemyType.Enviroment;

        public override void ExecuteEffect(Battle.Battle battle)
        {
            var treeLocation = battle.Enemies.FindIndex(hero => hero is GoomnutTree) +1;
            for (int i = treeLocation; i < battle.Enemies.Count; i++)
            {
                battle.Enemies[i].Health.TakeDamage(3);
            }
            //battle.Enemies.Where((hero) => hero is GoombaKing).Health.TakeDamage(3);
        }
    }
}
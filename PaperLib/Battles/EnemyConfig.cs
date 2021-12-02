using System;
using System.Collections.Generic;
using System.Linq;
using Enemies;

namespace Tests
{
    public class EnemyConfig
    {
        private Enemy[] enemies;

        public EnemyConfig(params Enemy[] enemies)
        {
            this.enemies = enemies;
        }

        internal List<Enemy> ToEnemies()
        {
            return enemies.ToList();
        }
    }
}
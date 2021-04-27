using System;
using System.Collections.Generic;
using System.Linq;
using Enemies;

namespace Tests
{
    public class Encounter
    {
        private Enemy[] enemies;

        public Encounter(params Enemy[] enemies)
        {
            this.enemies = enemies;
        }

        internal List<Enemy> ToEnemies()
        {
            return enemies.ToList();
        }
    }
}
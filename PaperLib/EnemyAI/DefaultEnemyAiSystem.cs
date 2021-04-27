using System;
using Tests;

namespace Battle
{
    internal class DefaultEnemyAiSystem : IEnemyAISysytem
    {
        private ITurnSystem turnSystem;
        private Battle battle;
        public DefaultEnemyAiSystem(Battle battle,ITurnSystem turnSystem)
        {
            this.turnSystem = turnSystem;
            //turnSystem.OnActiveChanged += OnActiveChanged;
            this.battle = battle;
        }

        //goomba king
        //big jump
        //kick mario
        private int count = 0;
        public void ExecuteEnemyTurn(Battle battle, object obj)
        {
            Console.WriteLine($"1 - TurnChange -  {obj}");
            if (obj is Enemies.Enemy)
            {
                Console.WriteLine($"DefaultEnemyAiSystem - OnActiveChanged -  {obj}");
                var enemy = obj as Enemies.Enemy;
                var move = enemy.GetRandomMove();
                battle.EnemyAttack(move);
                //TODO End turn here causes an infite loop
                if (count < 10)
                {
                  //battle.EndTurn();
                  count++;
                }
               
                
            }
        }
    }
}
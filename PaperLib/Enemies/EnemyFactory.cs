using System;
using Battle;
using Enemies;

namespace PaperLib.Enemies
{
    public class EnemyFactory
    {
        private ITattleStore _tattleStore = new TattleStore();
        public NewGoomba Fetch()
        {
            return new NewGoomba(_tattleStore);
        }

        public T FetchEnemy<T>() where  T : NewGoomba
        {
            return (T) Activator.CreateInstance(typeof(T), _tattleStore);
        }
        
        public T FetchEnemy<T>(int currentHealth) where  T : NewGoomba
        {
            return (T) Activator.CreateInstance(typeof(T), currentHealth,_tattleStore);
        }
    }
}
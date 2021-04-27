using System;
using System.Collections.Generic;
using System.Linq;
using Enemies;
using Heroes;
using Tests;

namespace Battle
{
    internal class DefaultTurnSystem : ITurnSystem
    {
        private object _active;

        public List<Hero> Heroes { get; private set; }

        private List<Enemy> Enemies;
        private HashSet<object> Completed = new HashSet<object>();
        public List<string> History { get; } = new List<string>();
        public object Active
        {
            get { return _active; }
            internal set
            {
               
                _active = value;
                History.Add(_active.GetType().Name);
                Console.WriteLine($"{GetType().Name} - Active change {value}");
                var old = _active;
                OnActiveChanged(_active);
                //Console.WriteLine($"Turns b - {old} end");
            }
        }
        public Action<object> OnActiveChanged { get; set; }

        public string LastActive
        {
            get
            {
                if (History.Count > 1)
                {
                    return History[History.Count - 2];

                }
                return null;
            }
        }

        private HashSet<string> currentTurn = new HashSet<string>();
        public void End()
        {

            Completed.Add(Active);
            var oldActive1 = Active;
            Console.WriteLine($"Turns - {oldActive1} ends turn - Start");
            bool containsEveryone = Enemies.All(enemy => enemy.EnemyType == EnemyType.Enviroment || Completed.Contains(enemy) || enemy.IsDead);
            bool containsHeroes = Heroes.Where(hero => hero is Hero && hero.Actions.Count() > 0)
                .All((hero) => Completed.Contains(hero));
            if (Heroes.Where(hero => hero is Hero && hero.Actions.Count() > 0).All((hero) => Completed.Contains(hero)) && !containsEveryone)
            {
               

                Active = Enemies.First(enemy => enemy.EnemyType != EnemyType.Enviroment && !Completed.Contains(enemy) && enemy.IsAlive());
            }
            else
            {
            
                var oldActive = Active;
                foreach (Hero hero in Heroes)
                {
                    Console.WriteLine($"------  {hero}");
                }

                if (containsEveryone && containsHeroes)
                {
                    Completed.Clear();
                    Console.WriteLine($"-------- yoyo");
                    oldActive = null;
                }
                var nextTurn = Heroes.First((hero) => hero != oldActive && hero.Actions.Length > 0);
                Console.WriteLine($"-------- next Turn = {nextTurn}");
                Active = nextTurn;
            }
            Console.WriteLine($"--------- {oldActive1} ends turn, new active is now {Active} - END");

        }

        public void Load(List<Hero> heroes, List<Enemy> enemies)
        {
            this.Heroes = heroes;
            foreach (Hero hero in Heroes)
            {
                Console.WriteLine($"------  {hero}");
            }
            this.Enemies = enemies;
            Active = heroes.First();
        }

        public void Swap()
        {
            Console.WriteLine($"Turns - Swap {Heroes.Count()}");
            if (Active is Hero hero)
            {


                if (Heroes.Count < 2)
                {
                    throw new Exception("cant swap if there is nothing swapable");
                }
                int index = Heroes.IndexOf(hero);
                if (index == 0)
                {
                    Active = Heroes.Last();
                }
                else
                {
                    Active = Heroes[index - 1];
                }
            } else
            {
                throw new Exception("can't swap if there isnt a hero");
            }

        }

        public void ExcuteTurn()
        {
            //throw new NotImplementedException();
        }

        public void Cleanup()
        {
            throw new NotImplementedException();
        }
    }
}
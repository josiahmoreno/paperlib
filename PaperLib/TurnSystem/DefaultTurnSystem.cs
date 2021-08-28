using System;
using System.Collections.Generic;
using System.Linq;
using Enemies;
using Heroes;
using PaperLib;
using Tests;

namespace Battle
{
    public delegate void OnSwapped(object sender, EventHandler<object> args);
    public class DefaultTurnSystem : ITurnSystem
    {
        private ILogger _logger;

        public DefaultTurnSystem(ILogger logger)
        {
            _logger = logger;
        }

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
                Console.WriteLine($"{GetType().Name} --- setting Active {value}");
                var old = _active;
                OnActiveChanged?.Invoke(_active);
                //Console.WriteLine($"Turns b - {old} end");
            }
        }

        public event EventHandler OnSwapped;
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
            _logger?.Log($"{GetType().Name} - starting to end turn {oldActive1}");
            bool containsEveryone = Enemies.All(enemy => enemy.EnemyType == EnemyType.Enviroment || Completed.Contains(enemy) || enemy.IsDead);
            bool containsHeroes = Heroes.Where(hero => hero is Hero && hero.Actions.Count() > 0)
                .All((hero) => Completed.Contains(hero));
            if (Heroes.Where(hero => hero is Hero && hero.Actions.Count() > 0).All((hero) => Completed.Contains(hero)) && !containsEveryone)
            {

                //switching to enemy
                Active = Enemies.First(enemy => enemy.EnemyType != EnemyType.Enviroment && !Completed.Contains(enemy) && enemy.IsAlive());
            }
            else
            {
                //switching to heroes
                var oldActive = Active;

                _logger?.Log($"{GetType().Name} ------ switching to heroes {string.Join(",", Heroes.Select(h => h.ToString()).ToArray())}, oldActive = {oldActive}");

                if (containsEveryone && containsHeroes)
                {
                    Completed.Clear();
                    //_logger?.Log($"{GetType().Name} - yoyo");
                    //why did i write this because this will reset the turns to null if the battle is ended
                    oldActive = null;
                }
                _logger?.Log($"{GetType().Name} ------ old active {oldActive}");
                try
                {
                    var nextTurn = Heroes.First((hero) => hero != oldActive && hero.Actions.Length > 0);
                    _logger?.Log($"{GetType().Name} ------ next turn will be {nextTurn}, oldActive {oldActive}");
                    Active = nextTurn;
                    if ((oldActive is Hero) && (Active is Hero) && oldActive != nextTurn)
                    {
                        _logger?.Log($"{GetType().Name} --- activating swap = {nextTurn} from {oldActive}");
                        OnSwapped?.Invoke(this, EventArgs.Empty);
                    }
                }
                catch (InvalidOperationException e)
                {
                    throw new InvalidOperationException(
                        $"can't find next hero, old active = {oldActive},\n {string.Join(",", Heroes.Select(h => h.ToString()).ToArray())}");
                }
               
              
            }
            _logger?.Log($"{GetType().Name} ------  ending turn finished {oldActive1} , new active is now {Active} - END");

        }

        public void Load(List<Hero> heroes, List<Enemy> enemies)
        {
            _logger?.Log($"{GetType().Name} - loading...");
            if (heroes.Count() == 0 || enemies.Count == 0)
            {
                throw new Exception("Must have enemies AND heroes with turns");
            }
            this.Heroes = heroes;
            _logger?.Log($"{GetType().Name} - just loaded {string.Join(",",Heroes.Select(h=> h.ToString()).ToArray())}");
           
            this.Enemies = enemies;
            Active = heroes.First();
        }

        public void Swap()
        {
            _logger?.Log($"{GetType().Name} - - Swap {Heroes.Count()}");
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

                OnSwapped?.Invoke(this,new EventArgs());
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
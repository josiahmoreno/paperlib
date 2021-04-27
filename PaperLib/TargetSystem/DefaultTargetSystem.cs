using System;
using System.Collections.Generic;
using System.Linq;
using Enemies;
using MenuData;

namespace TargetSystem
{
    public class DefaultTargetSystem : ITargetSystem
    {
        private List<Enemy> enemies;
        public Action<Enemy[]> ActiveChanged { get; set; }
        public int SelectedIndex { get; private set; }
        public Action<bool> OnShowing { get; set; }
        public DefaultTargetSystem(List<Enemy> enemies)
        {
            this.enemies = enemies;
        }

        public Enemy[] Actives { get; private set; }

        private bool _showing;
        private IOption currentOption;

        public bool Showing { get => _showing; private set {
                _showing = value;
                OnShowing?.Invoke(_showing);
            }}

        public void Hide()
        {
            Showing = false ;
            Actives = null;
            currentOption = null;
        }

        public void MoveTargetLeft()
        {
            if (SelectedIndex - 1 >= 0)
            {
                SelectedIndex -= 1;
                Actives[0] = enemies[SelectedIndex];
                ActiveChanged?.Invoke(Actives);
            }
           
        }
        public void MoveTargetRight()
        {
            if (SelectedIndex + 1 < enemies.Count)
            {
                
                bool isGood(Enemy enemy, AttackOption attackOption)
                {
                    return enemy.EnemyType != EnemyType.Enviroment && (enemy.Attrs == null || (enemy.Attrs.Length ==0 || (enemy.Attrs.Length >0 &&
                        enemy.Attrs.All(attr =>
                        {
                            Console.WriteLine($"{this.GetType().Name} -              {attr}");
                            return attr.CanAttack(null, attackOption.Attack);
                        }))));
                }
                if (this.currentOption is AttackOption attackOpt)
                {
                    if (isGood(enemies[SelectedIndex + 1], attackOpt))
                    {
                        SelectedIndex += 1;
                        Actives[0] = enemies[SelectedIndex];
                        ActiveChanged?.Invoke(Actives);
                    }
                }
                else
                {
                    SelectedIndex += 1;
                    Actives[0] = enemies[SelectedIndex];
                    ActiveChanged?.Invoke(Actives);
                }
                
            }
            
        }

        public void Cleanup()
        {
            this.enemies = this.enemies.Where((enemy, i) =>
            {
                return !enemy.IsDead;

            }).ToList();
        }

        public void Show(IOption option)
        {
            this.currentOption = option;
            if (option.TargetType == TargetType.Single)
            {
                Actives = new Enemy[1];
                if (currentOption is AttackOption attackOption)
                {
                    Console.WriteLine($"{this.GetType().Name} - {attackOption.Name} {attackOption.Attack}");

                    try
                    {
                        Actives[0] = enemies.First(enemy =>
                        {
                            
                            Console.WriteLine($"{this.GetType().Name} - {enemy} {(enemy.Attrs == null ? "null" : enemy.Attrs.Length.ToString())}");
                            return enemy.EnemyType != EnemyType.Enviroment && (enemy.Attrs == null || (enemy.Attrs.Length ==0 || (enemy.Attrs.Length >0 &&
                                   enemy.Attrs.All(attr =>
                                   {
                                       Console.WriteLine($"{this.GetType().Name} -              {attr}");
                                       return attr.CanAttack(null, attackOption.Attack);
                                   }))));
                        });
                        

                    }
                    catch (Exception e)
                    {
                        Console.WriteLine($"{attackOption} {enemies[0]}- {e}");
                        throw;
                    }
                    
                }
                else
                {
                    Actives[0] = enemies.First(enemy =>
                        enemy.EnemyType != EnemyType.Enviroment );
                }

                SelectedIndex = enemies.IndexOf(Actives[0]);
                ActiveChanged?.Invoke(Actives);
            }
            else if (option.TargetType == TargetType.All)
            {
                Actives = enemies.Where(enemy => enemy.EnemyType != EnemyType.Enviroment).ToArray();
                ActiveChanged?.Invoke(Actives);
            }
            Showing = true;
        }
    }
 
}
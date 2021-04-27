using System;
using System.Collections.Generic;
using Heroes;
using Enemies;
using Tests;
using System.Linq;
using MenuData;
using TargetSystem;

namespace Battle
{
    public class Battle
    {
        public IBattleStateStore BattleStateStore = new BattleStateStore();
        public List<Hero> Heroes = new List<Hero>();
        public List<Enemy> Enemies = new List<Enemy>();
        public List<BattleEvent> events = new List<BattleEvent>();

        public IMarioHealthCounter HealthCounter;
        public ITurnSystem TurnSystem { get; internal set; } = new DefaultTurnSystem();
        public IActionMenu ActionMenu { get; internal set; }
        public IOptionsListMenu OptionsListMenu { get; internal set; } = new DefaultOptionsListMenu();

        public IOptionsListMenu SubOptionsListMenu { get; private set; } = new DefaultOptionsListMenu();
        public IActionCommandCenter ActionCommandCenter = new ActionCommandCenter();
        internal IBattleAnimationSequence WaitForBattleAnimationSequence()
        {
            return ActionCommandCenter.FetchSequence();
        }

        public ITargetSystem TargetSystem { get; internal set; }
        public ITextBubbleSystem TextBubbleSystem { get; internal set; }

        private IOption ActiveOption;

        public IEnemyAISysytem enemyAISysytem { get; internal set; }

        private IActionMenuStore actionMenuStore = new DefaultActionMenuStore();
        public Battle()
        {
            TurnSystem = new DefaultTurnSystem();
            enemyAISysytem = new DefaultEnemyAiSystem(this, TurnSystem);
            ActionMenu = new DefaultActionMenu(TurnSystem);
            //TargetSystem = new DefaultTargetSystem(Enemies);
            //HealthCounter = new MarioHealthCounter(Heroes);
            //actionMenuStore ;
        }
        public Battle(List<Hero> heroes, List<Enemy> enemies)
        {
            Heroes = heroes;
            Enemies = enemies;
            TurnSystem = new DefaultTurnSystem();
            this.HealthCounter = new MarioHealthCounter(heroes);
            ActionMenu = new DefaultActionMenu(TurnSystem);
            TargetSystem = new DefaultTargetSystem(Enemies);
            TurnSystem.Load(heroes, enemies);
        }

        public Battle(List<Hero> heroes, List<Enemy> enemies, ITextBubbleSystem bubbleSystem)
        {
            Heroes = heroes;
            Enemies = enemies;
            TurnSystem = new DefaultTurnSystem();
            enemyAISysytem = new DefaultEnemyAiSystem(this, TurnSystem);
            this.HealthCounter = new MarioHealthCounter(heroes);
            ActionMenu = new DefaultActionMenu(TurnSystem);
            TargetSystem = new DefaultTargetSystem(Enemies);
            TextBubbleSystem = bubbleSystem;
            TurnSystem.Load(heroes, enemies);
        }

        public Battle(List<Hero> heroes, List<Enemy> enemies, Encounter encounter) : this(heroes, encounter.ToEnemies())
        {

        }

        public Battle(List<Hero> heroes, Encounter encounter) : this(heroes, encounter.ToEnemies())
        {

        }

        public void Cancel()
        {
            if (TargetSystem.Showing)
            {
                TargetSystem.Hide();
                if (ActionMenu.ActiveAction.Options.Count() == 1)
                {
                    ActionMenu.Show();
                    HealthCounter.Show();
                }
                else
                {
                    OptionsListMenu.Show(ActionMenu.ActiveAction.Options);
                }
                
                /*
                 ActionMenu.Hide();
            OptionsListMenu.Show(ActionMenu.ActiveAction.Options);
            ActiveOption = OptionsListMenu.Active;
                 * 
                    ActiveOption = OptionsListMenu.Active;
            OptionsListMenu.Hide();
            TargetSystem.Show(ActiveOption);
                 
                 */
            } else if (OptionsListMenu.Showing)
            {
                OptionsListMenu.Hide();
                ActionMenu.Start();
                HealthCounter.Show();
                
            }
        }

        private void CheckLoaded()
        {
            if (this.HealthCounter == null)
            {
                this.HealthCounter = new MarioHealthCounter(Heroes);
            }

            if (TargetSystem == null)
            {
                TargetSystem = new DefaultTargetSystem(Enemies);
            }
            TurnSystem.Load(Heroes, Enemies);
        }
        public void Start()
        {
                CheckLoaded();
            BattleStateStore.State = BattleState.STARTING;

            Enemies.ForEach(enemy => enemy.OnKilled += Enemy_OnKilled);
            events.ForEach(battleEvent =>
            {
                if (battleEvent.IsAtStart(this)  && battleEvent.IsReady(this))
                {
                   
                    battleEvent.Execute(this);
                    
                }
            });
            bool allCompleted = true;
            foreach(var ev in events)
            {
                if (!ev.Completed)
                {
                    allCompleted = false;
                    break;
                }
            }
            if (!allCompleted)
            {
                foreach (var ev in events)
                {
                    if (ev.IsAtStart(this) && !ev.Completed)
                    {
                        
                        ev.OnCompleted((isCompleted) => {
                            OnBattleEventCompleted(isCompleted, () =>
                            {
                                ActionMenu.Start();
                                HealthCounter.Show();
                                BattleStateStore.State = BattleState.STARTED;
                            });
                        });
                        
                    }
                }
            } else
            {
                ActionMenu.Start();
                HealthCounter.Show();
                BattleStateStore.State = BattleState.STARTED;
            }
            
        }

        private void OnBattleEventCompleted(bool isCompleted, Action action)
        {
            bool allCompleted = true;
            foreach (var ev in events)
            {
                if (ev.IsAtStart(this) && !ev.Completed)
                {
                    allCompleted = false;
                    break;
                }
            }
            if (allCompleted)
            {
                action();
            }
        }

        private void Enemy_OnKilled(object sender, EventArgs eventArgs)
        {
            if (Enemies.TrueForAll(enemy => enemy.IsDead) && !(TextBubbleSystem != null &&  TextBubbleSystem.Showing))
            {
                BattleStateStore.State = BattleState.ENDED;
                Enemies.ForEach(enemy => enemy.OnKilled -= Enemy_OnKilled);
            }
        }

        public void MoveTargetDown()
        {
            if (SubOptionsListMenu.Showing)
            {
                SubOptionsListMenu.MoveTargetDown();
                ActiveOption = SubOptionsListMenu.Active;
            } else if (OptionsListMenu.Showing)
            {
                OptionsListMenu.MoveTargetDown();
                ActiveOption = OptionsListMenu.Active;
            }
            if (ActionMenu.Showing)
            {
                ActionMenu.MoveTargetDown();
            }
        }
        
        public void MoveTargetUp()
        {
            if (OptionsListMenu.Showing)
            {
                OptionsListMenu.MoveTargetUp();
                ActiveOption = OptionsListMenu.Active;
            }
            if (ActionMenu.Showing)
            {
                ActionMenu.MoveTargetUp();
            }
        }

        public bool IsStarted()
        {
            return BattleStateStore.IsStarted();
        }

        public bool IsEnded()
        {
            return BattleStateStore.IsEnded() && !(TextBubbleSystem != null && TextBubbleSystem.Showing);
        }

        public void AddEventOnStart(BattleEvent @event)
        {
            events.Add(@event);
        }

        public void AddEventOnStarting(BattleEvent @event)
        {
            @event.AtStart = true;
            events.Add(@event);
        }

        public void EndTurn()
        {
            Console.WriteLine($"{GetType().Name} - ExecuteOption - End");
            TurnSystem.End();
            
            while (TurnSystem.Active is Enemy enemy && !enemy.IsDead && enemy.EnemyType != EnemyType.Enviroment)
            {
                enemyAISysytem.ExecuteEnemyTurn(this,TurnSystem.Active);
            }

            TargetSystem.Cleanup();
            if (Enemies.TrueForAll(enemy => enemy.IsDead))
            {
                BattleStateStore.State = BattleState.ENDED;
                Enemies.ForEach(enemy => enemy.OnKilled -= Enemy_OnKilled);
            }
            ActionMenu.Process();
            HealthCounter.Show();
        }

        public void End()
        {
            BattleStateStore.State = BattleState.ENDED;
        }

        public void ShowOptionsMenu()
        {
            ActionMenu.Hide();
            OptionsListMenu.Show(ActionMenu.ActiveAction.Options);
            ActiveOption = OptionsListMenu.Active;
        }

        public void Execute()
        {
            if (ActionMenu.Showing)
            {
                ExecuteFromActionMenu();
            } else if (SubOptionsListMenu.Showing)
            {
                ExecuteFromSubOptionMenu();
            } 
            else if (OptionsListMenu.Showing)
            {
                ExecuteFromOptionMenu();
            } 
            else if (TargetSystem.Showing)
            {
                ConfirmTarget();
            } else if (TextBubbleSystem.Showing)
            {
                TextBubbleSystem.Continue();
            } 
            
        }

        private void ExecuteFromSubOptionMenu()
        {
            ActiveOption = SubOptionsListMenu.Active;
            SubOptionsListMenu.Hide();
            ActiveOption.Execute(this,TurnSystem.Active,null,null);
            
        }


        public void ExecuteFromOptionMenu()
        {
            ActiveOption = OptionsListMenu.Active;
            if (OptionsListMenu.Active is ChangeMemberOption memberOptions)
            {
                SubOptionsListMenu.Show(memberOptions.PartnerOptions);
            }
            else
            {
                OptionsListMenu.Hide();
                TargetSystem.Show(ActiveOption);
            }
           
        }
        public void ShowTargeting(IOption active)
        {
            ActiveOption = active;
            OptionsListMenu.Hide();
            TargetSystem.Show(active);
        }

        public void ConfirmTarget()
        {
            var move = ActiveOption;
            var target = TargetSystem.Actives;
            var activeHero = TurnSystem.Active;

            Console.WriteLine($"ExecuteOption - {move}");
            TargetSystem.Hide();
            move.Execute(this,activeHero, target, (justDamaged) =>
            {
                if (target[0] is EnvironmentTarget environmentTarget)
                {
                    environmentTarget.ExecuteEffect(this);
                }

                HashSet<BattleEvent> battleEventsCompleted = new HashSet<BattleEvent>();
                events.Where((ev) => ev is BattleEvent);

                events.Where(ev => ev.IsReady(this)).ToList().ForEach(battleEvent => battleEvent.Execute(this));
                justDamaged?.ToList().ForEach((damaged) =>
                {
                   var sequence =  damaged.Item1.PostDamagePhase(damaged.Item2);

                });
                if (!TextBubbleSystem.Showing)
                {
                    EndTurn();
                }
               

            });
        }

        private void ShouldEndTurn()
        {
            events.Where(ev => ev.IsReady(this));
        }

        internal void EnemyAttack(IEnemyAttack move)
        {
            Console.WriteLine($"{GetType().Name} - EnemyAttack - {TurnSystem.Active} -  {move} on {Heroes.First()}");
            var sequence = ActionCommandCenter.FetchSequence();
            move.Execute(TurnSystem.Active, Heroes.First(), sequence, () =>
            {
                Console.WriteLine($"{GetType().Name} - move Execute ");
                TurnSystem.End();
                TurnSystem.ExcuteTurn();
                Console.WriteLine($"EnemyAttack - End");
            });
        }

        public void ExecuteFromActionMenu()
        {
            if (ActionMenu.ActiveAction.Options.Count() == 1)
            {
                ActionMenu.Hide();
                ShowTargeting(ActionMenu.ActiveAction.Options[0]);
            }
            else
            {
                ShowOptionsMenu();
            }
        }

        public string ActiveOptionName { get => actionMenuStore.FetchName(ActiveOption); }
        public BattleState State { get => BattleStateStore.State; }
        public ITattleStore TattleStore { get; internal set; } = new TattleStore();

        //public IOption[] PartnersOptions { get; set; } = 
             //{new PartnerOption("Goombario"), new PartnerOption("Koopa")};

        public string GetActiveOptionName()
        {
            return actionMenuStore.FetchName(OptionsListMenu.Active);
        }

        public void ShowText(GameText gameText)
        {
            HealthCounter.Hide();
            TextBubbleSystem.ShowText(gameText);
        }

        public void OnTextCompleted(Action<IEnumerable<Tuple<Enemy, bool>>> action)
        {
            TextBubbleSystem.OnTextCompleted((result) =>
            {
                HealthCounter.Show();
                action(result);
            });
        }

        public void ShowSubOptionsMenu(IOption option, IOption[] suboptions)
        {
            if (suboptions.Length == 0)
            {
                throw new ArgumentException($"suboptions cannot be empty");
            }
            this.SubOptionsListMenu.Show(suboptions,0);
            ActiveOption = OptionsListMenu.Active;
        }

       
    }

    public class PartnerOption : IOption
    {
        private Hero partner;
        public PartnerOption(string goombario,Hero partner)
        {
            this.Name = goombario;
            this.partner = partner;
        }

        public Guid? Guid { get; }
        public string Name { get; }
        public TargetType TargetType { get; }
        public void Execute(Battle battle, object activeHero, Enemy[] targets, Action<IEnumerable<Tuple<Enemy, bool>>> p)
        {
            battle.Heroes[1] = partner;
            battle.EndTurn();
            
            //throw new NotImplementedException();
        }
    }
}
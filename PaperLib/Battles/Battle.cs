using System;
using System.Collections.Generic;
using Heroes;
using Enemies;
using Tests;
using System.Linq;
using MenuData;
using TargetSystem;
using PaperLib;

namespace Battle
{
    public class Battle
    {
        public static ILogger Logger;
        public IBattleStateStore StateStore = new BattleStateStore();
        public List<Hero> Heroes  { get; private set; }
        public List<Enemy> Enemies { get; private set; }
        public List<BattleEvent> events = new List<BattleEvent>();
        //this is a view
        public IMarioHealthCounter ActivePlayerHud;
        public ITurnSystem TurnSystem { get; internal set; } = new DefaultTurnSystem(Logger);
        public List<IMenu> Menus { get; set; }
        public IActionMenu ActionMenu { get; internal set; }
        public IOptionsListMenu OptionsListMenu { get; internal set; } = new DefaultOptionsListMenu();
        public IOptionsListMenu SubOptionsListMenu { get; private set; } = new DefaultOptionsListMenu();
        public string ActiveOptionName { get => actionMenuStore.FetchName(ActiveOption); }
        public BattleState State { get => StateStore.State; }
        public ITattleStore TattleStore { get; internal set; } = new TattleStore();
        public static IActionCommandCenter ActionCommandCenter = new TestActionCommandCenter();
        public ITargetSystem TargetSystem { get; internal set; }
        public ITextBubbleSystem TextBubbleSystem { get; internal set; }
        private IOption ActiveOption;
        public IEnemyAISysytem enemyAISysytem { get; internal set; }
        //public IExecuteable ActiveExecutable { get; private set; }

        private IActionMenuStore actionMenuStore = new DefaultActionMenuStore();
        //private IHasMoveableTarget ActiveMenu;
       

        public void Load(List<Hero> heroes, List<Enemy> enemies)
        {
            this.Heroes = heroes;
            this.Enemies = enemies;
            TurnSystem.Load(heroes, enemies);
            this.ActivePlayerHud = new MarioHealthCounter(heroes);
            this.TargetSystem = new DefaultTargetSystem(enemies);
        }



        public Battle()
        {
            TurnSystem = new DefaultTurnSystem(Logger);
            enemyAISysytem = new DefaultEnemyAiSystem(this, TurnSystem);
            ActionMenu = new DefaultActionMenu(TurnSystem);
            TextBubbleSystem = new TextBubbleSystem();
            TargetSystem = new DefaultTargetSystem(Enemies);
            //HealthCounter = new MarioHealthCounter(Heroes);
            //actionMenuStore ;
        }
        public Battle(List<Hero> heroes, List<Enemy> enemies)
        {
            Heroes = heroes;
            Enemies = enemies;
            TurnSystem = new DefaultTurnSystem(Logger);
            enemyAISysytem = new DefaultEnemyAiSystem(this, TurnSystem);
            this.ActivePlayerHud = new MarioHealthCounter(heroes);
            ActionMenu = new DefaultActionMenu(TurnSystem);
            TargetSystem = new DefaultTargetSystem(Enemies);
            TextBubbleSystem = new TextBubbleSystem();
            TurnSystem.Load(heroes, enemies);
        }

        public Battle(List<Hero> heroes, List<Enemy> enemies, ITextBubbleSystem bubbleSystem)
        {
            Heroes = heroes;
            Enemies = enemies;
            TurnSystem = new DefaultTurnSystem(Logger);
            enemyAISysytem = new DefaultEnemyAiSystem(this, TurnSystem);
            this.ActivePlayerHud = new MarioHealthCounter(heroes);
            ActionMenu = new DefaultActionMenu(TurnSystem);
            TargetSystem = new DefaultTargetSystem(Enemies);
            TextBubbleSystem = bubbleSystem;
            TurnSystem.Load(heroes, enemies);
        }


        public Battle(List<Hero> heroes, EnemyConfig encounter) : this(heroes, encounter.ToEnemies())
        {

        }
        //maybe have an active system
        public void Cancel()
        {
            if (TargetSystem.Showing)
            {
                TargetSystem.Hide();
                if (ActionMenu.ActiveAction.Options.Count() == 1)
                {
                    ActionMenu.Show();
                    ActivePlayerHud.Show();
                }
                else
                {
                    OptionsListMenu.Show(ActionMenu.ActiveAction.Options);
                }
            }
            else if (OptionsListMenu.Showing)
            {
                OptionsListMenu.Hide();
                ActionMenu.OnBattleStarted();
                ActivePlayerHud.Show();

            }
        }


      
        public void Start()
        {
           
            StateStore.State = BattleState.STARTING;

            Enemies.ForEach(enemy => enemy.OnKilled += Enemy_OnKilled);
            //execute battleevent
            events.ForEach(battleEvent =>
            {
                if (battleEvent.IsAtStart(this) && battleEvent.IsReady(this))
                {

                    battleEvent.Execute(this);
                    battleEvent.SetOnCompleted((isCompleted) =>
                    {
                        CheckIfAllBattleEventsAreCompleted(isCompleted, () =>
                        {
                            ActionMenu.OnBattleStarted();
                            ActivePlayerHud.Show();
                            StateStore.State = BattleState.STARTED;
                        });
                    });

                }
            });
            bool allCompleted = true;
            foreach (var ev in events)
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
                        //executed but not completed
                       

                    }
                }
            }
            else
            {
                //ActiveMenu?.OnBattleStarted();
                ActionMenu.OnBattleStarted();
                ActivePlayerHud.Show();
                StateStore.State = BattleState.STARTED;

            }

        }

        private void CheckIfAllBattleEventsAreCompleted(bool isCompleted, Action action)
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
            if (Enemies.TrueForAll(enemy => enemy.IsDead) && !(TextBubbleSystem != null && TextBubbleSystem.Showing))
            {
                StateStore.State = BattleState.ENDED;
                Enemies.ForEach(enemy => enemy.OnKilled -= Enemy_OnKilled);
            }
        }

        public void MoveTargetDown()
        {
            //ActiveMenu?.MoveTargetDown();
            if (SubOptionsListMenu.Showing)
            {
                SubOptionsListMenu.MoveTargetDown();
                ActiveOption = SubOptionsListMenu.Active;
            }
            else if (OptionsListMenu.Showing)
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
            //ActiveMenu?.MoveTargetUp();
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
            return StateStore.IsStarted();
        }

        public bool IsEnded()
        {
            return StateStore.IsEnded() && !(TextBubbleSystem != null && TextBubbleSystem.Showing);
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

        internal IBattleAnimationSequence WaitForBattleAnimationSequence()
        {
            return ActionCommandCenter.FetchSequence();
        }

        public void EndTurn()
        {
            //Logger?.Log($"{GetType().Name} - ExecuteOption - End");
            
          
            if (Enemies.TrueForAll(enemy => enemy.IsDead))
            {
                StateStore.State = BattleState.ENDED;
                Enemies.ForEach(enemy => enemy.OnKilled -= Enemy_OnKilled);
            }

            if (State != BattleState.ENDED)
            {


                TurnSystem.End();
                TargetSystem.Cleanup();
                ActionMenu.Process();
                ActivePlayerHud.Show();
                //enemyaisystem
                enemyAISysytem.ExecuteEnemyTurn(this, TurnSystem.Active);
            }
            //}

            
        }

        public void End()
        {
            StateStore.State = BattleState.ENDED;
        }

        public void ShowOptionsMenu()
        {
            //FetchMenu<IActionMenu>().Hide();
            if (ActionMenu.ActiveAction.Options.Length == 0)
            {
                Logger?.Log($"Action {{{ActionMenu.ActiveAction.Name}}} needs some options");
            }
            ActionMenu.Hide();
            OptionsListMenu.Show(ActionMenu.ActiveAction.Options);
            ActiveOption = OptionsListMenu.Active;
        }
        //TODO: 
        private T FetchMenu<T>()
        {
            return (T) Menus.Find(menu => menu is T);
        }
        public void Execute()
        {
            //this.ActiveExecutable?.Execute();
            if(TargetSystem == null)
            {
                
                throw new NullReferenceException("targetSystem is nuii");
            }
            if (ActionMenu.Showing)
            {
                //onActionMenuExecuted
                ExecuteFromActionMenu();
            }
            else if (SubOptionsListMenu.Showing)
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
            }
            else if (TextBubbleSystem.Showing)
            {
                TextBubbleSystem.Continue();
            }
        }

        private void ExecuteFromSubOptionMenu()
        {
            Logger.Log("   ExecuteFromSubOptionMenu");
            ActiveOption = SubOptionsListMenu.Active;
            SubOptionsListMenu.Hide();
            ActiveOption.Execute(this, TurnSystem.Active, null, null);

        }


        public void ExecuteFromOptionMenu()
        {
            Logger.Log("   ExecuteFromSubOptionMenu");
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
            Logger?.Log($"Battle - ShowTargeting {active}");
            ActiveOption = active;
            OptionsListMenu.Hide();
            TargetSystem.Show(active);
        }

        public void ConfirmTarget()
        {
            var move = ActiveOption;
            var target = TargetSystem.Actives;
            var activeHero = TurnSystem.Active;

            //Logger?.Log($"ExecuteOption - {move}");
            TargetSystem.Confirm();
            move.Execute(this, activeHero, target, (justDamaged) =>
             {
                 //enviroment target
                 if (target[0] is EnvironmentTarget environmentTarget)
                 {
                     environmentTarget.ExecuteEffect(this);
                 }
                 
                 events.Where(ev => ev.IsReady(this)).ToList().ForEach(battleEvent => battleEvent.Execute(this));
                 justDamaged?.ToList().ForEach((damaged) =>
                 {
                     
                     var sequence = damaged.Item1.PostDamagePhase(damaged.Item2);

                 });

                 if (!TextBubbleSystem.Showing)
                 {
                     EndTurn();
                 }


             });
        }

        internal void EnemyAttack(IEnemyAttack move)
        {
            Logger?.Log($"{GetType().Name} - EnemyAttack - {TurnSystem.Active} -  {move} on {Heroes.First()}");
            IBattleAnimationSequence sequence = new DelayedSequence(ActionCommandCenter);
            //IBattleAnimationSequence sequence = null;
            move.Execute(TurnSystem.Active, Heroes.First(), sequence, () =>
            {
                Logger?.Log($"{GetType().Name} - move Execute {TurnSystem.Active}");
                TurnSystem.End();
                TurnSystem.ExcuteTurn();
                ExecuteNextTurn();
                //Logger?.Log($"EnemyAttack - End");
            });
        }

        private void ExecuteNextTurn()
        {
            if(TurnSystem.Active is Enemy)
            {
                enemyAISysytem.ExecuteEnemyTurn(this, TurnSystem.Active);
            }
            TargetSystem.Cleanup();
            if (Enemies.TrueForAll(enemy => enemy.IsDead))
            {
                StateStore.State = BattleState.ENDED;
                Enemies.ForEach(enemy => enemy.OnKilled -= Enemy_OnKilled);
            }
            ActionMenu.Process();
            ActivePlayerHud.Show();
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

        public string GetActiveOptionName()
        {
            return actionMenuStore.FetchName(OptionsListMenu.Active);
        }

        public void ShowText(GameText gameText)
        {
            ActivePlayerHud.Hide();
            TextBubbleSystem.ShowText(gameText);
        }

        public void OnTextCompleted(Action<IEnumerable<Tuple<Enemy, bool>>> action)
        {
            TextBubbleSystem.OnTextCompleted((result) =>
            {
                ActivePlayerHud.Show();
                action(result);
            });
        }

        public void ShowSubOptionsMenu(IOption option, IOption[] suboptions)
        {
            if (suboptions.Length == 0)
            {
                throw new ArgumentException($"suboptions cannot be empty");
            }
            this.SubOptionsListMenu.Show(suboptions, 0);
            ActiveOption = OptionsListMenu.Active;
        }

        public override bool Equals(object obj)
        {
            if (obj is Battle battle)
            {
                if (!EqualityComparer<IBattleStateStore>.Default.Equals(StateStore, battle.StateStore))
                {
                    Logger?.Log($"expected: {StateStore.State}, actual: {battle.StateStore.State}");
                    return false;
                }
                if (!Enumerable.SequenceEqual(Heroes, battle.Heroes))
                {
                    return false;
                }
                if (!Enumerable.SequenceEqual(Enemies, battle.Enemies))
                {
                    return false;
                }
                if (!Enumerable.SequenceEqual(events, battle.events))
                {
                    return false;
                }
                if (!EqualityComparer<IOption>.Default.Equals(ActiveOption, battle.ActiveOption))
                {
                    return false;
                }
                if (State != battle.State)
                {
                    return false;
                }
                return true;
            }
            return false;  
        }

        
    }

    public class PartnerOption : IOption
    {
        private Hero partner;
        public PartnerOption(string goombario, Hero partner)
        {
            this.Name = goombario;
            this.partner = partner;
        }

        public Guid? Guid { get; }
        public string Name { get; }
        public TargetType TargetType { get; }

        public bool Equals(IOption other)
        {
            return other != null && other is PartnerOption partnerOption
                && Guid == other.Guid && Name == other.Name &&
                TargetType == other.TargetType && partner == partnerOption.partner;
        }

        public void Execute(Battle battle, object activeHero, Enemy[] targets, Action<IEnumerable<Tuple<Enemy, bool>>> p)
        {
            battle.Heroes[1] = partner;
            battle.EndTurn();

            //throw new NotImplementedException();
        }
    }
}
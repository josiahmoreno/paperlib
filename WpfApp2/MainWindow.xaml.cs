using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Battle;
using Enemies;
using Heroes;
using Items;

namespace WpfApp2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        private Mario Mario;
        private Goompa Goompa;

        private JrTroopa JrTroopa;

        private Battle.Battle battle;
        private ActionMenuView actionMenu;
        private TargetSystemView targetingView;
        private TextBubbleView textBubbleView;

        public MainWindow()
        {
            InitializeComponent();
            start();

        }

        public void start()
        {
            var bubbleSystem = new TextBubbleSystem();
            Mario = new Mario(
                new Inventory(),
                new List<IJumps> { new Attacks.Jump() }.ToArray(),
                new Attacks.Hammer());
            Goompa = new Goompa();
            var scriptAttack = new ScriptAttack(EnemyAttack.JrTroopaPowerJump);
            JrTroopa = new JrTroopa(new List<IEnemyAttack> { new RegularAttack(EnemyAttack.JrTroopaJump, 1) });
           var JrTroopa2 = new JrTroopa(new List<IEnemyAttack> { new RegularAttack(EnemyAttack.JrTroopaJump, 1) });
            var enemies = new List<Enemy>()
            {
                JrTroopa
            };
            battle = new Battle.Battle(new List<Hero> { Mario, Goompa }, enemies, bubbleSystem);
            battle.AddEventOnStarting(new TextBubbleEvent((battleEvent, battle) =>
            {

                battle.TextBubbleSystem.ShowText(new GameText("1", "2", "3", "4"));
                battle.TextBubbleSystem.OnTextCompleted((_) => battleEvent.Complete());


            }, (battle) => battle.State == BattleState.STARTING));
            battle.AddEventOnStart(new BattleEvent((battleEvent, battle) =>
            {

                battle.TextBubbleSystem.ShowText(new GameText("Nice Job"));
                battleEvent.Completed = true;
                battle.TextBubbleSystem.OnTextCompleted((_) => battle.EndTurn());




            }, (battle) => battle.Enemies.First(enemy => enemy is JrTroopa).Health.CurrentValue == 4));

            battle.AddEventOnStart(new BattleEvent((battleEvent, battle) =>
            {

                battle.TextBubbleSystem.ShowText(new GameText("Mario is lame!"));
                battleEvent.Completed = true;
                battle.TextBubbleSystem.OnTextCompleted((_) => battle.EndTurn());
                // what i return a turn end enum, then battle events haave to end turns!

            }, (battle) => battle.Enemies.First(enemy => enemy is JrTroopa).Health.CurrentValue == 3));

            battle.AddEventOnStart(new BattleEvent((battleEvent, battle) =>
            {

                battle.TextBubbleSystem.ShowText(new GameText("Goompa: You are almost there mario!"));
                battleEvent.Completed = true;
                battle.TextBubbleSystem.OnTextCompleted((_) =>
                {
                    battle.EndTurn();
                });
                // what i return a turn end enum, then battle events haave to end turns!

            }, (battle) => battle.Enemies.First(enemy => enemy is JrTroopa).Health.CurrentValue == 2));

            battle.AddEventOnStart(new BattleEvent((battleEvent, battle) =>
            {

                battle.TextBubbleSystem.ShowText(new GameText("Goompa: You are almost there mario!"));
                battleEvent.Completed = true;

                battle.TextBubbleSystem.OnTextCompleted((_) =>
                {
                    battle.TextBubbleSystem.ShowText(new GameText("JrTroopa: All right, you asked for it", "Full power!!"));
                    battle.Enemies.First(o => o == JrTroopa).Sequence.Add(scriptAttack);
                    battle.TextBubbleSystem.OnTextCompleted(__ =>
                    {
                        battle.EndTurn();
                    });
                });
                // what i return a turn end enum, then battle events haave to end turns!

            }, (battle) => battle.Enemies.First(enemy => enemy is JrTroopa).Health.CurrentValue == 1));
            battle.AddEventOnStart(new BattleEvent((battleEvent, battle) =>
            {

                battle.TextBubbleSystem.ShowText(new GameText("Goompa: You got Star points", "You get em when u win", "Every 100 you level up", "Git Hard"));
                battle.Enemies.First(o => o == JrTroopa).Sequence.Add(scriptAttack);
                battle.TextBubbleSystem.OnTextCompleted((_) =>
                {
                    battle.EndTurn();
                });
                // what i return a turn end enum, then battle events haave to end turns!

            }, (battle) => battle.Enemies.First(enemy => enemy is JrTroopa).Health.CurrentValue == 0));

            double aa = this.Height;
            this.actionMenu = new ActionMenuView(aa,battle.ActionMenu);
            this.targetingView = new TargetSystemView(battle,battle.TargetSystem);

            targetingView.Height = Height;
            targetingView.Width = Height;
            targetingView.DrawAll();
            actionMenu.Height = Height;
            actionMenu.Width = Height;

            this.textBubbleView = new TextBubbleView(battle.TextBubbleSystem);
            textBubbleView.Height = Height;
            textBubbleView.Width = Width;
            textBubbleView.Draw();
            Grid.Children.Add(actionMenu);
            Grid.Children.Add(targetingView);
            Grid.Children.Add(textBubbleView);
            //actionMenu.MoveTargetUp();
            //battle.ActionMenu.MoveTargetUp();
            //actionMenu.MoveTargetUp();
            //battle.ActionMenu.MoveTargetUp();
            //actionMenu.MoveTargetUp();
            //battle.ActionMenu.MoveTargetUp();
            EventManager.RegisterClassHandler(typeof(Window), Keyboard.KeyUpEvent, new KeyEventHandler(keyUp), true);
            battle.Start();
            System.Diagnostics.Debug.WriteLine($"State {battle.State}");
        }

        private void keyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Up)
            {
                if (battle.ActionMenu.SelectedIndex != 0)
                {
                    actionMenu.MoveTargetUp();
                    battle.ActionMenu.MoveTargetUp();
                }
            }
            if (e.Key == Key.Left)
            {
                if (battle.TargetSystem.SelectedIndex != 0)
                {
                    battle.TargetSystem.MoveTargetLeft();
                    targetingView.DrawAll();
                }
              
            }
            if (e.Key == Key.Right)
            {
                if (battle.TargetSystem.SelectedIndex != battle.Enemies.Count() - 1)
                {
                    battle.TargetSystem.MoveTargetRight();
                    targetingView.DrawAll();
                }
                
            }
            if (e.Key == Key.Enter || e.Key == Key.Space)
            {
                battle.Execute();
            }
            if ( e.Key == Key.Delete || e.Key == Key.Back)
            {
                battle.Cancel();
            }
            if (e.Key == Key.Down)
            {
                if (battle.ActionMenu.SelectedIndex != battle.ActionMenu.Items.Count() -1 )
                {
                    actionMenu.MoveTargetDown();
                    battle.ActionMenu.MoveTargetDown();
                }
            }
            //battle.ActionMenu.MoveTargetUp();
        }
    }
}

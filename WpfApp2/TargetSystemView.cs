using Enemies;
using System;
using System.Collections.Generic;
using System.Windows.Controls;
using System.Windows.Media;
using TargetSystem;

namespace WpfApp2
{
    internal class TargetSystemView : Canvas
    {
        private ITargetSystem targetSystem;
        private Battle.Battle battle;
        private List<Dude> bads;

        public TargetSystemView(Battle.Battle battle, ITargetSystem targetSystem)
        {
            this.battle = battle;
            this.Background = new SolidColorBrush(Colors.BlueViolet);
            this.targetSystem = targetSystem;
            targetSystem.OnShowing += OnShowing;
            OnShowing(targetSystem.Showing);

            
        }

        public void DrawAll()
        {
            Children.Clear();
            DrawBads();
            DrawTargeter();
        }

        private void DrawTargeter()
        {
            if (battle.TargetSystem.Actives != null)
            {
                for (int i = 0; i < battle.Enemies.Count; i++)
                {
                    if (bads[i].enemy == battle.TargetSystem.Actives[0])
                    {
                        var button = new Frame();
                        button.Height = 30;
                        button.Width = 30;
                        double x = bads[i].position.Left + bads[i].button.Width / 2 - button.Width / 2;
                        double y = bads[i].button.Height + 10 + button.Height;
                        var pos = position(x, y);
                        button.Margin = pos;
                        button.Background = new SolidColorBrush(Colors.White);
                        Children.Add(button);
                        break;
                    }
                }
            }
        }

        private void DrawBads()
        {
            
            this.bads = new List<Dude>();
            for (int i = 0; i < battle.Enemies.Count; i++)
            {
                
                bads.Add(CreateDude(battle.Enemies[i], i));

            }
        }

        public struct Dude
        {
            public System.Windows.Thickness position;
            public Enemy enemy;
            public Frame button;

            public Dude(System.Windows.Thickness position, Enemy enemy, Frame button) : this()
            {
                this.position = position;
                this.enemy = enemy;
                this.button = button;
            }
        }
        private Dude CreateDude(Enemy enemy, int i)
        {
            int x = 300 + (i * 105);
            int y = 100;
            var button = new Frame();
            button.Height = 100;
            button.Width = 100;
            var label = new Label();
            button.Background = new SolidColorBrush(Colors.White);
            label.Content = enemy.GetType().Name;
            button.Navigate(label);
            var pos = position(x, y);
            button.Margin = pos;
            

           
            Children.Add(button);
            return new Dude(pos,enemy,button);
        }

        private System.Windows.Thickness position(double x, double y)
        {
            double newX = x;
            double newY = this.Height - y;
            return new System.Windows.Thickness(newX, newY, 0, 0);
        }

        private void OnShowing(bool isShowing)
        {
            if (isShowing)
            {
                this.Visibility = System.Windows.Visibility.Visible;
                DrawAll();

            }
            else
            {
                this.Visibility = System.Windows.Visibility.Hidden;
            }
        }
    }
}
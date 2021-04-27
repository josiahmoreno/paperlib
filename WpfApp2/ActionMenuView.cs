using MenuData;
using System;
using System.Windows.Controls;
using System.Windows.Media;
using System.Linq;
namespace WpfApp2
{

    internal class ActionMenuView: Canvas
    {
        private IActionMenu actionMenu;
        private double radius;
        private double height_;
        private double angle_;
        private double preStartingAngle_ , endingAngle  = Math.PI / 6;
        private Align align;
        public ActionMenuView(double height, IActionMenu actionMenu)
        {
            this.actionMenu = actionMenu;
            this.actionMenu.OnHide += onHide;
            //this.Orientation = Orientation.Vertical;
            Background = new SolidColorBrush(Colors.Red);
            radius = 300;
            this.actionMenu = actionMenu;
            this.height_ = height ;
            this.preStartingAngle_ = endingAngle;
            //endingAngle;
            
            var diff = actionMenu.Items.Count() - actionMenu.SelectedIndex - 1 ;
            endingAngle -= (diff * preStartingAngle_);
            System.Diagnostics.Debug.WriteLine($"ActionMenu - diff = {diff}");
            DrawAll();
            onHide(actionMenu.Showing);
        }

        public void onHide(bool isShowing)
        {
            if (isShowing)
            {
                this.Visibility = System.Windows.Visibility.Visible;

            } else
            {
                this.Visibility = System.Windows.Visibility.Hidden;
            }
        }

        private void DrawAll()
        {
            for (int i = 0; i < actionMenu.Items.Count(); i++)
            {
                var item = actionMenu.Items[actionMenu.Items.Count() - i - 1];
                var color = Colors.White;
                if (i == 0)
                {
                    color = Colors.PapayaWhip;
                }
                if (i == actionMenu.Items.Count() - 1)
                {
                    color = Colors.GhostWhite;
                }
                CreateView(endingAngle + (preStartingAngle_* i), item, color);
            }
        }
        private void preCreateViewFromTop()
        {
            this.angle_ = (Math.PI / 2) / (actionMenu.Items.Count() - 1);
            for (int i = 0; i < actionMenu.Items.Count(); i++)
            {
                var item = actionMenu.Items[i];
                CreateViewFromTop(i, item);
            }
        }
        // x=rcosθ and y=rsinθ.
        private void CreateViewFromTop(int i,IActionMenuData actionMenuData)
        {
            var button =  new Frame();
            button.Height = 100;
            button.Width = 100;
            double per = (Math.PI / 2) - ((angle_) * i);
            button.Margin = angled(radius, per);
            //button.Content = actionMenuData.Name;
            //button.Loc
           //button.
            button.Background =  new SolidColorBrush(Colors.Gold);
            Children.Add(button);
            
        }

        private void CreateView(double angle, IActionMenuData actionMenuData, Color color)
        {
            System.Diagnostics.Debug.WriteLine($"{actionMenuData.Name} {angle}");
            var button = new Frame();
            button.Height = 100;
            button.Width = 100;
            var label = new Label();
            label.Height = 100;
            label.Width = 100;
            label.VerticalContentAlignment = System.Windows.VerticalAlignment.Center;
            label.HorizontalContentAlignment = System.Windows.HorizontalAlignment.Center;
            label.Content = actionMenuData.Name;
            button.Navigate(label);
            button.Margin = angled(this.radius, angle);
            //button.Content = actionMenuData.Name;
            //button.Loc
            //button.
            button.Background = new SolidColorBrush(color);
            Children.Add(button);

        }

        private System.Windows.Thickness angled(double radius, double angle)
        {
            return position(radius * Math.Cos(angle),radius * Math.Sin(angle));
        }

        private System.Windows.Thickness position(double x, double y)
        {
            double newX = x;
            double newY = height_ - y;
            return new System.Windows.Thickness(newX, newY, 0,0);
        }

        internal enum Align
        {
            Top,Center,Bottom
        }

        internal void MoveTargetUp()
        {
            Children.Clear();
            endingAngle -= preStartingAngle_;
            DrawAll();
        }

        internal void MoveTargetDown()
        {
            Children.Clear();
            endingAngle += preStartingAngle_;
            DrawAll();
        }
    }
}
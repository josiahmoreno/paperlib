using Battle;
using System;
using System.Windows.Controls;
using System.Windows.Media;

namespace WpfApp2
{
    internal class TextBubbleView: Canvas
    {
        private ITextBubbleSystem textBubbleSystem;
        private Label text;

        public TextBubbleView(ITextBubbleSystem textBubbleSystem)
        {
            this.textBubbleSystem = textBubbleSystem;
            this.text = new Label();
            this.textBubbleSystem.OnShowing += OnShowing;
            this.textBubbleSystem.OnText += OnText;
            this.OnShowing(textBubbleSystem.Showing);
        }

        private void OnText(string obj)
        {
            text.Content = obj;
        }

        public void Draw()
        {
            
            text.Width = this.Width;
            text.Height = 250;
            text.Background = new SolidColorBrush(Colors.Gold);
            Children.Add(text);
        }
        private void OnShowing(bool isShowing)
        {
            if (isShowing)
            {
                this.Visibility = System.Windows.Visibility.Visible;
               

            }
            else
            {
                this.Visibility = System.Windows.Visibility.Hidden;
            }
        }
    }
}
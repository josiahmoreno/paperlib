using Enemies;
using System;
using System.Collections.Generic;

namespace Battle
{
    public class TextBubbleSystem : ITextBubbleSystem
    {
        private GameText gameText;
        int? index = 0;
        private bool _showing;
        public string CurrentText { get; private set; }

        public bool Showing
        {
            get => _showing; private set
            {
                _showing = value;
                OnShowing?.Invoke(_showing);
            }
        }
        public Action<IEnumerable<Tuple<Enemy, bool>>> OnTextComplete { get; private set; }
        public Action<bool> OnShowing { get; set; }
        public Action<string> OnText { get; set; }

        public void Continue()
        {
            if (!Showing)
            {
                throw new Exception("cant continue if not showing");
            }
            index += 1;
            if(index >= gameText.Text.Length)
            {

                
                index = null;
                Showing = false;
                
                var action = OnTextComplete;
                OnTextComplete = null;
                CurrentText = null;
                action?.Invoke(null);
            }
            if (index != null)
            {
                CurrentText = gameText.Text[(int) index];
                OnText?.Invoke(CurrentText);
            }
            
        }

        public void OnTextCompleted(Action<IEnumerable<Tuple<Enemy, bool>>> action)
        {

            this.OnTextComplete = action;
        }

        public void ShowText(GameText v)
        {
            this.index = 0;
            this.gameText = v;
            Showing = true;
            this.CurrentText = gameText.Text[(int) index];
            OnText?.Invoke(CurrentText);
        }
    }
}
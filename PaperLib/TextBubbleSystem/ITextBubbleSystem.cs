using Enemies;
using System;
using System.Collections.Generic;

namespace Battle
{
    public interface ITextBubbleSystem
    {
        bool Showing { get; }
        Action<bool> OnShowing { get; set; }
        Action<string> OnText { get; set; }
        void ShowText(GameText gameText);
        void Continue();
        
        string CurrentText { get; }
        void OnTextCompleted(Action<IEnumerable<Tuple<Enemy, bool>>> action);
    }
}
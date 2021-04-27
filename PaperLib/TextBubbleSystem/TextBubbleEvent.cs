using System;
using Battle;

namespace Battle
{
    public class TextBubbleEvent : BattleEvent
    {
        public TextBubbleEvent(Action<BattleEvent, Battle> battleEvent, Func<Battle, bool> qualifier) : base(battleEvent,qualifier)
        {
        }
    }
}
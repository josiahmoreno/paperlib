using System;
using System.Collections.Generic;
using Battle;

namespace Battle
{
    public class BattleEvent : IEquatable<BattleEvent>
    {
        private Action<BattleEvent,Battle> battleEvent;
        private Func<Battle, bool> qualifier;
        private bool _complete = false;
        public bool AtStart = false;
        public bool Completed
        {
            get => _complete; set
            {
                _complete = value;
                onBattleEventCompleted?.Invoke(_complete);
            }
        }
        private Action<bool> onBattleEventCompleted;

        //public BattleEvent()
        //{
        //}

        public BattleEvent(Action<BattleEvent,Battle> battleEvent, Func<Battle,bool> qualifier)
        {
            this.battleEvent = battleEvent;
            this.qualifier = qualifier;
        }

        internal void Execute(Battle battle)
        {
            battleEvent.Invoke(this,battle);
        }

        internal bool IsReady(Battle battle)
        {
            return qualifier.Invoke(battle);
        }

        internal void SetOnCompleted(Action<bool> onBattleEventCompleted)
        {
            this.onBattleEventCompleted = onBattleEventCompleted;
        }

        public void Complete()
        {
            if (Completed)
            {
                throw new UnauthorizedAccessException("can't complete an already completed event");
            }
            Completed = true;
        }

        internal bool IsAtStart(Battle battle)
        {
            return AtStart;
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as BattleEvent);
        }

        public bool Equals(BattleEvent other)
        {
            return other != null &&
                   //EqualityComparer<Action<BattleEvent, Battle>>.Default.Equals(battleEvent, other.battleEvent) &&
                   //EqualityComparer<Func<Battle, bool>>.Default.Equals(qualifier, other.qualifier) &&
                   _complete == other._complete &&
                   AtStart == other.AtStart 
                   //Completed == other.Completed &&
                   // EqualityComparer<Action<bool>>.Default.Equals(onBattleEventCompleted, other.onBattleEventCompleted);
                  ;
        }
    }
}
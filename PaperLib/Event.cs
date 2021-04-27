using System;
using Battle;

namespace Battle
{
    public class BattleEvent
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

        internal void OnCompleted(Action<bool> onBattleEventCompleted)
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
    }
}
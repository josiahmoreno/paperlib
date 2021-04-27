using System;

namespace Battle
{
    public interface IBattleStateStore
    {
        event BattleStateStore.OnBattleStateChanged BattleStateChanged;
        BattleState State { get; set; }
        bool IsStarted();
        bool IsEnded();
    }

    public class BattleStateStore : IBattleStateStore
    {
        public event OnBattleStateChanged BattleStateChanged;

        public delegate void OnBattleStateChanged(object sender, BatleStateChangeEventArgs args);
        private BattleState _state = BattleState.NONE;
        public BattleState State
        {
            get
            {
                return _state;
            }
            set
            {
                _state = value;
                BattleStateChanged?.Invoke(this,new BatleStateChangeEventArgs(value));
            }
        }

        public bool IsStarted()
        {
            return State == BattleState.STARTED;
        }

        public bool IsEnded()
        {
            return State == BattleState.ENDED;
        }
    }
    
    public class BatleStateChangeEventArgs : EventArgs
    {
        private readonly BattleState _businessObject;

        public BatleStateChangeEventArgs(BattleState businessObject)
        {
            _businessObject = businessObject;
        }

        public BattleState BusinessObject
        {
            get { return _businessObject; }
        }
    }

   

    public enum BattleState { NONE,STARTING, STARTED, ENDING, ENDED }
}
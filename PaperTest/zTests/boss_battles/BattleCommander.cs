using System;
using Battle;



namespace Tests
{
    public class BattleCommander: IBattleCommander
    {
        public BattleCommander(Battle.Battle battle)
        {
            this.Battle = battle;
        }

        public Battle.Battle Battle { get; set; }

        public bool IsStarted()
        {
            return Battle.IsStarted();
        }

        public BattleState State { get; }
        public void Start()
        {
            Battle.Start();
        }

        public void MoveTargetUp()
        {
            Battle.MoveTargetUp();
        }

        public void MoveTargetDown()
        {
            Battle.MoveTargetDown();
        }

        public void Execute()
        {
            Battle.Execute();
        }

        public void Cancel()
        {
            Battle.Cancel();
        }
        
        public static implicit operator Battle.Battle(BattleCommander d) => d.Battle;
    }
}
using Enemies;
using NUnit.Framework;

namespace Tests
{
    public static class AssertExtensions
    {
        public static void AssertIsDead(this Enemy fuzzie)
        {
            Assert.IsTrue(fuzzie.IsDead, $"{fuzzie.GetType().Name} isnt dead, their hp is {fuzzie.Health.CurrentValue}");
        }

        public static void AssertIsTurn(this Battle.Battle battle, object obj)
        {
            Assert.IsTrue(battle.TurnSystem.Active == obj, battle.TurnSystem.Active.ToString()); 
        }
		
        public static void AssertMenuIsJump(this Battle.Battle battle)
        {
            Assert.IsTrue(battle.ActionMenu.ActiveAction.Name == "Jump");
        }
		
        public static void AssertMenuIsAbilities(this Battle.Battle battle)
        {
            Assert.IsTrue(battle.ActionMenu.ActiveAction.Name == "Abilities");
        }
		
        public static void AssertExecuteToShowOption(this Battle.Battle battle, string option)
        {
            battle.Execute();
            Assert.IsTrue(battle.OptionsListMenu.Active.Name == option, $"{battle.OptionsListMenu.Active.GetType().Name} {battle.OptionsListMenu.Active.Name} \n{battle.ListOptions()}");
        }
        
      

        private static string ListOptions(this Battle.Battle battle)
        {
            return string.Join("\n", (object[]) battle.OptionsListMenu.Items);
        }
		
        public static void AssertIsEnded(this Battle.Battle battle)
        {
            Assert.IsTrue(battle.IsEnded());
        }
		
		
    }
}
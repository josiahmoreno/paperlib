using Battle;

using Heroes;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Attacks;
using Enemies;
using Items;
using PaperLib.Enemies;
using Tests.battlesequence;

namespace Tests
{
	public class boss_battle_fuzzies
	{
		private Mario Mario;
		private Kooper Kooper;

		private Fuzzie FuzzieA;

		internal Fuzzie FuzzieB { get; private set; }
		internal Fuzzie FuzzieC { get; private set; }
		internal Fuzzie FuzzieD { get; private set; }

		private Battle.Battle battle;
		//https://www.youtube.com/watch?v=hctclwVZzUw&list=PLgU0IdjAiGw4ibWVo_RPVsdxv0GvFGCtS&index=5
	   
		[SetUp]
		public void Setup()
		{
			Battle.Battle.Logger = new TestLogger();
			var bubbleSystem = new TextBubbleSystem();
			Kooper = new Kooper();
			Mario = new Mario(new Hero[]{new Goombario(),Kooper},
				new Inventory(),
				new List<IJumps> { new Attacks.Jump(), new PowerJump() }.ToArray(),
				new Attacks.Hammer());
			Mario.Sequenceable = new TestSequenceable();
			//var scriptAttack = new ScriptAttack(EnemyAttack.JrTroopaPowerJump);
			//JrTroopa = new JrTroopa(new List<IEnemyAttack> { new RegularAttack(EnemyAttack.JrTroopaJump, 1) });
			var enemyCreator = new EnemyFactory();
			FuzzieA =  enemyCreator.FetchEnemy<Fuzzie>();
			FuzzieA.Sequenceable = new TestSequenceable();
			FuzzieB = enemyCreator.FetchEnemy<Fuzzie>();
			FuzzieB.Sequenceable = new TestSequenceable();
			FuzzieC = enemyCreator.FetchEnemy<Fuzzie>();
			FuzzieC.Sequenceable = new TestSequenceable();
			FuzzieD = enemyCreator.FetchEnemy<Fuzzie>();
			FuzzieD.Sequenceable = new TestSequenceable();
			var enemies = new List<Enemy>()
			{
				FuzzieA,
				FuzzieB,
				FuzzieC,
				FuzzieD
			};

			
			battle = new Battle.Battle(new List<Hero> { Mario, Kooper }, enemies, bubbleSystem);
		 
		   
			//battle.Start();

		}



		[Test]
		public void boss_battle_fuzzies_1()
		{
			Assert.IsTrue(!battle.IsStarted(), $"battle state = {battle.State.ToString()}");
			Assert.IsTrue(!battle.ActionMenu.Showing);
			Assert.IsTrue(!battle.OptionsListMenu.Showing);
			Assert.IsTrue(battle.OptionsListMenu.Active == null);
			Assert.IsTrue(battle.TurnSystem.Active == Mario);
			//0:10
			battle.Start();
			Assert.IsTrue(!battle.TextBubbleSystem.Showing);
			//menu is shown with jump selected
			Assert.IsTrue(battle.ActionMenu.ActiveAction.Name == "Jump");
			Assert.IsTrue(battle.TargetSystem.Actives == null);
			battle.MoveTargetUp();
			Assert.IsTrue(battle.ActionMenu.ActiveAction.Name == "Items");
			battle.MoveTargetDown();
			battle.Execute();
			Assert.IsTrue(battle.OptionsListMenu.Showing);
			//0:11 jump option is chosen, and ablities are shown. Jump and power jump are shown
			Assert.IsTrue(battle.ActiveOptionName == "Jump");

			Assert.IsTrue(battle.TargetSystem.Actives == null);
			battle.Execute();
			//0:12 targeting system is shown with magikoop selected
			Assert.IsTrue(battle.TargetSystem.Actives[0] == FuzzieA);
			//0:14 cancel is pressed
			battle.Cancel();
			battle.MoveTargetDown();
			battle.MoveTargetUp();
			battle.MoveTargetDown();
			Assert.IsTrue(battle.ActiveOptionName == "Power Jump", $"{battle.ActiveOptionName}");
			//0:20
			battle.Cancel();
			Assert.IsTrue(battle.ActionMenu.ActiveAction.Name == "Jump");
			battle.MoveTargetUp();
			Assert.IsTrue(battle.ActionMenu.ActiveAction.Name == "Items");
			battle.MoveTargetUp();
			Assert.IsTrue(battle.ActionMenu.ActiveAction.Name == "Strategies", $"should of been Strategies, {battle.ActionMenu.ActiveAction.Name}");
			battle.Execute();
			Assert.IsTrue(battle.ActiveOptionName == "Change Member", $"active name = {battle.ActiveOptionName}");
			battle.MoveTargetDown();
			Assert.IsTrue(battle.ActiveOptionName == "Do Nothing");
			battle.MoveTargetUp();
			Assert.IsTrue(battle.ActiveOptionName == "Change Member");
			battle.Execute();
			Assert.IsTrue(battle.SubOptionsListMenu.Active.Name == "Goombario");
			battle.Execute();
			Assert.IsTrue(!battle.SubOptionsListMenu.Showing);
			Assert.IsTrue(battle.Heroes[1] is Goombario, battle.Heroes[1].ToString());
			Assert.IsTrue(battle.TurnSystem.Active is Goombario, battle.TurnSystem.Active.ToString());
			Assert.IsTrue(battle.ActionMenu.ActiveAction.Name == "Abilities", battle.ActionMenu.ActiveAction.Name);
			battle.Execute();
			battle.MoveTargetDown();
			Assert.IsTrue(battle.ActiveOptionName == "Tattle");

			Battle.Battle.ActionCommandCenter.AddFailedPress();
			Battle.Battle.ActionCommandCenter.AddFailedPress();
			Battle.Battle.ActionCommandCenter.AddSuccessfulPress();
			Battle.Battle.ActionCommandCenter.AddFailedPress();

			battle.Execute();
			battle.Execute();
			Assert.IsTrue(battle.TextBubbleSystem.Showing);
			battle.Execute();
			battle.Execute();
			battle.Execute();
			battle.Execute();
			Assert.IsTrue(battle.TurnSystem.LastActive == "Fuzzie", $"{battle.TurnSystem.LastActive}");
			//1:05 the third fuzzie gets their attacked blocked by mario
			Assert.IsTrue(Mario.Health.CurrentValue == 7, $"hp = {Mario.Health.CurrentValue}");
			//Flag is shwn when it returns back to marios turn;
			Assert.IsTrue(battle.ActionMenu.ActiveAction.Name == "Strategies", $"should of been Strategies, {battle.ActionMenu.ActiveAction.Name}");
			//108 mario swaps out goombario for kooper
			battle.Execute();
			battle.Execute();
			battle.MoveTargetDown();
			Assert.IsTrue(battle.SubOptionsListMenu.Active.Name == "Kooper",battle.SubOptionsListMenu.Active.Name);
			//then its koopers turn
			battle.Execute();
			//1:10 action menu is showing abilites
			Assert.IsTrue(battle.TurnSystem.Active is Kooper, battle.TurnSystem.Active.ToString()); 
			Assert.IsTrue(battle.ActionMenu.ActiveAction.Name == "Abilities",battle.ActionMenu.ActiveAction.Name);
			//kooper uses power shell and its every fuzzie for 2
			battle.Execute();
			battle.MoveTargetDown();
			Assert.IsTrue(battle.OptionsListMenu.Active.Name == "Power Shell",battle.OptionsListMenu.Active.Name);
			
			battle.Execute();

			Assert.IsTrue(battle.TargetSystem.Actives != null, "target system actives is null");
			Assert.IsTrue(battle.TargetSystem.Actives.Length > 0,$"active targets length = {battle.TargetSystem.Actives.Length}");
			Assert.IsTrue(battle.TargetSystem.Actives.ToList().ContainsAllItems(battle.Enemies),
				$"{ string.Join(",", battle.TargetSystem.Actives.ToList())}" +
				$"{string.Join(", ", battle.Enemies.ToList())}");
			Battle.Battle.ActionCommandCenter.AddFailedPress();
			Battle.Battle.ActionCommandCenter.AddSuccessfulPress();
			Battle.Battle.ActionCommandCenter.AddSuccessfulPress();
			Battle.Battle.ActionCommandCenter.AddFailedPress();
			battle.Execute();
			Console.WriteLine($"{string.Join(", ", battle.Enemies.ToList())}");
			Assert.IsTrue(FuzzieA.Health.CurrentValue == 2, $"FuzzieA hp = {FuzzieA.Health.CurrentValue}");
			Assert.IsTrue(FuzzieB.Health.CurrentValue == 1, $"FuzzieB hp = {FuzzieB.Health.CurrentValue}");
			Assert.IsTrue(FuzzieC.Health.CurrentValue == 1);
			Assert.IsTrue(FuzzieD.Health.CurrentValue == 2);
			//then its the fuzzies' turn the first one hits mario and heals 1

			//1:24 the second fuzzie attacks mario and gets its attack blocked, keeping it at 1 hp
			//third one gets blocked too
			//4th onehits mario and heals itsel for 1hp, so now fuziea = 2, and fuzzie b = 1, c =1, and fuzzie d = 2 hp
			//1:33 its mario's turn again and its showing flag/strategies
			Assert.IsTrue(battle.ActionMenu.ActiveAction.Name == "Strategies", $"should of been Strategies, {battle.ActionMenu.ActiveAction.Name}");
			//mario scrolls down to hammer and gits the first fuzzie, it dies
			battle.MoveTargetDown();
			battle.MoveTargetDown();
			battle.MoveTargetDown();
			battle.Execute();
			battle.Execute();
			//1:53 kiooper then shell toss's the second fuzzie for 2, killing it 
			Assert.IsTrue(battle.TurnSystem.Active is Kooper, $"turn is {battle.TurnSystem.Active}");
			battle.Execute();
			battle.Execute();
			Battle.Battle.ActionCommandCenter.AddSuccessfulPress();
			Battle.Battle.ActionCommandCenter.AddFailedPress();
			Battle.Battle.ActionCommandCenter.AddFailedPress();
			
			battle.Execute();
			//fuzzie c heal attacks mario for 1,
			//fuzzie d tries to attack mario for 1 but gets blocked\
			Assert.IsTrue(FuzzieC.Health.CurrentValue == 2, $"FuzzieC hp = {FuzzieC.Health.CurrentValue}");
			Assert.IsTrue(FuzzieD.Health.CurrentValue == 2, $"FuzzieD hp = {FuzzieD.Health.CurrentValue}");
			//mario hammers the fuzzie c, it dies
			//kooper shell tosses fuzzie d and it dies
			//gg

		}








	}
	public static class LinqExtras // Or whatever
	{
		public static bool ContainsAllItems<T>(this IEnumerable<T> a, IEnumerable<T> b)
		{
			return !b.Except(a).Any();
		}
	}
}

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
	public class boss_battle_fuzzies_as_serializable
	{
		private Mario Mario;
		private Kooper Kooper;

		private Fuzzie FuzzieA;

		internal Fuzzie FuzzieB { get; private set; }
		internal Fuzzie FuzzieC { get; private set; }
		internal Fuzzie FuzzieD { get; private set; }

		private IBattleCommander battle;
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

			
			battle = new BattleCommander(new Battle.Battle(new List<Hero> { Mario, Kooper }, enemies, bubbleSystem));
		 
		   
			//battle.Start();

		}
		
		



		[Test]
		public void boss_battle_fuzzies_1()
		{
			Assert.IsTrue(!battle.IsStarted(), $"battle state = {battle.State.ToString()}");
			//0:10
			battle.Start();
			//menu is shown with jump selected
			
			battle.MoveTargetUp();
			battle.MoveTargetDown();
			battle.Execute();
			
			//0:11 jump option is chosen, and ablities are shown. Jump and power jump are shown
			
			battle.Execute();
			//0:12 targeting system is shown with magikoop selected
			//0:14 cancel is pressed
			battle.Cancel();
			battle.MoveTargetDown();
			battle.MoveTargetUp();
			battle.MoveTargetDown();
			
			//0:20
			battle.Cancel();

			battle.MoveTargetUp();
		
			battle.MoveTargetUp();
			battle.Execute();
			
			battle.MoveTargetDown();
			
			battle.MoveTargetUp();
			
			battle.Execute();
			
			battle.Execute();
			battle.Execute();
			battle.MoveTargetDown();

			Battle.Battle.ActionCommandCenter.AddFailedPress();
			Battle.Battle.ActionCommandCenter.AddFailedPress();
			Battle.Battle.ActionCommandCenter.AddSuccessfulPress();
			Battle.Battle.ActionCommandCenter.AddFailedPress();

			battle.Execute();
			battle.Execute();
			
			battle.Execute();
			battle.Execute();
			battle.Execute();
			battle.Execute();
			
			//1:05 the third fuzzie gets their attacked blocked by mario
			Assert.IsTrue(Mario.Health.CurrentValue == 7, $"hp = {Mario.Health.CurrentValue}");
			//Flag is shwn when it returns back to marios turn;
			//108 mario swaps out goombario for kooper
			battle.Execute();
			battle.Execute();
			battle.MoveTargetDown();
			//then its koopers turn
			battle.Execute();
			//1:10 action menu is showing abilites
			
			//kooper uses power shell and its every fuzzie for 2
			battle.Execute();
			battle.MoveTargetDown();
			
			
			battle.Execute();
			
			Battle.Battle.ActionCommandCenter.AddFailedPress();
			Battle.Battle.ActionCommandCenter.AddSuccessfulPress();
			Battle.Battle.ActionCommandCenter.AddSuccessfulPress();
			Battle.Battle.ActionCommandCenter.AddFailedPress();
			battle.Execute();
			Assert.IsTrue(FuzzieA.Health.CurrentValue == 2, $"FuzzieA hp = {FuzzieA.Health.CurrentValue}");
			Assert.IsTrue(FuzzieB.Health.CurrentValue == 1, $"FuzzieB hp = {FuzzieB.Health.CurrentValue}");
			Assert.IsTrue(FuzzieC.Health.CurrentValue == 1);
			Assert.IsTrue(FuzzieD.Health.CurrentValue == 2);
			//then its the fuzzies' turn the first one hits mario and heals 1

			//1:24 the second fuzzie attacks mario and gets its attack blocked, keeping it at 1 hp
			//third one gets blocked too
			//4th onehits mario and heals itsel for 1hp, so now fuziea = 2, and fuzzie b = 1, c =1, and fuzzie d = 2 hp
			//1:33 its mario's turn again and its showing flag/strategies
			//1:45 mario scrolls down to hammer and gits the first fuzzie, it dies
			battle.MoveTargetDown();
			battle.MoveTargetDown();
			battle.MoveTargetDown();
			battle.Execute();
			Console.WriteLine($"****1:45 Mario kills the first fuzzie");
			Battle.Battle.ActionCommandCenter.AddSuccessfulPress();
			battle.Execute();
			FuzzieA.AssertIsDead();
			//1:53 kiooper then shell toss's the second fuzzie for 2, killing it 
			battle.Execute();
			battle.Execute();
			Battle.Battle.ActionCommandCenter.AddSuccessfulPress();
			Battle.Battle.ActionCommandCenter.AddFailedPress();
			Battle.Battle.ActionCommandCenter.AddFailedPress();
			Console.WriteLine($"**************Kooper is attacking at 1:53*(*************, FuzzieA is at {FuzzieA.Health.CurrentValue}, FuzzieB is at {FuzzieB.Health.CurrentValue}");
			battle.Execute();
			//1:57 fuzzie c heal attacks mario for 1,
			//2:02 fuzzie d tries to attack mario for 1 but gets blocked\
			Assert.IsTrue(FuzzieC.Health.CurrentValue == 2, $"FuzzieC hp = {FuzzieC.Health.CurrentValue}");
			Assert.IsTrue(FuzzieD.Health.CurrentValue == 2, $"FuzzieD hp = {FuzzieD.Health.CurrentValue}");
			//2:03 Hud appears for Mario, hammer is shown
			battle.Execute();
			Battle.Battle.ActionCommandCenter.AddSuccessfulPress();
			battle.Execute();
			//2:08 Mario hammers the fuzzie c with successful Action Command, it dies
			FuzzieC.AssertIsDead();
			//2:11 Kooper's turn, hud shown with Ablities shown
			
			//2:12 Kooper's Abilities options are shown
			
			//2:15 kooper shell tosses fuzzie d with successful Action COmmand and it dies
			battle.Battle.AssertExecuteToShowOption("Shell Toss");
			battle.Execute();
			Battle.Battle.ActionCommandCenter.AddSuccessfulPress();
			battle.Execute();
			FuzzieD.AssertIsDead();
			//gg
			
		}








	}

	internal interface IBattleCommander
	{
		bool IsStarted();
		BattleState State { get; }
		Battle.Battle Battle { get; }
		void Start();
		void MoveTargetUp();
		void MoveTargetDown();
		void Execute();
		void Cancel();
	}

	public static class CommanderExtensions
	{
		
		
		
	}
	
}

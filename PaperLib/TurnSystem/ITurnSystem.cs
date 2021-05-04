using System;
using System.Collections.Generic;
using Battle;
using Enemies;
using Heroes;

namespace Tests
{
    public interface ITurnSystem
    {
        object Active { get; }
        event EventHandler OnSwapped;

        Action<object> OnActiveChanged { get; set; }

        List<string> History { get; }
        string LastActive { get; }

        void Load(List<Hero> heroes, List<Enemy> enemies);
        void Swap();
        void End();
        void ExcuteTurn();
        void Cleanup();
    }
}
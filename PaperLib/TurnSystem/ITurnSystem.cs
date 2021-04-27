using System;
using System.Collections.Generic;
using Enemies;
using Heroes;

namespace Tests
{
    public interface ITurnSystem
    {
        object Active { get; }

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
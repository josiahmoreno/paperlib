using System;
using Enemies;
using Battle;
using System.Linq;
using Attributes;

namespace Enemies
{
    public abstract class EnvironmentTarget : Goomba
    {
        protected EnvironmentTarget(IHealth healthImpl): base(healthImpl)
        {
        }

        public abstract void ExecuteEffect(Battle.Battle battle);
    }
}
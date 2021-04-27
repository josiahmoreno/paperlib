using Attributes;
using Enemies;

namespace Enemies
{
    public class BlueGoomba : Goomba
    {

        public BlueGoomba() : base(new HealthImpl(6))
        {

        }
        public BlueGoomba(int currentHealth) : base(new HealthImpl(currentHealth, 6))
        {
            
        }
    }
}
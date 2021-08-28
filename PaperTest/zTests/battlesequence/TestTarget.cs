using PaperLib.Sequence;
using System;

namespace Tests.battlesequence
{
    internal class TestTarget: IMovementTarget
    {
        float x;
        float y;
        float z;
        public TestTarget()
        {
            Position = new Tuple<float, float, float>(x,y,z);
        }

        public TestTarget(Tuple<float, float, float> position)
        {
            Position = position;
        }

        public TestTarget(float x,float y, float z)
        {
            this.x = x;
            this.y = y;
            this.z = z;
        }

        public Tuple<float, float, float> Position { get; private set; }

        public override string ToString()
        {
            return $"TestTarget - position {{{x},{y},{z}}}";
        }
    }
}
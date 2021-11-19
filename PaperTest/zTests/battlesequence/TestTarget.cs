using PaperLib.Sequence;
using System;

namespace Tests.battlesequence
{
    internal class TestTarget: IPositionable
    {
        public float x { get; set; }
        public float y { get; set; }
        public float z { get; set; }
        public TestTarget()
        {
           
        }


        public TestTarget(float x,float y, float z)
        {
            this.x = x;
            this.y = y;
            this.z = z;
        }

   
        public IPositionable CopyPosition()
        {
            return new TestTarget(x,y,z);
        }

        public override string ToString()
        {
            return $"TestTarget - position {{{x},{y},{z}}}";
        }
    }
}
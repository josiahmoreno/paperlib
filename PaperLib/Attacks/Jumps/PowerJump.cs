using Attacks;
using Heroes;

namespace Attacks
{
    public class PowerJump : Jump
    {


        //public  override PowerModifier { get; internal set; } = 2;
        //public override Attacks.Attacks Identifier { get => Attacks.Attacks.PowerJump; set; }
        public override Attacks Identifier { get => Attacks.PowerJump; }
        public override int PowerModifier { get =>2; }
    }
}
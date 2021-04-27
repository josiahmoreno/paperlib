using Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace Enemies
{
    public class SpikedGoomba: Goomba
    {
        public SpikedGoomba() : base(new Spike())
        {

        }
    }
}

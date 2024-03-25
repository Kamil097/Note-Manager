using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pong.Tui
{
    public class Platform
    {
        public int UpperCorner => Center - ArmLength;
        public int LowerCorner => Center + ArmLength;
        public int Center;
        public int ArmLength = 2;
        public Platform(int center)
        {
            this.Center = center;
        }
        public Platform(int center, int length)
        {
            this.Center = center;
            this.ArmLength = length;
        }
    }
}

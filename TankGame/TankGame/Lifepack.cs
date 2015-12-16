using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TankGame
{
    class Lifepack : Block
    {
        int time_to_disappear;

        public Lifepack()
        {

        }

        public void setTime(int time)
        {
            this.time_to_disappear = time;
        }
        public int getTime()
        {
            return this.time_to_disappear;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TankGame
{
    class Coinpack:Block
    {
        int positionX;
        int positionY;
        int time_to_disappear;
        int amount;

        public Coinpack()
        {

        }

        public void setTime(int time)
        {
            this.time_to_disappear = time;
        }
        public void setAmount(int amount)
        {
            this.amount = amount;
        }
        public int getTime()
        {
            return this.time_to_disappear;
        }
    }

    }
}

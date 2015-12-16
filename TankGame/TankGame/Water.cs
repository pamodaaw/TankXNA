using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TankGame
{
    class Water:Block
    {
        private int water_no;
        public Water()
        {

        }

        public void setNo(int num)
        {
            this.water_no = num;
        }

    }
}

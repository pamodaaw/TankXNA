using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TankGame
{
    class Stone : Block
    {
        private int stone_no;

        public Stone()
        {

        }

        public void setNo(int num)
        {
            this.stone_no = num;
        }

    }
}

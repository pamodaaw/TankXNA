using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TankGame
{
    class Brick: Block
    {
        private int brick_no;
        private int damage;


        public Brick()
        {
            this.damage = 0;
        }

        public void setNo(int num)
        {
            this.brick_no = num;
        }

        public void setDamage(int damage)
        {
            this.damage = damage;
        }


        public int getNo()
        {
            return this.brick_no;
        }

        public int getDamage()
        {
            return this.damage;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TankGame
{
    class Block
    {
        private int positionX;
        private int positionY;
        public Block(int x, int y)
        {
            this.positionX = x;
            this.positionY = y;
        }

        public Block() { }
        public void setPostion(int x, int y)
        {
            this.positionX = x;
            this.positionY = y;
        }
        public int getPositionX(){
            return this.positionX;
        }
         
        public int setPositionY()
        {
            return this.positionY;
        }
    }
}

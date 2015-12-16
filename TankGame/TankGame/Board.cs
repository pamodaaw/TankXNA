using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TankGame
{

    class Board
    {

        public Player[] players;
        public Block[,] blocks;

        public Board()
        {
            players = new Player[5];
            blocks = new Block[10, 10];
            for (int a = 0; a < 10; a++)
            {
                for (int b = 0; b < 10; b++)
                {
                    blocks[a, b] = new Block(a, b);
                }
            }

        }

    }
}

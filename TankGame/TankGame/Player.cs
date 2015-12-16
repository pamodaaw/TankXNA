using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TankGame
{
    class Player : Block
    {
        private String name;
        private int isShot;
        private int health;
        private int coins;
        private int points;
        private int direction;

        public Player(String name)
        {
            this.name = name;
            isShot = 0;
            health = 0;
            coins = 0;
            points = 0;
        }

        

        public void setLife(int isShot)
        {
            this.isShot = isShot;
        }

        public void setPoints(int points)
        {
            this.points = points;
            Console.WriteLine("Point ==> " + points);
        }
        public void setHealth(int health)
        {
            this.health = health;
            Console.WriteLine("Health ==> " + health);
        }
        public void setCoins(int coins)
        {
            this.coins = coins;
            Console.WriteLine("Coins ==> " + coins);
        }

        public void setDirection(int dir)
        {
            this.direction = dir;
        }



    }
}

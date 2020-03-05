using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace DeoNarayanAsquarioSummative
{
    class Food
    {
        //variables in my class
        public SolidBrush foodBrush;
        public int x, y, size;

        //player speeds
        int speedAsInt = 5;

        //random generator
        Random randGen = new Random();

        public Food(int _x, int _y, int _size)
        {
            x = _x;
            y = _y;
            size = _size;

            //1/7 chance
            int randValue = randGen.Next(1, 8);

            //makes food random colours
            if (randValue == 1)
            {
                foodBrush = new SolidBrush(Color.Red);
            }
            else if (randValue == 2)
            {
                foodBrush = new SolidBrush(Color.Orange);
            }
            else if (randValue == 3)
            {
                foodBrush = new SolidBrush(Color.Yellow);
            }
            else if (randValue == 4)
            {
                foodBrush = new SolidBrush(Color.Blue);
            }
            else if (randValue == 5)
            {
                foodBrush = new SolidBrush(Color.Purple);
            }
            else if (randValue == 6)
            {
                foodBrush = new SolidBrush(Color.LightGreen);
            }
            else if (randValue == 7)
            {
                foodBrush = new SolidBrush(Color.Pink);
            }
        }
        public void Move(string direction, GameScreen gs)
        {
            //if movement speed isn't already 1
            if ((-0.014 * size) + 5>1)
            {
                //rounded movement speed relation to size
                double speed = Math.Round((-0.014 * size) + 5, 1);
                speedAsInt = Convert.ToInt32(speed);
            }

            //move players at movement speed but not off screen
            if (x > 0)
            {
                if (direction == "left")
                {
                    x = x - speedAsInt;
                }
            }

            if (x < gs.Width - size)
            {
                if (direction == "right")
                {
                    x = x + speedAsInt;
                }
            }

            if (y > 0)
            {
                if (direction == "up")
                {
                    y = y - speedAsInt;
                }
            }

            if (y < gs.Height - size)
            {
                if (direction == "down")
                {
                    y = y + speedAsInt;
                }
            }
        }

        public Boolean Eaten(Food p)
        {
            //rectangle on middle of other player and on the player
            Rectangle pCenterRec = new Rectangle(p.x + p.size/2, p.y+p.size/2, 1,1);
            Rectangle pRec = new Rectangle(x, y, size, size);

            //tells GameScreen if player reaches middle point of other player
            if (pCenterRec.IntersectsWith(pRec))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public Boolean Collision(Food p)
        {
            //rectangles on food and player
            Rectangle pRec = new Rectangle(p.x, p.y, p.size, p.size);
            Rectangle foodRec = new Rectangle(x, y, size, size);

            //tells GameScreen if collision occurs
            if (pRec.IntersectsWith(foodRec))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}

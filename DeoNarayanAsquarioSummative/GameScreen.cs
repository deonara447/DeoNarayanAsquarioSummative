using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
using System.Media;

namespace DeoNarayanAsquarioSummative
{
    public partial class GameScreen : UserControl
    {
        //MainScreen
        MainScreen ms = new MainScreen();

        //random number generator
        Random randGen = new Random();

        //declaring sounds
        SoundPlayer winnerSound = new SoundPlayer(Properties.Resources.winnerSound);
        SoundPlayer startSound = new SoundPlayer(Properties.Resources.startSound);

        //Movement booleans for p1 and p2
        Boolean leftArrowDown, rightArrowDown, upArrowDown, downArrowDown;
        Boolean wArrowDown, aArrowDown, sArrowDown, dArrowDown;

        //declaring powerup variables
        int p1StunnedCounter;
        int p2StunnedCounter;
        Boolean p1Inverse;
        Boolean p2Inverse;

        //holds random power up integer
        int powerUpDecider;

        //brushes creating player, powerups, and mines
        SolidBrush redBrush = new SolidBrush(Color.Red);
        SolidBrush blueBrush = new SolidBrush(Color.Blue);
        SolidBrush blackBrush = new SolidBrush(Color.Black);
        SolidBrush mustardBrush = new SolidBrush(Color.FromArgb(192, 192, 0));

        //location and size of food
        int foodX, foodY;
        int foodSize = 3;

        //size of powerup and landmines
        int powerUpSize = 5;

        //lists for food, powerups, mines and for when they are eaten
        List<Food> foodList = new List<Food>();
        List<Food> eatenList = new List<Food>();
        List<Food> powerUpList = new List<Food>();
        List<Food> powerEatenList = new List<Food>();
        List<Food> landmineList = new List<Food>();
        List<Food> landmineEatenList = new List<Food>();

        //making players part of the class
        Food p1;
        Food p2;

        //size of players
        int p1Size, p2Size;
     
        //speed of food generation
        int foodCounter;
        //speed of speed of food generation
        int foodCounterCounter;
        //amount food counter must reach to generate food
        int foodGenerator;

        //to control how long quit label is visible
        int quitCounter;

        public GameScreen()
        {
            InitializeComponent();
            
            //At beginning run this method
            OnStart();

        }
        public void OnStart()
        {
            //play startSound
            startSound.Play();

            //starting player sizes
            p1Size = p2Size = 5;

            //initial location of players relative to size of screen
            p1 = new Food(2 * Screen.PrimaryScreen.WorkingArea.Width / 3, Screen.PrimaryScreen.WorkingArea.Height / 2, p1Size);
            p2 = new Food(Screen.PrimaryScreen.WorkingArea.Width / 3, Screen.PrimaryScreen.WorkingArea.Height / 2, p2Size);

            //beginning food generation speeds
            foodCounter = 0;
            foodCounterCounter = 0;
            foodGenerator = 5;

            //beginning power up variables
            p1StunnedCounter = 100;
            p2StunnedCounter = 100;
            p1Inverse = false;
            p2Inverse = false;

            //make quit label visible
            quitLabel.Visible = true;
            quitCounter = 0;
        }

        private void GameScreen_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            //if key is pressed boolean is true
            switch (e.KeyCode)
            {
                case Keys.Left:
                    leftArrowDown = true;
                    break;
                case Keys.Right:
                    rightArrowDown = true;
                    break;
                case Keys.Up:
                    upArrowDown = true;
                    break;
                case Keys.Down:
                    downArrowDown = true;
                    break;
                case Keys.W:
                    wArrowDown = true;
                    break;
                case Keys.D:
                    dArrowDown = true;
                    break;
                case Keys.A:
                    aArrowDown = true;
                    break;
                case Keys.S:
                    sArrowDown = true;
                    break;
                case Keys.Escape:
                    //end program
                    Application.Exit();
                    break;
            }
        }

        private void GameScreen_KeyUp(object sender, KeyEventArgs e)
        {
            //if keys are released, booleans are false
            switch (e.KeyCode)
            {
                case Keys.Left:
                    leftArrowDown = false;
                    break;
                case Keys.Right:
                    rightArrowDown = false;
                    break;
                case Keys.Up:
                    upArrowDown = false;
                    break;
                case Keys.Down:
                    downArrowDown = false;
                    break;
                case Keys.W:
                    wArrowDown = false;
                    break;
                case Keys.D:
                    dArrowDown = false;
                    break;
                case Keys.A:
                    aArrowDown = false;
                    break;
                case Keys.S:
                    sArrowDown = false;
                    break;
            }
        }

        private void gameLoop_Tick(object sender, EventArgs e)
        {
            //each tick of timer

            //after 100 ticks remove quit label
            if (quitCounter == 100)
            {
                quitLabel.Visible = false;
            }

            //only use quit counter if neeeded
            if (quitLabel.Visible == true)
            {
                quitCounter++;
            }

            //if it is time to generate food
            if (foodCounter == foodGenerator)
            {
                //random locaton on screen
                foodX = randGen.Next(1, this.Width - powerUpSize - 1);
                foodY = randGen.Next(1, this.Height - powerUpSize - 1);

                //rectangles at location of players and potential new food
                Rectangle newFood = new Rectangle(foodX, foodY, foodSize, foodSize);
                Rectangle p1Rec = new Rectangle(p1.x, p1.y, p1.size, p1.size);
                Rectangle p2Rec = new Rectangle(p2.x, p2.y, p2.size, p2.size);

                //if they intersect nothing happens and foodCounter resets
                if (newFood.IntersectsWith(p1Rec) || newFood.IntersectsWith(p2Rec))
                {
                    foodCounter = 0;
                }

                //if they don't intersect
                else
                {
                    //1/50 chance
                    if (randGen.Next(1, 51) == 50)
                    {
                        //1/5 chance
                        if (randGen.Next(1, 6) == 5)
                        {
                            //create mine and reset food counter
                            Food landmine = new Food(foodX, foodY, powerUpSize);
                            landmineList.Add(landmine);
                            foodCounter = 0;
                            foodCounterCounter++;

                            //background colour returns to white
                            this.BackColor = Color.White;
                        }
                        else
                        {
                            //creates powerup and resets food counter
                            Food powerUp = new Food(foodX, foodY, powerUpSize);
                            powerUpList.Add(powerUp);
                            foodCounter = 0;
                            foodCounterCounter++;
                        }
                    }
                    else
                    {
                        //creates food and resets food counter
                        Food f = new Food(foodX, foodY, foodSize);
                        foodList.Add(f);
                        foodCounter = 0;
                        foodCounterCounter++;
                    }
                }
            }

            foodCounter++;

            //after 100 foods, powerups, and mines are placed food generation speed increases
            if (foodCounterCounter == 100 && foodGenerator > 1)
            {
                foodGenerator = foodGenerator - 1;
                foodCounterCounter = 0;
            }

            //if p2 isn't stunned
            if (p2StunnedCounter >= 100)
            {
                //if controls are not inversed, do not inverse controls
                if (p2Inverse == false)
                {

                    if (leftArrowDown)
                    {
                        p2.Move("left", this);
                    }

                    if (rightArrowDown)
                    {
                        p2.Move("right", this);
                    }

                    if (upArrowDown)
                    {
                        p2.Move("up", this);
                    }

                    if (downArrowDown)
                    {
                        p2.Move("down", this);
                    }
                }

                //if controls are inversed, inverse controls
                else
                {
                    if (leftArrowDown)
                    {
                        p2.Move("right", this);
                    }

                    if (rightArrowDown)
                    {
                        p2.Move("left", this);
                    }

                    if (upArrowDown)
                    {
                        p2.Move("down", this);
                    }

                    if (downArrowDown)
                    {
                        p2.Move("up", this);
                    }
                }
            }

            //same idea as p2
            if (p1StunnedCounter >= 100)
            {
                if (p1Inverse == false)
                {
                    if (aArrowDown)
                    {
                        p1.Move("left", this);
                    }

                    if (dArrowDown)
                    {
                        p1.Move("right", this);
                    }

                    if (wArrowDown)
                    {
                        p1.Move("up", this);
                    }

                    if (sArrowDown)
                    {
                        p1.Move("down", this);
                    }
                }
                else
                {
                    if (aArrowDown)
                    {
                        p1.Move("right", this);
                    }

                    if (dArrowDown)
                    {
                        p1.Move("left", this);
                    }

                    if (wArrowDown)
                    {
                        p1.Move("down", this);
                    }

                    if (sArrowDown)
                    {
                        p1.Move("up", this);
                    }
                }
            }

            //eventually results in stunned players no longer stunned
            p1StunnedCounter++;
            p2StunnedCounter++;

            //players eat food
            foreach (Food f in foodList)
            {
                //p2 eats
                if (f.Collision(p2) == true)
                {
                    eatenList.Add(f);
                    //increase size
                    p2.size++;
                }

                //p1 eats
                if (f.Collision(p1) == true)
                {
                    eatenList.Add(f);
                    p1.size++;
                }
            }

            //player hits mine
            foreach (Food l in landmineList)
            {
                //p2 hits mine
                if (l.Collision(p2) == true)
                {
                    landmineEatenList.Add(l);
                    //size reduced
                    p2.size = p2.size / 4;
                }

                //p1 hits mine
                if (l.Collision(p1) == true)
                {
                    landmineEatenList.Add(l);
                    p1.size = p1.size / 4;
                }
            }

            //player hits powerup
            foreach (Food p in powerUpList)
            {
                //p2 hits powerup
                if (p.Collision(p2) == true)
                {
                    //1/4 chance of each power up
                    powerUpDecider = randGen.Next(1, 5);

                    if (powerUpDecider == 1)
                    {
                        //changes background colour
                        this.BackColor = Color.Blue;
                    }

                    else if (powerUpDecider == 2)
                    {
                        //stuns opponent
                        p1StunnedCounter = 0;
                    }

                    else if (powerUpDecider == 3)
                    {
                        //inverses controls
                        p1Inverse = !p1Inverse;
                    }

                    else
                    {
                        //increases size
                        p2.size = p2.size + 25;
                    }

                    powerEatenList.Add(p);
                }

                //p1 hits powerup
                if (p.Collision(p1) == true)
                {
                    powerUpDecider = randGen.Next(1, 5);

                    if (powerUpDecider == 1)
                    {
                        this.BackColor = Color.Red;
                    }
                    else if (powerUpDecider == 2)
                    {
                        p2StunnedCounter = 0;
                    }
                    else if (powerUpDecider == 3)
                    {
                        p2Inverse = !p2Inverse;
                    }
                    else
                    {
                        p1.size = p1.size + 25;
                    }

                    powerEatenList.Add(p);
                }
            }
            
            //remove particles players collided with
            foreach (Food f in eatenList)
            {
                foodList.Remove(f);
            }
            foreach (Food l in landmineEatenList)
            {
                landmineList.Remove(l);
            }
            foreach (Food p in powerEatenList)
            {
                powerUpList.Remove(p);
            }

            //if p1 is bigger
            if (p1.size > p2.size)
            {
                //check to see if p1 ate p2
                if (p1.Eaten(p2) == true)
                {
                    //p1 wins logic

                    //play winnerSound
                    winnerSound.Play();

                    //background colour is white
                    this.BackColor = Color.White;
                    this.Refresh();

                    //stops timer
                    gameLoop.Enabled = false;

                    //1 second pause
                    Thread.Sleep(1000);

                    //goes to MainScreen
                    Form f = this.FindForm();
                    ms.Location = new Point((f.Width - ms.Width) / 2, (f.Height - ms.Height) / 2);
                    f.Controls.Add(ms);
                    f.Controls.Remove(this);

                    //depending on winner
                    ms.Winner(false);
                }
            }
            //p2 is bigger
            else if (p2.size > p1.size)
            {
                //checks to see if p2 
                if (p2.Eaten(p1) == true)
                {
                    //p2 wins logic

                    gameLoop.Enabled = false;

                    winnerSound.Play();

                    this.BackColor = Color.White;
                    this.Refresh();

                    Thread.Sleep(1000);

                    Form f = this.FindForm();
                    ms.Location = new Point((f.Width - ms.Width) / 2, (f.Height - ms.Height) / 2);
                    f.Controls.Remove(this);
                    ms.Size = f.Size;
                    f.Controls.Add(ms);
                    ms.Winner(true);
                    ms.Focus();
                }
            }

            //clear eaten lists
            eatenList.Clear();
            landmineEatenList.Clear();

            //refresh
            Refresh();
        }
        private void GameScreen_Paint(object sender, PaintEventArgs e)
        {
            //draw food, mines, and powerups
            foreach (Food f in foodList)
            {
                e.Graphics.FillRectangle(f.foodBrush, f.x, f.y, f.size, f.size);
            }

            foreach (Food p in powerUpList)
            {
                e.Graphics.FillRectangle(mustardBrush, p.x, p.y, p.size, p.size);
            }

            foreach (Food l in landmineList)
            {
                e.Graphics.FillRectangle(blackBrush, l.x, l.y, l.size, l.size);
            }

            //draw bigger player on top
            if (p1.size < p2.size)
            {
                e.Graphics.FillRectangle(blueBrush, p1.x, p1.y, p1.size, p1.size);
                e.Graphics.FillRectangle(redBrush, p2.x, p2.y, p2.size, p2.size);
            }
            
            if (p1.size >= p2.size)
            {
                e.Graphics.FillRectangle(redBrush, p2.x, p2.y, p2.size, p2.size);
                e.Graphics.FillRectangle(blueBrush, p1.x, p1.y, p1.size, p1.size);
            }
        }
    }
}
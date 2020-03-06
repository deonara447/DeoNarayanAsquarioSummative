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

namespace DeoNarayanAsquarioSummative
{
    public partial class MainScreen : UserControl
    {
        public MainScreen()
        {
            InitializeComponent();
        }

        public void Winner(Boolean redWinner)
        {
            //if red is winner
            if (redWinner == true)
            {
                //display who won on screen
                startLabel.Text = "Red Wins!";
                
                //one and hald second pause
                this.Refresh();
                Thread.Sleep(1500);

                //returns label to original
                startLabel.Text = "Press Green to Play";
            }

            //if blue wins
            else
            {
                startLabel.Text = "Blue Wins!";

                this.Refresh();
                Thread.Sleep(2000);

                startLabel.Text = "Press Green to Play";
            }
        }

        private void GameScreen_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            switch (e.KeyCode)
            {
                //if space key is pressed
                case Keys.Space:
                    //go to game screen
                    GameScreen gs = new GameScreen();
                    Form f = this.FindForm();
                    f.Controls.Remove(this);
                    gs.Size = f.Size;
                    f.Controls.Add(gs);
                    gs.Focus();
                    break;
                case Keys.Escape:
                    //close program
                    Application.Exit();
                    break;
            }
        }
    }
}
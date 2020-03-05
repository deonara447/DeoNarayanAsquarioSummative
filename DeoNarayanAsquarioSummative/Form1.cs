/*
 * Description: Asquar.io is a PvP game where the object is to consume
 * the other player. Power-ups give you abilites or hurts your opponent. 
 * Watch out for landmines!
 * Author: Deo Narayan         
 * Date: March 5, 2020    
 */

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DeoNarayanAsquarioSummative
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //go to MainScreen
            MainScreen ms = new MainScreen();
            this.Controls.Add(ms);
            ms.Location = new Point((this.Width - ms.Width) / 2, (this.Height - ms.Height) / 2);

            //hide cursor
            Cursor.Hide();
        }

       
    }
}

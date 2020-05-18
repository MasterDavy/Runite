using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Runite
{
    public partial class Game : Form
    {
        public Game()
        {
            InitializeComponent();     
        }
        public void Game_Load(object sender, EventArgs e)
        {
            this.Activate();

            Button button1 = new Button();
            button1.Size = new Size(100, 50);
            button1.Location = new Point(this.Width - 175, this.Height * 5 / 100);
            button1.Text = "REPLAY";
            button1.TextAlign = ContentAlignment.MiddleCenter;
            button1.BackColor = Color.Violet;
            button1.FlatStyle = FlatStyle.Flat;
            button1.Font = new Font("Bauhaus 93",16);
            button1.Click += Button1_Click;
            this.Controls.Add(button1);

            Button button2 = new Button();
            button2.Size = new Size(100, 50);
            button2.Location = new Point(50, this.Height * 5 / 100);
            button2.Text = "MENU";
            button2.TextAlign = ContentAlignment.MiddleCenter;
            button2.BackColor = Color.Violet;
            button2.FlatStyle = FlatStyle.Flat;
            button2.Font = new Font("Bauhaus 93", 16);
            button2.Click += Button2_Click;
            this.Controls.Add(button2);

            Engine.Start();
            timer1.Enabled = true;
        }

        public void Button1_Click(object sender, EventArgs e)
        {
           (sender as Button).Location = new Point(this.Width - 175, ActiveForm.Height * 5 / 100);
            Engine.Clear();
            Engine.Start();
            timer1.Start();
        }

        public void Button2_Click(object sender, EventArgs e)
        {
            ActiveForm.Close();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            Engine.progbar.PerformStep();
            Engine.label2.Text = Convert.ToString(Engine.progbar.Value);
            if (Engine.progbar.Value == 0)
            {              
                timer1.Stop();
                Engine.label2.Text = "TIME IS UP!";
                MessageBox.Show("CONGRATULATIONS\nSCORE : " + Engine.score.ToString());
            }
        }   
    }
}

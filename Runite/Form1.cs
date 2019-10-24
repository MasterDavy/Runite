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
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();     
        }
        public void Form1_Load(object sender, EventArgs e)
        {
            Button button1 = new Button();
            button1.Size = new Size(150, 75);
            button1.Location = new Point(this.Width - 200, 100);
            button1.Text = "PLAY";
            button1.Click += Button1_Click;
            this.Controls.Add(button1);
        }

        public void Button1_Click(object sender, EventArgs e)
        {
           (sender as Button).Location = new Point(this.Width - 200, 100);
            Engine.Clear();
            Engine.Start();
        }
    }
}

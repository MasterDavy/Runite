﻿using System;
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
    public partial class Rules : Form
    {
        public Rules()
        {
            InitializeComponent();
        }

        private void Rules_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            ActiveForm.Close();           
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;

namespace Runite
{
    public class Tile: PictureBox
    {
        public int line, col;
        public bool Empty;
        public Rune currentRune;
        public bool Selected;
        public Tile(int i,int j)
        {
            this.line = i;
            this.col = j;
            this.Empty = true;
            this.BackColor = Color.Empty;
            this.Selected = false;
        }
    }
}

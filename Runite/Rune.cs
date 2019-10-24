using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;

namespace Runite
{
    public class Rune
    {
        public string name;
        public Color color;
        public Rune(int random)
        {
            if (random >= 0 && random < 23)
            {
                this.name = "FireRune";
                this.color = Color.Red;
            }
            if (random >= 23 && random < 46)
            {
                this.name = "WaterRune";
                this.color = Color.Blue;
            }
            if (random >= 46 && random < 69)
            {
                this.name = "WindRune";
                this.color = Color.Green;
            }
            if (random >= 69 && random < 92)
            {
                this.name = "ThunderRune";
                this.color = Color.Yellow;
            }
            if (random >= 92 && random < 96)
            {
                this.name = "BombRune";
                this.color = Color.Black;
            }
            if (random >= 96 && random < 100)
            {
                this.name = "SlashRune";
                this.color = Color.Violet;
            }
        }
        
    }
}

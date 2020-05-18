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
    public static class Engine
    {
        public static Tile[,] tiles =new Tile[15,20];
        public static Random shuffle = new Random();
        public static List<Tile> selected = new List<Tile>();
        public static bool engaged = false;
        public static Tile lastTile;
        public static string currentType;
        public static int score = 0;
        public static Label label1 = new Label();
        public static Label label2 = new Label();
        public static ProgressBar progbar = new ProgressBar();
        public static void Start()
        {
            int x = Game.ActiveForm.Width, y = Game.ActiveForm.Height;
            for (int i = 0; i < 15; i++)
                for (int j = 0; j < 10; j++)
                {
                    tiles[i, j] = new Tile(i, j);
                    tiles[i, j].Size = new Size(y / 20, y / 20);
                    tiles[i, j].Location = new Point(j * (y / 20) + x * 25 / 100, i * (y / 20) + y * 5 / 100);
                    Game.ActiveForm.Controls.Add(tiles[i, j]);
                    tiles[i, j].currentRune = new Rune(shuffle.Next(100));
                    tiles[i, j].BackColor = tiles[i, j].currentRune.color;
                    tiles[i, j].BorderStyle = BorderStyle.FixedSingle;
                    tiles[i, j].Empty = false;
                    tiles[i, j].MouseDown += MouseHover1;
                    tiles[i, j].MouseEnter += MouseEnter1;
                    tiles[i, j].MouseUp += MouseUp1;
                }
            label1.Size = new Size(150, 75);
            label1.Location = new Point(x - 175, 200);
            label1.Text = "SCORE:\n0";
            label1.Font = new Font("Bauhaus 93", 16);
            label1.ForeColor = Color.Violet;
            Game.ActiveForm.Controls.Add(label1);
            progbar.Size = new Size((y/20)*10,30);
            progbar.Location = new Point(x*25/100, y-100);
            progbar.Maximum = 300;
            progbar.Minimum = 0;
            progbar.Step = -1;
            progbar.Value = 300;
            progbar.Style = ProgressBarStyle.Continuous;
            Game.ActiveForm.Controls.Add(progbar);
            label2.Size = new Size(150, 75);
            label2.Location = new Point(x - 175, y-100);
            label2.Text = "300";
            label2.Font = new Font("Bauhaus 93", 16);
            label2.ForeColor = Color.Green;
            Game.ActiveForm.Controls.Add(label2);           
        }
        private static void MouseEnter1(object sender, EventArgs e)
        {
            if (engaged)
                if ((sender as Tile).Selected)
                    Deselect();
                else
                    Select((sender as Tile));
        }

        private static void MouseUp1(object sender, MouseEventArgs e)
        {
            if (engaged)
                if (selected.Count >= 3)
                {
                    SpecialSelections();
                    Refill();
                }
                else
                    Disengage();
            
        }

        private static void MouseHover1(object sender, EventArgs e)
        {
                Engage((sender as Tile));
        }

        public static void Clear()
        {
            for (int i = 0; i < 15; i++)
                for (int j = 0; j < 10; j++)
                    Game.ActiveForm.Controls.Remove(tiles[i, j]);      
        }
        public static void Engage(Tile tile)
        {
            engaged = true;
            selected.Add(tile);
            tile.Selected = true;
            lastTile = tile;
            tile.BorderStyle =BorderStyle.Fixed3D;
            if (tile.currentRune.name != "BombRune" && tile.currentRune.name != "SlashRune")
                currentType = tile.currentRune.name;          
        }
        public static void Select(Tile tile)
        {
            if (tile.currentRune.name == currentType || tile.currentRune.name == "BombRune" || tile.currentRune.name == "SlashRune" || currentType==null)
                if (InRange(lastTile,tile))
                {
                    selected.Add(tile);
                    tile.Selected = true;
                    lastTile = tile;
                    tile.BorderStyle = BorderStyle.Fixed3D;
                    if (tile.currentRune.name != "BombRune" && tile.currentRune.name != "SlashRune")
                        currentType = tile.currentRune.name;
                }
                else
                    Disengage();
            else
                Disengage();
        }
        public static void Refill()
        {
            foreach (Tile t in selected)
                t.BackColor = Color.Orange;
            Form.ActiveForm.Refresh();
            System.Threading.Thread.Sleep(250);
            foreach (Tile t in selected)
            {
                t.Empty = true;
                t.BackColor = Color.Empty;
            }
            for (int i = 14; i > 0; i--)
            {
                for (int j = 0; j < 10; j++)
                    if (tiles[i, j].Empty)
                        do
                        {
                            for (int k = i; k > 0; k--)
                            {
                                tiles[k, j].currentRune = tiles[k - 1, j].currentRune;
                                if (!tiles[k - 1, j].Empty)
                                {
                                    tiles[k, j].Empty = false;
                                    tiles[k, j].BackColor = tiles[k, j].currentRune.color;
                                }
                                else
                                {
                                    tiles[k, j].BackColor = Color.Empty;
                                    tiles[k, j].Empty = true;
                                }
                            }
                            Form.ActiveForm.Refresh();
                            System.Threading.Thread.Sleep(50);
                            tiles[0, j].Empty = false;
                            tiles[0, j].currentRune = new Rune(shuffle.Next(100));
                            tiles[0, j].BackColor = tiles[0, j].currentRune.color;
                            tiles[0, j].BorderStyle = BorderStyle.FixedSingle;
                        } while (tiles[i, j].Empty);
                
            }
            for (int j = 0; j < 10; j++)
            {
                if(tiles[0,j].Empty)
                {
                    tiles[0, j].Empty = false;
                    tiles[0, j].currentRune = new Rune(shuffle.Next(100));
                    tiles[0, j].BackColor = tiles[0, j].currentRune.color;
                }
            }
            Form.ActiveForm.Refresh();
            score += selected.Count * selected.Count * 10;
            Disengage();
            label1.Text="SCORE:\n"+Convert.ToString(score);
        }
        public static bool InRange(Tile a, Tile b)
        {
            int i = a.line, j = a.col, i2 = b.line, j2 = b.col;  
            if ((i2 == i + 1 && j2 == j)   || (i2 == i && j2 == j - 1) || (i2 == i && j2 == j + 1) || (i2 == i -1 && j2 == j))
                    return true;
            return false;
        }
        public static bool InRangeBomb(Tile a, Tile b)
        {
            int i = a.line, j = a.col, i2 = b.line, j2 = b.col;
            if ((i2 == i + 1 && j2 == j) || (i2 == i + 1 && j2 == j -1) || (i2 == i + 1 && j2 == j + 1) || (i2 == i && j2 == j - 1) || (i2 == i && j2 == j + 1) || (i2 == i - 1 && j2 == j) || (i2 == i - 1 && j2 == j + 1) || (i2 == i - 1 && j2 == j - 1))
                return true;
            return false;
        }
        public static bool InRangeSlash(Tile a, Tile b)
        {
            int i = a.line, j = a.col, i2 = b.line, j2 = b.col;
            if (i2==i||j2==j)
                return true;
            return false;
        }
        public static void Disengage()
        {
            engaged = false;
            foreach (Tile t in selected)
            {
                t.Selected = false;
                t.BorderStyle = BorderStyle.FixedSingle;
            } 
            selected.Clear();
            currentType = null;
        }
        public static void Deselect()
        {
            lastTile.Selected = false;
            lastTile.BorderStyle = BorderStyle.FixedSingle;
            selected.Remove(lastTile);
            lastTile = selected[selected.Count-1];          
        }
        public static void SpecialSelections()
        {
            foreach (Tile t in selected)
            {
                
                if (t.currentRune.name == "BombRune")
                    for (int i = 0; i < 15; i++)
                        for (int j = 0; j < 10; j++)
                            if (!tiles[i,j].Selected && InRangeBomb(t,tiles[i,j]))
                                 tiles[i,j].Selected = true;  
                if(t.currentRune.name == "SlashRune")
                    for (int i = 0; i < 15; i++)
                        for (int j = 0; j < 10; j++)
                            if (!tiles[i, j].Selected && InRangeSlash(t, tiles[i, j]))
                                tiles[i, j].Selected = true;                                         
            }
            selected.Clear();
            for (int i = 0; i < 15; i++)
                for (int j = 0; j < 10; j++)
                    if (tiles[i,j].Selected)
                        selected.Add(tiles[i,j]);           
        }
    }
}

#pragma warning disable CS0252 // Possible unintended reference comparison; left hand side needs cast

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using System.Threading;

namespace T16
{

    public partial class Form1 : Form
    {
        private Random rnd = new Random();
        private PictureBox[,] Crd = new PictureBox[16, 16];
        private int[,] Num = new int[16, 16];
        private bool gameover = false;
        public Form1()
        {
            InitializeComponent();
        }

        private void CreatePicturebox(string name, int x, int y, Image image, int crdX, int crdY)
        {
            PictureBox picture = new PictureBox
            {
                Name = name,
                Size = new Size(25, 25),
                Location = new Point(x, y),
                Image = image,
            };
            this.Controls.Add(picture);

            picture.Click += new EventHandler(pictureBox_Click);

            Crd[crdX, crdY] = picture;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            bool Loop = true;
            int x = 25;
            int y = 25;
            int xname = 0;
            int yname = 0;
            string xnameS;
            string ynameS;
            string name;


            while (Loop)
            {
                for (int i = 0; i < 16; i++)
                {
                    xnameS = xname.ToString();
                    ynameS = yname.ToString();


                    name = ynameS + xnameS;

                    CreatePicturebox(name, x, y, Properties.Resources.Icon, xname, yname);

                    x = x + 25;
                    xname += 1;
                    //yname += 1;
                }

                if (yname != 15)
                {
                    yname++;
                    y += 25;
                    x = 25;
                    xname = 0;
                }
                else
                {
                    Loop = false;
                }
            }
            MineAdder();
        }

        private void pictureBox_Click(object sender, EventArgs e)
        {


            PictureBox pictureBox = sender as PictureBox;

            /*if (pictureBox.Tag == "Mine")
            {
                label1.Text = pictureBox.Name + ", mine";
                //pictureBox.Image = Properties.Resources.MineIcon;
            }
            else
            {
                label1.Text = pictureBox.Name;
            }*/

            MineTest(pictureBox);
        }

        private void MineAdder()
        {
            int x;
            int y;

            for (int i = 0; i < 41; i++)
            {
                x = rnd.Next(0, 16);
                y = rnd.Next(0, 16);

                Crd[x, y].Tag = "Mine";
                Crd[x, y].Image = Properties.Resources.MineIcon;
                NumAdder(x, y);
            }
        }

        private void NumAdder(int x, int y)
        {
            if (x == 0 && y == 0)
            {
                Num[x, y]++;
                Num[x, y + 1]++;
                Num[x + 1, y]++;
                Num[x + 1, y + 1]++;
            }
            else if (x > 0 && y == 0 && x < 15)
            {
                Num[x, y]++;
                Num[x - 1, y]++;
                Num[x - 1, y + 1]++;
                Num[x, y + 1]++;
                Num[x + 1, y + 1]++;
                Num[x + 1, y]++;
            }
            else if (x == 15 && y == 0)
            {
                Num[x, y]++;
                Num[x - 1, y]++;
                Num[x - 1, y + 1]++;
                Num[x, y + 1]++;
            }
            else if (x == 15 && y < 15 && y > 0)
            {
                Num[x, y]++;
                Num[x - 1, y]++;
                Num[x - 1, y + 1]++;
                Num[x, y + 1]++;
                Num[x, y - 1]++;
                Num[x - 1, y - 1]++;
            }
            else if (x == 15 && y == 15)
            {
                Num[x, y]++;
                Num[x - 1, y]++;
                Num[x, y - 1]++;
                Num[x - 1, y - 1]++;
            }
            else if (x < 15 && y == 15 && x > 0)
            {
                Num[x, y]++;
                Num[x - 1, y]++;
                Num[x + 1, y]++;
                Num[x + 1, y - 1]++;
                Num[x, y - 1]++;
                Num[x - 1, y - 1]++;
            }
            else if (x == 0 && y == 15)
            {
                Num[x, y]++;
                Num[x + 1, y]++;
                Num[x + 1, y - 1]++;
                Num[x, y - 1]++;
            }
            else if (x == 0 && y > 0 && y < 15)
            {
                Num[x, y + 1]++;
                Num[x + 1, y + 1]++;
                Num[x + 1, y]++;
                Num[x + 1, y - 1]++;
                Num[x, y - 1]++;
            }
            else
            {
                Num[x, y]++;
                Num[x - 1, y]++;
                Num[x - 1, y + 1]++;
                Num[x, y + 1]++;
                Num[x + 1, y + 1]++;
                Num[x + 1, y]++;
                Num[x + 1, y - 1]++;
                Num[x, y - 1]++;
                Num[x - 1, y - 1]++;
            }
        }

        private void test(int originX, int originY, PictureBox pictureBox)
        {

            for (int x = originX-1; x >= originX+1;)
            {
                for (int Y = originY - 1; Y >= originY + 1;)
                {

                }
            }

        }

        public void MineTest(PictureBox origin)
        {
            int originX = 0;
            int originY = 0;
            bool Loop = true;
            int x = 0;
            while (Loop)
            {
                for (int y = 0; y < 16; y++)
                {
                    if (Crd[x, y] == origin)
                    {
                        Loop = false;
                        originX = x;
                        originY = y;
                    }
                }

                x++;
            }

            if (origin.Tag == "Mine")
            {
                gameover = true;
            }
            else
            {
                if (origin.Tag != "ok")
                {
                    origin.Image = Properties.Resources.IconOK;
                    origin.Tag = Properties.Resources.IconOK;
                    MineSearch(originX, originY, origin);
                }
            }


        }

        public void MineSearch(int originX, int originY, PictureBox pictureBox)
        {
            bool found = false;
            int MinesFound = 0;
            if (originY == 0 && originX == 0)
            {
                if (MineNear(originX, originY))
                {
                    found = true;
                    MinesFound++;
                }

                if (MineNear(originX + 1, originY))
                {
                    found = true;
                    MinesFound++;
                }

                if (MineNear(originX + 1, originY +1))
                {
                    found = true;
                    MinesFound++;
                }

                if (MineNear(originX, originY + 1))
                {
                    found = true;
                    MinesFound++;
                }

            }
            else if(originY > 0 && originX == 0)
            {
                if (MineNear(originX, originY))
                {
                    found = true;
                    MinesFound++;
                }

                if (MineNear(originX + 1, originY))
                {
                    found = true;
                    MinesFound++;
                }

                if (MineNear(originX + 1, originY + 1))
                {
                    found = true;
                    MinesFound++;
                }

                if (MineNear(originX, originY + 1))
                {
                    found = true;
                    MinesFound++;
                }

                if (MineNear(originX, originY -1))
                {
                    found = true;
                    MinesFound++;
                }

                if (MineNear(originX + 1, originY - 1))
                {
                    found = true;
                    MinesFound++;
                }
            }
            else
            {
                if (MineNear(originX, originY))
                {
                    found = true;
                    MinesFound++;
                }

                if (MineNear(originX - 1, originY))
                {
                    found = true;
                    MinesFound++;
                }

                if (MineNear(originX - 1, originY - 1))
                {
                    found = true;
                    MinesFound++;
                }

                if (MineNear(originX, originY - 1))
                {
                    found = true;
                    MinesFound++;
                }

                if (MineNear(originX + 1, originY - 1))
                {
                    found = true;
                    MinesFound++;
                }

                if (MineNear(originX + 1, originY))
                {
                    found = true;
                    MinesFound++;
                }

                if (MineNear(originX + 1, originY - 1))
                {
                    found = true;
                    MinesFound++;
                }

                if (MineNear(originX, originY - 1))
                {
                    found = true;
                    MinesFound++;
                }

                if (MineNear(originX - 1, originY - 1))
                {
                    found = true;
                    MinesFound++;
                }

                if (MineNear(originX - 1, originY + 1))
                {
                    found = true;
                    MinesFound++;
                }

                if (MineNear(originX, originY + 1))
                {
                    found = true;
                    MinesFound++;
                }

                if (MineNear(originX + 1, originY + 1))
                {
                    found = true;
                    MinesFound++;
                }
            }
        }

        private bool MineNear(int originX, int originY)
        {
            if (originX == 0 && originY == 0)
            {
                if (Crd[originX, originY + 1].Tag == "Mine" || Crd[originX + 1, originY + 1].Tag == "Mine" || Crd[originX + 1, originY].Tag == "Mine" || Crd[originX, originY].Tag == "Mine")
                {
                    return true;
                }
                else
                {
                    Crd[originX, originY].Image = Properties.Resources.IconOK;
                    Crd[originX, originY].Tag = "ok";
                    return false;

                }
            }
            else if(originX == 0 && originY > 0)
            {
                if (Crd[originX, originY + 1].Tag == "Mine" || Crd[originX + 1, originY + 1].Tag == "Mine" || Crd[originX + 1, originY].Tag == "Mine" || Crd[originX, originY].Tag == "Mine" || Crd[originX, originY + 1].Tag == "Mine" || Crd[originX +1, originY+ 1].Tag == "Mine" || Crd[originX, originY - 1].Tag == "Mine" || Crd[originX + 1, originY - 1].Tag == "Mine")
                {
                    return true;
                }
                else
                {
                    Crd[originX, originY].Image = Properties.Resources.IconOK;
                    Crd[originX, originY].Tag = "ok";
                    return false;

                }
            }
            else
            {
                if (Crd[originX - 1, originY].Tag == "Mine" || Crd[originX - 1, originY - 1].Tag == "Mine" || Crd[originX, originY - 1].Tag == "Mine" || Crd[originX - 1, originY + 1].Tag == "Mine" || Crd[originX, originY + 1].Tag == "Mine" || Crd[originX - 1, originY + 1].Tag == "Mine" || Crd[originX + 1, originY + 1].Tag == "Mine" || Crd[originX, originY + 1].Tag == "Mine" || Crd[originX - 1, originY + 1].Tag == "Mine" || Crd[originX + 1, originY].Tag == "Mine" || Crd[originX + 1, originY - 1].Tag == "Mine")
                {
                    return true;
                }
                else
                {
                    Crd[originX, originY].Image = Properties.Resources.IconOK;
                    Crd[originX, originY].Tag = "ok";
                    return false;

                }
            }
        }
    }
}
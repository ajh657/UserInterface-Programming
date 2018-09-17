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
        private bool originFound = false;
        private int Nest;
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


                    name = xnameS + ynameS ;

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
                NumDis(x, y);
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

        private void NumDis(int x, int y)
        {
            if (x == 0 && y == 0)
            {
                if (Crd[x, y].Tag != "Mine") Crd[x, y].Tag = Num[x, y].ToString();
                if (Crd[x, y + 1].Tag != "Mine") Crd[x, y + 1].Tag = Num[x, y + 1].ToString();
                if (Crd[x + 1, y].Tag != "Mine") Crd[x + 1, y].Tag = Num[x + 1, y].ToString();
                if (Crd[x + 1, y + 1].Tag != "Mine") Crd[x + 1, y + 1].Tag = Num[x + 1, y + 1].ToString();
            }
            else if (x > 0 && y == 0 && x < 15)
            {
                if (Crd[x, y].Tag != "Mine") Crd[x, y].Tag = Num[x, y].ToString();
                if (Crd[x - 1, y].Tag != "Mine") Crd[x - 1, y].Tag = Num[x - 1, y].ToString();
                if (Crd[x - 1, y + 1].Tag != "Mine") Crd[x - 1, y + 1].Tag = Num[x - 1, y + 1].ToString();
                if (Crd[x, y + 1].Tag != "Mine") Crd[x, y + 1].Tag = Num[x, y + 1].ToString();
                if (Crd[x + 1, y + 1].Tag != "Mine") Crd[x + 1, y + 1].Tag = Num[x + 1, y + 1].ToString();
                if (Crd[x + 1, y].Tag != "Mine") Crd[x + 1, y].Tag = Num[x + 1, y].ToString();
            }
            else if (x == 15 && y == 0)
            {
                if (Crd[x, y].Tag != "Mine") Crd[x, y].Tag = Num[x, y].ToString();
                if (Crd[x - 1, y].Tag != "Mine") Crd[x - 1, y].Tag = Num[x - 1, y].ToString();
                if (Crd[x - 1, y + 1].Tag != "Mine") Crd[x - 1, y + 1].Tag = Num[x - 1, y + 1].ToString();
                if (Crd[x, y + 1].Tag != "Mine") Crd[x, y + 1].Tag = Num[x, y + 1].ToString();
            }
            else if (x == 15 && y < 15 && y > 0)
            {
                if (Crd[x, y].Tag != "Mine") Crd[x, y].Tag = Num[x, y].ToString();
                if (Crd[x, y].Tag != "Mine") Crd[x - 1, y].Tag = Num[x - 1, y].ToString();
                if (Crd[x, y].Tag != "Mine") Crd[x - 1, y + 1].Tag = Num[x - 1, y + 1].ToString();
                if (Crd[x, y + 1].Tag != "Mine") Crd[x, y + 1].Tag = Num[x, y + 1].ToString();
                if (Crd[x, y - 1].Tag != "Mine") Crd[x, y - 1].Tag = Num[x, y - 1].ToString();
                if (Crd[x - 1, y - 1].Tag != "Mine") Crd[x - 1, y - 1].Tag = Num[x - 1, y - 1].ToString();
            }
            else if (x == 15 && y == 15)
            {
                if (Crd[x, y].Tag != "Mine") Crd[x, y].Tag = Num[x, y].ToString();
                if (Crd[x - 1, y].Tag != "Mine") Crd[x - 1, y].Tag = Num[x - 1, y].ToString();
                if (Crd[x, y - 1].Tag != "Mine") Crd[x, y - 1].Tag = Num[x, y - 1].ToString();
                if (Crd[x - 1, y - 1].Tag != "Mine") Crd[x - 1, y - 1].Tag = Num[x - 1, y - 1].ToString();
            }
            else if (x < 15 && y == 15 && x > 0)
            {
                if (Crd[x, y].Tag != "Mine") Crd[x, y].Tag = Num[x, y].ToString();
                if (Crd[x - 1, y].Tag != "Mine") Crd[x - 1, y].Tag = Num[x - 1, y].ToString();
                if (Crd[x + 1, y].Tag != "Mine") Crd[x + 1, y].Tag = Num[x + 1, y].ToString();
                if (Crd[x + 1, y - 1].Tag != "Mine") Crd[x + 1, y - 1].Tag = Num[x + 1, y - 1].ToString();
                if (Crd[x, y - 1].Tag != "Mine") Crd[x, y - 1].Tag = Num[x, y - 1].ToString();
                if (Crd[x - 1, y - 1].Tag != "Mine") Crd[x - 1, y - 1].Tag = Num[x - 1, y - 1].ToString();
            }
            else if (x == 0 && y == 15)
            {
                if (Crd[x, y].Tag != "Mine") Crd[x, y].Tag = Num[x, y].ToString();
                if (Crd[x + 1, y].Tag != "Mine") Crd[x + 1, y].Tag = Num[x + 1, y].ToString();
                if (Crd[x + 1, y - 1].Tag != "Mine") Crd[x + 1, y - 1].Tag = Num[x + 1, y - 1].ToString();
                if (Crd[x, y - 1].Tag != "Mine") Crd[x, y - 1].Tag = Num[x, y - 1].ToString();
            }
            else if (x == 0 && y > 0 && y < 15)
            {
                if (Crd[x, y + 1].Tag != "Mine") Crd[x, y + 1].Tag = Num[x, y + 1].ToString();
                if (Crd[x + 1, y + 1].Tag != "Mine") Crd[x + 1, y + 1].Tag = Num[x + 1, y + 1].ToString();
                if (Crd[x + 1, y].Tag != "Mine") Crd[x + 1, y].Tag = Num[x + 1, y].ToString();
                if (Crd[x + 1, y - 1].Tag != "Mine") Crd[x + 1, y - 1].Tag = Num[x + 1, y - 1].ToString();
                if (Crd[x, y - 1].Tag != "Mine") Crd[x, y - 1].Tag = Num[x, y - 1].ToString();
            }
            else
            {
                if (Crd[x, y].Tag != "Mine") Crd[x, y].Tag = Num[x, y].ToString();
                if (Crd[x - 1, y].Tag != "Mine") Crd[x - 1, y].Tag = Num[x - 1, y].ToString();
                if (Crd[x - 1, y + 1].Tag != "Mine") Crd[x - 1, y + 1].Tag = Num[x - 1, y + 1].ToString();
                if (Crd[x, y + 1].Tag != "Mine") Crd[x, y + 1].Tag = Num[x, y + 1].ToString();
                if (Crd[x + 1, y + 1].Tag != "Mine") Crd[x + 1, y + 1].Tag = Num[x + 1, y + 1].ToString();
                if (Crd[x + 1, y].Tag != "Mine") Crd[x + 1, y].Tag = Num[x + 1, y].ToString();
                if (Crd[x + 1, y - 1].Tag != "Mine") Crd[x + 1, y - 1].Tag = Num[x + 1, y - 1].ToString();
                if (Crd[x, y - 1].Tag != "Mine") Crd[x, y - 1].Tag = Num[x, y - 1].ToString();
                if (Crd[x - 1, y - 1].Tag != "Mine") Crd[x - 1, y - 1].Tag = Num[x - 1, y - 1].ToString();
            }
        }

        private void test(int originX, int originY, PictureBox pictureBox)
        {

            for (int x = originX - 1; x >= originX + 1;)
            {
                for (int Y = originY - 1; Y >= originY + 1;)
                {

                }
            }

        }

        private void MineTest(PictureBox origin)
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
                    MineSearchBeta(originX, originY, Crd[originX, originY]);
                    if (!originFound)
                    {
                        MineSearchBeta(originX, originY - 1, Crd[originX, originY - 1]);
                        MineSearchBeta(originX + 1, originY - 1, Crd[originX + 1, originY - 1]);
                        MineSearchBeta(originX, originY + 1, Crd[originX, originY + 1]);
                        MineSearchBeta(originX + 1, originY + 1, Crd[originX + 1, originY + 1]);
                        MineSearchBeta(originX + 1, originY, Crd[originX + 1, originY]);
                        MineSearchBeta(originX + 1, originY - 1, Crd[originX + 1, originY - 1]);
                        MineSearchBeta(originX - 1, originY, Crd[originX - 1, originY]);
                        MineSearchBeta(originX - 1, originY - 1, Crd[originX - 1, originY - 1]);
                    }
                    originFound = false;
                }
            }


        }

        private void MineSearchAlpha(int originX, int originY, PictureBox pictureBox)
        {

            bool SearchLoop = true;

            Nest++;

            bool ok = false;

            if (originY > 15 || originX > 15 || originX < 0|| originY < 0) SearchLoop = false;

            if (Crd[originX, originY].Tag == "Mine") SearchLoop = false;

            if (Nest == 99) SearchLoop = false;

            if (SearchLoop)
            {
                if (originX == 0 && originY == 0)
                {
                    if (Crd[originX, originY + 1].Tag != "Mine" && Crd[originX, originY + 1].Tag != null)
                    {

                    }
                    else
                    {
                        ok = true;
                        MineSearchAlpha(originX, originY + 1, pictureBox);
                        Crd[originX, originY + 1].Image = Properties.Resources.IconOK;
                    }

                    if (Crd[originX + 1, originY].Tag != "Mine" && Crd[originX, originY + 1].Tag != null)
                    {

                    }
                    else
                    {
                        ok = true;
                        MineSearchAlpha(originX + 1, originY, pictureBox);
                        Crd[originX + 1, originY].Image = Properties.Resources.IconOK;
                    }

                    if (Crd[originX + 1, originY + 1].Tag != "Mine" && Crd[originX + 1, originY + 1].Tag != null)
                    {

                    }
                    else
                    {
                        ok = true;
                        MineSearchAlpha(originX + 1, originY + 1, pictureBox);
                        Crd[originX + 1, originY + 1].Image = Properties.Resources.IconOK;
                    }
                }
                else if (originX > 0 && originY == 0 && originX < 15)
                {
                    /*
                    Num[originX, originY]++;
                    Num[originX - 1, originY]++;
                    Num[originX - 1, originY + 1]++;
                    Num[originX, originY + 1]++;
                    Num[originX + 1, originY + 1]++;
                    Num[originX + 1, originY]++;
                    */

                    if (Crd[originX - 1, originY].Tag != "Mine" && Crd[originX - 1, originY].Tag != null)
                    {

                    }
                    else
                    {
                        ok = true;
                        MineSearchAlpha(originX - 1, originY, pictureBox);
                        Crd[originX - 1, originY].Image = Properties.Resources.IconOK;
                    }

                    if (Crd[originX - 1, originY + 1].Tag != "Mine" && Crd[originX - 1, originY + 1].Tag != null)
                    {

                    }
                    else
                    {
                        ok = true;
                        MineSearchAlpha(originX - 1, originY + 1, pictureBox);
                        Crd[originX - 1, originY + 1].Image = Properties.Resources.IconOK;
                    }

                    if (Crd[originX, originY + 1].Tag != "Mine" && Crd[originX - 1, originY + 1].Tag != null)
                    {

                    }
                    else
                    {
                        ok = true;
                        MineSearchAlpha(originX, originY + 1, pictureBox);
                        Crd[originX, originY + 1].Image = Properties.Resources.IconOK;
                    }

                    if (Crd[originX + 1, originY + 1].Tag != "Mine" && Crd[originX + 1, originY + 1].Tag != null)
                    {

                    }
                    else
                    {
                        ok = true;
                        MineSearchAlpha(originX + 1, originY + 1, pictureBox);
                        Crd[originX + 1, originY + 1].Image = Properties.Resources.IconOK;
                    }

                    if (Crd[originX + 1, originY].Tag != "Mine" && Crd[originX + 1, originY].Tag != null)
                    {

                    }
                    else
                    {
                        ok = true;
                        MineSearchAlpha(originX + 1, originY, pictureBox);
                        Crd[originX + 1, originY].Image = Properties.Resources.IconOK;
                    }
                }
                else if (originX == 15 && originY == 0)
                {
                    /*
                    Num[originX, originY]++;
                    Num[originX - 1, originY]++;
                    Num[originX - 1, originY + 1]++;
                    Num[originX, originY + 1]++; 
                    */

                    if (Crd[originX - 1, originY].Tag != "Mine" && Crd[originX - 1, originY].Tag != null)
                    {

                    }
                    else
                    {
                        ok = true;
                        MineSearchAlpha(originX - 1, originY, pictureBox);
                        Crd[originX - 1, originY].Image = Properties.Resources.IconOK;
                    }

                    if (Crd[originX - 1, originY + 1].Tag != "Mine" && Crd[originX - 1, originY + 1].Tag != null)
                    {

                    }
                    else
                    {
                        ok = true;
                        MineSearchAlpha(originX - 1, originY + 1, pictureBox);
                        Crd[originX - 1, originY + 1].Image = Properties.Resources.IconOK;
                    }

                    if (Crd[originX, originY + 1].Tag != "Mine" && Crd[originX, originY + 1].Tag != null)
                    {

                    }
                    else
                    {
                        ok = true;
                        MineSearchAlpha(originX, originY + 1, pictureBox);
                        Crd[originX, originY + 1].Image = Properties.Resources.IconOK;
                    }
                }
                else if (originX == 15 && originY < 15 && originY > 0)
                {
                    /*
                    Num[originX, originY]++;
                    Num[originX - 1, originY]++;
                    Num[originX - 1, originY + 1]++;
                    Num[originX, originY + 1]++;
                    Num[originX, originY - 1]++;
                    Num[originX - 1, originY - 1]++;
                    */

                    if (Crd[originX - 1, originY].Tag != "Mine" && Crd[originX - 1, originY].Tag != null)
                    {

                    }
                    else
                    {
                        ok = true;
                        MineSearchAlpha(originX - 1, originY, pictureBox);
                        Crd[originX - 1, originY].Image = Properties.Resources.IconOK;
                    }

                    if (Crd[originX - 1, originY + 1].Tag != "Mine" && Crd[originX - 1, originY + 1].Tag != null)
                    {

                    }
                    else
                    {
                        ok = true;
                        MineSearchAlpha(originX - 1, originY + 1, pictureBox);
                        Crd[originX - 1, originY + 1].Image = Properties.Resources.IconOK;
                    }

                    if (Crd[originX, originY + 1].Tag != "Mine" && Crd[originX, originY + 1].Tag != null)
                    {

                    }
                    else
                    {
                        ok = true;
                        MineSearchAlpha(originX, originY + 1, pictureBox);
                        Crd[originX, originY + 1].Image = Properties.Resources.IconOK;
                    }

                    if (Crd[originX, originY - 1].Tag != "Mine" && Crd[originX, originY - 1].Tag != null)
                    {

                    }
                    else
                    {
                        ok = true;
                        MineSearchAlpha(originX, originY - 1, pictureBox);
                        Crd[originX, originY - 1].Image = Properties.Resources.IconOK;
                    }

                    if (Crd[originX - 1, originY - 1].Tag != "Mine" && Crd[originX - 1, originY - 1].Tag != null)
                    {

                    }
                    else
                    {
                        ok = true;
                        MineSearchAlpha(originX - 1, originY - 1, pictureBox);
                        Crd[originX - 1, originY - 1].Image = Properties.Resources.IconOK;
                    }

                }
                else if (originX == 15 && originY == 15)
                {
                    /* Num[originX, originY]++;
                    Num[originX - 1, originY]++;
                    Num[originX, originY - 1]++;
                    Num[originX - 1, originY - 1]++; */

                    if (Crd[originX - 1, originY].Tag != "Mine" && Crd[originX - 1, originY].Tag != null)
                    {

                    }
                    else
                    {
                        ok = true;
                        MineSearchAlpha(originX - 1, originY, pictureBox);
                        Crd[originX - 1, originY].Image = Properties.Resources.IconOK;
                    }

                    if (Crd[originX, originY - 1].Tag != "Mine" && Crd[originX, originY - 1].Tag != null)
                    {

                    }
                    else
                    {
                        ok = true;
                        MineSearchAlpha(originX, originY - 1, pictureBox);
                        Crd[originX, originY - 1].Image = Properties.Resources.IconOK;
                    }

                    if (Crd[originX - 1, originY - 1].Tag != "Mine" && Crd[originX - 1, originY - 1].Tag != null)
                    {

                    }
                    else
                    {
                        ok = true;
                        MineSearchAlpha(originX - 1, originY - 1, pictureBox);
                        Crd[originX - 1, originY - 1].Image = Properties.Resources.IconOK;
                    }

                }
                else if (originX < 15 && originY == 15 && originX > 0)
                {
                    /* Num[originX, originY]++;
                    Num[originX - 1, originY]++;
                    Num[originX + 1, originY]++;
                    Num[originX + 1, originY - 1]++;
                    Num[originX, originY - 1]++;
                    Num[originX - 1, originY - 1]++; */

                    if (Crd[originX - 1, originY].Tag != "Mine" && Crd[originX - 1, originY].Tag != null)
                    {

                    }
                    else
                    {
                        ok = true;
                        MineSearchAlpha(originX - 1, originY, pictureBox);
                        Crd[originX - 1, originY].Image = Properties.Resources.IconOK;
                    }

                    if (Crd[originX + 1, originY].Tag != "Mine" && Crd[originX - 1, originY].Tag != null)
                    {

                    }
                    else
                    {
                        ok = true;
                        MineSearchAlpha(originX + 1, originY, pictureBox);
                        Crd[originX + 1, originY].Image = Properties.Resources.IconOK;
                    }

                    if (Crd[originX + 1, originY - 1].Tag != "Mine" && Crd[originX + 1, originY - 1].Tag != null)
                    {

                    }
                    else
                    {
                        ok = true;
                        MineSearchAlpha(originX + 1, originY - 1, pictureBox);
                        Crd[originX + 1, originY - 1].Image = Properties.Resources.IconOK;
                    }

                    if (Crd[originX, originY - 1].Tag != "Mine" && Crd[originX, originY - 1].Tag != null)
                    {

                    }
                    else
                    {
                        ok = true;
                        MineSearchAlpha(originX, originY - 1, pictureBox);
                        Crd[originX, originY - 1].Image = Properties.Resources.IconOK;
                    }

                    if (Crd[originX - 1, originY - 1].Tag != "Mine" && Crd[originX - 1, originY - 1].Tag != null)
                    {

                    }
                    else
                    {
                        ok = true;
                        MineSearchAlpha(originX - 1, originY - 1, pictureBox);
                        Crd[originX - 1, originY - 1].Image = Properties.Resources.IconOK;
                    }
                }
                else if (originX == 0 && originY == 15)
                {
                    /*
                    Num[originX, originY]++;
                    Num[originX + 1, originY]++;
                    Num[originX + 1, originY - 1]++;
                    Num[originX, originY - 1]++;
                    */

                    if (Crd[originX + 1, originY].Tag != "Mine" && Crd[originX + 1, originY].Tag != null)
                    {

                    }
                    else
                    {
                        ok = true;
                        MineSearchAlpha(originX + 1, originY, pictureBox);
                        Crd[originX + 1, originY].Image = Properties.Resources.IconOK;
                    }

                    if (Crd[originX + 1, originY - 1].Tag != "Mine" && Crd[originX + 1, originY - 1].Tag != null)
                    {

                    }
                    else
                    {
                        ok = true;
                        MineSearchAlpha(originX + 1, originY - 1, pictureBox);
                        Crd[originX + 1, originY - 1].Image = Properties.Resources.IconOK;
                    }

                    if (Crd[originX, originY - 1].Tag != "Mine" && Crd[originX, originY - 1].Tag != null)
                    {

                    }
                    else
                    {
                        ok = true;
                        MineSearchAlpha(originX, originY - 1, pictureBox);
                        Crd[originX, originY - 1].Image = Properties.Resources.IconOK;
                    }

                }
                else if (originX == 0 && originY > 0 && originY < 15)
                {
                    /* Num[originX, originY + 1]++;
                    Num[originX + 1, originY + 1]++;
                    Num[originX + 1, originY]++;
                    Num[originX + 1, originY - 1]++;
                    Num[originX, originY - 1]++;*/

                    if (Crd[originX, originY + 1].Tag != "Mine" && Crd[originX, originY + 1].Tag != null)
                    {

                    }
                    else
                    {
                        ok = true;
                        MineSearchAlpha(originX, originY + 1, pictureBox);
                        Crd[originX, originY + 1].Image = Properties.Resources.IconOK;
                    }

                    if (Crd[originX + 1, originY + 1].Tag != "Mine" && Crd[originX + 1, originY + 1].Tag != null)
                    {

                    }
                    else
                    {
                        ok = true;
                        MineSearchAlpha(originX + 1, originY + 1, pictureBox);
                        Crd[originX + 1, originY + 1].Image = Properties.Resources.IconOK;
                    }

                    if (Crd[originX + 1, originY].Tag != "Mine" && Crd[originX + 1, originY].Tag != null)
                    {

                    }
                    else
                    {
                        ok = true;
                        MineSearchAlpha(originX + 1, originY, pictureBox);
                        Crd[originX + 1, originY].Image = Properties.Resources.IconOK;
                    }

                    if (Crd[originX + 1, originY - 1].Tag != "Mine" && Crd[originX + 1, originY - 1].Tag != null)
                    {

                    }
                    else
                    {
                        ok = true;
                        MineSearchAlpha(originX + 1, originY - 1, pictureBox);
                        Crd[originX + 1, originY - 1].Image = Properties.Resources.IconOK;
                    }

                    if (Crd[originX, originY - 1].Tag != "Mine" && Crd[originX, originY - 1].Tag != null)
                    {

                    }
                    else
                    {
                        ok = true;
                        MineSearchAlpha(originX, originY - 1, pictureBox);
                        Crd[originX, originY - 1].Image = Properties.Resources.IconOK;
                    }
                }
                else
                {
                    /*
                    Num[originX, originY]++;
                    Num[originX - 1, originY]++;
                    Num[originX - 1, originY + 1]++;
                    Num[originX, originY + 1]++;
                    Num[originX + 1, originY + 1]++;
                    Num[originX + 1, originY]++;
                    Num[originX + 1, originY - 1]++;
                    Num[originX, originY - 1]++;
                    Num[originX - 1, originY - 1]++;
                     */

                    if (Crd[originX - 1, originY].Tag != "Mine" && Crd[originX - 1, originY].Tag != null)
                    {

                    }
                    else
                    {
                        ok = true;
                        MineSearchAlpha(originX - 1, originY, pictureBox);
                        Crd[originX - 1, originY].Image = Properties.Resources.IconOK;
                    }

                    if (Crd[originX - 1, originY + 1].Tag != "Mine" && Crd[originX - 1, originY + 1].Tag != null)
                    {

                    }
                    else
                    {
                        ok = true;
                        MineSearchAlpha(originX - 1, originY + 1, pictureBox);
                        Crd[originX - 1, originY + 1].Image = Properties.Resources.IconOK;
                    }

                    if (Crd[originX, originY + 1].Tag != "Mine" && Crd[originX, originY + 1].Tag != null)
                    {

                    }
                    else
                    {
                        ok = true;
                        MineSearchAlpha(originX, originY + 1, pictureBox);
                        Crd[originX, originY + 1].Image = Properties.Resources.IconOK;
                    }

                    if (Crd[originX + 1, originY + 1].Tag != "Mine" && Crd[originX + 1, originY + 1].Tag != null)
                    {

                    }
                    else
                    {
                        ok = true;
                        MineSearchAlpha(originX + 1, originY + 1, pictureBox);
                        Crd[originX + 1, originY + 1].Image = Properties.Resources.IconOK;
                    }

                    if (Crd[originX + 1, originY].Tag != "Mine" && Crd[originX + 1, originY].Tag != null)
                    {

                    }
                    else
                    {
                        ok = true;
                        MineSearchAlpha(originX + 1, originY, pictureBox);
                        Crd[originX + 1, originY].Image = Properties.Resources.IconOK;
                    }

                    if (Crd[originX + 1, originY - 1].Tag != "Mine" && Crd[originX + 1, originY - 1].Tag != null)
                    {

                    }
                    else
                    {
                        ok = true;
                        MineSearchAlpha(originX + 1, originY - 1, pictureBox);
                        Crd[originX + 1, originY - 1].Image = Properties.Resources.IconOK;
                    }

                    if (Crd[originX, originY - 1].Tag != "Mine" && Crd[originX, originY - 1].Tag != null)
                    {

                    }
                    else
                    {
                        ok = true;
                        MineSearchAlpha(originX, originY - 1, pictureBox);
                        Crd[originX, originY - 1].Image = Properties.Resources.IconOK;
                    }

                    if (Crd[originX - 1, originY - 1].Tag != "Mine" && Crd[originX - 1, originY - 1].Tag != null)
                    {

                    }
                    else
                    {
                        ok = true;
                        MineSearchAlpha(originX - 1, originY - 1, pictureBox);
                        Crd[originX - 1, originY - 1].Image = Properties.Resources.IconOK;
                    }
                }
            }
        }

        private void MineSearchBeta(int originX, int originY, PictureBox pictureBox)
        {
            
            bool MineFound = false;
            if (originX > 0 || originX < 15 || originY < 0 || originX < 15)
            {

                if (Crd[originX,originY].Tag != "Mine")
                {
                    if (Num[originX, originY] > 0)
                    {
                        originFound = true;
                        Crd[originX, originY].Image = Properties.Resources.FlagIcon;
                    }

                    for (int i = originX - 1; i <= originX + 1 && !originFound; i++)
                    {
                        for (int j = originY - 1; j <= originY + 1 && !originFound; j++)
                        {


                            if (Num[i, j] > 0 && Crd[i, j].Tag != "Mine" && !originFound)
                            {
                                MineFound = true;
                                Crd[i, j].Image = Properties.Resources.FlagIcon;
                            }
                            else
                            {
                                Crd[i, j].Image = Properties.Resources.IconOK;
                            }
                        }
                    }


                }   
            }

            /*if (originX > 0 || originX < 15 || originY < 0 || originX < 15)
            {

                if (Crd[originX, originY].Tag != "Mine")
                {
                    if (Num[originX, originY] > 0)
                    {
                        originFound = true;
                        Crd[originX, originY].Image = Properties.Resources.FlagIcon;
                    }

                    for (int i = originX - 1; i <= originX + 1 && !originFound; i++)
                    {
                        for (int j = originY - 1; j <= originY + 1 && !originFound; j++)
                        {


                            if (Num[i, j] > 0 && Crd[i, j].Tag != "Mine" && !originFound)
                            {
                                MineFound = true;
                                Crd[i, j].Image = Properties.Resources.FlagIcon;
                            }
                            else
                            {
                                Crd[i, j].Image = Properties.Resources.IconOK;
                            }
                        }
                    }


                }
            }*/
        }

        private void MineTester()
        {

        }
    }
}
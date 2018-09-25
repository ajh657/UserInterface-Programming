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

        private PictureBox[,] Crd;
        private PictureBox[,] Mine;
        private bool[,] Flag;
        private int[,] Num;

        private bool originFound = false;

        private int MinesLeft;
        private int MaxX;
        private int MaxY;
        private int Mines = 0;
        private int MaxXArr;
        private int MaxYArr;

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

            picture.MouseDown += new MouseEventHandler(pictureBox_Click);

            Crd[crdX, crdY] = picture;
            Crd[crdX, crdY].Tag = "";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            MaxX = 16;
            MaxY = 16;
            Mines = 40;
            MinesLeft = Mines - 2;

            MaxXArr = MaxX - 1;
            MaxYArr =  MaxY - 1;

            Num = new int[MaxX,MaxY];
            Flag = new bool[MaxX, MaxY];
            Mine = new PictureBox[MaxX, MaxY];
            Crd = new PictureBox[MaxX, MaxY];
            BoardCreate();   
        }

        private void button3_Click(object sender, EventArgs e)
        {
            MaxX = 9;
            MaxY = 9;
            Mines = 10;
            MinesLeft = Mines - 2;

            MaxXArr = MaxX - 1;
            MaxYArr = MaxY - 1;

            Num = new int[MaxX, MaxY];
            Flag = new bool[MaxX, MaxY];
            Mine = new PictureBox[MaxX, MaxY];
            Crd = new PictureBox[MaxX, MaxY];
            BoardCreate();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            MaxX = 30;
            MaxY = 16;
            Mines = 99;
            MinesLeft = Mines - 2;

            MaxXArr = MaxX - 1;
            MaxYArr = MaxY - 1;

            Num = new int[MaxX, MaxY];
            Flag = new bool[MaxX, MaxY];
            Mine = new PictureBox[MaxX, MaxY];
            Crd = new PictureBox[MaxX, MaxY];
            BoardCreate();
        }

        private void BoardCreate()
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
                for (int i = 0; i < MaxX; i++)
                {
                    xnameS = xname.ToString();
                    ynameS = yname.ToString();


                    name = xnameS + ynameS;

                    CreatePicturebox(name, x, y, Properties.Resources.Icon, xname, yname);

                    x = x + 25;
                    xname += 1;
                    //yname += 1;
                }

                if (yname != MaxY - 1)
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

        private void pictureBox_Click(object sender, MouseEventArgs e)
        {


            PictureBox pictureBox = sender as PictureBox;

            switch (e.Button)
            {
                case MouseButtons.Left:
                    MineTest(pictureBox);
                    break;
                case MouseButtons.Right:
                    MineFlag(pictureBox);
                    break;
            }


        }

        private void MineFlag(PictureBox pictureBox)
        {
            int originX = 0;
            int originY = 0;
            bool Loop = true;
            int x = 0;
            while (Loop)
            {
                for (int y = 0; y < MaxY; y++)
                {
                    if (Crd[x, y] == pictureBox)
                    {
                        Loop = false;
                        originX = x;
                        originY = y;
                    }
                }

                x++;
            }

            if (!Flag[originX,originY])
            {
                Flag[originX, originY] = true;
                Crd[originX, originY].Image = Display(404);
                if(pictureBox.Tag == "Mine")
                {
                    MinesLeft -= 1;
                }
            }
            else
            {
                Flag[originX, originY] = false;
                switch (pictureBox.Tag)
                {
                    case "Mine":
                        pictureBox.Image = Properties.Resources.Icon;
                        break;

                    case "":
                        pictureBox.Image = Properties.Resources.Icon;
                        break;

                    case "ok":
                        pictureBox.Image = Properties.Resources.IconOK;
                        break;

                    default:
                        int num;
                        int.TryParse(pictureBox.Tag.ToString(), out num);
                        pictureBox.Image = Display(num);
                        break;
                }
            }

            if (MinesLeft == 0) gameover(true);

        }

        private void MineAdder()
        {
            int x;
            int y;

            for (int i = 0; i < Mines - 1; i++)
            {
                x = rnd.Next(0, MaxX);
                y = rnd.Next(0, MaxY);

                Crd[x, y].Tag = "Mine";
                NumAdder(x, y);
                NumArr(x, y);
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
            else if (x > 0 && y == 0 && x < MaxXArr)
            {
                Num[x, y]++;
                Num[x - 1, y]++;
                Num[x - 1, y + 1]++;
                Num[x, y + 1]++;
                Num[x + 1, y + 1]++;
                Num[x + 1, y]++;
            }
            else if (x == MaxXArr && y == 0)
            {
                Num[x, y]++;
                Num[x - 1, y]++;
                Num[x - 1, y + 1]++;
                Num[x, y + 1]++;
            }
            else if (x == MaxXArr && y < MaxYArr && y > 0)
            {
                Num[x, y]++;
                Num[x - 1, y]++;
                Num[x - 1, y + 1]++;
                Num[x, y + 1]++;
                Num[x, y - 1]++;
                Num[x - 1, y - 1]++;
            }
            else if (x == MaxXArr && y == MaxYArr)
            {
                Num[x, y]++;
                Num[x - 1, y]++;
                Num[x, y - 1]++;
                Num[x - 1, y - 1]++;
            }
            else if (x < MaxXArr && y == MaxYArr && x > 0)
            {
                Num[x, y]++;
                Num[x - 1, y]++;
                Num[x + 1, y]++;
                Num[x + 1, y - 1]++;
                Num[x, y - 1]++;
                Num[x - 1, y - 1]++;
            }
            else if (x == 0 && y == MaxYArr)
            {
                Num[x, y]++;
                Num[x + 1, y]++;
                Num[x + 1, y - 1]++;
                Num[x, y - 1]++;
            }
            else if (x == 0 && y > 0 && y < MaxYArr)
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

        private void NumArr(int x, int y)
        {
            if (x == 0 && y == 0)
            {
                if (Crd[x, y].Tag != "Mine") Crd[x, y].Tag = Num[x, y].ToString();
                if (Crd[x, y + 1].Tag != "Mine") Crd[x, y + 1].Tag = Num[x, y + 1].ToString();
                if (Crd[x + 1, y].Tag != "Mine") Crd[x + 1, y].Tag = Num[x + 1, y].ToString();
                if (Crd[x + 1, y + 1].Tag != "Mine") Crd[x + 1, y + 1].Tag = Num[x + 1, y + 1].ToString();
            }
            else if (x > 0 && y == 0 && x < MaxXArr)
            {
                if (Crd[x, y].Tag != "Mine") Crd[x, y].Tag = Num[x, y].ToString();
                if (Crd[x - 1, y].Tag != "Mine") Crd[x - 1, y].Tag = Num[x - 1, y].ToString();
                if (Crd[x - 1, y + 1].Tag != "Mine") Crd[x - 1, y + 1].Tag = Num[x - 1, y + 1].ToString();
                if (Crd[x, y + 1].Tag != "Mine") Crd[x, y + 1].Tag = Num[x, y + 1].ToString();
                if (Crd[x + 1, y + 1].Tag != "Mine") Crd[x + 1, y + 1].Tag = Num[x + 1, y + 1].ToString();
                if (Crd[x + 1, y].Tag != "Mine") Crd[x + 1, y].Tag = Num[x + 1, y].ToString();
            }
            else if (x == MaxXArr && y == 0)
            {
                if (Crd[x, y].Tag != "Mine") Crd[x, y].Tag = Num[x, y].ToString();
                if (Crd[x - 1, y].Tag != "Mine") Crd[x - 1, y].Tag = Num[x - 1, y].ToString();
                if (Crd[x - 1, y + 1].Tag != "Mine") Crd[x - 1, y + 1].Tag = Num[x - 1, y + 1].ToString();
                if (Crd[x, y + 1].Tag != "Mine") Crd[x, y + 1].Tag = Num[x, y + 1].ToString();
            }
            else if (x == MaxXArr && y < MaxYArr && y > 0)
            {
                if (Crd[x, y].Tag != "Mine") Crd[x, y].Tag = Num[x, y].ToString();
                if (Crd[x, y].Tag != "Mine") Crd[x - 1, y].Tag = Num[x - 1, y].ToString();
                if (Crd[x, y].Tag != "Mine") Crd[x - 1, y + 1].Tag = Num[x - 1, y + 1].ToString();
                if (Crd[x, y + 1].Tag != "Mine") Crd[x, y + 1].Tag = Num[x, y + 1].ToString();
                if (Crd[x, y - 1].Tag != "Mine") Crd[x, y - 1].Tag = Num[x, y - 1].ToString();
                if (Crd[x - 1, y - 1].Tag != "Mine") Crd[x - 1, y - 1].Tag = Num[x - 1, y - 1].ToString();
            }
            else if (x == MaxXArr && y == MaxYArr)
            {
                if (Crd[x, y].Tag != "Mine") Crd[x, y].Tag = Num[x, y].ToString();
                if (Crd[x - 1, y].Tag != "Mine") Crd[x - 1, y].Tag = Num[x - 1, y].ToString();
                if (Crd[x, y - 1].Tag != "Mine") Crd[x, y - 1].Tag = Num[x, y - 1].ToString();
                if (Crd[x - 1, y - 1].Tag != "Mine") Crd[x - 1, y - 1].Tag = Num[x - 1, y - 1].ToString();
            }
            else if (x < MaxXArr && y == MaxYArr && x > 0)
            {
                if (Crd[x, y].Tag != "Mine") Crd[x, y].Tag = Num[x, y].ToString();
                if (Crd[x - 1, y].Tag != "Mine") Crd[x - 1, y].Tag = Num[x - 1, y].ToString();
                if (Crd[x + 1, y].Tag != "Mine") Crd[x + 1, y].Tag = Num[x + 1, y].ToString();
                if (Crd[x + 1, y - 1].Tag != "Mine") Crd[x + 1, y - 1].Tag = Num[x + 1, y - 1].ToString();
                if (Crd[x, y - 1].Tag != "Mine") Crd[x, y - 1].Tag = Num[x, y - 1].ToString();
                if (Crd[x - 1, y - 1].Tag != "Mine") Crd[x - 1, y - 1].Tag = Num[x - 1, y - 1].ToString();
            }
            else if (x == 0 && y == MaxYArr)
            {
                if (Crd[x, y].Tag != "Mine") Crd[x, y].Tag = Num[x, y].ToString();
                if (Crd[x + 1, y].Tag != "Mine") Crd[x + 1, y].Tag = Num[x + 1, y].ToString();
                if (Crd[x + 1, y - 1].Tag != "Mine") Crd[x + 1, y - 1].Tag = Num[x + 1, y - 1].ToString();
                if (Crd[x, y - 1].Tag != "Mine") Crd[x, y - 1].Tag = Num[x, y - 1].ToString();
            }
            else if (x == 0 && y > 0 && y < MaxYArr)
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
                for (int y = 0; y < MaxY; y++)
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
                gameover(false);
            }
            else
            {
                if (origin.Tag != "ok")
                {
                    origin.Image = Properties.Resources.IconOK;
                    MineSearchBeta(originX, originY, Crd[originX, originY]);
                    if(originFound)
                    if (!originFound)
                    {
                        if (originY - 1 >= 0) MineSearchBeta(originX, originY - 1, Crd[originX, originY - 1]);
                        if (originX + 1 <= 0 && originX + 1 <= MaxX && originY - 1 >= 0) MineSearchBeta(originX + 1, originY - 1, Crd[originX + 1, originY - 1]);
                        if (originX > 0 && originX < 15 && originY + 1 > 0 && originY + 1 < MaxX) MineSearchBeta(originX, originY + 1, Crd[originX, originY + 1]);
                        if (originX + 1 <= 0 && originX + 1 <= MaxX && originY + 1 >= 0) MineSearchBeta(originX + 1, originY + 1, Crd[originX + 1, originY + 1]);
                        if (originX + 1 <= 0 && originX + 1 <= MaxX && originY >= 0) MineSearchBeta(originX + 1, originY, Crd[originX + 1, originY]);
                        if (originX + 1 <= 0 && originX + 1 <= MaxX && originY - 1 >= 0) MineSearchBeta(originX + 1, originY - 1, Crd[originX + 1, originY - 1]);
                        if (originX - 1 >= 0 && originX - 1 <= MaxX && originY >= 0) MineSearchBeta(originX - 1, originY, Crd[originX - 1, originY]);
                        if (originX + 1 <= 0 && originX - 1 <= MaxX && originY - 1 >= 0) MineSearchBeta(originX - 1, originY - 1, Crd[originX - 1, originY - 1]);
                    }
                    originFound = false;
                }
            }


        }

        private void MineSearchBeta(int originX, int originY, PictureBox pictureBox)
        {
            bool overflow = false;
            bool MineFound = false;

            if (originX < 0 && originY < 0)
            {
                overflow = true;
            }

            if (originX == 0 && originY == 0 && !overflow)
            {

                if (Crd[originX, originY].Tag != "Mine")
                {
                    if (Num[originX, originY] > 0)
                    {
                        originFound = true;
                        Crd[originX, originY].Image = Display(Num[originX,originY]);
                    }

                    if (Num[originX, originY + 1] > 0 && Crd[originX, originY + 1].Tag != "Mine" && !originFound)
                    {
                        originFound = true;
                        Crd[originX, originY + 1].Image = Display(Num[originX, originY + 1]);
                    }

                    if (Num[originX + 1, originY + 1] > 0 && Crd[originX + 1, originY + 1].Tag != "Mine" && !originFound)
                    {
                        originFound = true;
                        Crd[originX + 1, originY + 1].Image = Display(Num[originX + 1, originY + 1]);
                    }

                    if (Num[originX + 1, originY] > 0 && Crd[originX + 1, originY].Tag != "Mine" && !originFound)
                    {
                        originFound = true;
                        Crd[originX + 1, originY].Image = Display(Num[originX + 1, originY]);
                    }
                }
            }
            else if (originX < MaxXArr && originX > 0 && originY == 0 && !overflow)
            {

                if (Crd[originX, originY].Tag != "Mine")
                {
                    if (Num[originX, originY] > 0)
                    {
                        originFound = true;
                        Crd[originX, originY].Image = Display(Num[originX, originY]);
                    }

                    if (Num[originX, originY + 1] > 0 && Crd[originX, originY + 1].Tag != "Mine" && !originFound)
                    {
                        originFound = true;
                        Crd[originX, originY + 1].Image = Display(Num[originX, originY + 1]);
                    }

                    if (Num[originX + 1, originY + 1] > 0 && Crd[originX + 1, originY + 1].Tag != "Mine" && !originFound)
                    {
                        originFound = true;
                        Crd[originX + 1, originY + 1].Image = Display(Num[originX + 1, originY + 1]);
                    }

                    if (Num[originX + 1, originY] > 0 && Crd[originX + 1, originY].Tag != "Mine" && !originFound)
                    {
                        originFound = true;
                        Crd[originX + 1, originY].Image = Display(Num[originX + 1, originY]);
                    }

                    if (Num[originX - 1, originY] > 0 && Crd[originX - 1, originY].Tag != "Mine" && !originFound)
                    {
                        originFound = true;
                        Crd[originX - 1, originY].Image = Display(Num[originX - 1, originY]);
                    }

                    if (Num[originX - 1, originY + 1] > 0 && Crd[originX - 1, originY + 1].Tag != "Mine" && !originFound)
                    {
                        originFound = true;
                        Crd[originX - 1, originY + 1].Image = Display(Num[originX - 1, originY + 1]);
                    }
                }
            }
            else if (originX == MaxXArr && originY == 0 && !overflow)
            {
                if (Crd[originX, originY].Tag != "Mine")
                {
                    if (Num[originX, originY] > 0)
                    {
                        originFound = true;
                        Crd[originX, originY].Image = Display(Num[originX, originY]);
                    }

                    if (Num[originX, originY + 1] > 0 && Crd[originX, originY + 1].Tag != "Mine" && !originFound)
                    {
                        originFound = true;
                        Crd[originX, originY + 1].Image = Display(Num[originX, originY + 1]);
                    }

                    if (Num[originX - 1, originY + 1] > 0 && Crd[originX - 1, originY + 1].Tag != "Mine" && !originFound)
                    {
                        originFound = true;
                        Crd[originX - 1, originY + 1].Image = Display(Num[originX - 1, originY + 1]);
                    }

                    if (Num[originX - 1, originY] > 0 && Crd[originX - 1, originY].Tag != "Mine" && !originFound)
                    {
                        originFound = true;
                        Crd[originX - 1, originY].Image = Display(Num[originX - 1, originY]);
                    }
                }

            }
            else if (originX == MaxXArr && originY > 0 && originY < MaxYArr && !overflow)
            {

                if (Crd[originX, originY].Tag != "Mine")
                {
                    if (Num[originX, originY] > 0)
                    {
                        originFound = true;
                        Crd[originX, originY].Image = Display(Num[originX, originY]);
                    }

                    if (Num[originX, originY + 1] > 0 && Crd[originX, originY + 1].Tag != "Mine" && !originFound)
                    {
                        originFound = true;
                        Crd[originX, originY + 1].Image = Display(Num[originX, originY + 1]);
                    }

                    if (Num[originX - 1, originY] > 0 && Crd[originX - 1, originY].Tag != "Mine" && !originFound)
                    {
                        originFound = true;
                        Crd[originX - 1, originY].Image = Display(Num[originX - 1, originY]);
                    }

                    if (Num[originX - 1, originY] > 0 && Crd[originX - 1, originY].Tag != "Mine" && !originFound)
                    {
                        originFound = true;
                        Crd[originX - 1, originY].Image = Display(Num[originX - 1, originY]);
                    }

                    if (Num[originX - 1, originY - 1] > 0 && Crd[originX - 1, originY - 1].Tag != "Mine" && !originFound)
                    {
                        originFound = true;
                        Crd[originX - 1, originY - 1].Image = Display(Num[originX - 1, originY - 1]);
                    }
                }
            }
            else if (originX == MaxXArr && originY == MaxYArr && !overflow)
            {
                if (Crd[originX, originY].Tag != "Mine")
                {
                    if (Num[originX, originY] > 0)
                    {
                        originFound = true;
                        Crd[originX, originY].Image = Display(Num[originX, originY]);
                    }

                    if (Num[originX, originY - 1] > 0 && Crd[originX, originY - 1].Tag != "Mine" && !originFound)
                    {
                        originFound = true;
                        Crd[originX, originY - 1].Image = Display(Num[originX, originY - 1]);
                    }

                    if (Num[originX - 1, originY - 1] > 0 && Crd[originX - 1, originY - 1].Tag != "Mine" && !originFound)
                    {
                        originFound = true;
                        Crd[originX - 1, originY - 1].Image = Display(Num[originX - 1, originY - 1]);
                    }

                    if (Num[originX - 1, originY - 1] > 0 && Crd[originX - 1, originY - 1].Tag != "Mine" && !originFound)
                    {
                        originFound = true;
                        Crd[originX - 1, originY - 1].Image = Display(Num[originX - 1, originY - 1]);
                    }
                }
            }
            else if (originX > 15 && originX < 0 && originY == MaxYArr && !overflow)
            {

                if (Crd[originX, originY].Tag != "Mine")
                {
                    if (Num[originX, originY] > 0)
                    {
                        originFound = true;
                        Crd[originX, originY].Image = Display(Num[originX, originY]);
                    }

                    if (Num[originX, originY - 1] > 0 && Crd[originX, originY - 1].Tag != "Mine" && !originFound)
                    {
                        originFound = true;
                        Crd[originX, originY - 1].Image = Display(Num[originX, originY - 1]);
                    }

                    if (Num[originX - 1, originY] > 0 && Crd[originX - 1, originY].Tag != "Mine" && !originFound)
                    {
                        originFound = true;
                        Crd[originX - 1, originY].Image = Display(Num[originX - 1, originY]);
                    }

                    if (Num[originX + 1, originY] > 0 && Crd[originX + 1, originY].Tag != "Mine" && !originFound)
                    {
                        originFound = true;
                        Crd[originX + 1, originY].Image = Display(Num[originX + 1, originY]);
                    }

                    if (Num[originX - 1, originY + 1] > 0 && Crd[originX - 1, originY + 1].Tag != "Mine" && !originFound)
                    {
                        originFound = true;
                        Crd[originX - 1, originY + 1].Image = Display(Num[originX - 1, originY + 1]);
                    }
                }
            }
            if (originX == MaxXArr && originY == MaxYArr && !overflow)
            {

                if (Crd[originX, originY].Tag != "Mine")
                {
                    if (Num[originX, originY] > 0 && Crd[originX,originY].Tag != "Mine")
                    {
                        originFound = true;
                        Crd[originX, originY].Image = Display(Num[originX, originY]);
                    }

                    if (Num[originX, originY - 1] > 0 && Crd[originX, originY - 1].Tag != "Mine")
                    {
                        originFound = true;
                        Crd[originX, originY - 1].Image = Display(Num[originX, originY - 1]);
                    }

                    if (Num[originX - 1, originY - 1] > 0 && Crd[originX - 1, originY - 1].Tag != "Mine")
                    {
                        originFound = true;
                        Crd[originX - 1, originY - 1].Image = Display(Num[originX - 1, originY - 1]);
                    }

                    if (Num[originX - 1, originY] > 0 && Crd[originX - 1, originY].Tag != "Mine")
                    {
                        originFound = true;
                        Crd[originX - 1, originY].Image = Display(Num[originX - 1, originY]);
                    }
                }
            }
            else if (originX > 0 && originX < MaxXArr && originY > 0 && originX < MaxXArr)
            {

                if (Crd[originX, originY].Tag != "Mine")
                {
                    if (Num[originX, originY] > 0)
                    {
                        originFound = true;
                        Crd[originX, originY].Image = Display(Num[originX, originY]);
                    }

                    for (int i = originX - 1; i <= originX && !originFound; i++)
                    {
                        for (int j = originY - 1; j <= originY && !originFound; j++)
                        {


                            if (Num[i, j] > 0 && Crd[i, j].Tag != "Mine" && !originFound)
                            {
                                MineFound = true;
                                Crd[i, j].Image = Display(Num[i,j]);
                            }
                            else
                            {
                                Crd[i, j].Image = Properties.Resources.IconOK;
                            }
                        }
                    }


                }
            }
        }

        private Image Display(int Number)
        {
            switch (Number)
            {
                case 1:
                    return Properties.Resources.IconOne;

                case 2:
                    return Properties.Resources.IconTwo;

                case 3:
                    return Properties.Resources.IconThree;

                case 4:
                    return Properties.Resources.IconFour;

                case 5:
                    return Properties.Resources.IconFive;

                case 6:
                    return Properties.Resources.IconSIX;

                case 7:
                    return Properties.Resources.IconSeven;

                case 8:
                    return Properties.Resources.IconEight;

                case 404:
                    return Properties.Resources.FlagIcon;

                default:
                    return Properties.Resources.IconOK;
            }

        }

        private void gameover(bool good)
        {
            if (good)
            {
                MessageBox.Show("You Win");
                Application.Exit();
            }
            else
            {
                MessageBox.Show("gameover");
                Application.Exit();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {

        }
    }
}
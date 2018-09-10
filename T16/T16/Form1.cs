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
        public Form1()
        {
            InitializeComponent();
        }
        
        private void CreatePicturebox(string name,int x,int y,Image image,int crdX,int crdY)
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

                MineTest(pictureBox, false, null);
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
                //Crd[x, y].Image = Properties.Resources.MineIcon;
            }
        }

        public void MineTest(PictureBox origin, bool originB, string dir)
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

            /*if (Crd[originX, originY].Tag == "Mine")
            {
                Crd[originX, originY].Image = Properties.Resources.MineIcon;
            }
            else
            {
                Crd[originX, originY].Image = Properties.Resources.IconOK;
            }*/


        }
    }
}
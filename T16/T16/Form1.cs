using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace T16
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        
        private void CreatePicturebox(string name,int x,int y,Image image)
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
        }

        private void button1_Click(object sender, EventArgs e)
        {
            bool Loop = true;
            int x = 25;
            int y = 25;
            int xname = 0;
            int yname = 0;
            string name;
            

            while (Loop)
            {
                for (int i = 0; i <= 16; i++)
                {

                    name = xname.ToString() + yname.ToString();

                    CreatePicturebox(name, x, y, Properties.Resources.Icon);

                    x = x + 25;
                    xname += 1;
                    //yname += 1;
                }

                if (yname != 16)
                {
                    yname++;
                    y += 25;
                    x = 25;
                }
                else
                {
                    Loop = false;
                }
            }
        }

        private void pictureBox_Click(object sender, EventArgs e)
        {


            PictureBox pictureBox = sender as PictureBox;

            label1.Text = pictureBox.Name;
        }
    }
}

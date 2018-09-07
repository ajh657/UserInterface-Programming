using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace T12
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            DoubleBuffered = true;
            
        }

        private void MainForm_Paint(object sender, PaintEventArgs e)
        {

        }


        private void MainForm_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                
                // Talletetaan hiiren klikkauskohdan koordinaatit. Piste (0, 0)
                // on formin työalueen vasemmassa ylä nurkassa
                pictureBox1.Top = e.Y;
                pictureBox1.Left = e.X;

                // Merkitään formin työalue epäkelvoksi, jolloin saadaan aikaiseksi
                // paint-eventin signalointi ja tämän jälkeen Paint()-metodin kutsu.
                Invalidate();
            }
        }

        private void MainForm_KeyDown(object sender, KeyEventArgs e)
        {
            //MessageBox.Show(e.KeyCode.ToString());
            switch (e.KeyCode)
            {
                case Keys.Left:
                    pictureBox1.Left -= 1;
                    break;
                case Keys.Up:
                    pictureBox1.Top -= 1;
                    break;
                case Keys.Right:
                    pictureBox1.Left += 1;
                    break;
                case Keys.Down:
                    pictureBox1.Top += 1;
                    break;
            }

            Invalidate();
        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace T11
{
    public partial class Form1 : Form
    {
        private Point piste = new Point(0, 0);
        public Form1()
        {
            InitializeComponent();
            

        }

        private void MainForm_Paint(object sender, PaintEventArgs e)
        {
            Graphics Graf = e.Graphics;

            // - Pää
            Graf.FillEllipse(Brushes.IndianRed, piste.X - 4, piste.Y - 8, 49, 49);
            Graf.DrawEllipse(Pens.Black, piste.X - 4, piste.Y - 8, 49, 49);
            // - Selkä
            Graf.DrawLine(Pens.Black, piste.X + 21, piste.Y + 41,
            piste.X + 21, piste.Y + 131);
            // - Kädet
            Graf.DrawLine(Pens.Black, piste.X - 30, piste.Y + 60,
            piste.X + 70, piste.Y + 60);
            // - Jalat
            Graf.DrawLine(Pens.Black, piste.X + 21, piste.Y + 131,
            piste.X - 30, piste.Y + 181);
            Graf.DrawLine(Pens.Black, piste.X + 21, piste.Y + 131,
            piste.X + 70, piste.Y + 181);


            // Kutsutaan DrawCoordinates()-metodia.
            DrawCordinates(Graf);
        }

        private void DrawCordinates(Graphics Graf)
        {
            // Piirretään piikoordinaattien arvot näytölle.
            Graf.DrawString("(" + piste.X + " ," + piste.Y + ")",
                            new Font("Arial", 14, System.Drawing.FontStyle.Regular),
                            new SolidBrush(Color.Black), 8, 30);
        }

        private void MainForm_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                // Talletetaan hiiren klikkauskohdan koordinaatit. Piste (0, 0)
                // on formin työalueen vasemmassa ylä nurkassa
                piste = e.Location;

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
                    piste.X -= 1;
                    break;
                case Keys.Up:
                    piste.Y -= 1;
                    break;
                case Keys.Right:
                    piste.X += 1;
                    break;
                case Keys.Down:
                    piste.Y += 1;
                    break;
            }

            Invalidate();
        }
    }
}


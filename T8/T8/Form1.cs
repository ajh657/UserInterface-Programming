using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace T8
{

    public partial class Form1 : Form
    {
        private Random rnd = new Random();

        private int[] Prime = new int[7];
        private int[] Bonus = new int[2];
        private NumericUpDown[] numericUpDowns = new NumericUpDown[9];
        private string output;
        public Form1()
        {
            InitializeComponent();
            numericUpDowns[0] = numericUpDown1;
            numericUpDowns[1] = numericUpDown2;
            numericUpDowns[2] = numericUpDown3;
            numericUpDowns[3] = numericUpDown4;
            numericUpDowns[4] = numericUpDown5;
            numericUpDowns[5] = numericUpDown6;
            numericUpDowns[6] = numericUpDown7;
            numericUpDowns[7] = numericUpDown8;
            numericUpDowns[8] = numericUpDown9;
        }

        private void button1_Click(object sender, EventArgs e)
        {



            int Newnumber;

            bool checkP = true;
            bool checkB = true;

            for (int i = 0; i < Prime.Length;)
            {
                Newnumber = rnd.Next(1, 41);

                for (int p = 0; p <= i; p++)
                {
                    if (Prime[p] == Newnumber && checkP == true)
                    {
                        checkP = false;
                        break;
                    }
                    checkP = true;
                }

                if (checkP)
                {
                    Prime[i] = Newnumber;
                    i++;
                }
            }

            for (int o = 0; o < Bonus.Length;)
            {
                Newnumber = rnd.Next(1, 10);

                for (int d = 0; d <= o; d++)
                {
                    if (Bonus[d] == Newnumber && checkB == true)
                    {
                        checkB = false;
                        break;
                    }
                    checkB = true;
                }

                if (checkB)
                {
                    Bonus[o] = Newnumber;
                    o++;
                }
            }

            for (int p = 0; p < Prime.Length; p++)
            {
                output += Prime[p] + ", ";
            }

            for (int pB = 0; pB < Bonus.Length; pB++)
            {
                if (pB != Bonus.Length - 1)
                {
                    output += Bonus[pB] + ", ";
                }
                else
                {
                    output += Bonus[pB];
                }
            }

            label1.Text = output;
            output = null;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            int correct = 0;
            if (numericUpDown1.Value == Prime[0]) correct++;
            if (numericUpDown2.Value == Prime[1]) correct++;
            if (numericUpDown3.Value == Prime[2]) correct++;
            if (numericUpDown4.Value == Prime[3]) correct++;
            if (numericUpDown5.Value == Prime[4]) correct++;
            if (numericUpDown6.Value == Prime[5]) correct++;
            if (numericUpDown7.Value == Prime[6]) correct++;
            if (numericUpDown8.Value == Bonus[0]) correct++;
            if (numericUpDown9.Value == Bonus[1]) correct++;

            if (correct == 9)
            {
                label2.Text = "Sait kaikki oikein";
            }
            else
            {
                label2.Text = "Sait: " + correct + " oikein";
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            bool checkP = true;
            int Newnumber;
            for (int i = 0; i < numericUpDowns.Length;)
            {
                Newnumber = rnd.Next(1, 41);

                for (int p = 0; p <= i; p++)
                {
                    if (numericUpDowns[i].Value == Newnumber && checkP == true)
                    {
                        checkP = false;
                        break;
                    }
                    checkP = true;
                }

                if (checkP)
                {
                    numericUpDowns[i].Value = Newnumber;
                    i++;
                }
            }
        }
    }
}

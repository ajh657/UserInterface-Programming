using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace T7
{
    public partial class Form1 : Form
    {
        private int[] Value;
        public Form1()
        {
            InitializeComponent();
            button1.Click += new EventHandler(button_Click);
            button2.Click += new EventHandler(button_Click);
            button3.Click += new EventHandler(button_Click);

        }

        private void button_Click(object sender, EventArgs e)
        {
            Button button = sender as Button;


            switch (button.Name)
            {
                case "button1":
                    int luku = int.Parse(textBox1.Text);
                    Value = new int[luku];
                    break;

                case "button2":
                    Value[int.Parse(textBox3.Text)] = int.Parse(textBox2.Text);
                    break;

                case "button3":
                    label8.Text = Value[int.Parse(textBox4.Text)].ToString();
                    break;

                default:
                    break;
            }
        }
    }
}
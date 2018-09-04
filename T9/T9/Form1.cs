using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace T9
{
    public partial class Form1 : Form
    {
        List<string> list;
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            list = new List<string>();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            list.Add(textBox1.Text);
            comboBox1.Items.Clear();
            foreach (var Item in list)
            {
                comboBox1.Items.Add(Item);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            foreach (var item in list)
            {
                if (item == comboBox1.Text) MessageBox.Show("Löytyi");
            }
        }
    }
}

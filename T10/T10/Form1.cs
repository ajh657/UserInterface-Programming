using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace T10
{
    public partial class Form1 : Form
    {
        private List<KeyValuePair<string, string>> list;
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            list = new List<KeyValuePair<string, string>>();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            list.Add(new KeyValuePair<string, string>(textBox2.Text, textBox1.Text));
        }

        private void button3_Click(object sender, EventArgs e)
        {
            foreach (var item in list)
            {
                if (item.Key == textBox3.Text) label7.Text = item.Value;
            }
        }
    }
}

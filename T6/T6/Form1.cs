using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace T6
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            bool Loop = true;
            while (Loop)
            {
                DialogResult dialogResult = MessageBox.Show("Valitse Yes jatkaaksesi tai No lopettaaksesi.", "Huom !", MessageBoxButtons.YesNo);
                if (dialogResult == DialogResult.Yes)
                {
                    Loop = false;
                }
            }
        }
    }
}

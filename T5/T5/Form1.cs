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

namespace T5
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            button1.Click += new EventHandler(Button1_Click);
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            Thread OmaSaie;

            OmaSaie = new Thread(new ThreadStart(this.SaieProceduuri));
            OmaSaie.Start();
        }

        private void SaieProceduuri()
        {
            Int32 Kierrokset = Convert.ToInt32(textBox1.Text);

            for (Int32 i = 0; i < Kierrokset; i++)
            {
                string uusiNumero = (i + 1).ToString();
                this.AsetaTeksti(uusiNumero);

                // Viive
                Thread.Sleep(10);
            }
        }

        delegate void StringArgReturningVoidDelegate(string texti);

        private void AsetaTeksti(string texti)
        {


            if (this.label1.InvokeRequired)
            {   
                StringArgReturningVoidDelegate d = new StringArgReturningVoidDelegate(AsetaTeksti);
                this.Invoke(d, new object[] { texti });
            }
            else
            {   
                this.label1.Text = texti;
            }
        }

    }
}

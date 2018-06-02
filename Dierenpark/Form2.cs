using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Dierenpark
{
    public partial class Form2 : Form
    {

        private string Clip(string str,int n)
        {
            string b = str;
            if (b.Length > n)
            {
                while (b.Length != n)
                    b = b.Substring(0, str.Length - 2);
                return b;
            }
            else
            {
                while(b.Length != n)
                    b += " ";
                return b;
            }
        }


        public Form2(List<Form1.CPrijs> bBon)
        {
            InitializeComponent();

            decimal prijsTotaal = 0.0m;
            string textBon = "";

            foreach(Form1.CPrijs c in bBon)
            {
                if (c.bedrag == 0.0m)
                {
                    textBon += c.reden + Environment.NewLine;
                }
                else
                {
                    prijsTotaal += c.bedrag;
                    textBon += "    €" + Clip(c.bedrag.ToString(), 15) + c.reden + Environment.NewLine;
                }
            }

            label2.Text = textBon;
            this.MinimumSize = new Size(MinimumSize.Width, label2.Height + 130);
        }


        private void button1_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(label2.Text);
        }
    }
}

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
    public class SecondaryField
    {
        Form mainForm;

        public SecondaryField(Form form, int x, int y)
        {
            mainForm = form;

            panel2 = new System.Windows.Forms.Panel();
            label5 = new System.Windows.Forms.Label();
            textBox5 = new System.Windows.Forms.TextBox();
            label6 = new System.Windows.Forms.Label();
            textBox6 = new System.Windows.Forms.TextBox();

            panel2.SuspendLayout();
            panel2.BackColor = System.Drawing.SystemColors.ActiveCaption;
            panel2.Controls.Add(label5);
            panel2.Controls.Add(textBox5);
            panel2.Controls.Add(label6);
            panel2.Controls.Add(textBox6);
            panel2.Location = new System.Drawing.Point(x, y);
            panel2.Name = "panel2";
            panel2.Size = new System.Drawing.Size(287, 78);
            panel2.TabIndex = 7;
            panel2.Visible = true;

            label5.AutoSize = true;
            label5.Location = new System.Drawing.Point(147, 9);
            label5.Name = "label5";
            label5.Size = new System.Drawing.Size(54, 13);
            label5.TabIndex = 3;
            label5.Text = "Geboortedatum:";

            textBox5.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            textBox5.Location = new System.Drawing.Point(150, 25);
            textBox5.Name = "textBox5";
            textBox5.Size = new System.Drawing.Size(123, 26);
            textBox5.TabIndex = 2;

            label6.AutoSize = true;
            label6.Location = new System.Drawing.Point(12, 9);
            label6.Name = "label6";
            label6.Size = new System.Drawing.Size(38, 13);
            label6.TabIndex = 1;
            label6.Text = "Naam:";

            textBox6.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            textBox6.Location = new System.Drawing.Point(15, 25);
            textBox6.Name = "textBox6";
            textBox6.Size = new System.Drawing.Size(129, 26);
            textBox6.TabIndex = 0;

            form.Controls.Add(panel2);
        }

        public void Delete()
        {
            mainForm.Controls.Remove(panel2);
        }

        public void SetYLocation(int y)
        {
            panel2.Location = new Point(panel2.Location.X,y);
        }

        //Control
        public System.Windows.Forms.Panel panel2;
        public System.Windows.Forms.Label label5;
        public System.Windows.Forms.TextBox textBox5;
        public System.Windows.Forms.Label label6;
        public System.Windows.Forms.TextBox textBox6;
    }
}

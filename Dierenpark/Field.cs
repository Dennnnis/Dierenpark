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
    public class CField
    {
        //Form
        private Form mainForm;
        private List<CField> mainFields;

        //Partner / Secondary
        public int SecondaryRelativeX = 370;
        public bool echtPaar = false;
        public SecondaryField partner;

        public CField(List<CField> fields, Form form)
        {
            mainFields = fields;
            mainForm = form;

            panel1 = new System.Windows.Forms.Panel();
            textBox3 = new System.Windows.Forms.TextBox();
            label3 = new System.Windows.Forms.Label();
            checkBox1 = new System.Windows.Forms.CheckBox();
            label2 = new System.Windows.Forms.Label();
            textBox2 = new System.Windows.Forms.TextBox();
            label1 = new System.Windows.Forms.Label();
            textBox1 = new System.Windows.Forms.TextBox();
            button1 = new System.Windows.Forms.Button();

            panel1.SuspendLayout();
            panel1.BackColor = System.Drawing.SystemColors.ActiveCaption;
            panel1.Controls.Add(button1);
            panel1.Controls.Add(textBox3);
            panel1.Controls.Add(label3);
            panel1.Controls.Add(checkBox1);
            panel1.Controls.Add(label2);
            panel1.Controls.Add(textBox2);
            panel1.Controls.Add(label1);
            panel1.Controls.Add(textBox1);
            panel1.Location = new System.Drawing.Point(12, 56);
            panel1.Name = "panel1";
            panel1.Size = new System.Drawing.Size(364, 78);
            panel1.TabIndex = 0;

            textBox3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            textBox3.Location = new System.Drawing.Point(279, 25);
            textBox3.Name = "textBox3";
            textBox3.Size = new System.Drawing.Size(59, 26);
            textBox3.TabIndex = 2;
            textBox3.Text = "0";

            label3.AutoSize = true;
            label3.Location = new System.Drawing.Point(276, 9);
            label3.Name = "label3";
            label3.Size = new System.Drawing.Size(52, 13);
            label3.TabIndex = 5;
            label3.Text = "Kinderen:";

            checkBox1.AutoSize = true;
            checkBox1.Location = new System.Drawing.Point(15, 57);
            checkBox1.Name = "checkBox1";
            checkBox1.Size = new System.Drawing.Size(69, 17);
            checkBox1.TabIndex = 3;
            checkBox1.Text = "Echtpaar";
            checkBox1.UseVisualStyleBackColor = true;
            checkBox1.CheckedChanged += new System.EventHandler(checkBox1_CheckedChanged);

            label2.AutoSize = true;
            label2.Location = new System.Drawing.Point(147, 9);
            label2.Name = "label2";
            label2.Size = new System.Drawing.Size(54, 13);
            label2.TabIndex = 5;
            label2.Text = "Geboortedatum:";

            textBox2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            textBox2.Location = new System.Drawing.Point(150, 25);
            textBox2.Name = "textBox2";
            textBox2.Size = new System.Drawing.Size(123, 26);
            textBox2.TabIndex = 1;

            label1.AutoSize = true;
            label1.Location = new System.Drawing.Point(12, 9);
            label1.Name = "label1";
            label1.Size = new System.Drawing.Size(38, 13);
            label1.TabIndex = 5;
            label1.Text = "Naam:";

            textBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            textBox1.Location = new System.Drawing.Point(15, 25);
            textBox1.Name = "textBox1";
            textBox1.Size = new System.Drawing.Size(129, 26);
            textBox1.TabIndex = 0;

            button1.Location = new System.Drawing.Point(341, 0);
            button1.Name = "button1";
            button1.Size = new System.Drawing.Size(23, 23);
            button1.TabIndex = 5;
            button1.Text = "X";
            button1.UseVisualStyleBackColor = true;
            button1.Click += new System.EventHandler(button1_click);

            form.Controls.Add(panel1);
        }

        //Exit
        public void button1_click(object sender, EventArgs e)
        {
             mainFields.Remove(this);
             Delete();
        }

        //Echtpaar
        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                echtPaar = true;
                partner = new SecondaryField(mainForm, panel1.Location.X + SecondaryRelativeX,panel1.Location.Y);
                RefreshSizes();
            }
            else
            {
                echtPaar = false;
                partner.Delete();
            }
        }

        //Delete
        public void Delete()
        {
            if (echtPaar)
                partner.Delete();

            mainForm.Controls.Remove(panel1);
            RefreshSizes();
        }

        //Update all sizes/positions
        public void RefreshSizes()
        {
            for (int i = 0; i < mainFields.Count; i++)
            {
                int y = 55 + ((i + 1) * (panel1.Size.Height + 5));

                if (y + panel1.Size.Height+60 > mainForm.Height)
                {
                    mainForm.Height = y + panel1.Size.Height+60;
                    mainForm.MinimumSize = new Size(mainForm.MinimumSize.Width, y + panel1.Size.Height + 60);
                }

                mainFields[i].panel1.Location = new System.Drawing.Point(12,y);
                if (mainFields[i].echtPaar)
                    mainFields[i].partner.SetYLocation(y);
            }
        }

        //Control
        public Panel panel1;
        public Button button1;
        public TextBox textBox3;
        public Label label3;
        public CheckBox checkBox1;
        public Label label2;
        public TextBox textBox2;
        public Label label1;
        public TextBox textBox1;
    }
}

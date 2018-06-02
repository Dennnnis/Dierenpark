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
    public partial class Form1 : Form
    {
        public static decimal prijs_persoonlijk = 30.0m;
        public static decimal prijs_echtpaar = 61.0m;
        public static decimal prijs_GezinMet1Kinderen = 71.0m; //Onduidelijk of dit ook geld voor 65+ ik neem aan van wel
        public static decimal prijs_GezinMet2Kinderen = 82.0m; //Als het niet geld voor 65+ zou deze stelling nutteloos zijn                     
        public static decimal prijs_extraKind = 11.0m;
        public static decimal prijs_echtpaarB65 = 65.0m;       //Zou eigenlijk lager moeten zijn ¯\_(ツ)_/¯
        public static decimal prijs_persoonlijkB65 = 26.0m;


        public Form1()
        {
            InitializeComponent();
        }


        public class CPrijs
        {
            public decimal bedrag = 0.0m;
            public string reden = "Weet ik niet.";
            public CPrijs Add(CPrijs cprijs,string reden)
            {
                CPrijs newCPrijs = new CPrijs();
                newCPrijs.bedrag = cprijs.bedrag + this.bedrag;
                newCPrijs.reden = reden;
                return newCPrijs;
            }
        }


        public List<CPrijs> bon = new List<CPrijs>();
        

        public List<CField> fields = new List<CField>();

        //field toevegen (aka persoon/echtpaar)
        private void button2_Click(object sender, EventArgs e)
        {
            fields.Add(new CField(fields,this));
            fields.Last().RefreshSizes();
        }

        //Bereken leeftijd
        private int LeeftijdBerekenen(DateTime datum)
        {
            var age = DateTime.Today.Year - datum.Year;
            if (datum > DateTime.Today.AddYears(-age)) age--;
            return age;
        }


        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            panel2.Visible = checkBox1.Checked;
        }

        //Field uitrekenen en toevoegen aan bon
        private bool CalculateField(string name,string birth, int kids, bool echtpaar, string partnerBirth = "", string partnerName = "")
        {
            //Variables
            int age = LeeftijdBerekenen(Convert.ToDateTime(birth));
            int partnerAge;

            if (age < 18)
            {
                MessageBox.Show("Je moet minimaal 18 zijn.", "Error");
                return false;
            }

            if (echtpaar)
            {
                //Echtpaar
                bon.Add(new CPrijs { reden = name + " en " + partnerName });

                partnerAge = LeeftijdBerekenen(Convert.ToDateTime(partnerBirth));

                if (partnerAge < 18)
                {
                    MessageBox.Show("Je moet minimaal 18 zijn.", "Error");
                    return false;
                }
                
                if (((age + partnerAge) / 2) > 65) //Onduidelijk
                {
                    //65+ Echtpaar
                    if (kids == 0) 
                        bon.Add(new CPrijs { bedrag = prijs_echtpaarB65, reden = "Echtpaar 65+"});
                    else if (kids == 1) //Gezin met 1 kind
                        bon.Add(new CPrijs { bedrag = prijs_GezinMet1Kinderen, reden = "Gezin met 1 kind" });
                    else if (kids == 2) //Gezin met 2 kinderen
                        bon.Add(new CPrijs { bedrag = prijs_GezinMet2Kinderen, reden = "Gezin met 2 kinderen" });
                    else if (kids > 2) //Gezin met meer kinderen
                        bon.Add(new CPrijs { bedrag = prijs_GezinMet2Kinderen + (11 * kids - 2), reden = "Gezin met " + kids.ToString() + " kinderen" });
                    return true;
                }
                else
                {
                    //Normaal Echtpaar
                    if (kids == 0) 
                        bon.Add(new CPrijs { bedrag = prijs_echtpaar, reden = "Kosten voor echtpaar" });
                    else if (kids == 1) //Gezin met 1 kind
                        bon.Add(new CPrijs { bedrag = prijs_GezinMet1Kinderen, reden = "Gezin met 1 kind" });
                    else if (kids == 2) //Gezin met 2 kinderen
                        bon.Add(new CPrijs { bedrag = prijs_GezinMet2Kinderen, reden = "Gezin met 2 kinderen" });
                    else if (kids > 2) //Gezin met meer kinderen
                        bon.Add(new CPrijs { bedrag = prijs_GezinMet2Kinderen + (11 * kids-2), reden = "Gezin met "+ kids.ToString() + " kinderen" });
                    return true;
                }
            }
            else
            {
                //Geen echtpaar
                bon.Add(new CPrijs {reden = name});

                if (age > 65)
                {
                    bon.Add(new CPrijs { bedrag = prijs_persoonlijkB65, reden = "Persoonlijk 65+" });
                    
                    if (kids > 0)
                        bon.Add(new CPrijs { bedrag = prijs_extraKind*kids, reden = kids.ToString() + " kind(eren)" });
                    return true;
                }
                else
                {
                    bon.Add(new CPrijs { bedrag = prijs_persoonlijk, reden = "Persoonlijk" });

                    if (kids > 0)
                        bon.Add(new CPrijs { bedrag = prijs_extraKind * kids, reden = kids.ToString() + " kind(eren)" });
                    return true;
                }
            }
        }


        private bool BerekenenBon()
        {
            bon.Clear();

            try
            {
                if (!CalculateField(textBox1.Text, textBox2.Text, Convert.ToInt32(textBox3.Text), checkBox1.Checked, textBox5.Text, textBox6.Text))
                    return false; 

                foreach (CField cf in fields)
                {
                    if (cf.checkBox1.Checked)
                    {
                        if (!CalculateField(cf.textBox1.Text, cf.textBox2.Text, Convert.ToInt32(cf.textBox3.Text), cf.checkBox1.Checked, cf.partner.textBox5.Text, cf.partner.textBox6.Text))
                            return false;
                    }
                    else
                    {
                        if (!CalculateField(cf.textBox1.Text, cf.textBox2.Text, Convert.ToInt32(cf.textBox3.Text), cf.checkBox1.Checked))
                            return false; 
                    }
                }
            }
            catch(Exception)
            {
                MessageBox.Show( "Ongeldige datum/kinderen", "Error");
                return false;
            }
            return true;
        }


        private void button1_Click(object sender, EventArgs e)
        {
            if (BerekenenBon())
            {
                Form2 f2 = new Form2(bon);
                f2.ShowDialog();
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Clinic_Business_Layer;

namespace Simple_Clinic
{
    public partial class Form1 : Form
    {
        int FailesCounter = 1;
        TimeSpan ReOpenSystem = new TimeSpan(0, 0, 0);
        public Form1()
        {
            InitializeComponent();
        }

        public string UserName=string.Empty;

        private void Form1_Load(object sender, EventArgs e)
        {
           label1.ForeColor = Color.FromArgb(74, 74, 74);  // Charcoal Gray
            lblLockedSystem.Enabled = false;
            lblLockedSystem.Text= string.Empty;
            lblLockedSystem.ForeColor = Color.Red;
            
        }

        private void DisabelAll()
        {
            tbPassword.Enabled = false;
            tbUserName.Enabled = false;
            btnLogin.Enabled = false;
        }

        private void EnabelAll()
        {
            tbPassword.Enabled = true;
            tbUserName.Enabled = true;
            btnLogin.Enabled =   true;
            ReOpenSystem = new TimeSpan(0, 0, 0);

        }

        private void OpenSystem()
        {
            timer1.Stop();
            EnabelAll();
            FailesCounter = 1;
            lblLockedSystem.Text = "";
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            if (tbUserName.Text==string.Empty||tbPassword.Text==string.Empty)
                {
                MessageBox.Show("Can Not Insert Empty Filed.");
                return; }
            if (clsUser.IsUserFound(tbUserName.Text,tbPassword.Text))
            {
                UserName = tbUserName.Text;
                this.DialogResult = DialogResult.OK;
                this.Close();
                return;
            }
            if(FailesCounter==3)
            {
                if(MessageBox.Show("System Is Locked For 1 Minute")==DialogResult.OK)
                {
                    DisabelAll();
                    timer1.Start();
                }

            }
            lblLockedSystem.Text = "You Can Try "+(3-FailesCounter).ToString() + "Times.";
            FailesCounter++;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            lblLockedSystem.Enabled = true;
            if (ReOpenSystem == new TimeSpan(0, 0, 59))
            {
                OpenSystem();
                return;
            }
            lblLockedSystem.Text = "System is Locked for :" + ReOpenSystem.ToString();
            ReOpenSystem += new TimeSpan(0, 0, 1);

        }
    }
}

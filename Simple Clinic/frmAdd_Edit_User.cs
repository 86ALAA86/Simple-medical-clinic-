using Clinic_Business_Layer;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Simple_Clinic
{
    public partial class frmAdd_Edit_User : Form
    {
        enum enMode { enAddNew, enUpdate }
        public frmAdd_Edit_User(int Mode)
        {
            InitializeComponent();
            if (Mode == -1)
            {
                _Mode = enMode.enAddNew;
            }
            else
            {
                _Mode = enMode.enUpdate;
            }
            _UserID = Mode;
        }
                
        int _UserID;
        enMode _Mode;
        clsUser _User;

        private void frmAdd_Edit_User_Load(object sender, EventArgs e)
        {
            if(_Mode == enMode.enAddNew)
            {
                lblTitle.Text = "Add New ";
                _User = new clsUser();
                return;
            }
            lblTitle.Text = "Update ";
            _User=clsUser.FindUser(_UserID);
            tbName.Text = _User.UserName;
            tbPassword.Text = _User.Password;

        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            _User.UserName = tbName.Text;
            _User.Password = tbPassword.Text;
            if(_User.Save())
            {
                MessageBox.Show("User Saved Successfully.");
                this.Close();
            }
            else
            {
                MessageBox.Show("User Was Not Saved.");
                this.Close();
            }
        }
    }
}

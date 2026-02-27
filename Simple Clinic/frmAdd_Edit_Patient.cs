using Clinic_Business_Layer;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Simple_Clinic
{
    public partial class frmAdd_Edit_Patient : Form
    {
        public frmAdd_Edit_Patient(int Mode)
        {
            InitializeComponent();
            if (Mode == -1)
                _Mode = enMode.enAddNew;
            else
            {
                _Mode = enMode.enUpdate;
                PatID = Mode;
            }
        }

        enum enMode { enAddNew,enUpdate}

        enMode _Mode;
        int PatID = -1;

        clsPerson _Person;
        clsPatient _Patient;


        private void frmAdd_Edit_Patient_Load(object sender, EventArgs e)
        {
            if (_Mode == enMode.enAddNew)
            {
                lblFormTitle.Text = "Add New Patient";
                _Person = new clsPerson();
               _Patient = new clsPatient();
                return;
            }
            if (_Mode == enMode.enUpdate)
            {
                lblFormTitle.Text = "Update Patient";
            }
            _Patient = clsPatient.FindPatient(PatID);
            _Person = clsPerson.FindPerson(_Patient.PersonID);

            lblID.Text = PatID.ToString();
            tbAddress.Text = _Person.Address;
            tbEmail.Text = _Person.Email;
            tbName.Text = _Person.Name;
            tbPhone.Text = _Person.Phone;
            dtpDateOfBirth.Value = _Person.DateOfBirth;
            if (_Person.Gender == 'M')
                cbGender.SelectedIndex = 0;
            else
                cbGender.SelectedIndex = 1;
        }

       

        private void btnSave_Click(object sender, EventArgs e)
        {
            
                _Person.Name = tbName.Text;
                _Person.Email = tbEmail.Text;

                if (cbGender.Text == "Male")
                {
                    _Person.Gender = 'M';
                }
                else
                {
                    _Person.Gender = 'F';
                }

                _Person.Phone = tbPhone.Text;
                _Person.Address = tbAddress.Text;
                _Person.DateOfBirth = dtpDateOfBirth.Value;

               
                if (_Mode == enMode.enAddNew)
                    _Patient.PersonID= _Person.AddNewPerson();
                if (_Mode == enMode.enUpdate)
                {
                    _Person.UpdatePerson();
                }


                if (_Patient.Save())
                {
                    MessageBox.Show("New Patient Saved");
                }
                else
                {
                    MessageBox.Show("New Patient is Not Saved");
                }

            

        }

        private void updateToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }
    }
}

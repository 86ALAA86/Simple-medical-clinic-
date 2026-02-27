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
using Clinic_Business_Layer;
using static Clinic_Business_Layer.clsDoctor;
using System.Diagnostics.Contracts;

namespace Simple_Clinic
{
    public partial class frmAdd_Edit_Doctor : Form
    {
        public frmAdd_Edit_Doctor(int Mode)
        {
            InitializeComponent();

            if (Mode == -1)
            {
                _Mode = enMode.AddNew;             
            }
            else
            {
                _Mode = enMode.Update;
                DocID = Mode;
            }
        }

        int DocID = -1;

        enum enMode
        {
            AddNew, Update
        }

        clsDoctor _Doctor;
        clsEmployee _Employee;
        clsPerson _Person;
        enMode _Mode;

      private void frmAdd_Edit_Load(object sender, EventArgs e)
        {
            _LoadSpecializationsCB();
            if (_Mode == enMode.AddNew)
            {
                lblFormTitle.Text = "Add New Doctor";
                _Person = new clsPerson();
                _Employee = new clsEmployee();
                _Doctor = new clsDoctor();
                return;
            }
            if(_Mode==enMode.Update)
            {
                lblFormTitle.Text = "Update Doctor";
            }

            _Doctor = clsDoctor.FindDoctor(DocID);
            _Employee = clsEmployee.FindEmployee(_Doctor.EmployeeID);
            _Person = clsPerson.FindPerson(_Employee.PersonID);

            lblID.Text =DocID.ToString();
            tbAddress.Text = _Person.Address;
            tbEmail.Text = _Person.Email;
            tbName.Text = _Person.Name;
            tbPhone.Text = _Person.Phone;
            tbSalary.Text = _Employee.Salary.ToString();
            dtpDateOfBirth.Value=_Person.DateOfBirth;
            cbSpecializations.SelectedIndex= cbSpecializations.FindString((clsSpecialization.Find(_Doctor.SpecializationID).Name));
            if (_Person.Gender == 'M')
                cbGender.SelectedIndex = 0;
            else
                cbGender.SelectedIndex = 1;

        }

      private void _LoadSpecializationsCB()
            {
                DataTable dt = clsSpecialization.GetAllSpecializations();
                foreach (DataRow dr in dt.Rows)
                {
                    cbSpecializations.Items.Add(dr["Name"]);
                }
            }

     
      private void btnSave_Click(object sender, EventArgs e)
      {
          int SpecializationID = clsSpecialization.Find(cbSpecializations.Text).ID;
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
            
            _Employee.JobID = 1;
          _Employee.Salary = Convert.ToDouble(tbSalary.Text);

          _Doctor.SpecializationID = SpecializationID;
            if (_Mode == enMode.AddNew)
            {
                _Employee.PersonID = _Person.AddNewPerson();
                _Doctor.EmployeeID = _Employee._AddNewEmployee();
            }

            if (_Mode == enMode.Update)
            {
                _Person.UpdatePerson();
                _Employee.UpdateEmployee();
            }


      if (_Doctor.Save())
      {
          MessageBox.Show("New Doctor Saved");
      }
      else
      {
          MessageBox.Show("New Doctor is Not Saved");
      }

      }

        private void cbSpecializations_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}

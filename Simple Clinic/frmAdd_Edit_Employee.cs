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
    public partial class frmAdd_Edit_Employee : Form
    {
        enum enMode { enAddNew,enUpdate}
        public frmAdd_Edit_Employee(int Mode)
        {
            InitializeComponent();
            if(Mode ==-1)
            {
                _EnMode=enMode.enAddNew;
            }
            else
            {
                _EnMode = enMode.enUpdate;
            }
            _EmpID = Mode;

        }

        int _EmpID;
        clsEmployee _Employee;
        clsPerson _Person;
        enMode _EnMode;

        private void _LoadcbJobs()
        {
            DataTable dt = clsJob.GetAllJobs();
            foreach(DataRow JobR in dt.Rows)
            {
                cbJobs.Items.Add(JobR["JobName"].ToString());
            }
        }

        private void frmAdd_Edit_Employee_Load(object sender, EventArgs e)
        {
            _LoadcbJobs();

            if(_EnMode == enMode.enAddNew)
            {
                _Employee =new clsEmployee();
                _Person = new clsPerson();
                lblFormTitle.Text = "Add New";
                return;
            }
            lblFormTitle.Text = "Update Employee";

            lblID.Text=_EmpID.ToString();

            _Employee=clsEmployee.FindEmployee(_EmpID);
            _Person=clsPerson.FindPerson(_Employee.PersonID);

            tbAddress.Text = _Person.Address;
            tbEmail.Text= _Person.Email;
            tbName.Text = _Person.Name;
            tbPhone.Text = _Person.Phone;
            dtpDateOfBirth.Value=_Person.DateOfBirth;

            if (_Person.Gender == 'M')
                cbGender.SelectedIndex = 0;
            else
                cbGender.SelectedIndex = 1;

            tbSalary.Text=_Employee.Salary.ToString(); 

            cbJobs.SelectedIndex=cbJobs.FindString(clsJob.FindJobName(_EmpID));

        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            _Employee.Salary = Convert.ToDouble(tbSalary.Text.ToString());
            _Employee.JobID = clsJob.FindJobID(cbJobs.Text);

            _Person.Phone=tbPhone.Text;
            _Person.Name=tbName.Text;
            _Person.Email=tbEmail.Text;
            _Person.Address=tbAddress.Text;
            if (cbGender.SelectedIndex == 0)
                _Person.Gender = 'M';
            else
                _Person.Gender = 'F';
            _Person.DateOfBirth=dtpDateOfBirth.Value;

            if(_EnMode==enMode.enAddNew)
            {
                _Employee.PersonID = _Person.AddNewPerson();
            }
            if(_EnMode==enMode.enUpdate)
            {
                _Person.UpdatePerson();
            }

            if(_Employee.Save())
            {
                MessageBox.Show("Employee Was Saved");
                this.Close();
            }
            else
            {
                MessageBox.Show("Employee Was Not Saved");
            }

        }
    }
}

using Clinic_Business_Layer;
using Guna.UI2.WinForms;
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
    public partial class frmMain : Form
    {
        private string UserName = string.Empty;
        public frmMain(string UserName)
        {
            InitializeComponent();
            this.UserName = UserName;
        }

        public frmMain()
        {
            InitializeComponent();

        }

        private void LogUserOut()
        {
            Application.Restart();
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            lblUserName.Text = UserName;
            pbUserPic.Image = imageList1.Images[7];
            RefreshDoctors();

        }

        private void tcMain_SelectedIndexChanged(object sender, EventArgs e)
        {
            isMedicalRecord = false;

            switch (tcMain.SelectedIndex)
            {
                case 0:
                    RefreshDoctors();
                    break;
                case 1:
                    RefreshPatients();
                    break;
                case 2:
                    RefreshAppointments();
                    break;
                case 3:
                    RefershPayments();
                    break;
                case 4:
                    RefreshEmployees();
                    break;
                case 5:
                    RefereshUsers();
                    break;
                case 6:
                    LogUserOut();
                    break;

                default:
                    break;

            }

        }


        //Doctors Tap Functions are Below :        
        private void RefreshDoctors()
        {
            dgvDoctors.DataSource = clsDoctor.GetAllDoctors();
        }
        private void btnAddDoctor_Click(object sender, EventArgs e)
        {

            frmAdd_Edit_Doctor frm = new frmAdd_Edit_Doctor(-1);
            frm.ShowDialog();
            RefreshDoctors();
        }

        private void updatToolStripMenuItem_Click(object sender, EventArgs e)
        {

            isMedicalRecord = false;
            switch (tcMain.SelectedIndex)
            {
                case 0:

                    frmAdd_Edit_Doctor frm = new frmAdd_Edit_Doctor((int)dgvDoctors.CurrentRow.Cells[0].Value);
                    frm.ShowDialog();
                    RefreshDoctors(); break;
                case 1:

                    frmAdd_Edit_Patient frm1 = new frmAdd_Edit_Patient((int)dgvPatients.CurrentRow.Cells[0].Value);
                    frm1.ShowDialog();
                    RefreshPatients();
                    break;

                case 2:
                    frmAdd_Edit_Appointment frm2 = new frmAdd_Edit_Appointment((int)dgvAppointments.CurrentRow.Cells[0].Value);
                    frm2.ShowDialog();
                    RefreshAppointments();
                    break;

                case 3:
                    frmAdd_Edit_Payment frm3 = new frmAdd_Edit_Payment((int)dgvPayments.CurrentRow.Cells[0].Value);
                    frm3.ShowDialog();
                    RefershPayments();
                    break;

                case 4:
                    frmAdd_Edit_Employee frm4 = new frmAdd_Edit_Employee((int)dgvEmployees.CurrentRow.Cells[0].Value);
                    frm4.ShowDialog();
                    RefreshEmployees();
                    break;

                case 5:
                    frmAdd_Edit_User frm5 = new frmAdd_Edit_User((int)dgvUsers.CurrentRow.Cells[0].Value);
                    frm5.ShowDialog();
                    RefereshUsers();
                    break;


                default:
                    break;
            }

        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {

            switch (tcMain.SelectedIndex)
            {
                case 0:
                    if (MessageBox.Show("Do you want to Delete this Doctor ?", "Confirmation", MessageBoxButtons.OKCancel)
                                   == DialogResult.OK)
                    {
                        clsDoctor.DeleteDoctor((int)dgvDoctors.CurrentRow.Cells[0].Value);
                        MessageBox.Show("Doctor Deleted Successfully.");
                        RefreshDoctors();
                    }
                    break;
                case 1:
                    if (MessageBox.Show("Do you want to Delete this Patient ?", "Confirmation", MessageBoxButtons.OKCancel)
                                                       == DialogResult.OK)
                    {
                        clsPatient.DeletePatient((int)dgvPatients.CurrentRow.Cells[0].Value);
                        MessageBox.Show("Patinet Deleted Successfully.");
                        RefreshPatients();
                    }
                    break;
                case 2:
                    if (MessageBox.Show("Do you want to Delete this Appointment ?", "Confirmation", MessageBoxButtons.OKCancel)
                                                       == DialogResult.OK)
                    {
                        //clsAppointment.DeleteAppointment((int)dgvAppointments.CurrentRow.Cells[0].Value);
                        //MessageBox.Show("Appointment Deleted Successfully.");
                        //RefreshAppointments();
                    }
                    break;
                case 3:
                    if (MessageBox.Show("Do you want to Delete this Payment ?", "Confirmation", MessageBoxButtons.OKCancel)
                                                       == DialogResult.OK)
                    {
                        clsPayment.DeletePayment((int)dgvPayments.CurrentRow.Cells[0].Value);
                        MessageBox.Show("Payment Deleted Successfully.");
                        RefershPayments();
                    }
                    break;

                case 4:
                    if (MessageBox.Show("Do you want to Delete this Employee ?", "Confirmation", MessageBoxButtons.OKCancel)
                                                       == DialogResult.OK)
                    {
                        clsEmployee.DeleteEmployee((int)dgvEmployees.CurrentRow.Cells[0].Value);
                        MessageBox.Show("Employee Deleted Successfully.");
                        RefreshEmployees();
                    }
                    break;

                case 5:

                    if ((int)dgvUsers.CurrentRow.Cells[0].Value == 1)
                    {
                        MessageBox.Show("Can Not Delete The Admin Of The System .");
                        return;
                    }

                    if (MessageBox.Show("Do you want to Delete this User ?", "Confirmation", MessageBoxButtons.OKCancel)
                                                       == DialogResult.OK)
                    {
                        clsUser.DeleteUser((int)dgvUsers.CurrentRow.Cells[0].Value);
                        MessageBox.Show("User Deleted Successfully.");
                        RefereshUsers();
                    }
                    break;

                default:
                    break;

            }

        }


        //Patients Tap Functions are Below :        
        private void RefreshPatients()
        {
            dgvPatients.DataSource = clsPatient.GetAllPatients();
        }

        private void btnAddPatient_Click_1(object sender, EventArgs e)
        {

            frmAdd_Edit_Patient frm = new frmAdd_Edit_Patient(-1);
            frm.ShowDialog();
            RefreshPatients();
        }

        //Appointments Tap Functions are Below :

        private void RefreshAppointments()
        {
            dgvAppointments.DataSource = clsAppointment.GetAllAppointments();
        }

        private void btnAddAppointment_Click(object sender, EventArgs e)
        {
            frmAdd_Edit_Appointment frm = new frmAdd_Edit_Appointment(-1);
            frm.ShowDialog();
            RefreshAppointments();
        }

        private bool isMedicalRecord = false;
        private void btnShowMedRecords_Click(object sender, EventArgs e)
        {
            dgvAppointments.DataSource = clsMedRecord.GetAllMedRecords();
            isMedicalRecord = true;
        }

        //Payments
        private void btnAddPayment_Click(object sender, EventArgs e)
        {
            frmAdd_Edit_Payment frm = new frmAdd_Edit_Payment(-1);
            frm.ShowDialog();
            RefershPayments();

        }

        private void RefershPayments()
        {
            dgvPayments.DataSource = clsPayment.GetAllPayments();
        }

        //Employees 

        private void RefreshEmployees()
        {
            dgvEmployees.DataSource = clsEmployee.GetAllEmployees();
        }

        private void btnAddEmployee_Click(object sender, EventArgs e)
        {
            frmAdd_Edit_Employee frm = new frmAdd_Edit_Employee(-1);
            frm.ShowDialog();
            RefreshEmployees();
        }

        private void contextMenuStrip1_Opening(object sender, CancelEventArgs e)
        {
            if (isMedicalRecord)
            {
                e.Cancel = true;
            }
        }

        private void RefereshUsers()
        {
            dgvUsers.DataSource=clsUser.GetAllUsers();
        }

        private void btnAddUser_Click(object sender, EventArgs e)
        {
            frmAdd_Edit_User frm = new frmAdd_Edit_User(-1);
            frm.ShowDialog();
            RefereshUsers();
            
        }
    }
}

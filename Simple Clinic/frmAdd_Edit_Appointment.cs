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
    public partial class frmAdd_Edit_Appointment : Form
    {
        public frmAdd_Edit_Appointment(int mode)
        {
            InitializeComponent();

            if (mode == -1)
            {
                _Mode = enMode.AddNew;
                _AppID = -1;
            }
            else
            {
                _Mode = enMode.Update;
                _AppID = mode;
            }

        }
        private int _AppID ;

        private void _LoadDocsShortCutCB()
        {
            DataTable dt = clsDoctor.GetAllDoctorsShortCut();
            foreach (DataRow dr in dt.Rows)
            {
                string item = dr["DoctorID"].ToString()+" ," + dr["Name"] +" ,"+ dr["Specialization"];
                cbDoctor.Items.Add(item);
            }
        }

        private void _LoadPatsShortCutCB()
        {
            DataTable dt = clsPatient.GetAllPatientsShortCut();
            foreach (DataRow pt in dt.Rows)
            {
                string item = pt["PatientID"].ToString() + " ," + pt["Name"];
                cbPatient.Items.Add(item);
            }
        }

        private void _LoadStatusCB()
        {
            DataTable dt = clsStatus.GetAllStatuses();
            foreach (DataRow st in dt.Rows)
            {
                cbStatus.Items.Add(st["Name"]);
            }
        }

        enum enMode { AddNew, Update };

        private enMode _Mode;

        private clsAppointment _Appointment;

        private string _cbDoctorsText(int ID)
        {
            string Name = string.Empty;
            string SP=string.Empty;
            if (clsDoctor.GetSelectedDoctorShortCut(ID, ref Name, ref SP))
            {
               
                return ID.ToString() + " ," + Name + " ," + SP;
            }
            else return "";

        }


        private string _cbPatientText(int ID)
        {
            string Name = string.Empty;
            if (clsPatient.GetSelectedPatientShortCut(ID, ref Name))
            {

                return ID.ToString() + " ," + Name ;
            }
            else return "";

        }


        private void frmAdd_Edit_Appointment_Load(object sender, EventArgs e)
        {
            dtpAppointmentTime.Format = DateTimePickerFormat.Custom;
            dtpAppointmentTime.CustomFormat = "HH:mm";
            _LoadDocsShortCutCB();
            _LoadPatsShortCutCB();
            _LoadStatusCB();

            if(_Mode==enMode.AddNew)
            {
                _Appointment = new clsAppointment();
                cbStatus.Enabled = false;
                cbStatus.Visible = false;
                lblchoseStatus.Visible = false;
                lblchoseStatus.Enabled = false;
                btnAddMedReocrd.Enabled = false;
                btnAddMedReocrd.Visible = false;
                return;
            }

            _Appointment = clsAppointment.FindAppointment(_AppID);

            //if (_Appointment.AppintmentDate < DateTime.Now)
            //{
            //    MessageBox.Show("The system prevents edits after the appointment date.");
            //    this.Close();

            //}

            cbDoctor.Text = _cbDoctorsText(_Appointment.DoctorID);
            cbDoctor.Enabled = clsDoctor.IsActiveDoctor(_Appointment.DoctorID);
            
            cbPatient.Text = _cbPatientText(_Appointment.PatientID); 
            cbPatient.Enabled=clsPatient.IsActivePatient(_Appointment.PatientID);

            dtpAppointmentDate.Value = _Appointment.AppintmentDate;
            DateTime Time = DateTime.Today.Add(_Appointment.AppointmentTime);
            dtpAppointmentTime.Value = Time;
            cbStatus.SelectedIndex = cbStatus.FindString(clsStatus.Find(_Appointment.StatusID).Name);
            if(_Appointment.StatusID!=3)
            {
                btnAddMedReocrd.Enabled = false;
            }

        }


        private int _FindcbIndex(ComboBox cb,int ID)
        {

            return cb.FindString((ID.ToString()));
        }

        private void cbDoctor_SelectedIndexChanged(object sender, EventArgs e)
        {
           // string test = cbDoctor.Items[cbDoctor.SelectedIndex].ToString();
              //lblTest.Text=test.Substring(0, test.IndexOf(' '));
        }

        private int _FindcbID(ComboBox cb)
        {
            string test =cb.Items[cb.SelectedIndex].ToString();
            return Convert.ToInt32(test.Substring(0, test.IndexOf(' ')));

        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (cbStatus.SelectedIndex != 2 && _Appointment.MedRecordID != -1)
            {
                if (MessageBox.Show("You Can't Have A Medical Record if Status is Not \"Completed\"\n" +
                     "Are You Want to Save ?", "Confirmation", MessageBoxButtons.OKCancel) == DialogResult.Cancel)
                {
                    return;
                }
                else
                {
                    clsAppointment.MakeMedRecordNull(_Appointment.AppointmentsID);
                    clsMedRecord.DeleteMR(_Appointment.MedRecordID);
                    _Appointment.MedRecordID = -1;
                }

        }
            if(cbPatient.Enabled==true)
           _Appointment.PatientID = _FindcbID(cbPatient);

            if(cbDoctor.Enabled==true)
            _Appointment.DoctorID = _FindcbID(cbDoctor);

            if (_Mode == enMode.AddNew)
            {
                _Appointment.StatusID = 1;
                
            }
            if (_Mode == enMode.Update)
            {
                _Appointment.StatusID = clsStatus.Find(cbStatus.Text).StatusID;
                
            }
            _Appointment.AppintmentDate = dtpAppointmentDate.Value;
            DateTime DT=dtpAppointmentTime.Value;
            _Appointment.AppointmentTime=DT.TimeOfDay;


            if (_Mode == enMode.AddNew)
            {
                if (clsAppointment.IsDoctorBusy(_Appointment.DoctorID,
                    _Appointment.AppintmentDate, _Appointment.AppointmentTime))
                {
                    MessageBox.Show("Doctor Can't Have This Appointment Because he Have Anouther Appointment.");
                    return;
                }

            }
           

            

            if (_Appointment.Save())
            {
                MessageBox.Show("New Appointment Saved");
                this.Close();
            }
            else
            {
                MessageBox.Show("New Appointment is Not Saved");
            }

        }

        private void btnAddMedReocrd_Click(object sender, EventArgs e)
        {
             frmAdd_Edit_MedRecord frm = new frmAdd_Edit_MedRecord(_Appointment.MedRecordID,_AppID
                 ,_Appointment.DoctorID,_Appointment.PatientID);
            frm.ShowDialog();
            _Appointment.MedRecordID=frm.MedicalId;
            
        }

        private void cbStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbStatus.SelectedIndex == 2)
            {
                btnAddMedReocrd.Enabled = true;
            }
            else
            {
                btnAddMedReocrd.Enabled=false;
            }
        }
    }
}


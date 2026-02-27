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
    public partial class frmAdd_Edit_MedRecord : Form
    {
        enum enMode { enAddNew,enUpdate}
        public frmAdd_Edit_MedRecord( int MedId,int AppId,int DocId,int PatId )
        {
            InitializeComponent();
            if(MedId==-1)
            {
                _Mode = enMode.enAddNew;
            }  
            else
            {
                _Mode=enMode.enUpdate;
            }
            _MedID = MedId;
            _AppID = AppId;
            _DocID= DocId;
            _PatID= PatId;
        }

        public int MedicalId { get; set; } 


        int _MedID,_AppID,_DocID,_PatID;
        enMode _Mode;
        clsMedRecord _MedicalRecord;

        private void btnSave_Click(object sender, EventArgs e)
        {
            _MedicalRecord.Description=tbDiscription.Text;
            _MedicalRecord.Diagnosis=tbDiagnosis.Text;
            _MedicalRecord.PreScribedMedication=tbPreMedication.Text;
            _MedicalRecord.AdditionalNotes=tbAddNotes.Text;
            _MedicalRecord.DoctorID = _DocID;
            _MedicalRecord.PatientID = _PatID;

            if(_MedicalRecord.Save(ref _MedID))
            {
                MedicalId = _MedID;
                MessageBox.Show("Medical Record is saved in Appointment " + _AppID.ToString());
            }
            else
            {
                MessageBox.Show("Medical Record is Not saved");

            }

        }

        private void frmAdd_Edit_MedRecord_Load(object sender, EventArgs e)
        {
            lblAppID.Text = _AppID.ToString();
            lblDoctorID.Text = _DocID.ToString();
            lblPatientID.Text = _PatID.ToString();

            if (_Mode == enMode.enAddNew)
            {
             
                _MedicalRecord = new clsMedRecord();
                return;

            }
            _MedicalRecord=clsMedRecord.FindMedRecord(_MedID);
    
            tbAddNotes.Text = _MedicalRecord.AdditionalNotes;
            tbDiagnosis.Text = _MedicalRecord.Diagnosis;
            tbDiscription.Text = _MedicalRecord.Description;
            tbPreMedication.Text = _MedicalRecord.PreScribedMedication;

        }







    }
}

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
    public partial class frmAdd_Edit_Payment : Form
    {
        enum enMode { AddNew ,Update}
        public frmAdd_Edit_Payment(int Mode)
        {
            InitializeComponent();

            if(Mode ==-1)
            {
                _Mode=enMode.AddNew;
            }
            else
            {
                _Mode = enMode.Update;
            }
            _PayID = Mode;
        }
        enMode _Mode;

        int _PayID;

        clsPayment _Payment;
       

        private void _LoadMethodsCB()
        {
            DataTable dt = clsPayMethod.GetAllMethods();
            foreach (DataRow st in dt.Rows)
            {
                cbMethod.Items.Add(st["MethodID"]+" ,"+st["MethodName"]);
            }
        }

        private void _LoadAppsShortCutCB()
        {
            DataTable dt = clsAppointment.GetAllAppointmentsShortCut();
            foreach (DataRow pt in dt.Rows)
            {
                string item = pt["AppointmentID"].ToString()
                    + " ," + pt["Patient"] + " ," + pt["Doctor"]+" ," + pt["Status"];
                cbAppointments.Items.Add(item);
            }
        }

        private int _FindcbIndex(ComboBox cb, int ID)
        {

            return cb.FindString((ID.ToString()));
        }

        private int _FindcbID(ComboBox cb)
        {
            string test = cb.Items[cb.SelectedIndex].ToString();
            return Convert.ToInt32(test.Substring(0, test.IndexOf(' ')));

        }


        private void frmAdd_Edit_Payment_Load(object sender, EventArgs e)
        {
            _LoadAppsShortCutCB();
            _LoadMethodsCB();
            if(_Mode==enMode.AddNew)
            {
                lblFormTitle.Text = "Add New Payment";
                _Payment=new clsPayment();
                return;
            }
            lblFormTitle.Text = "Update Payment";


            _Payment = clsPayment.FindPayment(_PayID);

            cbAppointments.SelectedIndex=_FindcbIndex(cbAppointments,_Payment.AppointmentID);
            cbMethod.SelectedIndex=_FindcbIndex(cbMethod,_Payment.MethodID);
            tbAddNotes.Text = _Payment.AdditionalNotes;
            tbAmount.Text=_Payment.PaymentAmount.ToString();
            dtpPaymentDate.Value = _Payment.PaymentDate;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            _Payment.AdditionalNotes =tbAddNotes.Text;
            _Payment.PaymentAmount = Convert.ToDouble(tbAmount.Text);

            _Payment.AppointmentID = _FindcbID(cbAppointments);
            _Payment.PatientID = clsAppointment.FindAppointment(_Payment.AppointmentID).PatientID;
            _Payment.MethodID = _FindcbID(cbMethod);
            _Payment.PaymentDate=dtpPaymentDate.Value;

            if(_Payment.Save())
            {
                MessageBox.Show("Payment is Saved Successfully");
            }
            else
            {
                MessageBox.Show("Payment Was Not Save ");
            }


        }


    }
}

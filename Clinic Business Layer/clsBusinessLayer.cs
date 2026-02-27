using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Clinic_DataAccess_Layer;
using Microsoft.Win32.SafeHandles;

namespace Clinic_Business_Layer
{

    enum enMode { enAddNew, enUpdate }

    public class clsPerson
    {
        public int ID {  get; set; }    
        public string Name { get; set; }
        public DateTime DateOfBirth { get; set; }
        public char Gender { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }

        public clsPerson(int ID,string Name,DateTime DateOfBirth,
            char Gender,string Email,string Phone ,string Address)
        {
            this.ID = ID;
            this.Name = Name;
            this.DateOfBirth = DateOfBirth;
            this.Gender = Gender;
            this.Email = Email;
            this.Phone = Phone;
            this.Address = Address;
        }

        public clsPerson()
        {
            this.ID = -1;
            this.Name = string.Empty;
            this.DateOfBirth = DateTime.Now;
            this.Gender = '*';
            this.Email = string.Empty;
            this.Phone = string.Empty;
            this.Address = string.Empty;
        }

        public int AddNewPerson()
        {

           this.ID=clsPersonData.AddNewPerson(this.Name,this.DateOfBirth,this.Gender,
                this.Email,this.Phone,this.Address);
            return (this.ID );
        }


        public void UpdatePerson()
        {

            clsPersonData.UpdatePerson(this.ID,this.Name, this.DateOfBirth, this.Gender,
                 this.Email, this.Phone, this.Address);
            
        }

        public static clsPerson FindPerson(int ID)
        {
            string Name = "", Email="",Phone = "",Address="";
            char Gender = '*';
            DateTime DateOfBirth = DateTime.Now;
            if(clsPersonData.FindPerson(ID,ref Name,ref DateOfBirth,ref Gender,ref Phone,ref Email,ref Address))
                return new clsPerson(ID,Name,DateOfBirth,Gender,Email,Phone,Address);
            else 
                return null;
        }

        public static bool DeletePerson(int ID)
        {
            return clsPersonData.DeletePerson(ID);
        }

    }

    public class clsEmployee
    {
        public int EmployeeID {  get; set; }
        public int PersonID { get; set; }
        public int JobID { get; set; }
        public double Salary { get; set; }

        enMode _Mode { get; set; }

        public clsEmployee()
        {
            EmployeeID = -1;
            PersonID = -1;
            JobID = -1;
            Salary = -1;
            _Mode = enMode.enAddNew;
        }

        private clsEmployee(int EmployeeID,int PerosnID,int JobID,double Salary)
        {
            this.EmployeeID = EmployeeID;
            this.PersonID = PerosnID;
            this.JobID = JobID;
            this.Salary = Salary;
            this._Mode = enMode.enUpdate;

        }

        public int _AddNewEmployee()
        {
            this.EmployeeID=clsEmployeeData.AddNewEmployee(this.PersonID,this.JobID,this.Salary);
            return this.EmployeeID;
        }

        public static DataTable GetAllEmployees()
        {
            return clsEmployeeData.GetAllEmployees();
        }

        public static clsEmployee FindEmployee(int ID)
        {
            int PersonID = -1, JobID = -1;
            double Salary = -1;
            if (clsEmployeeData.FindEmployee(ID, ref PersonID, ref JobID, ref Salary))
                return new clsEmployee(ID, PersonID, JobID, Salary);
            else
                return null;
        }

        public bool UpdateEmployee()
        {
            return clsEmployeeData.UpdateEmployee(this.EmployeeID, this.PersonID, this.JobID,this.Salary);

        }

        public static bool DeleteEmployee(int EmployeeID)
        {
            int ID = clsEmployee.FindEmployee(EmployeeID).PersonID;

            return clsPerson.DeletePerson(ID); ;
        }

        public bool Save()
        {
            switch (_Mode)
            {
                case enMode.enAddNew:
                    if (this._AddNewEmployee() != -1)
                    {
                        _Mode = enMode.enUpdate;
                        return true;
                    }
                    else
                        return false;



                case enMode.enUpdate:
                return this.UpdateEmployee();

                default:
                    return false;

            }

        }

    }

    public class clsJob
    {
        public int JobID { get; set; }
        public string JobName { get; set; }

        public static DataTable GetAllJobs()
        {
            return clsJobData.GetAllJobs();
        }

        public static string FindJobName(int JobID)
        {
            return clsJobData.FindJobName(JobID);
        }

        public static int FindJobID(string JobName)
        {
            return clsJobData.FindJobID(JobName);
        }



    }

    public class clsDoctor
    {
        private enMode _Mode;

        public int DoctorID { get; set; }
        public int EmployeeID { get; set; }
        public int SpecializationID { get; set; }


        public clsDoctor()
        {
            this.DoctorID = -1;
            this.EmployeeID = -1;
            this.SpecializationID = -1;
            this._Mode = enMode.enAddNew;
        }

        public static bool IsActiveDoctor(int ID)
        {
            return clsDoctorData.IsActiveDoctor(ID);
        }

        private clsDoctor(int DoctorID, int EmployeeID, int SpecializationID)
        {
            this.DoctorID = DoctorID;
            this.EmployeeID = EmployeeID;
            this.SpecializationID = SpecializationID;
            this._Mode = enMode.enUpdate;
        }


        public static DataTable GetAllDoctors()
        {
            return clsDoctorData.GetAllDoctors();
        }


       public static bool GetSelectedDoctorShortCut(int ID, ref string Name, ref string SP)
        {
            return clsDoctorData.GetSelectedDoctorShortCut(ID,ref Name, ref SP);
        }

        public static DataTable GetAllDoctorsShortCut()
        {
            return clsDoctorData.GetAllDoctorsShortCut();
        }

        private bool _AddNewDoctor()
        {
            this.DoctorID = clsDoctorData.AddNewDoctor(this.EmployeeID, this.SpecializationID);
            return (this.DoctorID != -1);
        }

        private bool _UpdateDoctor()
        {
           return clsDoctorData.UpdateDoctor(this.DoctorID,this.EmployeeID,
               this.SpecializationID);
                     
        }


        public bool Save()
        {
            switch (this._Mode)
            {
                case enMode.enAddNew:
                    if (_AddNewDoctor())
                    {
                        this._Mode = enMode.enUpdate;
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                 case enMode.enUpdate:
                    return _UpdateDoctor();

                default: return false;

            }

        }

        public static clsDoctor FindDoctor(int ID)
        {
            int EmployeeID=-1, SpecializationID=-1;
            if (clsDoctorData.FindDoctor(ID, ref EmployeeID,ref SpecializationID))
                return new clsDoctor(ID,EmployeeID,SpecializationID);
            else
                return null;
        }

        public static void DeleteDoctor(int ID)
        {
            int EmployeeID = clsDoctorData.GetDoctor_EmployeeID(ID);
           
            clsEmployee.DeleteEmployee(EmployeeID);
            
           
          

        }

    }

    public class clsPatient
    {
        public int PatientID { get; set; }
        public int PersonID { get; set; }

        private enMode _Mode;

        public clsPatient()
        {
            this.PatientID = -1;
            this.PersonID = -1;
            this._Mode = enMode.enAddNew;
        }

        private clsPatient(int PatientID,int PersonID)
        {
            this.PatientID =PatientID;
            this.PersonID = PersonID;
            this._Mode = enMode.enUpdate;
        }

        public static DataTable GetAllPatients()
        {
            return clsPatientData.GetAllPatients();
        }

        public static bool GetSelectedPatientShortCut(int ID, ref string Name)
        {
            return clsPatientData.GetSelectedPatientShortCut(ID, ref Name);
        }

        public static DataTable GetAllPatientsShortCut()
        {
            return clsPatientData.GetAllPatientsShortCut();
        }


        public static bool IsActivePatient(int ID)
        {
            return clsPatientData.IsActivePatient(ID);
        }

        public static clsPatient FindPatient(int ID)
        {
            int PersonID = -1;
            if(clsPatientData.FindPatient(ID,ref PersonID))
                return new clsPatient(ID,PersonID);
            else 
                return null;
        }

        private bool _AddNewPatient()
        {
            this.PatientID=clsPatientData.AddNewPatient(this.PersonID);
            return(this.PatientID!=-1);

        }

        private bool _UpdatePatient()
        {
            return clsPatientData.UpdatePatient(this.PatientID,this.PersonID);
        }

        public static bool DeletePatient(int ID)
        {
            
            return clsPerson.DeletePerson(clsPatientData.GetPatient_PersonID(ID)); ;
        }

        public bool Save()
        {
            switch (this._Mode)
            {
                case enMode.enAddNew:
                    if (_AddNewPatient())
                    {
                        this._Mode = enMode.enUpdate;
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                case enMode.enUpdate:
                    return _UpdatePatient();

                default: return false;

            }

        }

    }

    public class clsSpecialization
    {
        public int ID { get; set; }
        public string Name { get; set; }

        public clsSpecialization(string  Name,int ID)
        {
            this.Name = Name;
            this.ID = ID;
        }

        public static clsSpecialization Find(string SP)
        {
            int Id = -1;

            if(clsSpecializationData.FindSpecialization(SP,ref Id))
            return new clsSpecialization( SP,Id);
            else 
                return null;
        }

        public static clsSpecialization Find(int ID)
        {
           string SP="";

            if (clsSpecializationData.FindSpecialization(ref SP, ID))
                return new clsSpecialization(SP, ID);
            else
                return null;
        }

        public static DataTable GetAllSpecializations()
        {
            return clsSpecializationData.GetAllSpecializations();
        }


    }

    public class clsStatus
    {
        public int StatusID { get; set; }
        public string Name { get; set; }

        public clsStatus(string Name, int StatusID)
        {
            this.Name = Name;
            this.StatusID = StatusID;
        }

        public static clsStatus Find(string ST)
        {
            int Id = -1;

            if (clsStatusData.FindStatus(ST, ref Id))
                return new clsStatus(ST, Id);
            else
                return null;
        }

        public static clsStatus Find(int ID)
        {
            string ST = "";

            if (clsStatusData.FindStatus(ref ST, ID))
                return new clsStatus(ST, ID);
            else
                return null;
        }

        public static DataTable GetAllStatuses()
        {
            return clsStatusData.GetAllStatuses();
        }


    }

    public class clsUser
    {
        public int UserID { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }

        enMode _Mode;

        public clsUser()
        {
            this.UserID = -1;
            this.UserName = string.Empty;
            this.Password = string.Empty;
            _Mode = enMode.enAddNew;
        }

        clsUser(int UserID, string UserName, string Password)
        {
            this.UserName = UserName;
            this.Password = Password;
            this.UserID = UserID;
            _Mode = enMode.enUpdate;
        }

        public static DataTable GetAllUsers()
        {
            return clsUserData.GetAllUsers();
        }

        public static bool IsUserFound(string UserName, string Password)
        {
            return clsUserData.IsUserFound(UserName, Password);
        }

        public static clsUser FindUser(int ID)

        {
            int PersonID = -1;
            string UserName = string.Empty;
            string Password = string.Empty;
            if (clsUserData.FindUser(ID,ref UserName,ref Password))
                return new clsUser(ID,UserName,Password);
            else
                return null;
        }
        private bool _AddNewUser()
        {
            this.UserID=clsUserData.AddNewUser(this.UserName, this.Password);
            return(this.UserID!=-1);
        }

        private bool _UpdateUser()
        {
            return clsUserData.UpdateUser(this.UserID, this.UserName, this.Password);
        }

        public static bool DeleteUser(int ID)
        {
            return clsUserData.DeleteUser(ID);
        }

        public bool Save()
        {
            if(_Mode==enMode.enAddNew)
            {
                if(_AddNewUser())
                    { return true; }
                else
                { return false; }    
            }
            return _UpdateUser();
        }

    }

    public class clsAppointment
    {
        public int AppointmentsID { get; set; }

        public int DoctorID { get; set; }

        public int PatientID { get; set; }

        public DateTime AppintmentDate { get; set; }

        public TimeSpan AppointmentTime { get; set; }

        public int StatusID { get; set; }

        public int MedRecordID { get; set; }

        private enMode _Mode;

        public static DataTable GetAllAppointmentsShortCut()
        {
            return clsAppointmentsData.GetAllAppointmentsShortCut();
        }

        public static DataTable GetAllAppointments()
        {
            return clsAppointmentsData.GetAllAppointments();
        }

        public clsAppointment()
        {
            this.AppointmentsID = -1;
            this.PatientID = -1;
            this.DoctorID = -1;
            this.StatusID = -1;
            this.MedRecordID = -1;
            this.AppintmentDate = DateTime.Now;
            this.AppointmentTime = new TimeSpan(0,0,0);
            _Mode = enMode.enAddNew;
        }

        private clsAppointment(int appointmentsID, int doctorID, int patientID, DateTime appintmentDate, TimeSpan appointmentTime, int statusID, int medRecordID)
        {
            AppointmentsID = appointmentsID;
            DoctorID = doctorID;
            PatientID = patientID;
            AppintmentDate = appintmentDate;
            AppointmentTime = appointmentTime;
            StatusID = statusID;
            MedRecordID = medRecordID;
            _Mode = enMode.enUpdate;
        }

        public static clsAppointment FindAppointment(int ID)
        {
            int StatusID = -1, DoctorID = -1, MedRecordID = -1, PatientID = -1;
            DateTime AppDate = DateTime.Now; TimeSpan AppTime = new TimeSpan(0);
            if (clsAppointmentsData.FindAppointment(ID, ref DoctorID, ref PatientID, ref StatusID
             , ref MedRecordID, ref AppDate, ref AppTime))
                return new clsAppointment(ID, DoctorID, PatientID, AppDate, AppTime, StatusID, MedRecordID);
            else
                return null;
        }

        private bool _AddNewAppointment()
        {
            this.AppointmentsID = clsAppointmentsData.AddNewAppointment(this.DoctorID, this.PatientID,
                this.MedRecordID, this.StatusID, this.AppintmentDate, this.AppointmentTime);
            return (this.AppointmentsID != -1);
        }

        private bool _UpdateAppointment()
        {


            return clsAppointmentsData.UpdateAppointment(this.AppointmentsID,this.DoctorID, this.PatientID,
                this.MedRecordID, this.StatusID, this.AppintmentDate, this.AppointmentTime);
        }

        public  static bool IsDoctorBusy(int DocID,DateTime AppDate,TimeSpan AppTime)
        {
            return clsAppointmentsData.IsDoctorBusy(DocID,
                AppDate,AppTime);
        }

        public bool Save()
        {
            

            switch (this._Mode) {
                case enMode.enAddNew:
                    if (_AddNewAppointment())
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                    break;

                case enMode.enUpdate:
                    return _UpdateAppointment();
                    break;

                default:
                    return false;
            }


        }

        public static bool MakeMedRecordNull(int ID)
        {
            return clsAppointmentsData.MakeMedRecordNull(ID);
        }
        public static bool DeleteAppointment(int ID)
        {
            int MedId = clsAppointment.FindAppointment(ID).MedRecordID;
            if (clsAppointmentsData.DeleteAppointment(ID))
                return clsMedRecord.DeleteMR(MedId);
            else
                return false;
          
        }




    }

    public class clsMedRecord
    {
        public int MedID { get; set; }
        public int DoctorID { get; set; }
        public int PatientID { get; set; }
        public string Description { get; set; }
        public string Diagnosis { get; set; }
        public string PreScribedMedication { get; set; }
        public string AdditionalNotes { get; set; }

        enMode _Mode;

        public clsMedRecord()
        {

            this.MedID = -1;
            this.PatientID = -1;
            this.DoctorID = -1;
            this.Description = string.Empty;
            this.AdditionalNotes = string.Empty;
            this.Diagnosis = string.Empty;
            this.PreScribedMedication = string.Empty;
            this._Mode = enMode.enAddNew;
        }

        private clsMedRecord(int MedID,int DoctorID,int PatientID,string Description,string Diagnosis,
            string PreScribedMedication,string AdditionalNotes)
        {
            this.MedID = MedID;
            this.PatientID = PatientID;
            this.Description = Description;

            this.Diagnosis = Diagnosis;
            this.DoctorID= DoctorID;
            this.PreScribedMedication= PreScribedMedication;
            this.AdditionalNotes= AdditionalNotes;
            this._Mode = enMode.enUpdate;
        }

        public static clsMedRecord FindMedRecord(int MedID)
        {
            int DoctorId = -1, PatientId = -1;
            string Description = string.Empty, Diagnosis = string.Empty, PreScribedMedication = string.Empty,
            AddtionalNotes = string.Empty;

            if (clsMedRecordData.FindMedRecord(MedID, ref DoctorId, ref PatientId, ref Description,
                ref Diagnosis, ref PreScribedMedication, ref AddtionalNotes))
            {
                return new clsMedRecord(MedID, DoctorId, PatientId, Description,
                    Diagnosis, PreScribedMedication, AddtionalNotes);
            }
            else
                return null;
        }


        private bool _AddNewMedRecord(ref int MID)
        {
            this.MedID = clsMedRecordData.AddNewMedRecord(this.DoctorID, this.PatientID,
               this.Description,this.Diagnosis,this.PreScribedMedication,this.AdditionalNotes);
            MID = this.MedID;
            return (this.MedID != -1);
        }

        private bool _UpdateMedRecord()
        {
            return clsMedRecordData.UpdateMedRecord(this.MedID,this.DoctorID,this.PatientID,
                this.Description,this.Diagnosis,this.PreScribedMedication,this.AdditionalNotes);
        }



        public bool Save(ref int MID)
        {


            switch (this._Mode)
            {
                case enMode.enAddNew:
                    if (_AddNewMedRecord(ref MID))
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                    break;

                case enMode.enUpdate:
                    return _UpdateMedRecord();
                    break;

                default:
                    return false;
            }


        }

        public static bool DeleteMR(int ID)
        {
           return clsMedRecordData.DeleteMedicalRecord(ID);
        }

        public static DataTable GetAllMedRecords()
        {
            return clsMedRecordData.GetAllMedRecords();
        }




    }

    public class clsPayMethod
    {
        public int MethodeID { get; set; }
        public string MethodeName { get; set; }


        public clsPayMethod(string MethodName, int MethodID)
        {
           this.MethodeName = MethodName;
            this.MethodeID = MethodID;
        }

        public static clsPayMethod Find(string MT)
        {
            int Id = -1;

            if (clsMethodData.FindMethod(MT, ref Id))
                return new clsPayMethod(MT, Id);
            else
                return null;
        }

        public static clsPayMethod Find(int ID)
        {
            string MT = "";

            if (clsMethodData.FindMethod(ref MT, ID))
                return new clsPayMethod(MT, ID);
            else
                return null;
        }

        public static DataTable GetAllMethods()
        {
            return clsMethodData.GetAllMethods();
        }



    }

    public class clsPayment
    {
        public int PaymentID { get; set; }
        public int PatientID { get; set; }
        public int MethodID { get; set; }
        public DateTime PaymentDate { get; set; }
        public double PaymentAmount { get; set; }
        public int AppointmentID { get; set; }

        public string AdditionalNotes { get; set; }

        enMode _Mode;

        public clsPayment ()
        {
            this.PaymentID = -1;
            this.PatientID = -1;
            this.MethodID = -1;
            this.PaymentDate = DateTime.Now;
            this.AppointmentID = -1;
            this.PaymentAmount = -1;
            this.AdditionalNotes = string.Empty;

            _Mode = enMode.enAddNew;
        }

        public clsPayment (int PaymentID,int PatientID,int MethodID,DateTime PaymentDate
            ,double PaymentAmount, int AppointmentID,string AdditionalNotes)
        {
            this.PaymentID = PaymentID;
            this.PatientID = PatientID;
            this.MethodID = MethodID;
            this.PaymentDate = PaymentDate;
            this.PaymentAmount = PaymentAmount;
            this.AppointmentID = AppointmentID;
            this.AdditionalNotes = AdditionalNotes;

            this._Mode = enMode.enUpdate;
        }
  
        public static DataTable GetAllPayments()
        {
            return clsPaymentData.GetAllPayments();
        }

        public static clsPayment FindPayment(int  PaymentID)
        {
            int PatientID = -1, MethodID = -1, AppointmentID = -1;
            DateTime PayDate = DateTime.Now;
            double PaymentAmount = -1;
            string AdditionalNotes = string.Empty;

            if(clsPaymentData.FindPayment(PaymentID,ref PatientID,ref MethodID,
                ref AppointmentID,ref PayDate,ref PaymentAmount,ref AdditionalNotes))
            {
                return new clsPayment(PaymentID, PatientID, MethodID, PayDate,
                    PaymentAmount,AppointmentID,AdditionalNotes);
            }   
            else 
                return null;
        }

        private bool _AddNewPayment()
        {
            this.PaymentID = clsPaymentData.AddNewPayment(this.PatientID, this.MethodID, this.AppointmentID,
                this.PaymentDate, this.PaymentAmount, this.AdditionalNotes);
            return (this.PaymentID != -1);
        }

        private bool _UpdatePayment()
        {
            return clsPaymentData.UpdatePayment(this.PaymentID, this.PatientID, this.MethodID, this.AppointmentID,
                this.PaymentDate, this.PaymentAmount, this.AdditionalNotes);
        }

        public bool Save()
        {
            switch (_Mode)
            {
                case enMode.enAddNew:
                    if(_AddNewPayment())
                    {
                        return true;
                    }
                    else
                        return false;
                case enMode.enUpdate:

                    return _UpdatePayment();

                default:return false;
                 
            }

        }


        public static bool DeletePayment(int ID)
        {
            return clsPaymentData.DeletePayment(ID);
        }




    }




}

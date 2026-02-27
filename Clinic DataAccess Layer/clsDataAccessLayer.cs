using System;
using System.Data;
using System.Linq;
using System.Data.SqlClient;
using System.Net;
using System.Security.Policy;
using System.Xml.Linq;
using System.Diagnostics.Eventing.Reader;

namespace Clinic_DataAccess_Layer
{
    static class clsConnectionSetting
    {
        public static string ConnectionString = "Server=.;Database=ClinicDB;User Id=sa;Password=sa123456;";

    }

    public class clsEmployeeData
    {

        public static int AddNewEmployee(int PersonID, int JobID, double Salary)
        {
            int EmployeeID = -1;

            SqlConnection connnection = new SqlConnection(clsConnectionSetting.ConnectionString);

            string query = "insert into Employees Values(@PersonID,@Salary,@JobID) ;" +
                " SELECT SCOPE_IDENTITY();";
            SqlCommand cmd = new SqlCommand(query, connnection);

            cmd.Parameters.AddWithValue("@PersonID", PersonID);
            cmd.Parameters.AddWithValue("@Salary", Salary);
            cmd.Parameters.AddWithValue("@JobID", JobID);


            try
            {
                connnection.Open();
                object Result = cmd.ExecuteScalar();
                if (Result != null && int.TryParse(Result.ToString(), out int InsertdID))
                {
                    EmployeeID = InsertdID;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error" + ex.Message);
                EmployeeID = -1;
            }
            finally { connnection.Close(); }
            return EmployeeID;

        }

        public static bool FindEmployee(int EmployeeID, ref int PersonID, ref int JobID,
          ref double Salary)
        {
            SqlConnection connection = new SqlConnection(clsConnectionSetting.ConnectionString);
            bool isFound = false;
            string query = "select * from Employees where EmployeeID=@EmployeeID;";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@EmployeeID", EmployeeID);

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    PersonID = (int)reader["PersonID"];
                    JobID = (int)reader["JobID"];
                    decimal DSalary = (decimal)reader["Salary"];
                    Salary = (double)DSalary;
                    isFound = true;
                }
                reader.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error" + ex.Message);
                isFound = false;
            }
            finally
            {
                connection.Close();
            }
            return isFound;

        }

        public static DataTable GetAllEmployees()
        {
            DataTable dt = new DataTable();
            SqlConnection connection = new SqlConnection(clsConnectionSetting.ConnectionString);

            string query = "Select * from ShowActiveEmployees";

            SqlCommand command = new SqlCommand(query, connection);

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    dt.Load(reader);
                }
                reader.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error" + ex.Message);
            }
            finally { connection.Close(); }

            return dt;
        }



        public static bool UpdateEmployee(int EmployeeID, int PersonID, int JobId, double Salary)
        {
            bool isUpdated = false;
            SqlConnection connection = new SqlConnection(clsConnectionSetting.ConnectionString);

            string query = "Update Employees set " +
                "JobID=@JobId,PersonID=@PersonID,Salary=@Salary " +
                "where EmployeeID=@EmployeeID;";

            SqlCommand cmd = new SqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@JobId", JobId);
            cmd.Parameters.AddWithValue("@EmployeeID", EmployeeID);
            cmd.Parameters.AddWithValue("@PersonID", PersonID);
            cmd.Parameters.AddWithValue("@Salary", Salary);

            try
            {
                connection.Open();
                int RowsAffected = cmd.ExecuteNonQuery();
                isUpdated = (RowsAffected > 0);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error" + ex.Message);
                isUpdated = false;
            }
            finally { connection.Close(); }
            return isUpdated;
        }

        public static bool DeleteEmployee(int EmployeeID)
        {
            bool isDeleted = false;
            SqlConnection connnection = new SqlConnection(clsConnectionSetting.ConnectionString);

            string query = "Delete From Employees where " +
                "EmployeeID=@EmployeeID;";
            SqlCommand cmd = new SqlCommand(query, connnection);
            cmd.Parameters.AddWithValue("@EmployeeID", EmployeeID);

            try
            {
                connnection.Open();
                int RowsAffected = cmd.ExecuteNonQuery();
                isDeleted = (RowsAffected > 0);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error" + ex.Message);
            }
            finally { connnection.Close(); }
            return isDeleted;
        }

        public static int GetEmployee_PersonID(int EmployeeID)
        {
            int ID = -1;
            SqlConnection connection = new SqlConnection(clsConnectionSetting.ConnectionString);

            string query = "Select PersonID from Employees Where EmployeeID=@EmployeeID";

            SqlCommand cmd = new SqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@EmployeeID", EmployeeID);

            try
            {
                connection.Open();
                object Result = cmd.ExecuteScalar();
                if (Result != null && int.TryParse(Result.ToString(), out int PersonID))
                {
                    ID = PersonID;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error" + ex.Message);
                ID = -1;
            }
            finally { connection.Close(); }
            return ID;
        }


    }

    public class clsDoctorData
    {
        public static DataTable GetAllDoctors()
        {
            DataTable dt = new DataTable();
            SqlConnection connection = new SqlConnection(clsConnectionSetting.ConnectionString);

            string query = "Select * from ShowActiveDoctors";

            SqlCommand command = new SqlCommand(query, connection);

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    dt.Load(reader);
                }
                reader.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error" + ex.Message);
            }
            finally { connection.Close(); }

            return dt;
        }

      
        public static bool GetSelectedDoctorShortCut(int ID,ref string Name ,ref string SP)
        {
             bool isDone=false;
            SqlConnection connection = new SqlConnection(clsConnectionSetting.ConnectionString);

            string query = "select Name,Specialization from ShowActiveDoctors" +
                          " where DoctorID=@ID " +
                          " Union " +
                          "select Name,Specialization from ShowInactiveDoctors" +
                          " where DoctorID=@ID ;";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@ID", ID);

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        Name = reader["Name"].ToString();
                        SP = reader["Specialization"].ToString();
                        isDone = true;
                    }
                }
                reader.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error" + ex.Message);
            }
            finally { connection.Close(); }

            return isDone;
        }


        public static DataTable GetAllDoctorsShortCut()
        {
            DataTable dt = new DataTable();
            SqlConnection connection = new SqlConnection(clsConnectionSetting.ConnectionString);

            string query = "select DoctorID,Name,Specialization from ShowActiveDoctors";
                

            SqlCommand command = new SqlCommand(query, connection);

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    dt.Load(reader);
                }
                reader.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error" + ex.Message);
            }
            finally { connection.Close(); }

            return dt;
        }

        public static int AddNewDoctor(int EmployeeID, int SpId)
        {
            int ID = -1;
            SqlConnection connection = new SqlConnection(clsConnectionSetting.ConnectionString);

            string query = "insert into Doctors Values" +
                "(@SpId,@EmployeeID)" +
                " SELECT SCOPE_IDENTITY();";

            SqlCommand cmd = new SqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@SpId", SpId);
            cmd.Parameters.AddWithValue("@EmployeeID", EmployeeID);


            try
            {
                connection.Open();
                object Result = cmd.ExecuteScalar();
                if (Result != null && int.TryParse(Result.ToString(), out int InsertedID))
                {
                    ID = InsertedID;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error" + ex.Message);
                ID = -1;
            }
            finally { connection.Close(); }
            return ID;
        }

        public static bool UpdateDoctor(int DoctorID, int EmployeeID, int SpId)
        {
            bool isUpdated = false;
            SqlConnection connection = new SqlConnection(clsConnectionSetting.ConnectionString);

            string query = "Update Doctors set " +
                "SpecializationID=@SpId,EmployeeID=@EmployeeID " +
                "where DoctorID=@DoctorID;";

            SqlCommand cmd = new SqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@SpId", SpId);
            cmd.Parameters.AddWithValue("@EmployeeID", EmployeeID);
            cmd.Parameters.AddWithValue("@DoctorID", DoctorID);


            try
            {
                connection.Open();
                int RowsAffected = cmd.ExecuteNonQuery();
                isUpdated = (RowsAffected > 0);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error" + ex.Message);
                isUpdated = false;
            }
            finally { connection.Close(); }
            return isUpdated;
        }


        public static bool FindDoctor(int DoctorID, ref int EmployeeID, ref int SpID)
        {
            SqlConnection connection = new SqlConnection(clsConnectionSetting.ConnectionString);
            bool isFound = false;
            string query = "select * from Doctors where DoctorID=@DoctorID;";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@DoctorID", DoctorID);

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    EmployeeID = (int)reader["EmployeeID"];
                    SpID = (int)reader["SpecializationID"];
                    isFound = true;
                }
                reader.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error" + ex.Message);
                isFound = false;
            }
            finally
            {
                connection.Close();
            }
            return isFound;

        }

        public static bool IsActiveDoctor(int DoctorID)
        {
            SqlConnection connection = new SqlConnection(clsConnectionSetting.ConnectionString);
            bool isFound = false;
            string query = "select R=1 from ShowActiveDoctors where DoctorID=@DoctorID;";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@DoctorID", DoctorID);

            try
            {
                connection.Open();
                 object Result = command.ExecuteScalar();
                if (Result!=null)
                {
                   
                    isFound = true;
                }
                
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error" + ex.Message);
                isFound = false;
            }
            finally
            {
                connection.Close();
            }
            return isFound;

        }


        public static int GetDoctor_EmployeeID(int DoctorID)
        {
            int ID = -1;
            SqlConnection connection = new SqlConnection(clsConnectionSetting.ConnectionString);

            string query = "Select EmployeeID from Doctors Where DoctorID=@DoctorID";

            SqlCommand cmd = new SqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@DoctorID", DoctorID);

            try
            {
                connection.Open();
                object Result = cmd.ExecuteScalar();
                if (Result != null && int.TryParse(Result.ToString(), out int EmployeeID))
                {
                    ID = EmployeeID;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error" + ex.Message);
                ID = -1;
            }
            finally { connection.Close(); }
            return ID;
        }


        public static bool DeleteDoctor(int DoctorID)
        {
            bool isDeleted = false;
            SqlConnection connnection = new SqlConnection(clsConnectionSetting.ConnectionString);

            string query = "Delete From Doctors where " +
                "DoctorID=@DoctorID;";
            SqlCommand cmd = new SqlCommand(query, connnection);
            cmd.Parameters.AddWithValue("@DoctorID", DoctorID);

            try
            {
                connnection.Open();
                int RowsAffected = cmd.ExecuteNonQuery();
                isDeleted = (RowsAffected > 0);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error" + ex.Message);
            }
            finally { connnection.Close(); }
            return isDeleted;
        }
    }

    public class clsPersonData
    {

        public static bool DeletePerson(int PersonID)
        {
            bool isDeleted = false;
            SqlConnection connnection = new SqlConnection(clsConnectionSetting.ConnectionString);

            string query = "Update Persons set IsDeleted = 1 where PersonID=@PersonID ;";
            SqlCommand cmd = new SqlCommand(query, connnection);
            cmd.Parameters.AddWithValue("@PersonID", PersonID);

            try
            {
                connnection.Open();
                int RowsAffected = cmd.ExecuteNonQuery();
                isDeleted = (RowsAffected > 0);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error" + ex.Message);
            }
            finally { connnection.Close(); }
            return isDeleted;
        }


        public static int AddNewPerson(string Name, DateTime DateOfBirth,
            char Gender, string Email, string Phone, string Address)
        {
            int id = -1;
            SqlConnection connnection = new SqlConnection(clsConnectionSetting.ConnectionString);

            string query = "insert into Persons Values (@Name,@DateOfBirth," +
                "@Gender,@Phone,@Email,@Address,0);" +
                " SELECT SCOPE_IDENTITY();";
            SqlCommand cmd = new SqlCommand(query, connnection);
            cmd.Parameters.AddWithValue("@Name", Name);
            cmd.Parameters.AddWithValue("@DateOfBirth", DateOfBirth);
            cmd.Parameters.AddWithValue("@Gender", Gender);
            cmd.Parameters.AddWithValue("@Email", Email);
            cmd.Parameters.AddWithValue("@Phone", Phone);
            cmd.Parameters.AddWithValue("@Address", Address);

            try
            {
                connnection.Open();
                object Result = cmd.ExecuteScalar();
                if (Result != null && int.TryParse(Result.ToString(), out int InsertdID))
                {
                    id = InsertdID;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error" + ex.Message);
            }
            finally { connnection.Close(); }
            return id;
        }


        public static bool UpdatePerson(int PersonID, string Name, DateTime DateOfBirth,
           char Gender, string Email, string Phone, string Address)
        {
            bool isUpdated = false;
            SqlConnection connnection = new SqlConnection(clsConnectionSetting.ConnectionString);

            string query = "Update Persons set Name=@Name,DateOfBirth=@DateOfBirth," +
                "Gender=@Gender,Email=@Email,Phone=@Phone,Address=@Address where " +
                "PersonID=@PersonID;";
            SqlCommand cmd = new SqlCommand(query, connnection);
            cmd.Parameters.AddWithValue("@Name", Name);
            cmd.Parameters.AddWithValue("@DateOfBirth", DateOfBirth);
            cmd.Parameters.AddWithValue("@Gender", Gender);
            cmd.Parameters.AddWithValue("@Email", Email);
            cmd.Parameters.AddWithValue("@Phone", Phone);
            cmd.Parameters.AddWithValue("@Address", Address);
            cmd.Parameters.AddWithValue("@PersonID", PersonID);

            try
            {
                connnection.Open();
                int RowsAffected = cmd.ExecuteNonQuery();

                isUpdated = (RowsAffected > 0);

            }
            catch (Exception ex)
            {
                Console.WriteLine("Error" + ex.Message);
            }
            finally { connnection.Close(); }
            return isUpdated;
        }



        public static bool FindPerson(int PersonID, ref string Name, ref DateTime DateOfBirth,
            ref char Gender, ref string Phone, ref string Email, ref string Address)
        {
            SqlConnection connection = new SqlConnection(clsConnectionSetting.ConnectionString);
            bool isFound = false;
            string query = "select * from Persons where PersonID=@PersonID;";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@PersonID", PersonID);

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    Name = (string)reader["Name"];
                    DateOfBirth = (DateTime)reader["DateOfBirth"];
                    Gender = Convert.ToChar(reader["Gender"]);
                    Email = (string)reader["Email"];
                    Phone = (string)reader["Phone"];
                    Address = (string)reader["Address"];
                    isFound = true;
                }
                reader.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error" + ex.Message);
                isFound = false;
            }
            finally
            {
                connection.Close();
            }
            return isFound;

        }


    }

    public class clsPatientData
    {

        public static bool IsActivePatient(int PatientID)
        {
            SqlConnection connection = new SqlConnection(clsConnectionSetting.ConnectionString);
            bool isFound = false;
            string query = "select R=1 from ShowActivePatients where PatientID=@ID;";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@ID", PatientID);

            try
            {
                connection.Open();
                object Result = command.ExecuteScalar();
                if (Result != null)
                {

                    isFound = true;
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine("Error" + ex.Message);
                isFound = false;
            }
            finally
            {
                connection.Close();
            }
            return isFound;

        }


        public static DataTable GetAllPatients()
        {
            DataTable dt = new DataTable();
            SqlConnection connection = new SqlConnection(clsConnectionSetting.ConnectionString);

            string query = "Select * from ShowActivePatients";

            SqlCommand command = new SqlCommand(query, connection);

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    dt.Load(reader);
                }
                reader.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error" + ex.Message);
            }
            finally { connection.Close(); }

            return dt;
        }

        public static DataTable GetAllPatientsShortCut()
        {
            DataTable dt = new DataTable();
            SqlConnection connection = new SqlConnection(clsConnectionSetting.ConnectionString);

            string query = "select PatientID,Name from ShowActivePatients;";

            SqlCommand command = new SqlCommand(query, connection);

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    dt.Load(reader);
                }
                reader.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error" + ex.Message);
            }
            finally { connection.Close(); }

            return dt;
        }

        public static bool GetSelectedPatientShortCut(int ID, ref string Name)

        {
            bool isDone = false;
            SqlConnection connection = new SqlConnection(clsConnectionSetting.ConnectionString);

            string query = "select Name from ShowActivePatients" +
                          " where PatientID=@ID " +
                          " Union " +
                          "select Name from ShowInactivePatients" +
                          " where PatientID=@ID ;";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@ID", ID);

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        Name = reader["Name"].ToString();
                        isDone = true;
                    }
                }
                reader.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error" + ex.Message);
            }
            finally { connection.Close(); }

            return isDone;
        }

        public static bool FindPatient(int PatientID, ref int PersonID)
        {
            SqlConnection connection = new SqlConnection(clsConnectionSetting.ConnectionString);
            bool isFound = false;
            string query = "select * from Patients where PatientID=@PatientID;";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@PatientID", PatientID);

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    PersonID = (int)reader["PersonID"];
                    isFound = true;
                }
                reader.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error" + ex.Message);
                isFound = false;
            }
            finally
            {
                connection.Close();
            }
            return isFound;

        }

        public static int AddNewPatient(int PersonID)
        {
            int ID = -1;
            SqlConnection connection = new SqlConnection(clsConnectionSetting.ConnectionString);

            string query = "insert into Patients Values" +
                "(@PersonID)" +
                " SELECT SCOPE_IDENTITY();";

            SqlCommand cmd = new SqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@PersonID", PersonID);

            try
            {
                connection.Open();
                object Result = cmd.ExecuteScalar();
                if (Result != null && int.TryParse(Result.ToString(), out int InsertedID))
                {
                    ID = InsertedID;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error" + ex.Message);
                ID = -1;
            }
            finally { connection.Close(); }
            return ID;
        }

        public static bool UpdatePatient(int PatientID, int PersonID)
        {
            bool isUpdated = false;
            SqlConnection connection = new SqlConnection(clsConnectionSetting.ConnectionString);

            string query = "Update Patients set " +
                "PersonID=@PersonID " +
                "where PatientID=@PatientID;";

            SqlCommand cmd = new SqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@PatientID", PatientID);
            cmd.Parameters.AddWithValue("@PersonID", PersonID);

            try
            {
                connection.Open();
                int RowsAffected = cmd.ExecuteNonQuery();
                isUpdated = (RowsAffected > 0);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error" + ex.Message);
                isUpdated = false;
            }
            finally { connection.Close(); }
            return isUpdated;
        }

        public static bool DeletePatient(int PatientID)
        {
            bool isDeleted = false;
            SqlConnection connnection = new SqlConnection(clsConnectionSetting.ConnectionString);

            string query = "Delete From Patients where " +
                "PatientID=@PatientID;";
            SqlCommand cmd = new SqlCommand(query, connnection);
            cmd.Parameters.AddWithValue("@PatientID", PatientID);

            try
            {
                connnection.Open();
                int RowsAffected = cmd.ExecuteNonQuery();
                isDeleted = (RowsAffected > 0);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error" + ex.Message);
            }
            finally { connnection.Close(); }
            return isDeleted;
        }

        public static int GetPatient_PersonID(int PatientID)
        {
            int ID = -1;
            SqlConnection connection = new SqlConnection(clsConnectionSetting.ConnectionString);

            string query = "Select PersonID from Patients Where PatientID=@PatientID";

            SqlCommand cmd = new SqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@PatientID", PatientID);

            try
            {
                connection.Open();
                object Result = cmd.ExecuteScalar();
                if (Result != null && int.TryParse(Result.ToString(), out int PersonID))
                {
                    ID = PersonID;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error" + ex.Message);
                ID = -1;
            }
            finally { connection.Close(); }
            return ID;
        }
    }

    public class clsSpecializationData
    {

        public static bool FindSpecialization(string spName, ref int ID)
        {
            bool isFound = false;
            SqlConnection connnection = new SqlConnection(clsConnectionSetting.ConnectionString);

            string query = "Select * from Specializations where( Name=@Name);";

            SqlCommand cmd = new SqlCommand(query, connnection);

            cmd.Parameters.AddWithValue("@Name", spName);

            try
            {
                connnection.Open();

                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    isFound = true;
                    ID = (int)reader["SpecializationID"];
                }

            }
            catch (Exception ex) { Console.WriteLine("Error : " + ex.Message); }
            finally { connnection.Close(); }
            return isFound;

        }

        public static bool FindSpecialization(ref string spName, int ID)
        {
            bool isFound = false;
            SqlConnection connnection = new SqlConnection(clsConnectionSetting.ConnectionString);

            string query = "Select * from Specializations where(SpecializationID =@ID);";

            SqlCommand cmd = new SqlCommand(query, connnection);

            cmd.Parameters.AddWithValue("@ID", ID);

            try
            {
                connnection.Open();

                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    isFound = true;
                    spName = (string)reader["Name"];
                }

            }
            catch (Exception ex) { Console.WriteLine("Error : " + ex.Message); }
            finally { connnection.Close(); }
            return isFound;

        }


        public static DataTable GetAllSpecializations()
        {
            DataTable dt = new DataTable();
            SqlConnection connection = new SqlConnection(clsConnectionSetting.ConnectionString);
            string query = "select * from Specializations";

            SqlCommand command = new SqlCommand(query, connection);

            try
            {
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();
                dt.Load(reader);
                reader.Close();
            }
            catch (Exception ex)
            {
                // Console.WriteLine("Error" + ex.Message);
            }
            finally { connection.Close(); }


            return dt;
        }




    }

    public class clsStatusData
    {

        public static bool FindStatus(string stName, ref int ID)
        {
            bool isFound = false;
            SqlConnection connnection = new SqlConnection(clsConnectionSetting.ConnectionString);

            string query = "Select * from Statuses where( Name=@Name);";

            SqlCommand cmd = new SqlCommand(query, connnection);

            cmd.Parameters.AddWithValue("@Name", stName);

            try
            {
                connnection.Open();

                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    isFound = true;
                    ID = (int)reader["StatusID"];
                }

            }
            catch (Exception ex) { Console.WriteLine("Error : " + ex.Message); }
            finally { connnection.Close(); }
            return isFound;

        }

        public static bool FindStatus(ref string stName, int ID)
        {
            bool isFound = false;
            SqlConnection connnection = new SqlConnection(clsConnectionSetting.ConnectionString);

            string query = "Select * from Statuses where(StatusID =@ID);";

            SqlCommand cmd = new SqlCommand(query, connnection);

            cmd.Parameters.AddWithValue("@ID", ID);

            try
            {
                connnection.Open();

                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    isFound = true;
                    stName = (string)reader["Name"];
                }

            }
            catch (Exception ex) { Console.WriteLine("Error : " + ex.Message); }
            finally { connnection.Close(); }
            return isFound;

        }

        public static DataTable GetAllStatuses()
        {
            DataTable dt = new DataTable();
            SqlConnection connection = new SqlConnection(clsConnectionSetting.ConnectionString);
            string query = "select * from Statuses";

            SqlCommand command = new SqlCommand(query, connection);

            try
            {
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();
                dt.Load(reader);
                reader.Close();
            }
            catch (Exception ex)
            {
                // Console.WriteLine("Error" + ex.Message);
            }
            finally { connection.Close(); }


            return dt;
        }

    }

    public class clsUserData
    {

        public static DataTable GetAllUsers()

        {
            DataTable dt = new DataTable();
            SqlConnection connection = new SqlConnection(clsConnectionSetting.ConnectionString);
            string query = "select * from Users";

            SqlCommand command = new SqlCommand(query, connection);

            try
            {
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();
                dt.Load(reader);
                reader.Close();
            }
            catch (Exception ex)
            {
                // Console.WriteLine("Error" + ex.Message);
            }
            finally { connection.Close(); }


            return dt;
        }
        public static bool IsUserFound(string UserName, string UserPassword)
        {
            bool Found = false;

            SqlConnection connection = new SqlConnection(clsConnectionSetting.ConnectionString);
            string query = "select I=1 from Users" +
                " Where UserName=@UserName and UserPassword=@UserPassword";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@UserName", UserName);
            command.Parameters.AddWithValue("@UserPassword", UserPassword);

            try
            {
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();
                Found = (reader.HasRows);
                reader.Close();
            }
            catch (Exception ex)
            {
                // Console.WriteLine("Error" + ex.Message);
            }
            finally { connection.Close(); }


            return Found;

        }

        public static bool FindUser(int ID,ref string UserName,ref string Password)
        {
            bool isFound = false;
            SqlConnection connnection = new SqlConnection(clsConnectionSetting.ConnectionString);

            string query = "Select * from Users where(UserID=@ID);";

            SqlCommand cmd = new SqlCommand(query, connnection);

            cmd.Parameters.AddWithValue("@ID", ID);

            try
            {
                connnection.Open();

                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    isFound = true;
                    UserName = (string)reader["UserName"];
                    Password = (string)reader["UserPassword"];
                }

            }
            catch (Exception ex) { Console.WriteLine("Error : " + ex.Message); }
            finally { connnection.Close(); }
            return isFound;

        }

        public static bool DeleteUser(int UserID)
        {
            bool isDeleted = false;
            SqlConnection connnection = new SqlConnection(clsConnectionSetting.ConnectionString);

            string query = "Delete From Users where " +
                "UserID=@ID;";
            SqlCommand cmd = new SqlCommand(query, connnection);
            cmd.Parameters.AddWithValue("@ID", UserID);

            try
            {
                connnection.Open();
                int RowsAffected = cmd.ExecuteNonQuery();
                isDeleted = (RowsAffected > 0);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error" + ex.Message);
            }
            finally { connnection.Close(); }
            return isDeleted;
        }

        public static bool UpdateUser(int UserID, string UserName, string Password)

        {
            bool isUpdated = false;
            SqlConnection connection = new SqlConnection(clsConnectionSetting.ConnectionString);

            string query = "Update Users set " +
                "UserName=@UserName,UserPassword=@Password " +
                "where UserID=@UserID;";

            SqlCommand cmd = new SqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@UserID", UserID);
            cmd.Parameters.AddWithValue("@UserName", UserName);
            cmd.Parameters.AddWithValue("@Password", Password);

            try
            {
                connection.Open();
                int RowsAffected = cmd.ExecuteNonQuery();
                isUpdated = (RowsAffected > 0);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error" + ex.Message);
                isUpdated = false;
            }
            finally { connection.Close(); }
            return isUpdated;
        }


        public static int AddNewUser(string UserName, string UserPassword) 
        {
            int ID = -1;
            SqlConnection connection = new SqlConnection(clsConnectionSetting.ConnectionString);

            string query = "insert into Users Values " +
                " (@UserName,@UserPassword) " +
                " SELECT SCOPE_IDENTITY();";

            SqlCommand cmd = new SqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@UserName", UserName);
            cmd.Parameters.AddWithValue("@UserPassword", UserPassword);

            try
            {
                connection.Open();
                object Result = cmd.ExecuteScalar();
                if (Result != null && int.TryParse(Result.ToString(), out int InsertedID))
                {
                    ID = InsertedID;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error" + ex.Message);
                ID = -1;
            }
            finally { connection.Close(); }
            return ID;
        }


    }

    public class clsAppointmentsData
    {

        public static DataTable GetAllAppointments()
        {
            DataTable dt = new DataTable();
            SqlConnection connection = new SqlConnection(clsConnectionSetting.ConnectionString);

            string query = "Select * from ShowAppointments";

            SqlCommand command = new SqlCommand(query, connection);

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    dt.Load(reader);
                }
                reader.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error" + ex.Message);
            }
            finally { connection.Close(); }

            return dt;
        }

        public static DataTable GetAllAppointmentsShortCut()
        {
            DataTable dt = new DataTable();
            SqlConnection connection = new SqlConnection(clsConnectionSetting.ConnectionString);

            string query = "select AppointmentID ,Patient,Doctor,Status from ShowAppointments;";

            SqlCommand command = new SqlCommand(query, connection);

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    dt.Load(reader);
                }
                reader.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error" + ex.Message);
            }
            finally { connection.Close(); }

            return dt;
        }

        public static bool FindAppointment(int AppointmentID, ref int DoctorID,
            ref int PatientID,ref int StatusID,ref int MedRecordID,ref DateTime AppDate,
            ref TimeSpan AppTime)
        {
            SqlConnection connection = new SqlConnection(clsConnectionSetting.ConnectionString);
            bool isFound = false;
            string query = "select * from Appointments where AppointmentID=@AppointmentID;";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@AppointmentID", AppointmentID);

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    DoctorID = (int)reader["DoctorID"];
                    PatientID = (int)reader["PatientID"];
                    StatusID = (int)reader["StatusID"];
                    if (reader["MedRecordID"] == DBNull.Value)
                        MedRecordID = -1;
                    else
                        MedRecordID = (int)reader["MedRecordID"];

                    AppTime = (TimeSpan)reader["AppointmentTime"];
                    AppDate = (DateTime)reader["AppointmentDate"];

                    isFound = true;
                }
                reader.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error" + ex.Message);
                isFound = false;
            }
            finally
            {
                connection.Close();
            }
            return isFound;

        }

        public static int AddNewAppointment(int DoctorID,
            int PatientID, int MedRecordID, int StatusID, DateTime AppDate,
            TimeSpan AppTime)
        {

            
                int ID = -1;
                SqlConnection connection = new SqlConnection(clsConnectionSetting.ConnectionString);

                string query = "insert into Appointments Values" +
                    "(@PatientID,@DoctorID,@AppointmentDate,@AppointmentTime,@MedRecordID,@StatusID) " +
                    " SELECT SCOPE_IDENTITY();";

                SqlCommand cmd = new SqlCommand(query, connection);
                cmd.Parameters.AddWithValue("@PatientID",PatientID);
                cmd.Parameters.AddWithValue("@DoctorID", DoctorID);
            if(MedRecordID==-1)
                cmd.Parameters.AddWithValue("@MedRecordID", DBNull.Value);
            else
                cmd.Parameters.AddWithValue("@MedRecordID", MedRecordID);

                cmd.Parameters.AddWithValue("@StatusID", StatusID);
                cmd.Parameters.AddWithValue("@AppointmentDate", AppDate);
                cmd.Parameters.AddWithValue("@AppointmentTime", AppTime);


                try
                {
                    connection.Open();
                    object Result = cmd.ExecuteScalar();
                    if (Result != null && int.TryParse(Result.ToString(), out int InsertedID))
                    {
                        ID = InsertedID;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error" + ex.Message);
                    ID = -1;
                }
                finally { connection.Close(); }
                return ID;
            
        }

        public static bool UpdateAppointment(int AppID,int DoctorID,
            int PatientID, int MedRecordID, int StatusID, DateTime AppDate,
            TimeSpan AppTime)
        {


             bool isUpdated=false;
            SqlConnection connection = new SqlConnection(clsConnectionSetting.ConnectionString);

            string query = "Update Appointments set " +
                "PatientID=@PatientID,DoctorID=@DoctorID,AppointmentDate=@AppointmentDate" +
                ",AppointmentTime=@AppointmentTime,MedRecordID=@MedRecordID,StatusID=@StatusID " +
                " Where AppointmentID=@AppID;";

            SqlCommand cmd = new SqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@PatientID", PatientID);
            cmd.Parameters.AddWithValue("@DoctorID", DoctorID);

            if(MedRecordID==-1)
            cmd.Parameters.AddWithValue("@MedRecordID", DBNull.Value);
            else
            cmd.Parameters.AddWithValue("@MedRecordID", MedRecordID);

            cmd.Parameters.AddWithValue("@StatusID", StatusID);
            cmd.Parameters.AddWithValue("@AppointmentDate", AppDate);
            cmd.Parameters.AddWithValue("@AppointmentTime", AppTime);
            cmd .Parameters.AddWithValue("AppID",AppID);


            try
            {
                connection.Open();
                int RowsEffected=cmd.ExecuteNonQuery();
                isUpdated=(RowsEffected!=0);
               
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error" + ex.Message);
                isUpdated=false;
            }
            finally { connection.Close(); }
            return isUpdated;

        }

        public static bool IsDoctorBusy(int DocID,DateTime AppDate, TimeSpan AppTime)

        {
            bool isBusy = false;
            SqlConnection connection = new SqlConnection(clsConnectionSetting.ConnectionString);

            string query = "SELECT 1 AS Busy FROM ShowBusyDoctors "+
                 "WHERE DoctorID = @DocID AND AppointmentDate = @AppDate "+
                 "and @AppTime BETWEEN "+
                 "AppointmentTime AND AppEndTime; ";


            SqlCommand cmd = new SqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@DocID", DocID);
            cmd.Parameters.Add("@AppDate", SqlDbType.Date).Value = AppDate.Date;
            cmd.Parameters.Add("@AppTime", SqlDbType.Time).Value = new TimeSpan(AppTime.Hours, AppTime.Minutes, 0);


            try
            {
                connection.Open();
                object result = cmd.ExecuteScalar();
                if (result != null )
                {
                    isBusy = true;
                }



            }
            catch (Exception ex)
            {
                Console.WriteLine("Error" + ex.Message);
                isBusy = false;
            }
            finally { connection.Close(); }
            return isBusy;

        }

        public static bool DeleteAppointment(int ID)
        {
            bool isDeleted = false;
            SqlConnection connnection = new SqlConnection(clsConnectionSetting.ConnectionString);

            string query = "Delete From Appointments where " +
                "AppointmentID=@ID;";
            SqlCommand cmd = new SqlCommand(query, connnection);
            cmd.Parameters.AddWithValue("@ID", ID);

            try
            {
                connnection.Open();
                int RowsAffected = cmd.ExecuteNonQuery();
                isDeleted = (RowsAffected > 0);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error" + ex.Message);
            }
            finally { connnection.Close(); }
            return isDeleted;
        }

        public static bool MakeMedRecordNull(int ID)
             
        {


            bool isUpdated = false;
            SqlConnection connection = new SqlConnection(clsConnectionSetting.ConnectionString);

            string query = "Update Appointments set " +
                "MedRecordID= Null " +
                " Where AppointmentID=@ID;";

            SqlCommand cmd = new SqlCommand(query, connection);
 
            cmd.Parameters.AddWithValue("ID", ID);


            try
            {
                connection.Open();
                int RowsEffected = cmd.ExecuteNonQuery();
                isUpdated = (RowsEffected != 0);

            }
            catch (Exception ex)
            {
                Console.WriteLine("Error" + ex.Message);
                isUpdated = false;
            }
            finally { connection.Close(); }
            return isUpdated;

        }


    }

    public class clsMedRecordData
    { 
        public static bool FindMedRecord(int MedID, ref int DoctorId, ref int PatientId,
            ref string Description, ref string Diagnosis,
            ref string PreScribedMedication, ref string AddtionalNotes)

        {
            SqlConnection connection = new SqlConnection(clsConnectionSetting.ConnectionString);
            bool isFound = false;
            string query = "select * from MedicalRecords where MedRecordID=@MedID";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@MedID",MedID);

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    DoctorId = (int)reader["DoctorID"];
                    PatientId = (int)reader["PatientID"];

                    Diagnosis = (string)reader["Diagnosis"];
                    AddtionalNotes = (string)reader["AdditionalNotes"];
                    Description = (string)reader["Description"];
                    PreScribedMedication = (string)reader["PrescribedMedication"];


                    isFound = true;
                }
                reader.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error" + ex.Message);
                isFound = false;
            }
            finally
            {
                connection.Close();
            }
            return isFound;

        }

        public static int AddNewMedRecord(int DocID,int PatID,string Description,string Diagnosis
            ,string PrescribedMedication,string AddNotes)

        {


            int ID = -1;
            SqlConnection connection = new SqlConnection(clsConnectionSetting.ConnectionString);

            string query = "insert into MedicalRecords Values" +
                "(@PatientID,@DoctorID,@Description,@Diagnosis,@PrescribedMedication,@AddNotes) " +
                " SELECT SCOPE_IDENTITY();";

            SqlCommand cmd = new SqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@PatientID", PatID);
            cmd.Parameters.AddWithValue("@DoctorID", DocID);

            if (Description==string.Empty)
                cmd.Parameters.AddWithValue("@Description", DBNull.Value);
            else
                cmd.Parameters.AddWithValue("@Description", Description);

            if (Diagnosis == string.Empty)
                cmd.Parameters.AddWithValue("@Diagnosis", DBNull.Value);
            else
                cmd.Parameters.AddWithValue("@Diagnosis", Diagnosis);

            if (PrescribedMedication == string.Empty)
                cmd.Parameters.AddWithValue("@PrescribedMedication", DBNull.Value);
            else
                cmd.Parameters.AddWithValue("@PrescribedMedication", PrescribedMedication);
            
            if (AddNotes == string.Empty)
                cmd.Parameters.AddWithValue("@AddNotes", DBNull.Value);
            else
                cmd.Parameters.AddWithValue("@AddNotes", AddNotes);


            try
            {
                connection.Open();
                object Result = cmd.ExecuteScalar();
                if (Result != null && int.TryParse(Result.ToString(), out int InsertedID))
                {
                    ID = InsertedID;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error" + ex.Message);
                ID = -1;
            }
            finally { connection.Close(); }
            return ID;

        }



        public static bool UpdateMedRecord(int MedID, int DoctorID,
            int PatientID, string Description, string Diagnosis
            , string PrescribedMedication, string AddNotes)
        {


            bool isUpdated = false;
            SqlConnection connection = new SqlConnection(clsConnectionSetting.ConnectionString);

            string query = "Update MedicalRecords set " +
                "PatientID=@PatientID,DoctorID=@DoctorID,Description=@Description" +
                ",Diagnosis=@Diagnosis,PrescribedMedication=@PrescribedMedication" +
                ",AddtionalNotes=@AddNotes " +
                " Where MedicalRecordID=@MedID;";

            SqlCommand cmd = new SqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@PatientID", PatientID);
            cmd.Parameters.AddWithValue("@DoctorID", DoctorID);

            if (Description == string.Empty)
                cmd.Parameters.AddWithValue("@Description", DBNull.Value);
            else
                cmd.Parameters.AddWithValue("@Description", Description);

            if (Diagnosis == string.Empty)
                cmd.Parameters.AddWithValue("@Diagnosis", DBNull.Value);
            else
                cmd.Parameters.AddWithValue("@Diagnosis", Diagnosis);

            if (PrescribedMedication == string.Empty)
                cmd.Parameters.AddWithValue("@PrescribedMedication", DBNull.Value);
            else
                cmd.Parameters.AddWithValue("@PrescribedMedication", PrescribedMedication);

            if (AddNotes == string.Empty)
                cmd.Parameters.AddWithValue("@AddNotes", DBNull.Value);
            else
                cmd.Parameters.AddWithValue("@AddNotes", AddNotes);



            try
            {
                connection.Open();
                int RowsEffected = cmd.ExecuteNonQuery();
                isUpdated = (RowsEffected != 0);

            }
            catch (Exception ex)
            {
                Console.WriteLine("Error" + ex.Message);
                isUpdated = false;
            }
            finally { connection.Close(); }
            return isUpdated;

        }

        public static bool DeleteMedicalRecord(int ID)
        {
            bool isDeleted = false;
            SqlConnection connnection = new SqlConnection(clsConnectionSetting.ConnectionString);

            string query = "Delete From MedicalRecords where " +
                "MedRecordID=@ID;";
            SqlCommand cmd = new SqlCommand(query, connnection);
            cmd.Parameters.AddWithValue("@ID", ID);

            try
            {
                connnection.Open();
                int RowsAffected = cmd.ExecuteNonQuery();
                isDeleted = (RowsAffected > 0);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error" + ex.Message);
            }
            finally { connnection.Close(); }
            return isDeleted;
        }


        public static DataTable GetAllMedRecords()
        {
            DataTable dt = new DataTable();
            SqlConnection connection = new SqlConnection(clsConnectionSetting.ConnectionString);

            string query = "Select * from ShowMedRecords";

            SqlCommand command = new SqlCommand(query, connection);

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    dt.Load(reader);
                }
                reader.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error" + ex.Message);
            }
            finally { connection.Close(); }

            return dt;
        }



    }

    public class clsMethodData
    {

        public static bool FindMethod(string stName, ref int ID)
        {
            bool isFound = false;
            SqlConnection connnection = new SqlConnection(clsConnectionSetting.ConnectionString);

            string query = "Select * from PayMethods where( MethodName=@Name);";

            SqlCommand cmd = new SqlCommand(query, connnection);

            cmd.Parameters.AddWithValue("@Name", stName);

            try
            {
                connnection.Open();

                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    isFound = true;
                    ID = (int)reader["MethodID"];
                }

            }
            catch (Exception ex) { Console.WriteLine("Error : " + ex.Message); }
            finally { connnection.Close(); }
            return isFound;

        }

        public static bool FindMethod(ref string stName, int ID)
        {
            bool isFound = false;
            SqlConnection connnection = new SqlConnection(clsConnectionSetting.ConnectionString);

            string query = "Select * from PayMethods where(MethodID =@ID);";

            SqlCommand cmd = new SqlCommand(query, connnection);

            cmd.Parameters.AddWithValue("@ID", ID);

            try
            {
                connnection.Open();

                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    isFound = true;
                    stName = (string)reader["MethodName"];
                }

            }
            catch (Exception ex) { Console.WriteLine("Error : " + ex.Message); }
            finally { connnection.Close(); }
            return isFound;

        }

        public static DataTable GetAllMethods()
        {
            DataTable dt = new DataTable();
            SqlConnection connection = new SqlConnection(clsConnectionSetting.ConnectionString);
            string query = "select * from PayMethods";

            SqlCommand command = new SqlCommand(query, connection);

            try
            {
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();
                dt.Load(reader);
                reader.Close();
            }
            catch (Exception ex)
            {
                // Console.WriteLine("Error" + ex.Message);
            }
            finally { connection.Close(); }


            return dt;
        }

    }

    public class clsPaymentData
    {

        public static DataTable GetAllPayments()
        {
            DataTable dt = new DataTable();
            SqlConnection connection = new SqlConnection(clsConnectionSetting.ConnectionString);

            string query = "Select * from ShowPayments";

            SqlCommand command = new SqlCommand(query, connection);

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    dt.Load(reader);
                }
                reader.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error" + ex.Message);
            }
            finally { connection.Close(); }

            return dt;
        }

        public static bool FindPayment(int PaymentID, ref int PatientID, ref int MethodID,
                ref int AppointmentID, ref DateTime PayDate
            , ref double PaymentAmount,ref string AdditionalNotes)


        {
            SqlConnection connection = new SqlConnection(clsConnectionSetting.ConnectionString);
            bool isFound = false;
            string query = "select * from Payments where PaymentID=@PaymentID";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@PaymentID", PaymentID);

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    AppointmentID= (int)reader["AppointmentID"];
                    PatientID = (int)reader["PatientID"];

                    MethodID = (int)reader["Method"];
                    PayDate = (DateTime)reader["PaymentDate"];

                   decimal AmountDecimal = (decimal)reader["AmountPayed"];
                    PaymentAmount = (double)AmountDecimal;

                    if (reader["AdditionalNotes"] == null)
                        AdditionalNotes = string.Empty;
                    else
                        AdditionalNotes = (string)reader["AdditionalNotes"];
                            
                    isFound = true;
                }
                reader.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error" + ex.Message);
                isFound = false;
            }
            finally
            {
                connection.Close();
            }
            return isFound;

        }


        public static int AddNewPayment(int PatientID, int MethodID, int AppointmentID,
            DateTime PaymentDate, double PayAmount, string AddNotes)

        {


            int ID = -1;
            SqlConnection connection = new SqlConnection(clsConnectionSetting.ConnectionString);

            string query = "insert into Payments " +
                "Values (@PatientID,@PaymentDate,@MethodID,@PayAmount,@AddNotes,@AppointmentID);" +
                " SELECT SCOPE_IDENTITY();";

            SqlCommand cmd = new SqlCommand(query, connection);

            cmd.Parameters.AddWithValue("@PatientID", PatientID);
            cmd.Parameters.AddWithValue("@PaymentDate", PaymentDate);
            cmd.Parameters.AddWithValue("@AddNotes", AddNotes);
            cmd.Parameters.AddWithValue("@AppointmentID", AppointmentID);
            cmd.Parameters.AddWithValue("@MethodID", MethodID);
            cmd.Parameters.AddWithValue("@PayAmount", PayAmount);




            try
            {
                connection.Open();
                object Result = cmd.ExecuteScalar();
                if (Result != null && int.TryParse(Result.ToString(), out int InsertedID))
                {
                    ID = InsertedID;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error" + ex.Message);
                ID = -1;
            }
            finally { connection.Close(); }
            return ID;

        }


        public static bool UpdatePayment(int PaymentID,int PatientID, int MethodID, int AppointmentID,
           DateTime PaymentDate, double PayAmount, string AddNotes)

        {

            bool isUpdated=false;
            SqlConnection connection = new SqlConnection(clsConnectionSetting.ConnectionString);

            string query = "Update Payments " +
                "set PatientID=@PatientID,PaymentDate=@PaymentDate,Method=@MethodID," +
                "AmountPayed=@PayAmount,AdditionalNotes=@AddNotes,AppointmentID=@AppointmentID " +
                "where PaymentID=@PaymentID;";

            SqlCommand cmd = new SqlCommand(query, connection);

            cmd.Parameters.AddWithValue("@PaymentID", PaymentID);
            cmd.Parameters.AddWithValue("@PatientID", PatientID);
            cmd.Parameters.AddWithValue("@PaymentDate", PaymentDate);
            cmd.Parameters.AddWithValue("@AddNotes", AddNotes);
            cmd.Parameters.AddWithValue("@AppointmentID", AppointmentID);
            cmd.Parameters.AddWithValue("@MethodID", MethodID);
            cmd.Parameters.AddWithValue("@PayAmount", PayAmount);


            try
            {
                connection.Open();
                int Result = cmd.ExecuteNonQuery();
                isUpdated = (Result>0);
               
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error" + ex.Message);
                isUpdated = false;
            }
            finally { connection.Close(); }
            return isUpdated;

        }


        public static bool DeletePayment(int ID)
        {
            bool isDeleted = false;
            SqlConnection connnection = new SqlConnection(clsConnectionSetting.ConnectionString);

            string query = "Delete From Payments where " +
                "PaymentID=@ID;";
            SqlCommand cmd = new SqlCommand(query, connnection);
            cmd.Parameters.AddWithValue("@ID", ID);

            try
            {
                connnection.Open();
                int RowsAffected = cmd.ExecuteNonQuery();
                isDeleted = (RowsAffected > 0);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error" + ex.Message);
            }
            finally { connnection.Close(); }
            return isDeleted;
        }




    }

    public class clsJobData
    {

        public static DataTable GetAllJobs()
        {
            DataTable dt = new DataTable();
            SqlConnection connection = new SqlConnection(clsConnectionSetting.ConnectionString);
            string query = "select * from Jobs where JobName <> 'Doctor' ;";

            SqlCommand command = new SqlCommand(query, connection);

            try
            {
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();
                dt.Load(reader);
                reader.Close();
            }
            catch (Exception ex)
            {
                // Console.WriteLine("Error" + ex.Message);
            }
            finally { connection.Close(); }


            return dt;
        }

        public static string FindJobName(int ID)

        {
            string Name = string.Empty;
            SqlConnection connnection = new SqlConnection(clsConnectionSetting.ConnectionString);

            string query = "Select * from Jobs where( JobID=@ID);";

            SqlCommand cmd = new SqlCommand(query, connnection);

            cmd.Parameters.AddWithValue("@ID", ID);

            try
            {
                connnection.Open();

                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    Name = (string)reader["JobName"];
                }

            }
            catch (Exception ex) { Console.WriteLine("Error : " + ex.Message); }
            finally { connnection.Close(); }
            return Name;

        }

        public static int FindJobID(string JobName)

        {
            int ID = -1;
            SqlConnection connnection = new SqlConnection(clsConnectionSetting.ConnectionString);

            string query = "Select JobID from Jobs where( JobName=@JobName);";

            SqlCommand cmd = new SqlCommand(query, connnection);

            cmd.Parameters.AddWithValue("@JobName", JobName);

            try
            {
                connnection.Open();

                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    ID=(int)reader["JobID"];
                }

            }
            catch (Exception ex) { Console.WriteLine("Error : " + ex.Message); }
            finally { connnection.Close(); }
            return ID; ;

        }



    }



}

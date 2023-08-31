using Microsoft.Data.SqlClient;
using PMS_API.Models;

namespace PMS_API.Data
{
    public class LabTestResultData
    {
        static SqlConnection conn;
        public IConfiguration Configuration;

        public LabTestResultData(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        public static dynamic AddLabTestResult(LabTestResult labTestResult, string connection)
        {
            using(conn = new SqlConnection(connection))
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand("insert into LabTestResult(Id_Patient,Id_MedicalAppointment,Id_LabTest,Id_Doctor,Test_Result,State_Result,Date_TestResult) " +
                    "values(@patient,@medicalAppointmen,@labtest,@doctor,@testResult,@state,GETDATE())", conn);

                cmd.Parameters.AddWithValue("@patient", labTestResult.Id_Patient);
                cmd.Parameters.AddWithValue("@medicalAppointmen", labTestResult.Id_MedicalAppointment);
                cmd.Parameters.AddWithValue("@labtest", labTestResult.Id_LabTest);
                cmd.Parameters.AddWithValue("@doctor", labTestResult.Id_Doctor);
                cmd.Parameters.AddWithValue("@testResult", "test");
                cmd.Parameters.AddWithValue("@state", 0);

                cmd.ExecuteNonQuery();

                conn.Close();

                return true;
            }
        }
        public static dynamic GetLabTestResults(string connection) 
        {
            using(conn = new SqlConnection(connection))
            {
                List<LabTestResultGet> list = new List<LabTestResultGet>();
                conn.Open();

                SqlCommand cmd = new SqlCommand("select lr.Id_LabTestResult, lr.Id_Patient, p.Name_Patient,lr.Id_MedicalAppointment,l.Name_LabTest,d.Name_Doctor,lr.Test_Result,lr.State_Result,lr.Date_TestResult from LabTestResult lr " +
                    "inner join Patients p on lr.Id_Patient=p.Id_Patient " +
                    "inner join LabTest l on lr.Id_LabTest=l.Id_LabTest " +
                    "inner join Doctors d on lr.Id_Doctor = d.Id_Doctors ", conn);


                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    list.Add(new LabTestResultGet
                    {
                        Id_LabTestResult = reader.GetInt32(0),
                        Id_Patient = reader.GetInt32(1),
                        Patient = reader.GetString(2),
                        Id_MedicalAppointment = reader.GetInt32(3),
                        LabTest = reader.GetString(4),
                        Doctor = reader.GetString(5),
                        Test_Result = reader.GetString(6),
                        State_Result = reader.GetInt32(7),
                        Date_TestResult = reader.GetDateTime(8),
                    });
                }
                reader.Close();
                reader.Dispose();

                conn.Close();

                return new
                {
                    success = true,
                    data = list
                };
            }
        }
        public static dynamic GetLabTestResultsByPatient(int id, string connection)
        {
            using(conn= new SqlConnection(connection))
            {
                List<LabTestResultsByPatient> list = new List<LabTestResultsByPatient>();
                
                conn.Open();

                SqlCommand cmd = new SqlCommand("select lt.Id_LabTestResult,lt.Id_Patient,p.Name_Patient,l.Name_LabTest,lt.State_Result,lt.Id_MedicalAppointment from LabTestResult lt " +
                    "inner join Patients p on lt.Id_Patient=p.Id_Patient " +
                    "inner join LabTest l on lt.Id_LabTest=l.Id_LabTest " +
                    "where lt.Id_Patient=@id", conn);

                cmd.Parameters.AddWithValue("@id", id);

                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    list.Add(new LabTestResultsByPatient
                    {
                        Id_LabTestResult = reader.GetInt32(0),
                        Id_Patient = reader.GetInt32(1),
                        Patient= reader.GetString(2),
                        LabTest = reader.GetString(3),
                        State_Result = reader.GetInt32(4),
                        Id_MedicalAppointment = reader.GetInt32(5),
                    });
                }

                reader.Close();
                reader.Dispose();

                conn.Close();

                return new
                {
                    success = true,
                    data = list
                };
            }
        }
        public static dynamic GetAllLabTestResultsByPatient(int id, string connection)
        {
            using (conn = new SqlConnection(connection))
            {
                List<LabTestResultGet> list = new List<LabTestResultGet>();

                conn.Open();

                //SqlCommand cmd = new SqlCommand("select * from LabTestResult where Id_Patient=@id", conn);
                SqlCommand cmd = new SqlCommand("select lr.Id_LabTestResult, lr.Id_Patient, p.Name_Patient,lr.Id_MedicalAppointment,l.Name_LabTest,d.Name_Doctor,lr.Test_Result,lr.State_Result,lr.Date_TestResult from LabTestResult lr " +
                    "inner join Patients p on lr.Id_Patient=p.Id_Patient " +
                    "inner join LabTest l on lr.Id_LabTest=l.Id_LabTest " +
                    "inner join Doctors d on lr.Id_Doctor = d.Id_Doctors " +
                    "where lr.Id_Patient=@id", conn);

                cmd.Parameters.AddWithValue("@id", id);

                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    list.Add(new LabTestResultGet
                    {
                        Id_LabTestResult = reader.GetInt32(0),
                        Id_Patient = reader.GetInt32(1),
                        Patient = reader.GetString(2),
                        Id_MedicalAppointment = reader.GetInt32(3),
                        LabTest = reader.GetString(4),
                        Doctor = reader.GetString(5),
                        Test_Result= reader.GetString(6),
                        State_Result = reader.GetInt32(7),
                        Date_TestResult = reader.GetDateTime(8),
                        
                    });
                }

                reader.Close();
                reader.Dispose();

                conn.Close();

                return new
                {
                    success = true,
                    data = list
                };
            }
        }


        public static dynamic LabTestResult_PendingResults(int id, string connection)
        {
            using (conn = new SqlConnection(connection))
            {
                try
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand("update LabTestResult set State_Result=1 where Id_LabTestResult=@id", conn);

                    cmd.Parameters.AddWithValue("@id", id);

                    cmd.ExecuteNonQuery();

                    conn.Close();

                    return true;
                }
                catch (Exception)
                {
                    return false;
                }
                
            }
        }
    }
}

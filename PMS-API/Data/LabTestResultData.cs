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
                List<LabTestResult> list = new List<LabTestResult>();
                conn.Open();

                SqlCommand cmd = new SqlCommand("select * from LabTestResult", conn);


                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    list.Add(new LabTestResult
                    {
                        Id_LabTestResult= reader.GetInt32(0),
                        Id_Patient= reader.GetInt32(1),
                        Id_MedicalAppointment= reader.GetInt32(2),
                        Id_LabTest = reader.GetInt32(3),
                        Id_Doctor = reader.GetInt32(4),
                        Test_Result = reader.GetString(5),
                        State_Result = reader.GetInt32(6),
                        Date_TestResult = reader.GetDateTime(7),
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

                SqlCommand cmd = new SqlCommand("select Id_LabTestResult,Id_Patient,Id_LabTest,State_Result,Id_MedicalAppointment from LabTestResult where Id_Patient=@id", conn);

                cmd.Parameters.AddWithValue("@id", id);

                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    list.Add(new LabTestResultsByPatient
                    {
                        Id_LabTestResult = reader.GetInt32(0),
                        Id_Patient = reader.GetInt32(1),
                        Id_LabTest = reader.GetInt32(2),
                        State_Result = reader.GetInt32(3),
                        Id_MedicalAppointment = reader.GetInt32(4),
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
                conn.Open();

                SqlCommand cmd = new SqlCommand("update LabTestResult set State_Result=1 where Id_LabTestResult=@id", conn);

                cmd.Parameters.AddWithValue("@id", id);

                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    reader.Close();
                    reader.Dispose();

                    conn.Close();

                    return true;

                }
                conn.Close();

                return false;
            }
        }
    }
}

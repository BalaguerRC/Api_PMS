using Microsoft.Data.SqlClient;
using PMS_API.Models;

namespace PMS_API.Data
{
    public class MedicalAppointmentsData
    {
        static SqlConnection conn;
        public IConfiguration Configuration;

        public MedicalAppointmentsData(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public static dynamic AddMedicalAppointment(MedicalAppointments medical, string connection)
        {
            using(conn= new SqlConnection(connection))
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand("insert into MedicalAppointments(Id_Patient,Id_Doctors,Date_MA,Cause_MA,State_MA) " +
                    "values(@idpatient,@iddoctor,@date,@cause,0)", conn);

                cmd.Parameters.AddWithValue("@idpatient", medical.Id_Patient);
                cmd.Parameters.AddWithValue("@iddoctor", medical.Id_Doctros);
                cmd.Parameters.AddWithValue("@cause", medical.Cause_MA);
                cmd.Parameters.AddWithValue("@date", medical.Date_MA);

                cmd.ExecuteNonQuery();

                conn.Close();

                return true;
            }
        }

        public static dynamic GetMedicalAppointments(string connection)
        {
            using(conn= new SqlConnection(connection))
            {
                List<MedicalAppointments> list = new List<MedicalAppointments>();
                conn.Open();

                SqlCommand cmd = new SqlCommand("select * from MedicalAppointments", conn);

                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    list.Add(new MedicalAppointments
                    {
                        Id_MA = reader.GetInt32(0),
                        Id_Patient = reader.GetInt32(1),
                        Id_Doctros = reader.GetInt32(2),
                        Date_MA = reader.GetDateTime(3),
                        Cause_MA = reader.GetString(4),
                        State_MA = reader.GetInt32(5),
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
        public static dynamic GetMAByID_LabTestResult(int id, string connection)
        {
            using (conn = new SqlConnection(connection))
            {

                MedicalAppointments_LabTestResult MALabTest=new MedicalAppointments_LabTestResult();

                conn.Open();

                SqlCommand cmd = new SqlCommand("select Id_MA,Id_Patient,Id_Doctors from MedicalAppointments where Id_MA=@id ", conn);

                cmd.Parameters.AddWithValue("@id", id);

                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    reader.Close();
                    reader.Dispose();

                    SqlDataReader reader2 = cmd.ExecuteReader();
                    while (reader2.Read())
                    {
                        MALabTest.Id_MA= reader2.GetInt32(0);
                        MALabTest.Id_Patient = reader2.GetInt32(1);
                        MALabTest.Id_Doctros = reader2.GetInt32(2);
                    }
                    reader2.Close();
                    reader2.Dispose();
                    conn.Close();

                    return new
                    {
                        success = true,
                        data = MALabTest
                    };
                }

                return new
                {
                    success = false,
                    message = "MA does not exist"
                };
            }
        }
    }
}

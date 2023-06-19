using Microsoft.CodeAnalysis.Elfie.Diagnostics;
using Microsoft.Data.SqlClient;
using PMS_API.Models;

namespace PMS_API.Data
{
    public class PatientsData
    {
        static SqlConnection conn;

        public IConfiguration Configuration;

        public PatientsData(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        public static dynamic AddPatient(Patients patients,string connection)
        {
            using(conn = new SqlConnection(connection))
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand("insert into Patients(Name_Patient,LastName_Patient,Phone_Patient,Address_Patient,Identity_Patient,Birthdate_Patient,Smoker_Patient,Allergies_Patient,Img_Patient,Date_Patient) " +
                    "values(@name,@lastname,@phone,@address,@identity,@birthdate,@smoker,@allergies,@img,GETDATE())", conn);

                cmd.Parameters.AddWithValue("@name", patients.Name_Patient);
                cmd.Parameters.AddWithValue("@lastname", patients.LastName_Patient);
                cmd.Parameters.AddWithValue("@phone", patients.Phone_Patient);
                cmd.Parameters.AddWithValue("@address", patients.Address_Patient);
                cmd.Parameters.AddWithValue("@identity", patients.Identity_Patient);
                cmd.Parameters.AddWithValue("@birthdate", patients.Birthdate_Patient);
                cmd.Parameters.AddWithValue("@smoker", patients.Smoker_Patient);
                cmd.Parameters.AddWithValue("@allergies", patients.Address_Patient);
                cmd.Parameters.AddWithValue("@img", patients.Img_Patient);

                cmd.ExecuteNonQuery();

                conn.Close();

                return true;
            }
        }
        public static dynamic GetPatients(string connection)
        {
            using( conn = new SqlConnection(connection))
            {
                List<Patients> list= new List<Patients>();
                conn.Open();

                SqlCommand cmd = new SqlCommand("select * from Patients", conn);

                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    list.Add(new Patients
                    {
                        Id_Patient = reader.GetInt32(0),
                        Name_Patient = reader.GetString(1),
                        LastName_Patient = reader.GetString(2),
                        Phone_Patient = reader.GetString(3),
                        Address_Patient = reader.GetString(4),
                        Identity_Patient = reader.GetString(5),
                        Birthdate_Patient = reader.GetDateTime(6),
                        Smoker_Patient = reader.GetInt32(7),
                        Allergies_Patient = reader.GetString(8),
                        Img_Patient = reader.GetString(9),
                        Date_Patient = reader.GetDateTime(10),
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
    }
}

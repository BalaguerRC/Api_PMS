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
                cmd.Parameters.AddWithValue("@allergies", patients.Allergies_Patient);
                cmd.Parameters.AddWithValue("@img", patients.Img_Patient);

                cmd.ExecuteNonQuery();

                conn.Close();

                return true;
            }
        }
        public static dynamic EditPatient(int id,Patients patients,string connection) 
        { 
            using( conn = new SqlConnection(connection))
            {
                try
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand("update Patients set Name_Patient=@name,LastName_Patient=@lastname,Phone_Patient=@phone,Address_Patient=@address,Identity_Patient=@identity,Birthdate_Patient=@birthdate,Smoker_Patient=@smoker,Allergies_Patient=@allergies,Img_Patient=@img " +
                        "where Id_Patient=@id", conn);

                    cmd.Parameters.AddWithValue("@name", patients.Name_Patient);
                    cmd.Parameters.AddWithValue("@lastname", patients.LastName_Patient);
                    cmd.Parameters.AddWithValue("@phone", patients.Phone_Patient);
                    cmd.Parameters.AddWithValue("@address", patients.Address_Patient);
                    cmd.Parameters.AddWithValue("@identity", patients.Identity_Patient);
                    cmd.Parameters.AddWithValue("@birthdate", patients.Birthdate_Patient);
                    cmd.Parameters.AddWithValue("@smoker", patients.Smoker_Patient);
                    cmd.Parameters.AddWithValue("@allergies", patients.Allergies_Patient);
                    cmd.Parameters.AddWithValue("@img", patients.Img_Patient);
                    cmd.Parameters.AddWithValue("@id", id);
                   

                    cmd.ExecuteNonQuery();

                    conn.Close();

                    return new
                    {
                        success = true,
                        message = "Edited"
                    };
                }
                catch (Exception)
                {
                    return new
                    {
                        message = "Error"
                    };
                }
            }
        }
        public static dynamic DeletePatient(int id, string connection)
        {
            using (conn = new SqlConnection(connection))
            {
                try
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand("delete Patients where Id_Patient=@id", conn);

                    cmd.Parameters.AddWithValue("@id", id);

                    cmd.ExecuteNonQuery();

                    conn.Close();

                    return true;
                }
                catch (Exception)
                {
                    return new
                    {
                        success = false,
                        message = "Error"
                    };
                }

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
        public static dynamic GetPatientById(int id,string connection)
        {
            using(conn=new SqlConnection(connection))
            {
                Patients patients = new Patients();

                conn.Open();

                SqlCommand cmd = new SqlCommand("select * from Patients where Id_Patient=@id", conn);

                cmd.Parameters.AddWithValue("@id", id);

                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    reader.Close();
                    reader.Dispose();

                    SqlDataReader reader2 = cmd.ExecuteReader();
                    while (reader2.Read())
                    {
                        patients.Id_Patient = reader2.GetInt32(0);
                        patients.Name_Patient = reader2.GetString(1);
                        patients.LastName_Patient = reader2.GetString(2);   
                        patients.Phone_Patient = reader2.GetString(3);
                        patients.Address_Patient = reader2.GetString(4);
                        patients.Identity_Patient = reader2.GetString(5);
                        patients.Birthdate_Patient = reader2.GetDateTime(6);
                        patients.Smoker_Patient = reader2.GetInt32(7);
                        patients.Allergies_Patient = reader2.GetString(8);
                        patients.Img_Patient = reader2.GetString(9);
                        patients.Date_Patient = reader2.GetDateTime(10);
                    }
                    reader2.Close();
                    reader2.Dispose();
                    conn.Close();

                    return new
                    {
                        success = true,
                        data = patients
                    };
                }

                return new
                {
                    success = false,
                    message = "Patient does not exist"
                };
            }
        }
        public static dynamic GetPatientsInMA(string connection)
        {
            using (conn = new SqlConnection(connection))
            {
                List<Patients_MA> list = new List<Patients_MA>();
                conn.Open();

                SqlCommand cmd = new SqlCommand("select Id_Patient,Name_Patient,LastName_Patient,Identity_Patient from Patients", conn);

                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    list.Add(new Patients_MA
                    {
                        Id_Patient = reader.GetInt32(0),
                        Name_Patient = reader.GetString(1),
                        LastName_Patient = reader.GetString(2),
                        Identity_Patient = reader.GetString(3)
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

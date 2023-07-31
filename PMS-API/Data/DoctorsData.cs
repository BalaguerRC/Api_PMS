using Microsoft.Data.SqlClient;
using Microsoft.DotNet.Scaffolding.Shared.Messaging;
using PMS_API.Models;

namespace PMS_API.Data
{
    public class DoctorsData
    {
        static SqlConnection conn;
        public IConfiguration Configuration;

        public DoctorsData(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public static dynamic AddDoctor(Doctors doctors, string connection)
        {
            using(conn=new SqlConnection(connection))
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand("insert into Doctors(Name_Doctor,LastName_Doctor,Email_Doctor,Phone_Doctor,Identity__Doctor,Img_Doctor,Date_Doctor) " +
                    "values(@name,@lastname,@email,@phone,@identity,@img,GETDATE())", conn);

                cmd.Parameters.AddWithValue("@name", doctors.Name_Doctor);
                cmd.Parameters.AddWithValue("@lastname", doctors.LastName_Doctor);
                cmd.Parameters.AddWithValue("@email", doctors.Email_Doctor);
                cmd.Parameters.AddWithValue("@phone", doctors.Phone_Doctor);
                cmd.Parameters.AddWithValue("@identity", doctors.Identity_Doctor);
                cmd.Parameters.AddWithValue("@img", doctors.Img_Doctor);


                cmd.ExecuteNonQuery();

                conn.Close();

                return true;
            }
        }
        public static dynamic EditDoctor(int id, Doctors doctors, string connection)
        {
            using(conn = new SqlConnection(connection))
            {
                try
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand("update Doctors set Name_Doctor=@name,LastName_Doctor=@lastname,Email_Doctor=@email,Phone_Doctor=@phone,Identity__Doctor=@identity,Img_Doctor=@img " +
                        "where Id_Doctors=@id", conn);

                    //string PassEncry = encrypted.Encryting(users.Password_User);

                    cmd.Parameters.AddWithValue("@id", id);
                    cmd.Parameters.AddWithValue("@name", doctors.Name_Doctor);
                    cmd.Parameters.AddWithValue("@lastname", doctors.LastName_Doctor);
                    cmd.Parameters.AddWithValue("@email", doctors.Email_Doctor);
                    cmd.Parameters.AddWithValue("@phone", doctors.Phone_Doctor);
                    cmd.Parameters.AddWithValue("@identity", doctors.Identity_Doctor);
                    cmd.Parameters.AddWithValue("@img", doctors.Img_Doctor);

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

        public static dynamic DeleteDoctor(int id, string connection)
        {
            using(conn = new SqlConnection(connection))
            {
                try
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand("delete Doctors where Id_Doctors=@id", conn);

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
        public static dynamic GetDoctos(string connection)
        {
           
            using(conn=new SqlConnection(connection))
            { 
                List<Doctors> list = new List<Doctors>();
                conn.Open();

                SqlCommand cmd = new SqlCommand("select * from Doctors", conn);

                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    list.Add(new Doctors
                    {
                        Id_Doctor = reader.GetInt32(0),
                        Name_Doctor = reader.GetString(1),
                        LastName_Doctor = reader.GetString(2),
                        Email_Doctor = reader.GetString(3),
                        Phone_Doctor = reader.GetString(4),
                        Identity_Doctor = reader.GetString(5),
                        Img_Doctor = reader.GetString(6),
                        Date_Doctor = reader.GetDateTime(7),
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

        public static dynamic GetDoctorById(int id, string connection)
        {
            using(conn=new SqlConnection(connection))
            {
                Doctors doctors = new Doctors();
                conn.Open();

                SqlCommand cmd = new SqlCommand("select * from Doctors where Id_Doctors=@id", conn);
                cmd.Parameters.AddWithValue("@id", id);

                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    reader.Close();
                    reader.Dispose();

                    SqlDataReader reader2 = cmd.ExecuteReader();
                    while (reader2.Read())
                    {
                        doctors.Id_Doctor = reader2.GetInt32(0);
                        doctors.Name_Doctor = reader2.GetString(1);
                        doctors.LastName_Doctor = reader2.GetString(2);
                        doctors.Email_Doctor = reader2.GetString(3);
                        doctors.Phone_Doctor = reader2.GetString(4);
                        doctors.Identity_Doctor = reader2.GetString(5);
                        doctors.Img_Doctor = reader2.GetString(6);
                        doctors.Date_Doctor = reader2.GetDateTime(7);
                    }
                    reader2.Close();
                    reader2.Dispose();
                    conn.Close();

                    return new
                    {
                        success = true,
                        data = doctors
                    };
                }

                return new
                {
                    success = false,
                    message = "User does not exist"
                };
            }
        }
        public static dynamic GetDoctosInMA(string connection)
        {

            using (conn = new SqlConnection(connection))
            {
                List<Doctors_MA> list = new List<Doctors_MA>();
                conn.Open();

                SqlCommand cmd = new SqlCommand("select Id_Doctors,Name_Doctor,LastName_Doctor,Identity__Doctor from Doctors", conn);

                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    list.Add(new Doctors_MA
                    {
                        Id_Doctor = reader.GetInt32(0),
                        Name_Doctor = reader.GetString(1),
                        LastName_Doctor = reader.GetString(2),
                        Identity_Doctor = reader.GetString(3),
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

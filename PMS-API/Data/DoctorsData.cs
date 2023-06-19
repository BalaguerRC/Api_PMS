using Microsoft.Data.SqlClient;
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
    }
}

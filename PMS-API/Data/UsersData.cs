using Microsoft.Data.SqlClient;
using PMS_API.Models;

namespace PMS_API.Data
{
    public class UsersData
    {
        static SqlConnection conn;

        private IConfiguration Configuration;

        public UsersData(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public static dynamic AddUsers(Users users, string connection)
        {
            using(conn= new SqlConnection(connection))
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand("insert into Users(Name_User,LastName_User,Email_User,UserName,Password_User,Date_User) " +
                    "values(@name,@lastname,@email,@username,@password,GETDATE())", conn);

                cmd.Parameters.AddWithValue("@name", users.Name_User);
                cmd.Parameters.AddWithValue("@lastname", users.LastName_User);
                cmd.Parameters.AddWithValue("@email", users.Email_User);
                cmd.Parameters.AddWithValue("@username", users.UserName);
                cmd.Parameters.AddWithValue("@password", users.Password_User);

                cmd.ExecuteNonQuery();

                conn.Close();

                return true;
            }
        }

        public static dynamic GetUsers(string connection)
        {
            using (conn= new SqlConnection(connection))
            {
                conn.Open();
                List<Users> users = new List<Users>();
                SqlCommand cmd = new SqlCommand("Select * from Users", conn);

                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    users.Add(new Users
                    {
                        Id_User= reader.GetInt32(0),
                        Name_User= reader.GetString(1),
                        LastName_User= reader.GetString(2),
                        Email_User= reader.GetString(3),
                        UserName= reader.GetString(4),
                        Password_User= reader.GetString(5),
                        Date_User= reader.GetDateTime(6),
                    });
                }
                reader.Close();
                reader.Dispose();

                conn.Close();
                return new
                {
                    success = true,
                    data = users
                };
            }
        }
    }
}

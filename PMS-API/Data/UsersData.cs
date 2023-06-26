using Microsoft.Data.SqlClient;
using PMS_API.Models;
using System.Security.Cryptography.Xml;

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
            Encryption encrypted= new Encryption();
            
            bool validate= false;
            using(conn= new SqlConnection(connection))
            {
                try
                {
                    conn.Open();

                    SqlCommand cmd1 = new SqlCommand("select UserName from Users where UserName=@username", conn);

                    cmd1.Parameters.AddWithValue("@username", users.UserName);

                    SqlDataReader reader = cmd1.ExecuteReader();

                    if (reader.Read()) validate = true;

                    reader.Close();
                    reader.Dispose();
                    if (!validate)
                    {
                        string PassEncry = encrypted.Encryting(users.Password_User);
                        SqlCommand cmd = new SqlCommand("insert into Users(Name_User,LastName_User,Email_User,UserName,Password_User,Date_User) " +
                            "values(@name,@lastname,@email,@username,@password,GETDATE())", conn);

                        cmd.Parameters.AddWithValue("@name", users.Name_User);
                        cmd.Parameters.AddWithValue("@lastname", users.LastName_User);
                        cmd.Parameters.AddWithValue("@email", users.Email_User);
                        cmd.Parameters.AddWithValue("@username", users.UserName);
                        cmd.Parameters.AddWithValue("@password", PassEncry);

                        cmd.ExecuteNonQuery();

                        conn.Close();

                        return new
                        {
                            success = true
                        };
                    }

                    return new
                    {
                        message = "this username already exists"
                    };
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

        public static dynamic EditUser(int id, EditUser users,string connection)
        {
            using (conn = new SqlConnection(connection))
            {
                try
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand("update Users set Name_User=@name,LastName_User=@lastname,Email_User=@email,Password_User=@password " +
                        "where Id_User=@id", conn);

                    cmd.Parameters.AddWithValue("@id", id);
                    cmd.Parameters.AddWithValue("@name", users.Name_User);
                    cmd.Parameters.AddWithValue("@lastname", users.LastName_User);
                    cmd.Parameters.AddWithValue("@email", users.Email_User);
                    cmd.Parameters.AddWithValue("@password", users.Password_User);

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

        public static dynamic DeleteUser(int id, string connection) 
        { 
            using(conn= new SqlConnection(connection))
            {
                try
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("delete Users where Id_User=@id", conn);

                    cmd.Parameters.AddWithValue("@id", id);

                    cmd.ExecuteNonQuery();

                    conn.Close();

                    return true;
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

        public static dynamic GetUserById(int id, string connection) 
        {
            using(conn= new SqlConnection(connection))
            {
                try
                {
                    UserById users = new UserById();
                    conn.Open();

                    SqlCommand cmd = new SqlCommand("select Id_User,Name_User,LastName_User,Email_User,UserName,Date_User from Users where Id_User=@id", conn);
                    cmd.Parameters.AddWithValue("@id", id);

                    SqlDataReader reader = cmd.ExecuteReader();

                    if(reader.Read())
                    {
                        while (reader.Read())
                        {
                            users.Id_User = reader.GetInt32(0);
                            users.Name_User = reader.GetString(1);
                            users.LastName_User = reader.GetString(2);
                            users.Email_User = reader.GetString(3);
                            users.UserName = reader.GetString(4);
                            users.Date_User = reader.GetDateTime(5);
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

                    return new
                    {
                        success = false,
                        message = "User does not exist"
                    };
                }
                catch (Exception e)
                {
                    return new
                    {
                        message = "Error",
                        message2 = e.Message
                    };
                }
            }
        }
    }
}

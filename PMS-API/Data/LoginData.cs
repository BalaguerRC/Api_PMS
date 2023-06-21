using Microsoft.Data.SqlClient;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using PMS_API.Models;
using System.Data;
using System.Security.Claims;
using System.Text;

namespace PMS_API.Data
{
    public class LoginData
    {
        static SqlConnection conn;
        public IConfiguration Configuration;

        public LoginData(IConfiguration configuration)
        {
            //Configuration=configuration;
        }
        public static dynamic SignIn(Login login, string connection,IConfiguration configuration)
        {
            Encryption encryption = new Encryption();
            using(conn= new SqlConnection(connection))
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand("select Id_User,Name_User,LastName_User,Email_User,UserName,Password_User from Users" +
                    " where UserName=@user", conn);

                cmd.Parameters.AddWithValue("user", login.UserName);

                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                DataTable tb= new DataTable();
                adapter.Fill(tb);

                if(tb!= null && tb.Rows.Count>0) 
                { 
                    Users users = new Users();

                    users.Id_User = Convert.ToInt32(tb.Rows[0]["Id_User"].ToString());
                    users.Name_User = tb.Rows[0]["Name_User"].ToString();
                    users.LastName_User = tb.Rows[0]["LastName_User"].ToString();
                    users.Email_User = tb.Rows[0]["Email_User"].ToString();
                    users.UserName = tb.Rows[0]["UserName"].ToString();
                    users.Password_User = tb.Rows[0]["Password_User"].ToString();

                    string descPass = encryption.DesEncryting(users.Password_User);

                    if (descPass == login.Password)
                    {
                        UserModel model = new UserModel();
                        model.UserName = login.UserName;
                        model.Password = login.Password;

                        var token = GenerateToken(users.Email_User, login.UserName, configuration);

                        return new
                        {
                            success = true,
                            data = new
                            {
                                Id= users.Id_User,
                                Name= users.Name_User,
                                LastName= users.LastName_User,
                                Email= users.Email_User,
                                UserName=users.UserName
                            },
                            token=token
                        };
                    }
                }

                /*
                                if(reader.Read())
                                {
                                    reader.Close();
                                    reader.Dispose();
                                    conn.Close();

                                    return true;
                                }*/
                return new
                {
                    success = false
                };
            }
        }
        public static string GenerateToken(string email,string username, IConfiguration configuration)
        {
            string tokenString = "";

            var issuer = configuration["Jwt:Issuer"];
            var audience = configuration["Jwt:Audience"];
            var key = configuration["Jwt:Key"];

            var securityKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(key));

            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature);

            var claims = new[]
            {
                new Claim("Id", Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Name, username),
                new Claim(JwtRegisteredClaimNames.Email, email),
                new Claim("email", email)
            };
            var token = new JwtSecurityToken(issuer, audience, claims, expires: DateTime.Now.AddMinutes(10), signingCredentials: credentials);
            var tokenHandler= new JwtSecurityTokenHandler();

            tokenString = tokenHandler.WriteToken(token);

            return tokenString;
        }
    }
}

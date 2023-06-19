using Microsoft.Data.SqlClient;
using PMS_API.Models;

namespace PMS_API.Data
{
    public class LabTestData
    {
        static SqlConnection conn;
        public IConfiguration Configuration;

        public LabTestData(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public static dynamic AddLabTest(LabTest labTest,string connection)
        {
            using(conn = new SqlConnection(connection))
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand("insert into LabTest(Name_LabTest,Date_LabTest) values(@name,GETDATE())", conn);

                cmd.Parameters.AddWithValue("@name", labTest.Name_LabTest);

                cmd.ExecuteNonQuery();

                conn.Close();

                return true;
            }
        }

        public static dynamic GetLabTest(string connection) 
        {
            using(conn = new SqlConnection(connection))
            {
                List<LabTest> list = new List<LabTest>();

                conn.Open();

                SqlCommand cmd = new SqlCommand("select * from LabTest", conn);

                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    list.Add(new LabTest
                    {
                        Id_LabTest = reader.GetInt32(0),
                        Name_LabTest = reader.GetString(1),
                        Date_LabTest = reader.GetDateTime(2),
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

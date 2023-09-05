using Microsoft.Data.SqlClient;
using PMS_API.Models;

namespace PMS_API.Data
{
    public class DashboardsData
    {
        static SqlConnection conn;
        public IConfiguration Configuration;

        public DashboardsData(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        
        public static dynamic GetDasbhoardAdmin(string connection)
        {
            /*
             create view vwDashboardAdmin AS
  SELECT
    (select count(*) from Users) Users,
    (select count(*) from Doctors) Doctors,
    (select count(*) from LabTest) LabTest;
  go
             */
            using (conn = new SqlConnection(connection))
            {
                List<DashboardAdmin> list = new List<DashboardAdmin>();

                conn.Open();

                SqlCommand cmd = new SqlCommand("select * from vwDashboardAdmin", conn);

                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    list.Add(new DashboardAdmin
                    {
                        Users = reader.GetInt32(0),
                        Doctors = reader.GetInt32(1),
                        Lab_Tests = reader.GetInt32(2),
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
        public static dynamic GetDasbhoardDoctor(string connection)
        {
            using (conn = new SqlConnection(connection))
            {
                List<DashboardDoctors> list = new List<DashboardDoctors>();

                conn.Open();

                SqlCommand cmd = new SqlCommand("select * from vwDashboardDoctor", conn);

                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    list.Add(new DashboardDoctors
                    {
                        Patients = reader.GetInt32(0),
                        MedicalAppointmests = reader.GetInt32(1),
                        Lab_Tests_Results = reader.GetInt32(2),
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

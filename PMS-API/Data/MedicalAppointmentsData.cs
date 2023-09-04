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
                List<MedicalAppointmentsGet> list = new List<MedicalAppointmentsGet>();
                conn.Open();

                //SqlCommand cmd = new SqlCommand("select * from MedicalAppointments", conn);
                SqlCommand cmd = new SqlCommand("select ma.Id_MA,ma.Id_Patient,p.Name_Patient,d.Name_Doctor, ma.Date_MA, ma.Cause_MA, ma.State_MA from MedicalAppointments ma " +
                    "inner join Patients p on ma.Id_Patient=p.Id_Patient " +
                    "inner join Doctors d on ma.Id_Doctors = d.Id_Doctors", conn);

                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    list.Add(new MedicalAppointmentsGet
                    {
                        Id_MA = reader.GetInt32(0),
                        Id_Patient = reader.GetInt32(1),
                        Patient = reader.GetString(2),
                        Doctor = reader.GetString(3),
                        Date_MA = reader.GetDateTime(4),
                        Cause_MA = reader.GetString(5),
                        State_MA = reader.GetInt32(6),
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
        public static dynamic GetMedicalAppointmentsByPatientOrDoctor(string name,string connection)
        {
            using (conn = new SqlConnection(connection))
            {
                List<MedicalAppointmentsGet> list = new List<MedicalAppointmentsGet>();
                conn.Open();

                //SqlCommand cmd = new SqlCommand("select * from MedicalAppointments", conn);
                SqlCommand cmd = new SqlCommand("select ma.Id_MA,ma.Id_Patient,p.Name_Patient,d.Name_Doctor, ma.Date_MA, ma.Cause_MA, ma.State_MA from MedicalAppointments ma " +
                    "inner join Patients p on ma.Id_Patient=p.Id_Patient " +
                    "inner join Doctors d on ma.Id_Doctors = d.Id_Doctors " +
                    "where p.Name_Patient=@name or d.Name_Doctor=@name", conn);

                cmd.Parameters.AddWithValue("@name", name);

                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    list.Add(new MedicalAppointmentsGet
                    {
                        Id_MA = reader.GetInt32(0),
                        Id_Patient = reader.GetInt32(1),
                        Patient = reader.GetString(2),
                        Doctor = reader.GetString(3),
                        Date_MA = reader.GetDateTime(4),
                        Cause_MA = reader.GetString(5),
                        State_MA = reader.GetInt32(6),
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

        public static dynamic MedicalAppointment_PendingConsultation(int id, string connection)
        {
            using(conn = new SqlConnection(connection))
            {
                try
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand("update MedicalAppointments set State_MA=1 where Id_MA=@id", conn);

                    cmd.Parameters.AddWithValue("@id", id);

                    cmd.ExecuteNonQuery();

                    conn.Close();

                    return true;
                }
                catch (Exception)
                {

                    return false;
                }
                
            }
        }

        public static dynamic MedicalAppointment_PendingResults(int id, string connection)
        {
            using(conn = new SqlConnection(connection))
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand("update MedicalAppointments set State_MA=2 where Id_MA=@id", conn);

                cmd.Parameters.AddWithValue("@id", id);

                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    reader.Close();
                    reader.Dispose();

                    conn.Close();

                    return true;

                }
                conn.Close();

                return false;
            }
        }

        public static dynamic MedicalAppointment_Dashboard(string connection)
        {
            /*
              create view vwDashboard AS
  SELECT
	(select COUNT(State_MA) from dbo.[MedicalAppointments] where State_MA=0) Pending_Consultation,
	(select COUNT(State_MA) from dbo.[MedicalAppointments] where State_MA=1) Pending_Results,
	(select COUNT(State_MA) from dbo.[MedicalAppointments] where State_MA=2) Results;
  go

  select * from vwDashboard
             */
            using (conn = new SqlConnection(connection))
            {
                try
                {
                    List<MedicalAppointments_Dashboard> list = new List<MedicalAppointments_Dashboard>();
                    conn.Open();

                    SqlCommand cmd = new SqlCommand("select * from vwDashboard", conn);


                    SqlDataReader reader = cmd.ExecuteReader();

                    while(reader.Read())
                    {
                        list.Add(new MedicalAppointments_Dashboard
                        {
                            Pending_Consultation = reader.GetInt32(0),
                            Pending_Results = reader.GetInt32(1),
                            Results = reader.GetInt32(2),
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
                catch (Exception)
                {

                    return false;
                }
            }
        }
    }
}

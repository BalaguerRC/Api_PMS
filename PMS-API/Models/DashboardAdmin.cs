namespace PMS_API.Models
{
    public class DashboardAdmin
    {
        public int Users { get; set; }
        public int Doctors { get; set; }
        public int Lab_Tests { get; set; }
    }
    public class DashboardDoctors
    {
        public int Patients { get; set; }
        public int MedicalAppointmests { get; set; }
        public int Lab_Tests_Results { get; set; }
    }
}

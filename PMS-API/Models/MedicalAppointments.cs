namespace PMS_API.Models
{
    public class MedicalAppointments
    {
        public int? Id_MA { get; set; }
        public int Id_Patient { get; set; }
        public int Id_Doctros { get; set; }
        public DateTime Date_MA { get; set; }
        public string Cause_MA { get; set; }
        public int State_MA { get; set; }
    }
}

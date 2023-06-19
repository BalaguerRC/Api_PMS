namespace PMS_API.Models
{
    public class Patients
    {
        public int? Id_Patient { get; set; }
        public string Name_Patient { get; set; }
        public string LastName_Patient { get; set; }
        public string Phone_Patient { get; set; }
        public string Address_Patient { get; set; }
        public string Identity_Patient { get; set; }
        public DateTime Birthdate_Patient { get; set; }
        public int Smoker_Patient { get; set; }
        public string Allergies_Patient { get; set; }
        public string Img_Patient { get; set; }
        public DateTime? Date_Patient { get; set; }
    }
}

namespace PMS_API.Models
{
    public class Doctors
    {
        public int? Id_Doctor { get; set; }
        public string Name_Doctor { get; set; }
        public string LastName_Doctor { get; set; }
        public string Email_Doctor { get; set; }
        public string Phone_Doctor { get; set; }
        public string Identity_Doctor { get;set; }
        public string Img_Doctor { get; set; }
        public DateTime? Date_Doctor { get; set;}
    }
    public class Doctors_MA
    {
        public int? Id_Doctor { get; set; }
        public string Name_Doctor { get; set; }
        public string LastName_Doctor { get; set; }
        public string Identity_Doctor { get; set; }
    }
}

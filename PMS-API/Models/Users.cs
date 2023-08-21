namespace PMS_API.Models
{
    public class Users
    {
        public int? Id_User { get; set; }
        public string Name_User { get; set; }
        public string LastName_User { get; set; }
        public string Email_User { get; set; }
        public string UserName { get; set; }
        public string Password_User { get; set;}
        public DateTime? Date_User {  get; set; }
        public int Type_User { get; set; }
    }
    public class EditUser
    {
        public string Name_User { get; set; }
        public string LastName_User { get; set; }
        public string Email_User { get; set; }
        public string UserName { get; set; }
        public string Password_User { get; set; }
        public int Type_User { get; set; }
    }
    public class UserById
    {
        public int Id_User { get; set; }
        public string Name_User { get; set; }
        public string LastName_User { get; set; }
        public string Email_User { get; set; }
        public string UserName { get; set; }
        public int Type_User { get; set; }

        public DateTime? Date_User { get; set; }
    }
    public class UserByName
    {
        public string Name_User { get; set; }

    }
}

namespace LoginMVC.Models
{
    public class UserModel
    {
        public int id { get; set; }
        public string username { get; set; }
        public string password { get; set; }
        public string email { get; set; }
        public string name { get; set; }
        public string lastname { get; set; }
        public DateOnly birthday { get; set; }

    }
}

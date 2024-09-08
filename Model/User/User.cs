using Microsoft.VisualBasic;

namespace ChatApp.Model.User
{
    public class User
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string UPIId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EmailId { get; set; }
        public bool IsActive { get; set; }
        public bool IsOnline { get; set; }
        public string ConnectionId { get; set; }
        public DateTime LastLogin { get; set; }
    }
}

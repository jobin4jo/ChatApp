namespace ChatApp.DataTransferObjects
{
    public class UserResponseDTO
    {
        public string AccessToken { get; set; } 
        public string UPIId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EmailId { get; set; }
    }
}

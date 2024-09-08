namespace ChatApp.DataTransferObjects
{
    public class UserListResponseDTO
    {
        public int Id { get; set; }
        public string? UserName { get; set; }
        public string? UPIId { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? EmailId { get; set; }
        public string? ActiveStatus { get; set; }
        public bool IsOnline { get; set; }
        public string ConnectionId { get; set; }
    }
}

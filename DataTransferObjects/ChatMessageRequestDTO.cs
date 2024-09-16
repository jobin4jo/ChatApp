namespace ChatApp.DataTransferObjects;
public class ChatMessageRequestDTO
{
    public string SenderName { get; set; }
    public string ReceiverName { get; set; }
    public string Message { get; set; }
    public DateTime Timestamp { get; set; }
}
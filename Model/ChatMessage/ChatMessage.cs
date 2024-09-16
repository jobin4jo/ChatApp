using ChatApp.Model.User;
namespace ChatApp.Model.ChatMessage;
public class ChatMessage
{
    public int Id { get; set; }
    public string SenderName { get; set; }
    public string ReceiverName { get; set; }
    public string Message { get; set; }
    public DateTime Timestamp { get; set; }
}
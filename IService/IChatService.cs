using ChatApp.DataTransferObjects;
using ChatApp.Model.ChatMessage;

namespace ChatApp.IService
{
    public interface IChatService
    {
        Task UpdateChatMessage(ChatMessageRequestDTO chatMessageRequest);
        Task<List<ChatMessage>> ChatMessagesList(string SenderName, string ReceiverName);
    }
}
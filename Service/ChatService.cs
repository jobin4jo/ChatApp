
using ChatApp.DataTransferObjects;
using ChatApp.DB_Context;
using ChatApp.IService;
using ChatApp.Model.ChatMessage;
using Microsoft.EntityFrameworkCore;

namespace ChatApp.Service
{
    public class ChatService : IChatService
    {
        private readonly ChatDBContext context;
        public ChatService(ChatDBContext context)
        {
            this.context = context;
        }

        public async Task<List<ChatMessage>> ChatMessagesList(string SenderName, string ReceiverName)
        {
            var response = await context.chatMessages.Where(x => (x.SenderName.ToLower() == SenderName.ToLower() && x.ReceiverName.ToLower() == ReceiverName.ToLower()) || (x.SenderName.ToLower() == ReceiverName.ToLower() && x.ReceiverName.ToLower() == SenderName.ToLower())).OrderBy(x => x.Timestamp).ToListAsync();
            return response;
        }

        public async Task UpdateChatMessage(ChatMessageRequestDTO chatMessageRequest)
        {
            var req = new ChatMessage
            {
                ReceiverName = chatMessageRequest.ReceiverName,
                SenderName = chatMessageRequest.SenderName,
                Message = chatMessageRequest.Message,
                Timestamp = DateTime.UtcNow,
            };
            context.chatMessages.Add(req);
            await context.SaveChangesAsync();
        }
    }
}
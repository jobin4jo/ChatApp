using ChatApp.Common.Static;
using ChatApp.DataTransferObjects;
using ChatApp.IService;
using Microsoft.AspNetCore.SignalR;
using System.Collections.Concurrent;
using System.Security.Claims;

namespace ChatApp.ChatHUB
{
    public class MessageHub : Hub
    {
        private readonly IUserService userService;
        private readonly IChatService chatService;
        public MessageHub(IUserService userService, IChatService chatService)
        {
            this.userService = userService;
            this.chatService = chatService;
        }

        public async Task Login(string userName)
        {
            var userDetail = await userService.GetUserByName(userName);
            if (userDetail == null)
            {
                await Clients.Caller.SendAsync("LoginFailed", "User not found");
                return;
            }
            await userService.UpdateUserStatus(userDetail.Id, Context.ConnectionId);
            await Clients.Caller.SendAsync("LoginSuccess", userName);
        }
        public override async Task OnConnectedAsync()
        {
            await UpdateOnlineUsers();
            await base.OnConnectedAsync();
        }
        public override async Task OnDisconnectedAsync(Exception exception)
        {
            try
            {
                var user = userService.DisconnectionUpdate(Context.ConnectionId);
                await UpdateOnlineUsers();
            }
            catch (Exception ex)
            {
                throw ex;
            }

            await base.OnDisconnectedAsync(exception);
        }
        private Task UpdateOnlineUsers()
        {
            var onlineUsers = userService.GetOnlineUsers();
            return Clients.All.SendAsync("UpdateOnlineUsers", onlineUsers);
        }
        public async Task SendMessageToUser(string receiverUserName, string message)
        {
            var receiver = await userService.RecieverUserName(receiverUserName);

            if (receiver != null)
            {
                var SenderUser = userService.GetUserByConnectionId(Context.ConnectionId);
                var chatmessage = new ChatMessageRequestDTO
                {
                    SenderName = SenderUser.UserName,
                    ReceiverName = receiver.UserName,
                    Message = message,
                    Timestamp = DateTime.UtcNow
                };
                await chatService.UpdateChatMessage(chatmessage);
                await Clients.Client(receiver.ConnectionId).SendAsync("ReceiveMessage", SenderUser.UserName, message);
            }
        }
        public async Task GetMessages(string ReceiverName)
        {
            var SenderUser = userService.GetUserByConnectionId(Context.ConnectionId);
            var messages = await chatService.ChatMessagesList(SenderUser.UserName, ReceiverName);
            await Clients.Caller.SendAsync("ReceiveMessageHistory", messages);
        }

    }

}

using ChatApp.Common.Static;
using ChatApp.IService;
using Microsoft.AspNetCore.SignalR;
using System.Collections.Concurrent;
using System.Security.Claims;

namespace ChatApp.ChatHUB
{
    public class MessageHub : Hub
    {
        private readonly IUserService userService;
        public MessageHub(IUserService userService)
        {
            this.userService = userService;
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
                await Clients.Client(receiver.ConnectionId).SendAsync("ReceiveMessage", Context.User.Identity.Name, message);
            }
        }

    }
    public class Message
    {
        public string clientuniqueid { get; set; }
        public string type { get; set; }
        public string message { get; set; }
        public DateTime date { get; set; }
    }

}

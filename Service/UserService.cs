using ChatApp.Common.Static;
using ChatApp.DB_Context;
using ChatApp.IService;
using ChatApp.Model.User;
using Microsoft.EntityFrameworkCore;

namespace ChatApp.Service
{
    public class UserService : IUserService
    {
        private readonly ChatDBContext context;
        public UserService(ChatDBContext context)
        {
            this.context = context;
        }

        public async Task DisconnectionUpdate(string ConnectionId)
        {
            var user = context.Users.SingleOrDefault(x => x.ConnectionId == ConnectionId);
            if (user != null)
            {
                user.IsOnline = false;
                user.ConnectionId = null;
                context.SaveChanges();
            }
        }

        public async Task<List<string>> GetOnlineUsers()
        {
            var user = context.Users.Where(x => x.IsOnline).Select(x => x.UserName).ToList();
            return user;
        }

        public async Task<User?> GetUserByName(string userName)
        {
            var user = context.Users.SingleOrDefault(x => x.UserName.ToLower() == userName.ToLower());
            return user == null ? null : user;
        }

        public async Task<User> RecieverUserName(string receiverUserName)
        {
            var user = context.Users.SingleOrDefault(x => x.UserName.ToLower() == receiverUserName.ToLower() && x.IsOnline);
            return user == null ? null : user;
        }

        public async Task UpdateStatus(int id, string status)
        {
            var user = await context.Users.Where(x => x.Id == id).FirstOrDefaultAsync();
            if (user != null)
            {
                await context.SaveChangesAsync();
            }
        }
        public async Task UpdateUserStatus(int id, string connectionId)
        {
            var user = await context.Users.Where(x => x.Id == id).FirstOrDefaultAsync();
            if (user != null)
            {
                user.IsOnline = true;
                user.ConnectionId = connectionId;
                user.LastLogin = DateTime.UtcNow;
                await context.SaveChangesAsync();
            }
        }

    }
}

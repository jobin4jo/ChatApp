using ChatApp.Model.User;

namespace ChatApp.IService
{
    public interface IUserService
    {
        Task UpdateStatus(int id, string status);
        Task UpdateUserStatus(int id, string connectionId);
        Task<User> GetUserByName(string userName);
        Task<List<string>> GetOnlineUsers();
        Task<User> RecieverUserName(string receiverUserName);
        Task DisconnectionUpdate(string ConnectionId);
    }
}

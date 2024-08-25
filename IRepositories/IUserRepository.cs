using ChatApp.DataTransferObjects;
using ChatApp.Model.User;

namespace ChatApp.IRepositories
{
    public interface IUserRepository
    {
        Task<int>AddUser(User user);
        Task<List<UserListResponseDTO>> GetAllUserList(int id);
        Task<UserResponseDTO> UserLogin(UserRequestDTO userRequest);
    }
}

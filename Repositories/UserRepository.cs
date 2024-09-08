using AutoMapper;
using ChatApp.ChatHUB;
using ChatApp.Common.jwt;
using ChatApp.Common.Static;
using ChatApp.DataTransferObjects;
using ChatApp.DB_Context;
using ChatApp.IRepositories;
using ChatApp.IService;
using ChatApp.Model.User;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Security.Claims;
using System.Threading;

namespace ChatApp.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly ChatDBContext context;
        private readonly IConfiguration _configuration;
        private readonly IMapper mapper;
        private readonly IUserService userService;
        private readonly IHubContext<MessageHub> _hubContext;
        public UserRepository(ChatDBContext context, IConfiguration configuration, IMapper mapper, IUserService userService, IHubContext<MessageHub> _hubContext)
        {
            this.context = context;
            this._configuration = configuration;
            this.mapper = mapper;
            this.userService = userService;
            this._hubContext = _hubContext;
        }

        public async Task<int> AddUser(User user)
        {
            try
            {
                context.Users.Add(user);
                await context.SaveChangesAsync();
                return user.Id;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<List<UserListResponseDTO>> GetAllUserList(int id)
        {
            try
            {
                var response = await context.Users.ToListAsync();
                if (id > 0)
                {
                    var userToRemove = response.SingleOrDefault(x => x.Id == id);
                    if (userToRemove != null)
                    {
                        response.Remove(userToRemove);
                    }
                }
                var res = mapper.Map<List<UserListResponseDTO>>(response);
                return res;

            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<UserResponseDTO> UserLogin(UserRequestDTO userRequest)
        {
            try
            {
                User userdata = context.Users.FirstOrDefault(x => x.UserName.ToLower() == userRequest.UserName.ToLower() && x.Password == userRequest.Password);
                if (userdata == null)
                {
                    return new UserResponseDTO();
                }
                else
                {
                    return new UserResponseDTO
                    {
                        AccessToken = Jwt.GenerateToken("User", userdata.Id, _configuration["Jwt:Key"], _configuration["Jwt:Issuer"]),
                        EmailId = userdata.EmailId == null ? null : userdata.EmailId,
                        UPIId = userdata.UPIId == null ? null : userdata.UPIId,
                        FirstName = userdata.FirstName == null ? null : userdata.FirstName,
                        LastName = userdata.LastName == null ? null : userdata.LastName,
                        Id = userdata.Id == null ? null : userdata.Id,
                        UserName = userdata.UserName == null ? null : userdata.UserName
                    };
                }
            }

            catch (Exception)
            {
                throw;
            }

        }
        public async Task<bool> Logout(int id)
        {
            try
            {
                await userService.UpdateStatus(id, UserConstant.Offline);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}

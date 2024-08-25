using AutoMapper;
using ChatApp.Common.jwt;
using ChatApp.DataTransferObjects;
using ChatApp.DB_Context;
using ChatApp.IRepositories;
using ChatApp.Model.User;
using Microsoft.EntityFrameworkCore;

namespace ChatApp.Repositories
{
    public class UserRepository:IUserRepository
    {
        private readonly ChatDBContext context;
        private readonly IConfiguration _configuration;
        private readonly IMapper mapper;
        public UserRepository(ChatDBContext context, IConfiguration configuration, IMapper mapper)
        {
            this.context = context;
            this._configuration = configuration;
            this.mapper = mapper;   
        }

        public async Task<int> AddUser(User user)
        {
            try
            {
                context.Users.Add( user );
                await context.SaveChangesAsync();
                return user.Id;
            }
            catch (Exception ex) {
                throw ex;
            }
        }

        public async  Task<List<UserListResponseDTO>> GetAllUserList(int id)
        {
            try
            {
                var response = await  context.Users.ToListAsync();
                if (id > 0)
                {
                    var userToRemove = response.SingleOrDefault(x => x.Id == id);
                    if (userToRemove != null)
                    {
                        response.Remove(userToRemove);
                    }
                }
                var res= mapper.Map<List<UserListResponseDTO>>(response);
                return res;

            }
            catch (Exception ex) { 
               throw ex;
            }
        }

        public async Task<UserResponseDTO> UserLogin(UserRequestDTO userRequest)
        {
            try
            {
               User userdata=  context.Users.FirstOrDefault(x=>x.UserName.ToLower()==userRequest.UserName.ToLower()&& x.Password==userRequest.Password);
                if (userdata == null)
                {
                    return new UserResponseDTO();
                }
                else
                {
                    return new UserResponseDTO
                    {
                        AccessToken = Jwt.GenerateToken("User",userdata.Id,_configuration["Jwt:Key"], _configuration["Jwt:Issuer"]),
                        EmailId = userdata.EmailId==null?null: userdata.EmailId,
                        UPIId=userdata.UPIId==null ? null : userdata.UPIId,
                        FirstName=userdata.FirstName==null?null: userdata.FirstName,
                        LastName=userdata.LastName==null?null: userdata.LastName

                    };
                }
            }
                
            catch (Exception ex) {
                throw ex;
            }
        }
    }
}

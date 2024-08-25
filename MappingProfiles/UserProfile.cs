using AutoMapper;
using ChatApp.DataTransferObjects;
using ChatApp.Model.User;

namespace ChatApp.MappingProfiles
{
    public class UserProfile:Profile
    {
        public UserProfile()
        {
            CreateMap<User,UserListResponseDTO>();
        }
    }
}

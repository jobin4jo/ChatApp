using ChatApp.ChatHUB;
using ChatApp.DataTransferObjects;
using ChatApp.IRepositories;
using ChatApp.Model.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace ChatApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository userRepository;

        public UserController(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }
        [AllowAnonymous]
        [HttpGet("getUser")]

        public async Task<ActionResult> GetUserDetails([FromQuery] int id)
        {
            try
            {
                var response = await userRepository.GetAllUserList(id);
                return Ok(new { Code = 200, Status = true, Message = "", data = response });
            }
            catch (Exception ex)
            {
                return Ok(new { Code = 400, Status = false, Message = ex, Data = new { } });
            }
        }
        [HttpPost("AddUser")]
        public async Task<ActionResult> CreateUser([FromBody] User userResponse)
        {
            try
            {
                var response = await userRepository.AddUser(userResponse);
                return Ok(new { Code = 200, Status = true, Message = "DATA_INSERT_SUCCESS", Data = new { data = response } });
            }
            catch (Exception ex)
            {
                return Ok(new { Code = 400, Status = false, Message = ex, Data = new { } });
            }
        }
        [HttpPost("Login")]
        public async Task<ActionResult> LoginUser([FromBody] UserRequestDTO userRequest)
        {
            try
            {
                var response = await userRepository.UserLogin(userRequest);
                return Ok(new { Code = 200, Status = true, Message = "USER_LOGIN_SUCCESS", Data = response });
            }
            catch (Exception ex)
            {
                return Ok(new { Code = 400, Status = false, Message = ex, Data = new { } });
            }
        }
        [HttpPost("logout")]
        public async Task<ActionResult> Logout(int id)
        {
            try
            {
                var response = await userRepository.Logout(id);
                return Ok(new { Code = 200, Status = true, Message = "Success", Data = response });
            }
            catch (Exception ex)
            {
                return Ok(new { Code = 400, Status = false, Message = ex, Data = new { } });
            }
        }

    }
}

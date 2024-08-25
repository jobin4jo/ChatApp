using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ChatApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        public UserController()
        {

        }
        [HttpGet("getUser")]
        public async Task<ActionResult> GetUserDetails()
        {
            try
            {
                return Ok(new { Code = 200, Status = true, Message = "", Data = "userdetails" });
            }
            catch (Exception ex)
            {
                return Ok(new { Code = 401, Status = false, Message = ex, Data = new { } });
            }
        }
    }
}

using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Server_Try02.Services;

namespace Server_Try02.Controllers
{   [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        public UserController(IUserService UserService)
        {
            _userService = UserService;

        }

        [HttpGet("GetAllUsers")]
        public async Task<IActionResult> GetAllUsers()
        {
            return Ok(await _userService.GetAllUsers());
        }


    }
}
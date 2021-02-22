using System;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using server_new_try.DTOs;
using Server_Try02.Models;
using Server_Try02.Services;
using WebApi.Data;

namespace Server_Try02.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    //This is the controller class for the user
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        private readonly IHttpContextAccessor _httpContext;
        private readonly DataContext _context;

        public UserController(IUserService UserService, IHttpContextAccessor HttpContext, DataContext DataContext)
        {
            _context = DataContext;
            _httpContext = HttpContext;
            _userService = UserService;

        }

        [HttpGet("GetAllUsers")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> GetAllUsers()
        {

           
            return Ok(await _userService.GetAllUsers());
        }

        [HttpPost("UpdateUser/{Username}")]
        
        public async Task<IActionResult> UpdateUser(RegisterUserDto UpdatedUser,String Username)
        {
            var role = _httpContext.HttpContext.User.FindFirstValue(ClaimTypes.Role);
            var name = _httpContext.HttpContext.User.FindFirstValue(ClaimTypes.Name);
            var response = await _userService.UpdateUser(UpdatedUser,Username);

            if (role != "admin" || name != UpdatedUser.Username)
            //check if user has admin rights
            {

                return Unauthorized(response);
            }
            else if (response.Success)
            {
                return Ok(response);
            }
            else
            {
                return BadRequest(response);
            }

        }



        [HttpGet("{Username}")]
        public async Task<IActionResult> GetUserByName(String Username)
        {
            var response = await _userService.GetUserByName(Username);
            if (response.Data == null)
            {
                return NotFound(response);
            }
            else
            {
                return Ok(response.Data);
            }

        }

        [HttpDelete("{Username}")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> Delete(String Username)
        {
            var response = await _userService.DeleteUser(Username);
            if (response.Data == null)
            {
                return NotFound(response);
            }
            else
            {
                return Ok(response.Data);
            }

        }
    }
}
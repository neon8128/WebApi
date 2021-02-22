using System;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using server_new_try;
using server_new_try.DTOs;
using Server_Try02.Models;
using Server_Try02.Services;
using WebApi.Data;
using WebApi.DTOs;
using WebApi.Validators;

namespace WebApi.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthRepository _auth;
        private IAuditService _audit;
        private readonly DataContext _context;

        public AuthController(IAuthRepository Auth, IAuditService Audit, DataContext Context)
        {
            _context = Context;
            _auth = Auth;
            _audit = Audit;

        }

        [HttpPost("Register")]
        public async Task<IActionResult> Register(RegisterUserDto RegisterUserDto)
        {
            var validator = new RegisterUserValidator();
            var results = validator.Validate(RegisterUserDto);

           
            var response = await _auth.Register(
                new UserModel
                {
                    Username = RegisterUserDto.Username,
                    Email = RegisterUserDto.Email
                },
                RegisterUserDto.Password
            );

            if (!results.IsValid)
            {
               
                return BadRequest(response);
            }
            else
            {
                return Ok(response);
            }
        }
        [HttpPost("Login")]
        public async Task<IActionResult> Login(LoginDto RequestedUser)
        {
            var response = await _auth.Login(RequestedUser.Username, RequestedUser.Password);
            if (!response.Success)
            {
                return BadRequest(response);
            }
            else
            {
                return Ok(response);
            }
        }
    }
}
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using server_new_try;
using server_new_try.DTOs;

namespace Server_Try02.Services
{
    public interface IUserService
    {
        Task<ServiceResponse<List<GetUserDto>>> GetAllUsers();
        Task<ServiceResponse<List<GetUserDto>>> GetUserByName(String Username);
        Task<ServiceResponse<List<GetUserDto>>> AddUser(GetUserDto NewUser);
        Task<ServiceResponse<List<GetUserDto>>> UpdateCharacter();
        Task<ServiceResponse<List<GetUserDto>>> DeleteUser(String Username);
    }
}
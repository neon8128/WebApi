using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using server_new_try;
using server_new_try.DTOs;
using Server_Try02.Models;

namespace Server_Try02.Services
{
    //Interface for User services
    public interface IUserService
    {
        Task<ServiceResponse<List<GetUserDto>>> GetAllUsers();
        Task<ServiceResponse<GetUserDto>> GetUserByName(String Username);
        Task<ServiceResponse<RegisterUserDto>> UpdateUser(RegisterUserDto NewUser,String Username);
        Task<ServiceResponse<GetUserDto>> DeleteUser(String Username);

        bool IsAdmin();
    }
}
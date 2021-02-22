using System;
using System.Threading.Tasks;
using server_new_try;
using server_new_try.DTOs;
using Server_Try02.Models;


namespace WebApi.Data
{
    public interface IAuthRepository
    {
        Task<ServiceResponse<String>> Login(String Username, String Password);

        Task<ServiceResponse<String>> Register(UserModel User,String Password);

        
    }
}
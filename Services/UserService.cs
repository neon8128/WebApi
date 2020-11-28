using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using server_new_try;
using server_new_try.DTOs;
using Server_Try02.Models;

namespace Server_Try02.Services
{
    public class UserService : IUserService
    {
        private readonly IMapper _mapper;
        private readonly locationContext _context;
        public UserService(IMapper Mapper, locationContext Context)
        {
            _mapper = Mapper;
            _context = Context;
        }

       public Task<ServiceResponse<List<GetUserDto>>> AddUser(GetUserDto NewUser)
        {
            throw new System.NotImplementedException();
        }

        public Task<ServiceResponse<List<GetUserDto>>> DeleteUser(string Username)
        {
            throw new System.NotImplementedException();
        }

        public  async Task<ServiceResponse<List<GetUserDto>>> GetAllUsers()
        {
            var serviceResponse  = new ServiceResponse<List<GetUserDto>>();
            List<User> dbuser = await _context.Users.ToListAsync();
            serviceResponse.Data = (dbuser.Select(a =>_mapper.Map<GetUserDto>(a))).ToList();
            return serviceResponse;

        }

        public Task<ServiceResponse<List<GetUserDto>>> GetUserByName(string Username)
        {
            throw new System.NotImplementedException();
        }

        public Task<ServiceResponse<List<GetUserDto>>> UpdateCharacter()
        {
            throw new System.NotImplementedException();
        }
    }
}
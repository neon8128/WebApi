using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Server_Try02.Models;
using server_new_try;
using server_new_try.DTOs;
using System;
using WebApi.Data;
using WebApi.Hashing;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace Server_Try02.Services
{
    //Service class for user
    public class UserService : IUserService
    {
        private readonly IMapper _mapper;
        private readonly DataContext _context;
        private readonly IHttpContextAccessor _httpContext;
        private readonly IAuditService _audit;

        public UserService(IMapper Mapper, DataContext Context, IHttpContextAccessor HttpContext, IAuditService Audit)
        {
            _audit = Audit;
            _httpContext = HttpContext;
            _mapper = Mapper;
            _context = Context;
        }

        private String GetUserRole() => _httpContext.HttpContext.User.FindFirstValue(ClaimTypes.Role);

        private String GetUserName() => _httpContext.HttpContext.User.FindFirstValue(ClaimTypes.Name);

        public bool IsAdmin() => GetUserRole() == "admin" ? true : false;





        //Delete user from the database
        public async Task<ServiceResponse<GetUserDto>> DeleteUser(String Username)
        {
            var serviceResponse = new ServiceResponse<GetUserDto>();
            var transaction = _context.Database.BeginTransaction();

            var dbuser = await _context.DbUsers
                 .FirstAsync(c => c.Username == Username);

            if (dbuser != null)
            {
                _context.DbUsers.Remove(dbuser);
                await _context.SaveChangesAsync();
                transaction.Commit();
                serviceResponse.Message = "The user was deleted Successfully";
                serviceResponse.Success = true;
            }
            else
            {
                serviceResponse.Message = "The user was not found";
                serviceResponse.Success = false;
            }

            return serviceResponse;
        }

        //Getting all the users from db using linq
        public async Task<ServiceResponse<List<GetUserDto>>> GetAllUsers()
        {
            var serviceResponse = new ServiceResponse<List<GetUserDto>>();

            var dbuser = await _context.DbUsers.ToListAsync(); // Users from dbset
            if(dbuser !=null)
            {
                serviceResponse.Success = true;
                serviceResponse.Data = (dbuser.Select(a => _mapper.Map<GetUserDto>(a))).ToList();            
            }
            else
            {
                serviceResponse.Success = false;
            }

            return serviceResponse;

        }

        //Get a certain user by username
        public async Task<ServiceResponse<GetUserDto>> GetUserByName(String Username)
        {
            var serviceResponse = new ServiceResponse<GetUserDto>();

            var dbuser = await _context.DbUsers
                .FirstOrDefaultAsync(a => a.Username == Username); //Select user with specified username from user table

            serviceResponse.Data = _mapper.Map<GetUserDto>(dbuser);
            if (serviceResponse.Data == null)
            {
                serviceResponse.Success = false;
                serviceResponse.Errors.Add("Could not find selected user");

            }
            return serviceResponse;
        }


        //Update a user
        public async Task<ServiceResponse<RegisterUserDto>> UpdateUser(RegisterUserDto UpdatedUser,String Username)
        {
            var serviceResponse = new ServiceResponse<RegisterUserDto>();
            var user = await _context.DbUsers.FirstOrDefaultAsync(c => c.Username == Username);// get the user which has the same username      
            var transaction =  await _context.Database.BeginTransactionAsync();
            var hashing = new HashingAlgorithms();
            

            try
            {
                if (user != null) // check if users exists
                {
                    hashing.CreateHash(UpdatedUser.Password, out byte[] hash, out byte[] salt); //hash new password
                    user.Email = UpdatedUser.Email;
                    user.PasswordHash = hash;
                    user.Salt = salt;
                    user.Version++; // update version
                    _context.DbUsers.Update(user);
                    _context.DbUserHists.Add(_mapper.Map<UserHistModel>(user));
                    await _audit.InsertAudit(user.Username,$"The user was updated successfully by '{user.Role}'");
                    await _context.SaveChangesAsync();
                    await transaction.CommitAsync();


                    serviceResponse.Success = true;
                    serviceResponse.Message = $"The user was updated successfully by '{user.Role}'";
                }
                else
                {
                    serviceResponse.Success = false;
                    serviceResponse.Message = "User could not be found";
                }

            }
            catch (Exception e)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = e.ToString();
                await transaction.RollbackAsync();
            }
            transaction.Dispose();
            return serviceResponse;
        }


    }
}
using System;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using server_new_try;
using Server_Try02.Models;
using Server_Try02.Services;
using WebApi.Hashing;

namespace WebApi.Data
{
    public class AuthRepository : IAuthRepository
    {
        private readonly DataContext _context;
        private readonly IAuditService _audit;
        private readonly IMapper _mapper;

        public AuthRepository(DataContext Context, IAuditService Audit, IMapper Mapper)
        {

            _mapper = Mapper;
            _audit = Audit;
            _context = Context;
        }

        public async Task<ServiceResponse<String>> Login(String Username, String Password)
        {
            var response = new ServiceResponse<String>();
  
           var user = await _context.DbUsers.FirstOrDefaultAsync(x =>x.Username == Username);
            var hashingObject = new HashingAlgorithms();
            
            if (user == null)
            {
                response.Success = false;
                response.Errors.Add("User not found");
            }
            else if (!hashingObject.VerifyHash(Password, user.PasswordHash, user.Salt))
            {
                response.Success = false;
                response.Errors.Add("Wrong Password");
            }
            else
            {
                response.Data = hashingObject.CreateToken(user);
                response.Success = true;
                response.Message = "You have successfully loged in!";
                await _audit.InsertAudit(Username, "User has logged in!");

            }
            return response;
        }



        public async Task<ServiceResponse<String>> Register(UserModel User, String Password)
        {
            var response = new ServiceResponse<String>();
            var hashingObject = new HashingAlgorithms();

            if (await IsUserDuplicateAsync(User.Username))
            {
                response.Success = false;
                response.Errors.Add("Username already taken");
                return response;
            }

            if (await IsEmailDuplicateAsync(User.Email))
            {
                response.Success = false;
                response.Errors.Add("Email is already in use");
                return response;
            }


            hashingObject.CreateHash(Password, out byte[] hash, out byte[] salt);
            User.PasswordHash = hash;
            User.Salt = salt;

            var transaction = await  _context.Database.BeginTransactionAsync();
            var userHist = _mapper.Map<UserHistModel>(User);
            
            try
            {
                await _context.DbUsers.AddAsync(User); //Inserting in the user table
                await _context.SaveChangesAsync();
                await _context.DbUserHists.AddAsync(userHist); // inserting in the userhist table 
                await _context.SaveChangesAsync();
                await transaction.CommitAsync(); //commit to the database
            }
            catch (Exception e)
            {
                response.Success = false;
                response.Errors.Add(e.ToString());
                await transaction.RollbackAsync(); // rollback to a previous version
            }

            transaction.Dispose();          
            response.Success = true;
            response.Message = "User was created";

            return response;
        }
        public async Task<bool> IsUserDuplicateAsync(String Username) => await _context.DbUsers
                .AnyAsync(x => x.Username.ToLower() == Username.ToLower());  // checks for duplicates in the user table 

        public async Task<bool> IsEmailDuplicateAsync(String Email) => await _context.DbUsers
                .AnyAsync(x => x.Email.ToLower() == Email.ToLower()); //Checks for duplicates in Email 

    }
}
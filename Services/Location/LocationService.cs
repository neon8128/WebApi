using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Server_Try02.Models;
using server_new_try;
using System;
using WebApi.Data;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;
using Server_Try02.Services;
using WebApi.Models;

namespace WebApi.Services.Location
{
    public class LocationService : ILocationService
    {
        private readonly IMapper _mapper;
        private readonly DataContext _context;
        private readonly IHttpContextAccessor _httpContext;
        private readonly IAuditService _audit;



        public LocationService(IHttpContextAccessor HttpContext, IAuditService AuditService, DataContext DataContext)
        {
           _context = DataContext;
            _audit = AuditService;
            _httpContext = HttpContext;

        }
        private String GetUserName() => _httpContext.HttpContext.User.FindFirstValue(ClaimTypes.Name);
        private String GetUserRole() => _httpContext.HttpContext.User.FindFirstValue(ClaimTypes.Role);
        public async Task<ServiceResponse<LocationModel>> AddLocation(LocationModel NewLocation)
        {
            var response = new ServiceResponse<LocationModel>();
            
            var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
              var auditId= await   _audit.GetAuditId("Location added");
                NewLocation.AuditId = auditId;
               await _context.DbLocations.AddAsync(NewLocation);

                await transaction.CommitAsync();
                await _context.SaveChangesAsync();
                response.Data = NewLocation;
                response.Success = true;
                response.Message = "Location added";
                
                transaction.Dispose();
            }
            catch (Exception e)
            {
                await transaction.RollbackAsync();
                response.Success = false;
                response.Message = e.ToString();
            }
           
            return response;

        }

        public async  Task<ServiceResponse<LocationModel>> DeleteLocation(UInt64 Id)
        {
            var response = new ServiceResponse<LocationModel>();
            var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
               var locations = await _context.DbLocations.FirstAsync(x => x.LocationId == Id);
                _context.DbLocations.Remove(locations);
                await _audit.InsertAudit(GetUserName(),"Location Deleted");

                await transaction.CommitAsync();
                await _context.SaveChangesAsync();

                response.Success = true;
                response.Message = "Location Deleted";

            }
            catch(Exception e)
            {
                response.Success = false;
                response.Message = e.ToString();
            }
            return response;
        }

        public  async Task<ServiceResponse<LocationModel>> GetLocationByEndDate(DateTime End)
        {
            var response = new ServiceResponse<LocationModel>();
           

            var locations = await _context.DbLocations.FirstOrDefaultAsync(x => x.EndDate == End);
               
            if(locations!= null)
            {
                response.Success = true;
                response.Data = locations;
            }
            else
            {
                response.Success = false;
                response.Message ="The location does not exist in the database";
            }

            return response;
        }

        public  async Task<ServiceResponse<LocationModel>> GetLocationById(UInt64 Id)
        {
            var response = new ServiceResponse<LocationModel>();

            var location = await _context.DbLocations
                    .FirstOrDefaultAsync(x => x.LocationId == Id);
               
            if(location!= null)
            {
                response.Success = true;
                response.Data = location;
            }
            else
            {
                response.Success = false;
                response.Message ="The location does not exist in the database";
            }

            return response;
        }

        public async  Task<ServiceResponse<LocationModel>> GetLocationByStartDate(DateTime Start)
        {
            var response = new ServiceResponse<LocationModel>();

            var locations = await _context.DbLocations.FirstOrDefaultAsync(x => x.StartDate == Start);
               
            if(locations!= null)
            {
                response.Success = true;
                response.Data = locations;
            }
            else
            {
                response.Success = false;
                response.Message ="The location does not exist in the database";
            }

            return response;
        }

        public  async Task<ServiceResponse<List<object>>> GetLocationByUser(String UserName)
        {
            var response = new ServiceResponse<List<object>>();
           var finalEntries = new List<object>();

            var userLocation =  from UserModel  u in _context.DbUsers 
                join AuditModel a in _context.DbAudit on u.Username equals UserName
                join LocationModel k in _context.DbLocations on a.Id equals  k.AuditId
                select new 
                {
                Id=k.LocationId, 
                k.Latitude,
                k.Longitude, 
                k.StartDate,
                k.EndDate}; //join 3 tables in  order to get all location by each username
            
            if(userLocation !=null)
            {
                finalEntries.Add(userLocation);
            }
            
            if(finalEntries !=null)
            {
                response.Success = true;
                response.Data = finalEntries;
            }
            else
            {
                response.Success = false;
                response.Message ="The location does not exist in the database";
            }

            return response;
        }

        public  async Task<ServiceResponse<LocationModel>> UpdateLocation(LocationModel UpdatedLocation,UInt64 Id)
        {
            var response = new ServiceResponse<LocationModel>();
            var location = await _context.DbLocations.FirstOrDefaultAsync(x=> x.LocationId ==Id );
          

            if(location !=null)
            {               
                location.StartDate = UpdatedLocation.StartDate;
                location.EndDate = UpdatedLocation.EndDate;
                location.Latitude = UpdatedLocation.Latitude;
                location.Longitude = UpdatedLocation.Longitude;

                response.Success = true;
                response.Data = location;
                response.Message = "The location was updated";
                await _context.SaveChangesAsync();
            }
            else
            {
                response.Success = false;
                response.Message = "The location was not found in the database";
            }

            return response;
        }
    
        public async Task<ServiceResponse<List<LocationModel>>> GetAll()
        {
            var response = new ServiceResponse<List<LocationModel>>();
            response.Data = await _context.DbLocations.ToListAsync();
            
            if(response.Data!=null)
            {
                response.Success = true;                
            }
            else
            {
                response.Success = false;
                response.Message = "The are no locations saved in the database";
            }
            return response;

        }
    }
}
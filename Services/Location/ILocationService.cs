using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using server_new_try;
using WebApi.Models;

namespace WebApi.Services.Location
{
    public interface ILocationService
    {
        Task<ServiceResponse<LocationModel>> AddLocation(LocationModel NewLocation);
        Task<ServiceResponse<LocationModel>> GetLocationById(UInt64 Id);
        Task<ServiceResponse<LocationModel>> UpdateLocation( LocationModel UpdatedLocation,UInt64 Id);
        Task<ServiceResponse<LocationModel>> DeleteLocation(UInt64 Id);
        Task<ServiceResponse<List<object>>> GetLocationByUser(String UserName);
        Task<ServiceResponse<LocationModel>> GetLocationByStartDate(DateTime Start);
        Task<ServiceResponse<LocationModel>> GetLocationByEndDate(DateTime End);

        Task<ServiceResponse<List<LocationModel>>> GetAll();
    }
}
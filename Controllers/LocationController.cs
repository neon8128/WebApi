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
using WebApi.Models;
using WebApi.Services.Location;
using WebApi.Validators;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;

namespace WebApi.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class LocationController:ControllerBase
    {
        private readonly DataContext _dataContext;
        private readonly ILocationService _locationService;
        public LocationController(ILocationService LocationService, DataContext DataContext)
        {
            _locationService = LocationService;
            _dataContext = DataContext;

        }

        [HttpPost("AddLocation")]
        public async  Task<IActionResult> AddLocation(LocationModel NewLocation)
        {
            var response = await _locationService.AddLocation( 
                new LocationModel
                {
                    Longitude = NewLocation.Longitude,
                    Latitude = NewLocation.Latitude,
                    StartDate = NewLocation.StartDate,
                    EndDate = NewLocation.EndDate,
                }
                );
            var validator = new LocationValidator();
            var results = validator.Validate(NewLocation);

             if (!results.IsValid || !response.Success)
            {
               
                return BadRequest(response);
            }         
            return Ok(response);
                    
        }

        [HttpGet("end/{End}")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> GetLocationByEndDate(DateTime End)
        {    
            
            var response = await _locationService.GetLocationByEndDate(End);
            if(!response.Success)
            {
                return BadRequest(response);                
            }
            else
            {
                return Ok(response);
            }

        }

        [HttpGet("start/{Start}")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> GetLocationByStartDate(DateTime Start)
        {    
            
            var response = await _locationService.GetLocationByStartDate(Start);
            if(!response.Success)
            {   
                return BadRequest(response);                
            }
            else
            {
                return Ok(response);
            }

        }

        [HttpGet("user/{Username}")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> GetLocationByUser(String Username)
        {
            var response = await _locationService.GetLocationByUser(Username);
            if(!response.Success)
            {
                return BadRequest(response);                
            }
            else
            {
                return Ok(response);
            }
            

        }

        [HttpGet("id/{Id}")]
        public async Task<IActionResult> GetLocationById(UInt64 Id)
        {
            var response = await _locationService.GetLocationById(Id);
            if(!response.Success)
            {
                return BadRequest(response);                
            }
            else
            {
                return Ok(response);
            }
            
        }
    
        [HttpPost("Update/{Id}")]
        
        public async Task<IActionResult> UpdateLocation(LocationModel UpdatedLocation,UInt64 Id)
        {
            var response = await _locationService.UpdateLocation(UpdatedLocation,Id);

            if(!response.Success)
            {
                return BadRequest(response);
            }
            else
            {
                return Ok(response);
            }
            
        }
    
        [HttpPost("Delete/{Id}")]
        [Authorize(Roles ="admin")]
        public async Task<IActionResult> DeleteLocation(UInt64 Id)
        {
            var response = await _locationService.DeleteLocation(Id);

            if(!response.Success)
            {
                return BadRequest(response);
            }
            else
            {
                return Ok(response);
            }
            
        }
    
        [HttpGet("getall")]
        [Authorize(Roles ="admin")]
        public async Task<IActionResult> GetAll()
        {
            var response = await _locationService.GetAll();
            if(!response.Success)
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
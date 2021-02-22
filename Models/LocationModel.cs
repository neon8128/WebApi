using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using Server_Try02.Models;

namespace WebApi.Models
{
    public class LocationModel
    {
        [Key]
        public UInt64 LocationId { get; set; }
        public Double Longitude { get; set; }
        public Double Latitude { get; set; }
        public UInt64 AuditId { get; set; }
        
        public DateTime StartDate { get; set; }
       
        public DateTime EndDate { get; set; }

        
    }
}
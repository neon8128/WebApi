using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using WebApi.Models;

#nullable disable

namespace Server_Try02.Models
{
    public partial class AuditModel
    {      
        [Key]
        public UInt64 Id { get; set; }
        public string Username { get; set; }
        public DateTime? Ts { get; set; }
        public string Details { get; set; }
        public string MachineName { get; set; }
        public string Ip { get; set; }

       

        
    }
}

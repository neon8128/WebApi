using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace Server_Try02.Models
{
    [Keyless]
    public partial class UserHistModel
    {
       
        public UInt64 Id { get; set; }
        public String Username { get; set; }
        public String Email { get; set; }
        public byte[] PasswordHash { get; set; }
        public String Role { get; set; }
        public Int32 Version { get; set; }
        public byte[] Salt { get; set; }
    }
}

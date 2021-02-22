using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace Server_Try02.Models
{
    public partial class UserModel
    {
       
        
        public String Username { get; set; }
        public String Email { get; set; }
        public byte[] PasswordHash { get; set; }
        public String Role { get; set; }
        public Int32 Version { get; set; }
        public byte[] Salt { get; set; }

      
    }
}

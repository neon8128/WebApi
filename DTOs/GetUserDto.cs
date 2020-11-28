using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace server_new_try.DTOs
{
    public class GetUserDto
    {
        public String Username { get; set; }
        public String Email { get; set; }
        public String Password { get; set; }
        public String Role { get; set; }
    }
}

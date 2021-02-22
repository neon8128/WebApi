using System;
using System.Collections.Generic;

#nullable disable

namespace Server_Try02.fake
{
    public partial class UserHist
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Role { get; set; }
        public string PasswordHash { get; set; }
        public int Version { get; set; }
        public string Salt { get; set; }
    }
}

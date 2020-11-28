using System;
using System.Collections.Generic;

#nullable disable

namespace Server_Try02.Models
{
    public partial class UserHist
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public int Version { get; set; }
    }
}

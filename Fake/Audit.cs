using System;
using System.Collections.Generic;

#nullable disable

namespace Server_Try02.fake
{
    public partial class Audit
    {
        public Audit()
        {
            Locations = new HashSet<Location>();
        }

        public int AuditId { get; set; }
        public string Username { get; set; }
        public DateTime? Ts { get; set; }
        public string Details { get; set; }
        public string MachineName { get; set; }
        public string Ip { get; set; }

        public virtual ICollection<Location> Locations { get; set; }
    }
}

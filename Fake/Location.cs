using System;
using System.Collections.Generic;

#nullable disable

namespace Server_Try02.fake
{
    public partial class Location
    {
        public int LocationId { get; set; }
        public double? Longitude { get; set; }
        public double? Latitude { get; set; }
        public int AuditId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public virtual Audit Audit { get; set; }
    }
}

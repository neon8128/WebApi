using System;
using System.Collections.Generic;

#nullable disable

namespace Server_Try02.Models
{
    public partial class Audit
    {
        public int AuditId { get; set; }
        public string Username { get; set; }
        public DateTime? Ts { get; set; }
        public string Details { get; set; }
        public string MachineName { get; set; }
        public string Ip { get; set; }
        public double? Longidute { get; set; }
        public double? Latitude { get; set; }
    }
}

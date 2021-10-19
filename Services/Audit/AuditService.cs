using System;
using System.Net;
using System.Net.Sockets;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using server_new_try;
using Server_Try02.Models;
using WebApi.Data;

namespace Server_Try02.Services
{
    public class AuditService : IAuditService
    {
        private readonly DataContext _context;
        private readonly IHttpContextAccessor _httpContext;

        

        public AuditService(DataContext DataContext, IHttpContextAccessor HttpContext)
        {
            
            _httpContext = HttpContext;
            _context = DataContext;

        }
        private String GetUserName() => _httpContext.HttpContext.User.FindFirstValue(ClaimTypes.Name);

        // Inserting in the audit table and return last inserted id in order to track user activity
        public async  Task<UInt64> GetAuditId(String Message)
        {
            var audit = new AuditModel();
            audit.Ip = GetLocalIPAddress();
            audit.Ts = DateTime.UtcNow;
            audit.Username = GetUserName();
            audit.Details = Message;
            audit.MachineName = System.Environment.MachineName;
            await  _context.DbAudit.AddAsync(audit);
            await _context.SaveChangesAsync();

            return audit.Id;
        }

        public async Task InsertAudit(String Username, String Message)
        {
            var audit = new AuditModel();
            audit.Ip = GetLocalIPAddress();
            audit.Ts = DateTime.UtcNow;
            audit.Username = Username;
            audit.Details = Message;
            audit.MachineName = System.Environment.MachineName;
            await _context.DbAudit.AddAsync(audit);
            await _context.SaveChangesAsync();

        }


        //     Get local ip address        
        public String GetLocalIPAddress()
        {
            var host = Dns.GetHostEntry(Dns.GetHostName());
            foreach (var ip in host.AddressList)
            {
                if (ip.AddressFamily == AddressFamily.InterNetwork)
                {
                    return ip.ToString();
                }
            }
            throw new Exception("No network adapters with an IPv4 address in the system!");
        }


    }
}
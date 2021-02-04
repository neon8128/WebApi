using System;
using System.Threading.Tasks;
using server_new_try;
using Server_Try02.Models;

namespace Server_Try02.Services
{
    public interface IAuditService
    {
        Task<UInt64> GetAuditId(String Message);
        Task  InsertAudit(String Username,String Message);

    }
}
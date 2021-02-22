using System;
using System.Data.Entity;
using MySql.Data.MySqlClient;
using Server_Try02.Models;
using WebApi.Models;

namespace WebApi.Data
{
    public class AppDb:IDisposable
    {
        public MySqlConnection Connection { get; }
       

        public AppDb(string connectionString)
        {
            Connection = new MySqlConnection(connectionString);
        }

        public void Dispose() => Connection.Dispose();
    }
}
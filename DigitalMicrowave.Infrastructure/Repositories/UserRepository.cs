using DigitalMicrowave.Domain.Entities;
using DigitalMicrowave.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;

namespace DigitalMicrowave.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private IDbConnection Connection =>
            new SqlConnection(ConfigurationManager.ConnectionStrings["DigitalMicrowave"].ConnectionString);
        public Users GetUser(string user, string password)
        {
            using (var db = Connection)
            {
                return db.QueryFirstOrDefault<Users>(
                "SELECT UserName FROM Users WHERE UserName = @user AND PasswordHash = @password", new {user, password });
            }
        }
    }
}

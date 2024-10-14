using MySql.Data.MySqlClient;
using Microsoft.Extensions.Configuration;

namespace PortfolioAPI.Services
{
    public class DatabaseContext
    {
        private readonly string _connectionString;

        public DatabaseContext(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public MySqlConnection CreateConnection()
        {
            return new MySqlConnection(_connectionString);
        }
    }
}

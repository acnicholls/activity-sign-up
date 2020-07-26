using Dapper.AmbientContext;
using Microsoft.Extensions.Configuration;
using System.Data;
using System.Data.SqlClient;


namespace ActivitySignUp.Repositories
{
    public class DbConnectionFactory : IDbConnectionFactory
    {

        private IConfiguration _configuration;

        public DbConnectionFactory(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public IDbConnection Create()
        {
            return new SqlConnection(_configuration.GetConnectionString("ActivitySignUpDatabase"));
        }
       

    }
}

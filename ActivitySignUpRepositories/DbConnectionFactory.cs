using Dapper.AmbientContext;
using Microsoft.Extensions.Configuration;
using System.Data;
using System.Data.SqlClient;


namespace ActivitySignUp.Repositories
{
    /// <summary>
    /// this class constructs the db connections
    /// </summary>
    public class DbConnectionFactory : IDbConnectionFactory
    {

        private IConfiguration _configuration;

        /// <summary>
        /// basic ctor
        /// </summary>
        /// <param name="configuration">the application configuration</param>
        public DbConnectionFactory(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        /// <summary>
        /// creates the connection
        /// </summary>
        /// <returns>IDbConnection</returns>
        public IDbConnection Create()
        {
            return new SqlConnection(_configuration.GetConnectionString("ActivitySignUpDatabase"));
        }
       

    }
}

using Dapper.AmbientContext;
using Microsoft.Extensions.Configuration;
using System.Data;
using System.Data.SqlClient;
using Microsoft.Extensions.Hosting;

namespace ActivitySignUp.Repositories
{
    /// <summary>
    /// this class constructs the db connections
    /// </summary>
    public class DbConnectionFactory : IDbConnectionFactory
    {

        private IConfiguration _configuration;

        private IHostEnvironment _environment;

        /// <summary>
        /// basic ctor
        /// </summary>
        /// <param name="configuration">the application configuration</param>
        public DbConnectionFactory(
            IConfiguration configuration,
            IHostEnvironment environment)
        {
            _configuration = configuration;
            _environment = environment;
        }

        /// <summary>
        /// creates the connection
        /// </summary>
        /// <returns>IDbConnection</returns>
        public IDbConnection Create()
        {
            switch(_environment.EnvironmentName)
            {
                case "Production":
                {
                    return new SqlConnection(_configuration.GetConnectionString("ActivitySignUpDatabase_Production"));
                }
                case "arm64-latest":
                {
                    return new SqlConnection(_configuration.GetConnectionString("ActivitySignUpDatabase_arm64-latest"));
                }
                case "local":
                {
                    // ActivitySignUpDatabase_local
                    return new SqlConnection(_configuration.GetConnectionString("ActivitySignUpDatabase_local"));                    
                }
            }
            return new SqlConnection(_configuration.GetConnectionString("ActivitySignUpDatabase"));            
        }
       

    }
}

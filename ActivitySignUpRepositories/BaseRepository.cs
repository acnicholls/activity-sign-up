using ActivitySignUp.Repositories.Interfaces;
using System.Data;

namespace ActivitySignUp.Repositories
{
    public class BaseRepository
    {

        protected readonly IDbConnection DbContext;

        public BaseRepository(IDbConnectionFactory connectionFactory)
        {
            DbContext = connectionFactory.CreateConnection();
        }


    }
}

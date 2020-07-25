using ActivitySignUp.Constants;
using ActivitySignUp.Models.Person;
using ActivitySignUp.Repositories.Interfaces;
using Dapper;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace ActivitySignUp.Repositories
{
    /// <summary>
    /// this class handles the person table
    /// </summary>
    public class PersonRepository : BaseRepository, IPersonRepository
    {

        public PersonRepository(IDbConnectionFactory connectionFactory) : base(connectionFactory)
        { }

        /// <summary>
        /// this method inserts a new record into the person table
        /// </summary>
        /// <param name="model">this is the data model containing the field values</param>
        /// <returns>the Id value of the new record</returns>
        public async Task<int> InsertPersonAsync(PersonInsertModel model)
        {
            var parameters = new DynamicParameters(model);
            parameters.Add("NewId", dbType: DbType.Int32, direction: ParameterDirection.Output);

            await DbContext.ExecuteAsync(StoredProcedures.PersonInsert, parameters, commandType: CommandType.StoredProcedure);

            return parameters.Get<int>("NewId");
        }
    }
}

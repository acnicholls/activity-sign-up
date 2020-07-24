using ActivitySignUp.Constants;
using ActivitySignUp.Models.Person;
using ActivitySignUp.Repositories.Interfaces;
using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace ActivitySignUp.Repositories
{
    public class PersonRepository : BaseRepository, IPersonRepository
    {

        public PersonRepository(IDbConnectionFactory connectionFactory) : base(connectionFactory)
        { }



        public async Task<List<PersonListModel>> GetParticipantList(int activityId)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@ActivityId", activityId, DbType.Int32);

            return (await DbContext.QueryAsync<PersonListModel>(StoredProcedures.ActivityGetPersonList, parameters, commandType: CommandType.StoredProcedure)).ToList();
        }

        public async Task<int> InsertPerson(string firstName, string lastName, string email, int activityId)
        {
            var parameters = new DynamicParameters();
            parameters.Add("PersonFirstName", firstName, DbType.String, size:50);
            parameters.Add("PersonLastName", lastName, DbType.String, size: 50);
            parameters.Add("PersonEmail", email, DbType.String, size: 120);
            parameters.Add("PersonActivityId", activityId, DbType.Int32);
            parameters.Add("NewId", dbType: DbType.Int32, direction: ParameterDirection.Output);

            await DbContext.ExecuteAsync(StoredProcedures.PersonInsert, parameters, commandType: CommandType.StoredProcedure);

            return parameters.Get<int>("NewId");
        }
    }
}

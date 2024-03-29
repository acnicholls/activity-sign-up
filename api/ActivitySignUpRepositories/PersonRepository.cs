﻿using ActivitySignUp.Constants;
using ActivitySignUp.Models.Person;
using ActivitySignUp.Repositories.Interfaces;
using Dapper;
using Dapper.AmbientContext;
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

        /// <summary>
        /// basic ctor
        /// </summary>
        /// <param name="ambientDbContextLocator">the ambient context locator</param>
        public PersonRepository(IAmbientDbContextLocator ambientDbContextLocator) : base(ambientDbContextLocator)
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

        /// <summary>
        /// this method checks to see if the person with the passed email has already signed up for the activity with the passed Id value
        /// </summary>
        /// <param name="activityId">the Id value of the activity to check</param>
        /// <param name="email">the email of the person to check</param>
        /// <returns></returns>
        public async Task<bool> CheckPersonExistsInActivityAsync(int activityId, string email)
        {
            var parameters = new DynamicParameters();
            parameters.Add("PersonActivityId", activityId, DbType.Int32);
            parameters.Add("PersonEmail", email, DbType.String);
            parameters.Add("Result", dbType: DbType.Boolean, direction: ParameterDirection.Output);

            await DbContext.ExecuteAsync(StoredProcedures.PersonExistsInActivityCheck, parameters, commandType: CommandType.StoredProcedure);

            return parameters.Get<bool>("Result");

        }
    }
}

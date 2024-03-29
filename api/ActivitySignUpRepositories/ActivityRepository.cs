﻿using ActivitySignUp.Constants;
using ActivitySignUp.Models.Activity;
using ActivitySignUp.Models.Comment;
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
    /// this class handles the activity table
    /// </summary>
    public class ActivityRepository : BaseRepository, IActivityRepository
    {

        public ActivityRepository(IAmbientDbContextLocator ambientDbContextLocator) : base(ambientDbContextLocator)
        { }

        /// <summary>
        /// this method checks to see if the passed value already exists in the database as an Activity Name
        /// </summary>
        /// <param name="nameToCheck">the value to check for</param>
        /// <returns>true if exists, false if not</returns>
        public async Task<bool> CheckActivityExistsAsync(string nameToCheck)
        {
            var parameters = new DynamicParameters();
            parameters.Add("ActivityName", nameToCheck, DbType.String);
            parameters.Add("Result", dbType: DbType.Boolean, direction: ParameterDirection.Output);

            await DbContext.QueryAsync<bool>(StoredProcedures.ActivityExistsCheck, parameters, commandType: System.Data.CommandType.StoredProcedure);
            
            return parameters.Get<bool>("Result");
        }

        /// <summary>
        /// this method inserts a new record into the activity table
        /// </summary>
        /// <param name="model">this is the data model containing the field values</param>
        /// <returns>the Id of the new record</returns>
        public async Task<int> InsertActivityAsync(ActivityInsertModel model, byte[] fileContent)
        {
            var parameters = new DynamicParameters();
            parameters.Add("NewId", dbType: DbType.Int32, direction: ParameterDirection.Output);
            parameters.Add("ActivityName", model.ActivityName, dbType: DbType.String, direction: ParameterDirection.Input);
            parameters.Add("ActivityDescription", model.ActivityDescription, dbType: DbType.String, direction: ParameterDirection.Input);
            parameters.Add("ActivityDateTime", model.ActivityDateTime, dbType: DbType.DateTime, direction: ParameterDirection.Input);
            parameters.Add("ActivityImage", fileContent, dbType: DbType.Binary, direction: ParameterDirection.Input);
            await DbContext.ExecuteAsync(StoredProcedures.ActivityInsert, parameters, commandType: CommandType.StoredProcedure);
            return parameters.Get<int>("NewId");
        }

        /// <summary>
        /// this method retrieves the inital view of an individual activity
        /// </summary>
        /// <param name="activityId">the Id value of the activity to retrieve</param>
        /// <returns>the activity view model</returns>
        public async Task<ActivityModel> GetInitialActivityViewAsync(int activityId)
        {
            var parameters = new DynamicParameters();
            parameters.Add("ActivityId", activityId, DbType.Int32);
            return await DbContext.QuerySingleOrDefaultAsync<ActivityModel>(StoredProcedures.ActivityGetInitialView, parameters, commandType: CommandType.StoredProcedure);
        }

        /// <summary>
        /// this method retrieves the view of an activity after a user is signed up
        /// </summary>
        /// <param name="activityId">the Id of the activity to retrieve</param>
        /// <returns>the signed up activity view model</returns>
        public async Task<ActivitySignedUpViewModel> GetSignedUpActivityViewAsync(int activityId)
        {
            var parameters = new DynamicParameters();
            parameters.Add("ActivityId", activityId, DbType.Int32);
            var queryResult = await DbContext.QueryMultipleAsync(StoredProcedures.ActivityGetView, parameters, commandType: CommandType.StoredProcedure);

            var tmpActivity = await queryResult.ReadSingleOrDefaultAsync<ActivityModel>();

            var returnvalue = new ActivitySignedUpViewModel
            {
                ActivityId = tmpActivity.ActivityId,
                ActivityName = tmpActivity.ActivityName,
                ActivityDescription = tmpActivity.ActivityDescription,
                ActivityDateTime = tmpActivity.ActivityDateTime,
                ActivityImage = tmpActivity.ActivityImage,
                ParticipantList = (await queryResult.ReadAsync<PersonListModel>()).ToList(),
                CommentList = (await queryResult.ReadAsync<CommentListModel>()).ToList()
            };

            return returnvalue;
        }

        /// <summary>
        /// this method returns a list of activities 
        /// </summary>
        /// <returns>list of activities</returns>
        public async Task<List<ActivityListModel>> GetActivityListAsync()
        {
            return (await DbContext.QueryAsync<ActivityListModel>(StoredProcedures.ActivityGetList, commandType: CommandType.StoredProcedure)).ToList();
        }
    }
}

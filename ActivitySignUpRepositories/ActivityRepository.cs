using ActivitySignUp.Constants;
using ActivitySignUp.Models.Activity;
using ActivitySignUp.Models.Comment;
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
    /// this class handles the activity table
    /// </summary>
    public class ActivityRepository : BaseRepository, IActivityRepository
    {

        public ActivityRepository(IDbConnectionFactory connectionFactory) : base(connectionFactory)
        { }

        /// <summary>
        /// this method checks to see if the passed value already exists in the database as an Activity Name
        /// </summary>
        /// <param name="nameToCheck">the value to check for</param>
        /// <returns>true if exists, false if not</returns>
        public async Task<bool> ActivityExistsAsync(string nameToCheck)
        {
            var parameters = new DynamicParameters(nameToCheck);
            var result = (await DbContext.QueryAsync<bool>(Functions.ActivityExists, parameters, commandType: System.Data.CommandType.Text)).First();
            return result;
        }

        /// <summary>
        /// this method inserts a new record into the activity table
        /// </summary>
        /// <param name="model">this is the data model containing the field values</param>
        /// <returns>the Id of the new record</returns>
        public async Task<int> ActivityInsertAsync(ActivityInsertModel model)
        {
            var parameters = new DynamicParameters(model);
            parameters.Add("NewId", dbType: DbType.Int32, direction: ParameterDirection.Output);
            await DbContext.ExecuteAsync(StoredProcedures.ActivityInsert, parameters, commandType: CommandType.StoredProcedure);
            return parameters.Get<int>("NewId");
        }

        /// <summary>
        /// this method retrieves the inital view of an individual activity
        /// </summary>
        /// <param name="activityId">the Id value of the activity to retrieve</param>
        /// <returns>the activity view model</returns>
        public async Task<ActivityViewModel> GetInitialActivityViewAsync(int activityId)
        {
            var parameters = new DynamicParameters(activityId);
            return await DbContext.QuerySingleOrDefaultAsync<ActivityViewModel>(StoredProcedures.ActivityGetInitialView, parameters, commandType: CommandType.StoredProcedure);
        }

        /// <summary>
        /// this method retrieves the view of an activity after a user is signed up
        /// </summary>
        /// <param name="activityId">the Id of the activity to retrieve</param>
        /// <returns>the signed up activity view model</returns>
        public async Task<ActivitySignedUpViewModel> GetSignedUpActivityViewAsync(int activityId)
        {
            var parameters = new DynamicParameters(activityId);
            var queryResult = await DbContext.QueryMultipleAsync(StoredProcedures.ActivityGetView, parameters, commandType: CommandType.StoredProcedure);

            var returnvalue = new ActivitySignedUpViewModel
            {
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

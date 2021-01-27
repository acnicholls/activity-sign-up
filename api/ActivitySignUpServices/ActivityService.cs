using ActivitySignUp.Models;
using ActivitySignUp.Models.Activity;
using ActivitySignUp.Repositories.Interfaces;
using ActivitySignUp.Services.Interfaces;
using ActivitySignUp.Validation.Interfaces;
using Dapper.AmbientContext;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ActivitySignUp.Services
{
    /// <summary>
    /// this service class handles all methods for Activity
    /// </summary>
    public class ActivityService : BaseService, IActivityService
    {
        private IActivityRepository _repository;
        private IActivityValidators _validators;

        /// <summary>
        /// basic ctor
        /// </summary>
        /// <param name="factory">the ambient context factory, used to create a db connection and context</param>
        /// <param name="repository">the activity repository</param>
        /// <param name="validators">the activity validator</param>
        public ActivityService(
            IAmbientDbContextFactory factory,
            IActivityRepository repository, 
            IActivityValidators validators)
            : base(factory)
        {
            _repository = repository;
            _validators = validators;
        }

       /// <summary>
       /// this method gets a list of activities
       /// </summary>
       /// <returns>a list of activityListModel records</returns>
        public async Task<ServiceResult<List<ActivityListModel>>> GetActivityListAsync()
        {
            try
            {
                using(var dbScope = ContextFactory.Create())
                {
                    return new ServiceResult<List<ActivityListModel>>(await _repository.GetActivityListAsync());
                }
            }
            catch(Exception x)
            {
                return new ServiceResult<List<ActivityListModel>>(new ServiceError() { Location = "GetActivityListAsync", Exception = x.Message });
            }
        }

        /// <summary>
        /// this method returns a single activity
        /// </summary>
        /// <param name="activityId">the Id value of the activity to return</param>
        /// <returns>a single ActivityModel record</returns>
        public async Task<ServiceResult<ActivityModel>> GetInitialActivityViewAsync(int activityId)
        {
            try
            {
                using (var dbScope = ContextFactory.Create())
                {
                    return new ServiceResult<ActivityModel>(await _repository.GetInitialActivityViewAsync(activityId));
                }
            }
            catch (Exception x)
            {
                return new ServiceResult<ActivityModel>(new ServiceError() { Location = "GetActivityListAsync", Exception = x.Message });
            }
        }

        /// <summary>
        /// this method returns data regarding an activity that a person has already signed up for
        /// </summary>
        /// <param name="activityId">the Id of the activity to return</param>
        /// <returns>a single ActivitySignedUpModel record</returns>
        public async Task<ServiceResult<ActivitySignedUpViewModel>> GetSignedUpActivityViewAsync(int activityId)
        {
            try
            {
                using (var dbScope = ContextFactory.Create())
                {
                    return new ServiceResult<ActivitySignedUpViewModel>(await _repository.GetSignedUpActivityViewAsync(activityId));
                }
            }
            catch (Exception x)
            {
                return new ServiceResult<ActivitySignedUpViewModel>(new ServiceError() { Location = "GetActivityListAsync", Exception = x.Message });
            }
        }

        /// <summary>
        /// this method inserts a new activity into the Activity db Table
        /// </summary>
        /// <param name="model">the model containing the new activity data</param>
        /// <returns>the Id of the inserted record</returns>
        public async Task<ServiceResult<int>> InsertActivityAsync(ActivityInsertModel model)
        {
            try
            {
                // validate
                var validationResult = _validators.ValidateInsertModel(model);
                if (validationResult.IsValid)
                {
                    using (var dbScope = ContextFactory.Create())
                    {
                        // check that the activity doesn't already exist
                        if (!(await _repository.CheckActivityExistsAsync(model.ActivityName)))
                        {
                            var activityId = await _repository.InsertActivityAsync(model);
                            dbScope.Transaction.Commit();
                            return new ServiceResult<int>(activityId);
                        }
                        return new ServiceResult<int>(new ValidationError() { FieldName = "ActivityName", ErrorDetail = "An activity with that name already exists." });
                    }
                }
                return new ServiceResult<int>(validationResult.ValidationErrors);
            }
            catch(Exception x)
            {
                return new ServiceResult<int>(new ServiceError() { Location = "InsertActivityAsync", Exception = x.Message });
            }
        }
    }
}

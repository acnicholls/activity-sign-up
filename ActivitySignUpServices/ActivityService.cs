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
    public class ActivityService : BaseService, IActivityService
    {
        private IActivityRepository _repository;
        private IActivityValidators _validators;

        public ActivityService(
            IAmbientDbContextFactory factory,
            IActivityRepository repository, 
            IActivityValidators validators)
            : base(factory)
        {
            _repository = repository;
            _validators = validators;
        }

       
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
                            dbScope.Commit();
                            return new ServiceResult<int>(activityId);
                        }
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

using ActivitySignUp.Models.Activity;
using System.Collections.Generic;
using System.Threading.Tasks;
using ActivitySignUp.Models;

namespace ActivitySignUp.Services.Interfaces
{
    public interface IActivityService
    {

        Task<ServiceResult<int>> InsertActivityAsync(ActivityInsertModel model);

        Task<ServiceResult<ActivityModel>> GetInitialActivityViewAsync(int activityId);

        Task<ServiceResult<ActivitySignedUpViewModel>> GetSignedUpActivityViewAsync(int activityId);

        Task<ServiceResult<List<ActivityListModel>>> GetActivityListAsync();
    }
}

using ActivitySignUp.Models.Activity;
using System.Collections.Generic;
using System.Threading.Tasks;


namespace ActivitySignUp.Repositories.Interfaces
{
    public interface IActivityRepository
    {

        Task<int> InsertActivityAsync(ActivityInsertModel model);

        Task<bool> CheckActivityExistsAsync(string nameToCheck);

        Task<ActivityModel> GetInitialActivityViewAsync(int activityId);

        Task<ActivitySignedUpViewModel> GetSignedUpActivityViewAsync(int activityId);

        Task<List<ActivityListModel>> GetActivityListAsync();
    }
}

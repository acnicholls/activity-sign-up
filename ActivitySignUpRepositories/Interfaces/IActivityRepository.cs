using ActivitySignUp.Models.Activity;
using System.Collections.Generic;
using System.Threading.Tasks;


namespace ActivitySignUp.Repositories.Interfaces
{
    public interface IActivityRepository
    {

        Task<int> ActivityInsertAsync(ActivityInsertModel model);

        Task<bool> ActivityExistsAsync(string nameToCheck);

        Task<ActivityViewModel> GetInitialActivityViewAsync(int activityId);

        Task<ActivitySignedUpViewModel> GetSignedUpActivityViewAsync(int activityId);

        Task<List<ActivityListModel>> GetActivityListAsync();
    }
}

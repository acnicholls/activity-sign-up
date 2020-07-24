using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ActivitySignUp.Models.Activity;


namespace ActivitySignUp.Repositories.Interfaces
{
    public interface IActivityRepository
    {

        Task<int> ActivityInsert(string name, string description, DateTime date, string image);

        Task<bool> ActivityExists(string nameToCheck);

        Task<List<ActivityListModel>> GetActivityList();

        Task<ActivityViewModel> GetActivity(int activityId);
    }
}

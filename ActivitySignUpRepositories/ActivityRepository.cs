using ActivitySignUp.Models.Activity;
using ActivitySignUp.Repositories;
using ActivitySignUp.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;


namespace ActivitySignUpRepositories
{
    public class ActivityRepository : BaseRepository, IActivityRepository
    {


        public ActivityRepository(IDbConnectionFactory connectionFactory) : base(connectionFactory)
        { }


        public Task<bool> ActivityExists(string nameToCheck)
        {
            throw new NotImplementedException();
        }

        public Task<int> ActivityInsert(string name, string description, DateTime date, string image)
        {
            throw new NotImplementedException();
        }

        public Task<ActivityViewModel> GetActivity(int activityId)
        {
            throw new NotImplementedException();
        }

        public Task<List<ActivityListModel>> GetActivityList()
        {
            throw new NotImplementedException();
        }
    }
}

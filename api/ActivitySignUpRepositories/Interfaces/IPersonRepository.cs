﻿using ActivitySignUp.Models.Person;
using System.Threading.Tasks;

namespace ActivitySignUp.Repositories.Interfaces
{
    public interface IPersonRepository
    {
        Task<int> InsertPersonAsync(PersonInsertModel model);

        Task<bool> CheckPersonExistsInActivityAsync(int activityId, string email);
    }
}

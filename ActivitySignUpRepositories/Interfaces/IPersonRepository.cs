using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ActivitySignUp.Models.Person;

namespace ActivitySignUp.Repositories.Interfaces
{
    public interface IPersonRepository
    {
        Task<int> InsertPerson(string firstName, string lastName, string email, int activityId);

        Task<List<PersonListModel>> GetParticipantList(int activityId);
    }
}

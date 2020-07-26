using ActivitySignUp.Models;
using ActivitySignUp.Models.Person;
using System.Threading.Tasks;

namespace ActivitySignUp.Services.Interfaces
{
    public interface IPersonService
    {

        Task<ServiceResult<int>> InsertPersonAsync(PersonInsertModel model);

        Task<ServiceResult<bool>> CheckPersonExistsInActivityAsync(int activityId, string email);
    }
}

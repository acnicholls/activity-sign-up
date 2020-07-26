using ActivitySignUp.Models;
using ActivitySignUp.Models.Person;
using ActivitySignUp.Repositories.Interfaces;
using ActivitySignUp.Services.Interfaces;
using ActivitySignUp.Validation.Interfaces;
using System;
using System.Threading.Tasks;

namespace ActivitySignUp.Services
{
    public class PersonService : IPersonService
    {
        private IPersonRepository _repository;
        private IPersonValidators _validators;

        public PersonService(IPersonRepository repository, IPersonValidators validators)
        {
            _repository = repository;
            _validators = validators;
        }


        public async Task<ServiceResult<int>> InsertPersonAsync(PersonInsertModel model)
        {
            try
            {
                // validate
                var validationResult = _validators.ValidateInsertModel(model);

                return validationResult.IsValid ?
                     new ServiceResult<int>(await _repository.InsertPersonAsync(model)) :
                     new ServiceResult<int>(validationResult.ValidationErrors);
            }
            catch (Exception x)
            {
                return new ServiceResult<int>(new ServiceError() { Location = "InsertPersonAsync", Exception = x.Message });
            }

        }


        public async Task<ServiceResult<bool>> CheckPersonExistsInActivityAsync(int activityId, string email)
        {
            try
            {
                // validate 
                var validationResult = _validators.ValidateCheckEmail(email);
                return validationResult.IsValid ?
                    new ServiceResult<bool>(await _repository.CheckPersonExistsInActivityAsync(activityId, email)) :
                    new ServiceResult<bool>(validationResult.ValidationErrors);
            }
            catch (Exception x)
            {
                return new ServiceResult<bool>(new ServiceError() { Location = "CheckPersonExistsInActivityAsync", Exception = x.Message });
            }

        }

    }
}

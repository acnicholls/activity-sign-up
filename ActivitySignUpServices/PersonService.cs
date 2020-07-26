using ActivitySignUp.Models;
using ActivitySignUp.Models.Person;
using ActivitySignUp.Repositories.Interfaces;
using ActivitySignUp.Services.Interfaces;
using ActivitySignUp.Validation.Interfaces;
using Dapper.AmbientContext;
using System;
using System.Threading.Tasks;

namespace ActivitySignUp.Services
{
    public class PersonService : BaseService, IPersonService
    {
        private IPersonRepository _repository;
        private IPersonValidators _validators;

        public PersonService(
            IAmbientDbContextFactory factory,
            IPersonRepository repository,
            IPersonValidators validators)
            : base(factory)
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

                using (var dbScope = ContextFactory.Create())
                {

                    if (validationResult.IsValid)
                    {
                        if (!(await _repository.CheckPersonExistsInActivityAsync(model.PersonActivityId, model.PersonEmail)))
                        {
                            return new ServiceResult<int>(await _repository.InsertPersonAsync(model));
                        }
                    }

                    return new ServiceResult<int>(validationResult.ValidationErrors);
                }
            }
            catch (Exception x)
            {
                return new ServiceResult<int>(new ServiceError() { Location = "InsertPersonAsync", Exception = x.Message });
            }

        }
    }
}

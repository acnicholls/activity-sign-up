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
    /// <summary>
    /// this service class handles all methods of the person 
    /// </summary>
    public class PersonService : BaseService, IPersonService
    {
        private IPersonRepository _repository;
        private IPersonValidators _validators;

        /// <summary>
        /// basic ctor
        /// </summary>
        /// <param name="factory">the ambient context factory</param>
        /// <param name="repository">the person repository</param>
        /// <param name="validators">the person validator</param>
        public PersonService(
            IAmbientDbContextFactory factory,
            IPersonRepository repository,
            IPersonValidators validators)
            : base(factory)
        {
            _repository = repository;
            _validators = validators;
        }

        /// <summary>
        /// this method inserts a new person record into the Person db Table
        /// </summary>
        /// <param name="model">the model containing the details of the new person</param>
        /// <returns>the Id value of the new person record</returns>
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
                            var personId = await _repository.InsertPersonAsync(model);
                            dbScope.Transaction.Commit();
                            return new ServiceResult<int>(personId);
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

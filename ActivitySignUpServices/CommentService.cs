using ActivitySignUp.Models;
using ActivitySignUp.Models.Comment;
using ActivitySignUp.Models.Person;
using ActivitySignUp.Repositories.Interfaces;
using ActivitySignUp.Services.Interfaces;
using ActivitySignUp.Validation.Interfaces;
using System;
using System.Threading.Tasks;

namespace ActivitySignUp.Services
{
    public class CommentService : ICommentService
    {

        private ICommentRepository _repository;
        private ICommentValidators _validators;

        public CommentService(ICommentRepository repository, ICommentValidators validators)
        {
            _repository = repository;
            _validators = validators;
        }


        public async Task<ServiceResult<int>> InsertCommentAsync(CommentInsertModel model)
        {
            try
            {
                // validate
                var validationResult = _validators.ValidateInsertModel(model);

                return validationResult.IsValid ?
                     new ServiceResult<int>(await _repository.InsertCommentAsync(model)) :
                     new ServiceResult<int>(validationResult.ValidationErrors);
            }
            catch (Exception x)
            {
                return new ServiceResult<int>(new ValidationError() { FieldName = "InsertCommentAsync", ErrorDetail = x.Message });
            }

        }


    }
}

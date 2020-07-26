using ActivitySignUp.Models;
using ActivitySignUp.Models.Comment;
using ActivitySignUp.Repositories.Interfaces;
using ActivitySignUp.Services.Interfaces;
using ActivitySignUp.Validation.Interfaces;
using Dapper.AmbientContext;
using System;
using System.Threading.Tasks;

namespace ActivitySignUp.Services
{
    public class CommentService : BaseService, ICommentService
    {

        private ICommentRepository _repository;
        private ICommentValidators _validators;

        public CommentService(
            IAmbientDbContextFactory factory,
            ICommentRepository repository,
            ICommentValidators validators)
            : base(factory)
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

                using (var dbScope = ContextFactory.Create())
                {
                    var commentId = await _repository.InsertCommentAsync(model);
                    dbScope.Commit();
                    return validationResult.IsValid ?
                         new ServiceResult<int>(commentId) :
                         new ServiceResult<int>(validationResult.ValidationErrors);
                }
            }
            catch (Exception x)
            {
                return new ServiceResult<int>(new ValidationError() { FieldName = "InsertCommentAsync", ErrorDetail = x.Message });
            }

        }


    }
}

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
    /// <summary>
    /// this service class handles all methods for Comments
    /// </summary>
    public class CommentService : BaseService, ICommentService
    {

        private ICommentRepository _repository;
        private ICommentValidators _validators;

        /// <summary>
        /// basic ctor
        /// </summary>
        /// <param name="factory">the ambient context factory</param>
        /// <param name="repository">the comment repository</param>
        /// <param name="validators">the comment validator</param>
        public CommentService(
            IAmbientDbContextFactory factory,
            ICommentRepository repository,
            ICommentValidators validators)
            : base(factory)
        {
            _repository = repository;
            _validators = validators;
        }

        /// <summary>
        /// this method inserts a new comment record into the Comment db Table
        /// </summary>
        /// <param name="model">the comment model containing the details of the new comment</param>
        /// <returns>the Id value of the new comment</returns>
        public async Task<ServiceResult<int>> InsertCommentAsync(CommentInsertModel model)
        {
            try
            {
                // validate
                var validationResult = _validators.ValidateInsertModel(model);

                using (var dbScope = ContextFactory.Create())
                {
                    var commentId = await _repository.InsertCommentAsync(model);
                    dbScope.Transaction.Commit();
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

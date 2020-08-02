using ActivitySignUp.Models;
using ActivitySignUp.Models.Comment;
using ActivitySignUp.Validation.Interfaces;

namespace ActivitySignUp.Validation
{
    /// <summary>
    /// this validator class checks the comment model for violations of data requirements
    /// </summary>
    public class CommentValidators : ICommentValidators
    {

        /// <summary>
        /// this method validates the comment model on insert
        /// </summary>
        /// <param name="model">the model containing the details of the comment</param>
        /// <returns>a ValidationResults model</returns>
        public ValidationResults ValidateInsertModel(CommentInsertModel model)
        {
            var returnValue = new ValidationResults();
            if (model.CommentContent.Length > 250)
            {
                returnValue.ValidationErrors.Add(new ValidationError() { FieldName = "CommentContent", ErrorDetail = "The content is too long." });
            }

            return returnValue;

        }
    }
}

using ActivitySignUp.Models;
using ActivitySignUp.Models.Comment;

namespace ActivitySignUp.Validation
{
    public class CommentValidators
    {

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

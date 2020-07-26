using ActivitySignUp.Models;
using ActivitySignUp.Models.Comment;

namespace ActivitySignUp.Validation.Interfaces
{
    public interface ICommentValidators
    {
        ValidationResults ValidateInsertModel(CommentInsertModel model);
    }
}

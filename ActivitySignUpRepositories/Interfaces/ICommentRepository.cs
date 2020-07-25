using ActivitySignUp.Models.Comment;
using System.Threading.Tasks;

namespace ActivitySignUp.Repositories.Interfaces
{
    public interface ICommentRepository
    {
        Task<int> InsertCommentAsync(CommentInsertModel model);

    }
}

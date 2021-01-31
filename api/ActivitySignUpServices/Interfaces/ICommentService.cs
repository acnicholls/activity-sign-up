using ActivitySignUp.Models;
using ActivitySignUp.Models.Comment;
using System.Threading.Tasks;

namespace ActivitySignUp.Services.Interfaces
{
    public interface ICommentService
    {
        Task<ServiceResult<int>> InsertCommentAsync(CommentInsertModel model);
    }
}

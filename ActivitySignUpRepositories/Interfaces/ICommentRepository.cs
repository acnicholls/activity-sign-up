using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ActivitySignUp.Models.Comment;

namespace ActivitySignUp.Repositories.Interfaces
{
    public interface ICommentRepository
    {
        Task<int> InsertComment(string content, int personId, int activityId);

        Task<List<CommentListModel>> GetActivityComments(int activityId);
    }
}

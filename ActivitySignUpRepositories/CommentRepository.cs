using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ActivitySignUp.Models.Comment;
using ActivitySignUp.Repositories.Interfaces;

namespace ActivitySignUp.Repositories
{
    public class CommentRepository : BaseRepository, ICommentRepository
    {

        public CommentRepository(IDbConnectionFactory connectionFactory) : base(connectionFactory)
        { }

        public Task<List<CommentListModel>> GetActivityComments(int activityId)
        {
            throw new NotImplementedException();
        }

        public Task<int> InsertComment(string content, int personId, int activityId)
        {
            throw new NotImplementedException();
        }
    }
}

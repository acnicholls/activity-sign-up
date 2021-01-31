using ActivitySignUp.Constants;
using ActivitySignUp.Models.Comment;
using ActivitySignUp.Repositories.Interfaces;
using Dapper;
using Dapper.AmbientContext;
using System.Data;
using System.Threading.Tasks;

namespace ActivitySignUp.Repositories
{
    /// <summary>
    /// this class handles the comments table
    /// </summary>
    public class CommentRepository : BaseRepository, ICommentRepository
    {

        /// <summary>
        /// basic ctor
        /// </summary>
        /// <param name="ambientDbContextLocator">context locator</param>
        public CommentRepository(IAmbientDbContextLocator ambientDbContextLocator) : base(ambientDbContextLocator)
        { }

        /// <summary>
        /// this method inserts a new record into the comments table
        /// </summary>
        /// <param name="model">this is the data model containing the field values</param>
        /// <returns>the Id of the new record</returns>
        public async Task<int> InsertCommentAsync(CommentInsertModel model)
        {
            var parameters = new DynamicParameters(model);
            parameters.Add("NewId", DbType.Int32, direction: ParameterDirection.Output);

            await DbContext.ExecuteAsync(StoredProcedures.CommentInsert, parameters, commandType: CommandType.StoredProcedure);

            return parameters.Get<int>("NewId");
        }
    }
}

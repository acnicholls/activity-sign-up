using ActivitySignUp.Models.Comment;
using ActivitySignUp.Repositories;
using ActivitySignUp.Repositories.Interfaces;
using Dapper.AmbientContext;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Data;
using System.Threading.Tasks;

namespace ActivitySignUp.RespositoryTests
{
    [TestClass]
    public class CommentRepositoryTests : BaseTest
    {

        protected readonly ICommentRepository _repository;

        public CommentRepositoryTests()
        {
            _repository = new CommentRepository(new AmbientDbContextLocator());
        }


        [TestMethod]
        public async Task InsertTestCommentAsync()
        {
            // arrange
            CommentModel retrieved;
            CommentInsertModel inserted;
            using (var contextScope = ContextFactory.Create())
            {
                var activity = await GetTestActivityAsync();
                var person = await GetTestPersonAsync(activityId: activity.ActivityId);

                inserted = new CommentInsertModel()
                {
                    CommentActivityId = activity.ActivityId,
                    CommentPersonId = person.PersonId,
                    CommentContent = "Here's a comment from the test method."
                };

                // act
                var newId = await _repository.InsertCommentAsync(inserted);

                retrieved = await DbContext.QuerySingleOrDefaultAsync<CommentModel>($"Select * from Comment where CommentId = {newId}", commandType: CommandType.Text);

            }

            // assert
            Assert.IsTrue(inserted.CommentContent == retrieved.CommentContent, "Comment Content is not equal");
            Assert.IsTrue(inserted.CommentPersonId == retrieved.CommentPersonId, "Comment PersonId is not equal");
            Assert.IsTrue(inserted.CommentActivityId == retrieved.CommentActivityId, "Comment ActivityId is not equal");
        }
    }
}

using Microsoft.VisualStudio.TestTools.UnitTesting;
using ActivitySignUp.Models.Comment;
using ActivitySignUp.Repositories.Interfaces;
using ActivitySignUp.Repositories;

namespace ActivitySignUp.RespositoryTests
{
    [TestClass]
    public class CommentRepositoryTests : BaseTest
    {

        protected readonly ICommentRepository _repository;

        public CommentRepositoryTests()
        {
            _repository = new CommentRepository(DbConnectionFactory);
        }


        [TestMethod]
        public void InsertCommentTest()
        {



            CommentInsertModel model = new CommentInsertModel
            {

            }
        }


    }
}

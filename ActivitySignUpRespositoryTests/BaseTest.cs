using ActivitySignUp.Models.Activity;
using ActivitySignUp.Models.Comment;
using ActivitySignUp.Models.Person;
using ActivitySignUp.Repositories;
using Dapper.AmbientContext;
using Dapper.AmbientContext.Storage;
using Microsoft.Extensions.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Data;
using System.Threading;
using System.Threading.Tasks;

namespace ActivitySignUp.RespositoryTests
{
    [TestClass]
    public class BaseTest 
    {
        protected static TestContext Context;

        protected static IConfiguration Configuration;
        protected static IAmbientDbContextQueryProxy DbContext;
        protected static IAmbientDbContextFactory ContextFactory;
        protected static IAmbientDbContextLocator ContextLocator;

        public static IConfigurationRoot GetIConfigurationRoot(string outputPath)
        {
            return new ConfigurationBuilder()
                .SetBasePath(outputPath)
                .AddJsonFile("appsettings.json", optional: true)
                .AddUserSecrets("b59d811a-504b-41cd-a801-6cc22fcd2e2a", reloadOnChange: true)
                .Build();
        }

        [AssemblyInitialize]
        public static void AssemblyInit(TestContext testContext)
        {

            Configuration = GetIConfigurationRoot(testContext.TestRunDirectory);

            AmbientDbContextStorageProvider.SetStorage(new AsyncLocalContextStorage());

            ContextFactory = new AmbientDbContextFactory(new DbConnectionFactory(Configuration));

            ContextFactory.Create();

            ContextLocator = new AmbientDbContextLocator();

            DbContext = ContextLocator.Get();

        }


        private readonly Mutex testMutex = new Mutex(true, "RepositoryTestMutex");

        [TestInitialize]
        public void Init()
        {
            testMutex.WaitOne(TimeSpan.FromSeconds(1));
        }

        [TestCleanup]
        public void CleanUp()
        {
            testMutex.ReleaseMutex();
        }

        public async Task<ActivityModel> GetTestActivityAsync(
            string name = "TestActivity",
            string description = "This is a test activity, it will be fun, I promise.",
            DateTime date = default(DateTime),
            string image = "/images/defaultImage.jpg"
            )
        {

            ActivityInsertModel insertModel = new ActivityInsertModel
            {
                ActivityDateTime = date,
                ActivityDescription = description,
                ActivityImage = image,
                ActivityName = name
            };

            return await InsertTestActivityAsync(insertModel);

        }


        public async Task<ActivityModel> InsertTestActivityAsync(ActivityInsertModel model)
        {
            var sql = @$"
declare @NewID int;

INSERT INTO dbo.Activity (
    ActivityName,
    ActivityDescription,
    ActivityDateTime,
    ActivityImage)
VALUES (
    '{model.ActivityName.Replace("'","''")}',
    '{model.ActivityDescription.Replace("'","''")}',
    {model.ActivityDateTime.ToShortDateString()},
    '{model.ActivityImage}'
);

set @NewId = (SELECT SCOPE_IDENTITY());

SELECT
    ActivityId,
    ActivityName,
    ActivityDescription,
    ActivityDateTime,
    ActivityImage
FROM
    dbo.Activity
WHERE
    ActivityId = @newId;
";

            return await DbContext.QuerySingleOrDefaultAsync<ActivityModel>(sql, commandType: CommandType.Text);

        }


        public async Task<PersonModel> GetTestPersonAsync(
            string first = "FirstName",
            string last = "LastName",
            string email = "fake@email.com",
            int? activityId = null
            )
        {
            if (activityId == null)
            {
                activityId = (await GetTestActivityAsync()).ActivityId;
            }


            PersonInsertModel insertModel = new PersonInsertModel
            {
                PersonFirstName = first,
                PersonLastName = last,
                PersonEmail = email,
                PersonActivityId = activityId.Value
            };

            return await InsertTestPersonAsync(insertModel);

        }


        public async Task<PersonModel> InsertTestPersonAsync(PersonInsertModel model)
        {
            var sql = @$"
declare @NewID int;

INSERT INTO dbo.Person (
    PersonFirstName,
    PersonLastName,
    PersonEmail,
    PersonActivityId)
VALUES (
    '{model.PersonFirstName.Replace("'","''")}',
    '{model.PersonLastName.Replace("'","''")}',
    '{model.PersonEmail}',
    {model.PersonActivityId}
);

set @NewId = (SELECT SCOPE_IDENTITY());

SELECT
    PersonId,
    PersonFirstName,
    PersonLastName,
    PersonEmail,
    PersonActivityId
FROM
    dbo.Person
WHERE
    PersonId = @newId;
";

            return await DbContext.QuerySingleOrDefaultAsync<PersonModel>(sql, commandType: CommandType.Text);

        }

        public async Task<CommentModel> GetTestCommentAsync(
            int? personId = null,
            int? activityId = null,
            string content = "Hey all, can't wait to start this awesome adventure!"
            )
        {

            if (activityId == null)
            {
                activityId = (await GetTestActivityAsync()).ActivityId;
            }


            if (personId == null)
            {
                personId = (await GetTestPersonAsync(activityId: activityId)).PersonId;
            }

            var model = new CommentInsertModel()
            {
                CommentActivityId = activityId.Value,
                CommentPersonId = personId.Value,
                CommentContent = content
            };

            return await InsertTestCommentAsync(model);

        }


        public async Task<CommentModel> InsertTestCommentAsync(CommentInsertModel model)
        {
            var sql = @$"
declare @NewID int;

INSERT INTO dbo.Comment (
    CommentPersonId,
    CommentActivityId,
    CommentContent,
    CommentDateTime)
VALUES (
    {model.CommentPersonId},
    {model.CommentActivityId},
    '{model.CommentContent.Replace("'","''")}',
    GETDATE()
);

set @NewId = (SELECT SCOPE_IDENTITY());

SELECT
    CommentId,
    CommentActivityId,
    CommentPersonId,
    CommentContent,
    CommentDateTime
FROM
    dbo.Comment
WHERE
    CommentId = @newId;
";

            return await DbContext.QuerySingleOrDefaultAsync<CommentModel>(sql, commandType: CommandType.Text);

        }


    }
}

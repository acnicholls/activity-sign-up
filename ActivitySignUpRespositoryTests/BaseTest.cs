using ActivitySignUp.Models.Activity;
using ActivitySignUp.Models.Person;
using ActivitySignUp.Models.Comment;
using ActivitySignUp.Repositories;
using ActivitySignUp.Repositories.Interfaces;
using Dapper;
using Microsoft.Extensions.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Data;
using System.Threading.Tasks;

namespace ActivitySignUp.RespositoryTests
{
    [TestClass]
    public class BaseTest
    {
        protected static TestContext Context;

        protected static IConfiguration Configuration;
        protected static IDbConnection DbContext;
        protected static IDbConnectionFactory DbConnectionFactory;

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

            DbConnectionFactory = new DbConnectionFactory(Configuration);

            DbContext = DbConnectionFactory.CreateConnection();
        }


        public async Task<ActivityViewModel> GetTestActivity(
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


        public async Task<ActivityViewModel> InsertTestActivityAsync(ActivityInsertModel model)
        {
            var sql = @$"
declare @NewID int;

INSERT INTO dbo.Activity (
    ActivityName,
    ActivtyDescription,
    ActivityDate,
    ActivityImage)
VALUES (
    '{model.ActivityName}',
    '{model.ActivityDescription}',
    '{model.ActivityDateTime.ToShortDateString()}',
    '{model.ActivityImage}'
);

set @NewId = (SELECT SCOPE_IDENTITY());

SELECT
    ActivtyId,
    ActivityName,
    ActivtyDescription,
    ActivityDate,
    ActivityImage
FROM
    dbo.Activity
WHERE
    ActivityId = @newId;
";

            return await DbContext.QuerySingleOrDefaultAsync<ActivityViewModel>(sql, commandType: CommandType.Text);
        
        }


        public async Task<PersonModel> GetTestPerson(
            string first = "FirstName",
            string last = "LastName",
            string email = "fake@email.com",
            int? activityId = null
            )
        {
            if(activityId == null)
            {
                activityId = (await GetTestActivity()).ActivityId;
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
    '{model.PersonFirstName}',
    '{model.PersonLastName}',
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

    }
}

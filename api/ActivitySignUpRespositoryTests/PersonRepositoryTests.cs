using Microsoft.VisualStudio.TestTools.UnitTesting;
using ActivitySignUp.Models.Person;
using ActivitySignUp.Repositories.Interfaces;
using ActivitySignUp.Repositories;
using Dapper;
using System.Data;
using System.Threading.Tasks;
using Dapper.AmbientContext;

namespace ActivitySignUp.RespositoryTests
{
    [TestClass]
    public class PersonRepositoryTests : BaseTest
    {

        protected readonly IPersonRepository _repository;

        public PersonRepositoryTests()
        {
            _repository = new PersonRepository(new AmbientDbContextLocator());
        }


        [TestMethod]
        public async Task PersonInsertTestAsync()
        {
            // arrange
            PersonModel retrieved;
            PersonInsertModel inserted;

            using (var contextScope = ContextFactory.Create())
            {
                var activity = await GetTestActivityAsync();

                inserted = new PersonInsertModel()
                {
                    PersonActivityId = activity.ActivityId,
                    PersonFirstName = "TestFirstName",
                    PersonLastName = "TestLastName",
                    PersonEmail = "test@email.com"
                };

                // act
                var newId = await _repository.InsertPersonAsync(inserted);

                retrieved = await DbContext.QuerySingleOrDefaultAsync<PersonModel>($"Select * from Person where PersonId = {newId}", commandType: CommandType.Text);

            }

            // assert
            Assert.IsTrue(inserted.PersonActivityId == retrieved.PersonActivityId, "Person ActivityId is not equal");
            Assert.IsTrue(inserted.PersonFirstName == retrieved.PersonFirstName, "Person First Name is not equal");
            Assert.IsTrue(inserted.PersonLastName == retrieved.PersonLastName, "Person Last Name is not equal");
            Assert.IsTrue(inserted.PersonEmail == retrieved.PersonEmail, "Person Email is not equal");
        }


    }
}

using ActivitySignUp.Models.Activity;
using ActivitySignUp.Models.Comment;
using ActivitySignUp.Models.Person;
using ActivitySignUp.Repositories;
using ActivitySignUp.Repositories.Interfaces;
using Dapper.AmbientContext;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace ActivitySignUp.RespositoryTests
{
    [TestClass]
    public class ActivityRepositoryTests : BaseTest
    {

        protected readonly IActivityRepository _repository;

        public ActivityRepositoryTests()
        {
            _repository = new ActivityRepository(new AmbientDbContextLocator());
        }


        [TestMethod]
        public async Task InsertActivityTestAsync()
        {
            // arrange
            ActivityModel retrieved;

            var model = new ActivityInsertModel()
            {
                ActivityName = "First Test Activity",
                ActivityDescription = "This activity will be awesome!",
                ActivityDateTime = DateTime.Now.AddDays(40),
                ActivityImage = "/images/FirstActivity.jpg"
            };

            // act
            using (var contextScope = ContextFactory.Create())
            {
                var newId = await _repository.InsertActivityAsync(model, new byte[0]);

                retrieved = await DbContext.QuerySingleOrDefaultAsync<ActivityModel>($"Select * from Activity where ActivityId = {newId}", commandType: CommandType.Text);

            }

            // assert
            Assert.IsTrue(model.ActivityName == retrieved.ActivityName, "Activity Name is not equal");
            Assert.IsTrue(model.ActivityDescription == retrieved.ActivityDescription, "Activity Description is not equal");
            Assert.IsTrue(model.ActivityDateTime.ToString() == retrieved.ActivityDateTime.ToString(), "Activity DateTime is not equal");
            // the following test would need to change since the image has changed from being a varchar to a varbinary
            //Assert.IsTrue(model.ActivityImage == retrieved.ActivityImage, "Activity Image is not equal");
        }


        [TestMethod]
        public async Task CheckActivityExistsTestAsync()
        {
            // arrange
            bool retrieved;
            using (var contextScope = ContextFactory.Create())
            {

                var activity = await GetTestActivityAsync();

                // act
                retrieved = await _repository.CheckActivityExistsAsync(activity.ActivityName);
            }

            // assert
            Assert.IsTrue(retrieved, "Activity does not exist");
        }

        [TestMethod]
        public async Task GetInitialActivityViewTestAsync()
        {
            // arrange
            ActivityModel inserted;
            ActivityModel retrieved;

            using (var contextScope = ContextFactory.Create())
            {
                inserted = await GetTestActivityAsync();

                // act
                retrieved = await _repository.GetInitialActivityViewAsync(inserted.ActivityId);

            }

            // assert
            Assert.IsTrue(inserted.ActivityName == retrieved.ActivityName, "Activity Name is not equal");
            Assert.IsTrue(inserted.ActivityDescription == retrieved.ActivityDescription, "Activity Description is not equal");
            Assert.IsTrue(inserted.ActivityDateTime.ToString() == retrieved.ActivityDateTime.ToString(), "Activity DateTime is not equal");
            Assert.IsTrue(inserted.ActivityImage == retrieved.ActivityImage, "Activity Image is not equal");
        }

        [TestMethod]
        public async Task GetActivityListTestAsync()
        {
            // arrange
            List<ActivityModel> insertedList;
            List<ActivityListModel> retrieved;

            using (var contextScope = ContextFactory.Create())
            {
                insertedList = new List<ActivityModel>()
                {
                    await GetTestActivityAsync(name: "Activity1"),
                    await GetTestActivityAsync(name: "Activity2")
                };

                // act
                retrieved = await _repository.GetActivityListAsync();
            }

            // assert
            Assert.IsTrue(insertedList.Count == retrieved.Count, "The list counts are not equal");
            foreach (var listItem in retrieved)
            {
                var inserted = insertedList.First(x => x.ActivityId == listItem.ActivityId);
                Assert.IsTrue(inserted.ActivityName == listItem.ActivityName, "Activity Name is not equal");
                Assert.IsTrue(inserted.ActivityDateTime.ToString() == listItem.ActivityDateTime.ToString(), "Activity DateTime is not equal");
                Assert.IsTrue(inserted.ActivityImage == listItem.ActivityImage, "Activity Image is not equal");
            }
        }

        [TestMethod]
        public async Task GetSignedUpActivityViewTestAsync()
        {
            // arrange
            List<PersonModel> participantList;
            PersonModel person1;
            PersonModel person2;
            List<CommentModel> commentList;
            ActivityModel activity;

            ActivitySignedUpViewModel retrieved;

            using (var contextScope = ContextFactory.Create())
            {

                activity = await GetTestActivityAsync();

                person1 = await GetTestPersonAsync(
                                 first: "First1",
                                 last: "Last1",
                                 email: "single@email.com",
                                 activityId: activity.ActivityId
                                 );

                person2 = await GetTestPersonAsync(
                                 first: "First2",
                                 last: "Last2",
                                 email: "double@email.com",
                                 activityId: activity.ActivityId
                                 );

                participantList = new List<PersonModel>()
                {
                    person1,
                    person2
                };

                commentList = new List<CommentModel>()
                {
                    await GetTestCommentAsync(
                        personId:person1.PersonId,
                        activityId: activity.ActivityId,
                        content:"I might be a bit late...I hope not, but it's a long drive and a long day at work."
                        ),
                    await GetTestCommentAsync(
                        personId:person2.PersonId,
                        activityId: activity.ActivityId,
                        content:"I can't wait to try kayaking again, it's been so long."
                        )
                };

                // act
                retrieved = await _repository.GetSignedUpActivityViewAsync(activity.ActivityId);
            }

            // assert
            Assert.IsTrue(participantList.Count == retrieved.ParticipantList.Count, "The participant list counts are not equal");
            Assert.IsTrue(commentList.Count == retrieved.CommentList.Count, "The comment list counts are not equal");
            foreach (var person in participantList)
            {
                var listPerson = new PersonListModel()
                { 
                    PersonName = person.PersonFirstName + " " + person.PersonLastName 
                };
                Assert.IsTrue(retrieved.ParticipantList.Exists(x=>x.PersonName == listPerson.PersonName), "The person's name was not found in the list");
            }

            foreach(var comment in commentList)
            {

                var person = participantList.First<PersonModel>(x => x.PersonId == comment.CommentPersonId);

                var listComment = new CommentListModel()
                {
                    CommentContent = comment.CommentContent,
                    CommentDetail = person.PersonFirstName + " " + person.PersonLastName + " on " + comment.CommentDateTime.ToString("yyyy-mm-dd") + " at " + comment.CommentDateTime.ToString("HH:mm")
                };
                Assert.IsTrue(retrieved.CommentList.Exists(x=>x.CommentContent == listComment.CommentContent && x.CommentDetail == listComment.CommentDetail), "The comment was not found in the list");
            }
        }


    }
}

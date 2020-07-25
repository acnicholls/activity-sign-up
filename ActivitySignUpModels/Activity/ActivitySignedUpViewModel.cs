using ActivitySignUp.Models.Comment;
using ActivitySignUp.Models.Person;
using System.Collections.Generic;


namespace ActivitySignUp.Models.Activity
{
    public class ActivitySignedUpViewModel
    {
        public List<PersonListModel> ParticipantList { get; set; }

        public List<CommentListModel> CommentList { get; set; }
    }
}

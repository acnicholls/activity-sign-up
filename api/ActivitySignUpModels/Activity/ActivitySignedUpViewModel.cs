using ActivitySignUp.Models.Comment;
using ActivitySignUp.Models.Person;
using System;
using System.Collections.Generic;


namespace ActivitySignUp.Models.Activity
{
    public class ActivitySignedUpViewModel
    {
        public int ActivityId { get; set; }
        public string ActivityName { get; set; }
        public string ActivityDescription { get; set; }
        public DateTime ActivityDateTime { get; set; }
        public string ActivityImage { get; set; }
        public List<PersonListModel> ParticipantList { get; set; }

        public List<CommentListModel> CommentList { get; set; }
    }
}

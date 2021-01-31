using System;
using System.Collections.Generic;
using System.Text;

namespace ActivitySignUp.Models.Comment
{
    public class CommentModel
    {

        public int CommentId { get; set; }
        public int CommentPersonId { get; set; }
        public int CommentActivityId { get; set; }
        public string CommentContent { get; set; }
        public DateTime CommentDateTime { get; set; }
    }
}

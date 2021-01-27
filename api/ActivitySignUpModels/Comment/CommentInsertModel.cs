namespace ActivitySignUp.Models.Comment
{
    public class CommentInsertModel
    {
        public int CommentPersonId { get; set; }
        public int CommentActivityId { get; set; }
        public string CommentContent { get; set; }
    }
}

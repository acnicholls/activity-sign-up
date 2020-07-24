using System;
using System.Reflection.Metadata;

namespace ActivitySignUp.Constants
{
    public class StoredProcedures
    {
        public const string ActivityGetCommentList = "dbo.ActivityGetCommentList";
        public const string ActivityGetList = "dbo.ActivtyGetList";
        public const string ActivityGetPersonList = "dbo.ActivityGetPersonList";
        public const string ActivityInsert = "dbo.ActivityInsert";

        public const string CommentInsert = "dbo.CommentInsert";

        public const string PersonInsert = "dbo.PersonInsert";
    }
}

using System;
using System.Reflection.Metadata;

namespace ActivitySignUp.Constants
{
    public class StoredProcedures
    {
        public const string ActivityGetInitialView = "dbo.ActivityGetInitialView";
        public const string ActivityGetView = "dbo.ActivityGetView";
        //public const string ActivityGetCommentList = "dbo.ActivityGetCommentList";
        public const string ActivityGetList = "dbo.ActivtyGetList";
        //public const string ActivityGetPersonList = "dbo.ActivityGetPersonList";
        public const string ActivityInsert = "dbo.ActivityInsert";

        public const string CommentInsert = "dbo.CommentInsert";

        public const string PersonInsert = "dbo.PersonInsert";
    }
}

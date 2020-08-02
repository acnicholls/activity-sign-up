using System;
using System.Reflection.Metadata;

namespace ActivitySignUp.Constants
{
    /// <summary>
    /// stores names of stored procedures
    /// </summary>
    public class StoredProcedures
    {
        public const string ActivityExistsCheck = "dbo.ActivityExistsCheck";
        public const string ActivityGetInitialView = "dbo.ActivityGetInitialView";
        public const string ActivityGetView = "dbo.ActivityGetView";
        public const string ActivityGetList = "dbo.ActivityGetList";
        public const string ActivityInsert = "dbo.ActivityInsert";
        public const string PersonExistsInActivityCheck = "dbo.PersonExistsInActivityCheck";
        public const string CommentInsert = "dbo.CommentInsert";
        public const string PersonInsert = "dbo.PersonInsert";
    }
}

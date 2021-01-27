CREATE TABLE [dbo].[Comment]
(
	CommentId int primary key clustered identity,
	CommentPersonId int not null constraint FK_Comment_Person foreign key references Person (PersonId),
	CommentActivityId int not null constraint FK_Comment_Activity foreign key references Activity (ActivityId),
	CommentContent varchar(250) not null,
	CommentDateTime datetime not null
);	
GO

CREATE INDEX [IDX_Comment_Activity_CommentDate] ON [dbo].[Comment] ([CommentActivityId], [CommentDateTime] DESC)

GO


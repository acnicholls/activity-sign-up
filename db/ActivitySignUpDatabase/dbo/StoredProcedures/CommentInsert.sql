CREATE PROCEDURE [dbo].[CommentInsert]
	@CommentPersonId int,
	@CommentActivityId int,
	@CommentContent varchar(250),
	@NewId int output
AS
begin

	insert into Comment (CommentPersonId, CommentActivityId, CommentContent, CommentDateTime)
	values (@CommentPersonId, @CommentActivityId, @CommentContent, GETDATE());

	set @NewId = SCOPE_IDENTITY();
end

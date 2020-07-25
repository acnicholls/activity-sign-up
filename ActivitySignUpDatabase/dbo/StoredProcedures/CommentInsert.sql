CREATE PROCEDURE [dbo].[CommentInsert]
	@CommentPersonId int,
	@CommentActivityId int,
	@CommentContent varchar(250),
	@CommentDateTime datetime
AS
begin
	declare @NewId int;

	insert into Comment (CommmentPersonId, CommentActivityId, CommentContent, CommentDateTime)
	values (@CommentPersonId, @CommentActivityId, @CommentContent, @CommentDateTime);

	set @NewId = SCOPE_IDENTITY();
end

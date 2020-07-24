CREATE PROCEDURE [dbo].[CommentInsert]
	@CommentPersonId int,
	@CommentActivityId int,
	@CommentContent varchar(250),
	@CommentDateTime datetime
AS
begin
	insert into Comment (CommmentPersonId, CommentActivityId, CommentContent, CommentDateTime)
	values (@CommentPersonId, @CommentActivityId, @CommentContent, @CommentDateTime);
end

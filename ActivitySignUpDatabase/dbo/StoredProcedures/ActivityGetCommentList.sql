CREATE PROCEDURE [dbo].[ActivityGetCommentList]
	@ActivityId int
AS
begin
	SELECT 
		concat (
		p.PersonFirstName, ' ',
		p.PersonLastName, ' on ',
		format(c.CommentDateTime, 'yyyy-mm-dd'), ' at ',
		format(c.CommentDateTime, 'HH:mm') ) as CommentDetail,
		CommentContent
	from
		Comment c inner join Person p on c.CommmentPersonId = p.PersonId
	where
		CommentActivityId = @ActivityId
	order by
		c.CommentDateTime desc
end
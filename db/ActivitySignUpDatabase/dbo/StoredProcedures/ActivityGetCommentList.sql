CREATE PROCEDURE [dbo].[ActivityGetCommentList]
	@ActivityId int
AS
begin
	SELECT 
		concat (
		p.PersonFirstName, ' ',
		p.PersonLastName, ' on ',
		format(c.CommentDateTime at time zone 'Eastern Standard Time', 'yyyy-mm-dd'), ' at ',
		format(c.CommentDateTime at time zone 'Eastern Standard Time', 'HH:mm') ) as CommentDetail,
		CommentContent
	from
		Comment c inner join Person p on c.CommentPersonId = p.PersonId
	where
		CommentActivityId = @ActivityId
	order by
		c.CommentDateTime desc
end
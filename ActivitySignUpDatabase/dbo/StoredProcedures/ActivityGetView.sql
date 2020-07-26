CREATE PROCEDURE [dbo].[ActivityGetView]
	@ActivityId int
AS
begin

	Select
		concat (PersonFirstName, ' ', PersonLastName) as PersonName
	from
		Person
	where
		PersonActivityId = @ActivityId
	order by
		PersonId;


	SELECT 
		concat (
		p.PersonFirstName, ' ',
		p.PersonLastName, ' on ',
		format(c.CommentDateTime, 'yyyy-mm-dd'), ' at ',
		format(c.CommentDateTime, 'HH:mm') ) as CommentDetail,
		CommentContent
	from
		Comment c inner join Person p on c.CommentPersonId = p.PersonId
	where
		CommentActivityId = @ActivityId
	order by
		c.CommentDateTime desc;

end

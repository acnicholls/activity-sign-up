CREATE PROCEDURE [dbo].[ActivityGetPersonList]
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
		PersonId

end
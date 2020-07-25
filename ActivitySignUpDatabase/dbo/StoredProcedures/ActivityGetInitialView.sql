CREATE PROCEDURE [dbo].[ActivityGetInitialView]
	@ActivityId int
AS
begin
	SELECT 
		a.ActivityId,
		a.ActivityName,
		a.ActivityDescription,
		a.ActivityDate,
		a.ActivityImage
	FROM
		Activity a
	WHERE
		a.ActivityId = @ActivityId;

end

CREATE PROCEDURE [dbo].[ActivityGetList]
AS
begin
	SELECT
		ActivityId,
		ActivityName,
		ActivityDateTime,
		ActivityImage
	from
		Activity
end

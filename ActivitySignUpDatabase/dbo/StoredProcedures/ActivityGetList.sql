CREATE PROCEDURE [dbo].[ActivityGetList]
AS
begin
	SELECT
		ActivityId,
		ActivityName,
		ActivityDate,
		ActivityImage
	from
		Activity
end

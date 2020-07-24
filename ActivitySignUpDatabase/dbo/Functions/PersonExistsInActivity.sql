CREATE FUNCTION [dbo].[PersonExistsInActivity]
(
	@PersonActivityId int, @PersonEmail varchar(120)
)
RETURNS bit
AS
BEGIN
	if exists (Select 1 from Person where PersonActivityId = @PersonActivityId and PersonEmail = @PersonEmail)
	begin
		return 1;
	end
	return 0;
END

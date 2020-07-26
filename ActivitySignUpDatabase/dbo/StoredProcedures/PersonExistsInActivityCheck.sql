CREATE Procedure [dbo].[PersonExistsInActivityCheck]
(
	@PersonActivityId int, @PersonEmail varchar(120),
	@Result bit output
)
AS
BEGIN
	if exists (Select 1 from Person where PersonActivityId = @PersonActivityId and PersonEmail = @PersonEmail)
	begin
		set @result =  1;
		return;
	end
	set @Result = 0;
END

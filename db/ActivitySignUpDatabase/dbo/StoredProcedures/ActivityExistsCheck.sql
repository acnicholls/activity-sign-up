CREATE Procedure [dbo].[ActivityExistsCheck]
(
	@ActivityName varchar(50),
	@Result bit output
)
AS
BEGIN
	if exists (Select 1 from Activity where ActivityName = @ActivityName)
	begin
		set @Result = 1;
		return;
	end
	set @result =  0;
END

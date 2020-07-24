CREATE FUNCTION [dbo].[ActivityExists]
(
	@ActivityName varchar(50)
)
RETURNS bit
AS
BEGIN
	if exists (Select 1 from Activity where ActivityName = @ActivityName)
	begin
		return 1;
	end
	return 0;
END

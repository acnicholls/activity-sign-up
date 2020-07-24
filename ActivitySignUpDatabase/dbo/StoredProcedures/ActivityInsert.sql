CREATE PROCEDURE [dbo].[ActivityInsert]
	@ActivityName varchar(50),
	@ActivityDescription varchar(250),
	@ActivityDate datetime,
	@ActivityImage varchar(100)
AS
begin
	insert into Activity (ActivityName, ActivityDescription, ActivityDate, ActivityImage)
	values (@ActivityName, @ActivityDescription, @ActivityDate, @ActivityImage);
end
go

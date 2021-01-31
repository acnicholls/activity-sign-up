CREATE PROCEDURE [dbo].[ActivityInsert]
	@ActivityName varchar(50),
	@ActivityDescription varchar(250),
	@ActivityDateTime datetime,
	@ActivityImage varchar(100),
	@NewId int output
AS
begin

	insert into Activity (ActivityName, ActivityDescription, ActivityDateTime, ActivityImage)
	values (@ActivityName, @ActivityDescription, @ActivityDateTime, @ActivityImage);

	set @NewId = SCOPE_IDENTITY();
end
go

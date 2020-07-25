CREATE PROCEDURE [dbo].[PersonInsert]
	@PersonFirstName varchar(50),
	@PersonLastName varchar(50),
	@PersonEmail varchar(120),
	@PersonActivityId int
AS
begin
	declare @NewId int;

	insert into Person (PersonFirstName, PersonLastName, PersonEmail, PersonActivityId)
	values (@PersonFirstName, @PersonLastName, @PersonEmail, @PersonActivityId);

	set @NewId = SCOPE_IDENTITY();
end
go
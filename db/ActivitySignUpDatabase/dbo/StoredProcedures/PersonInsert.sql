﻿CREATE PROCEDURE [dbo].[PersonInsert]
	@PersonFirstName varchar(50),
	@PersonLastName varchar(50),
	@PersonEmail varchar(120),
	@PersonActivityId int,
	@NewId int output
AS
begin

	insert into Person (PersonFirstName, PersonLastName, PersonEmail, PersonActivityId)
	values (@PersonFirstName, @PersonLastName, @PersonEmail, @PersonActivityId);

	set @NewId = SCOPE_IDENTITY();
end
go
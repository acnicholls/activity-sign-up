CREATE TABLE [dbo].[Person]
(
	PersonId int Primary key clustered identity,
	PersonFirstName varchar(50) not null,
	PersonLastName varchar(50) not null,
	PersonEmail varchar(120) not null,
	PersonActivityId int not null constraint FK_Person_Activity foreign key references Activity (ActivityId)
)

GO

CREATE UNIQUE INDEX [IDX_Person_Email_Activity] ON [dbo].[Person] ( [PersonActivityId] asc, [PersonEmail] asc)

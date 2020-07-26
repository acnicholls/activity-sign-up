CREATE TABLE [dbo].[Activity]
(
	ActivityId int PRIMARY KEY CLUSTERED IDENTITY,
	ActivityName  varchar(50) not null,
	ActivityDescription  varchar(250) not null,
	ActivityDateTime  datetime not null,
	ActivityImage  varchar(100) null
)

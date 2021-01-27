

use master
go

create database ActitivySignUp;
go

use ActivitySignUp;
go

create table Activity (
	ActivityId  identity(1,1) PRIMARY KEY CLUSTERED,
	ActivityName  varchar(50) not null,
	ActivityDescription  varchar(250) not null,
	ActivityDate  datetime not null,
	ActivityImage  varchar(100) null
);
go

create table Person (
	PersonId identity(1,1) Primary key clustered,
	PersonFirstName varchar(50) not null,
	PersonLastName varchar(50) not null,
	PersonEmail varchar(120) not null,
	PersonActivityId int not null,
	constraint FK_Person_Activity foreign key (PersonActivtyId) 
		references Activity (ActivityId)
);
go

create unique index IDX_Person_Email_Activity on Person (PersonEmail, PersonActivityId);
go

create table Comment (
	CommentId identity(1,1) primary key clustered,
	CommmentPersonId int not null,
	CommentActivityId int not null,
	CommentContent varchar(250) not null,
	CommentDateTime datetime not null,
	constraint FK_Comment_Person foreign key (CommentPersonId) references Person (PersonId),
	constraint FK_Comment_Activity foreign key (CommentActivityId) references Activty (ActivityId)
);
go

create index IDX_Comment_Activity on Comment (ActivityId);
go



use master;
go

CREATE DATABASE ActivitySignUpDatabase;
go

CREATE LOGIN [activity] WITH PASSWORD='4c1vIty[}';
go

use master;
go

RESTORE DATABASE [ActivitySignUpDatabase] FROM DISK='/tmp/ActivitySignUpDatabase.bak' 
WITH 
REPLACE, 
MOVE 'ActivitySignUpDatabase' TO '/var/opt/mssql/data/ActivitySignUpDatabase.mdf',
MOVE 'ActivitySignUpDatabase_log' TO '/var/opt/mssql/data/ActivitySignUpDatabase_log.ldf';
go

use ActivitySignUpDatabase;
go

CREATE USER [activity] FOR LOGIN [activity];
go

ALTER ROLE [db_owner] ADD MEMBER [activity];
go

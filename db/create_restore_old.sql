

use master;
go

CREATE DATABASE ActivitySignUpDatabase;
go

CREATE LOGIN [activity] WITH PASSWORD='4c1vIty';
go

use ActivitySignUpDatabase;
go

CREATE USER [activity] FOR LOGIN [activity];
go

ALTER ROLE [db_owner] ADD MEMBER [activity];
go

use master;
go

RESTORE DATABASE [ActivitySignUpDatabase] FROM DSIK='/tmp/activity.bak' 
WITH 
REPLACE, 
MOVE 'ActivitySignUpDatabase.mdf' TO '/var/opt/mssql/data/ActivitySignUpDatabase.mdf',
MOVE 'ActivitySignUpDatabase_Log.ldf' TO '/var/opt/mssql/data/ActivitySignUpDatabase.ldf';
go


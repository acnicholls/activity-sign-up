FROM mcr.microsoft.com/mssql/server

ENV ACCEPT_EULA=Y
ENV MSSQL_SA_PASSWORD=S3cureP4ssW0rd

# copy sql backup
COPY ./ActivitySignUpDatabase.bak /tmp/ActivitySignUpDatabase.bak

# copy the create and restore script
COPY ./create_restore.sql /tmp/create_restore.sql

RUN (/opt/mssql/bin/sqlservr & ) | grep -q "Service Broker manager has started" && \
/opt/mssql-tools/bin/sqlcmd -s tcp:localhost -U sa -P S3cureP4ssW0rd -c /tmp/create_restore.sql
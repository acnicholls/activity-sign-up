FROM mcr.microsoft.com/mssql/server

ENV ACCEPT_EULA=Y
ENV MSSQL_SA_PASSWORD=S3cureP4ssW0rd

# copy sql backup
COPY ./activity.bak /tmp/activity.bak

# copy the create and restore script
COPY ./create_restore.sql /tmp/create_restore.sql

RUN (/var/opt/mssql/bin/sqlservr & ) | grep -q "Service Broker has started" && \
/var/opt/mssql-tools/bin/sqlcmd -s localhost -u sa -p S3cureP4ssW0rd -c /tmp/create_restore.sql
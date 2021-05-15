FROM mcr.microsoft.com/dotnet/sdk:3.1 as build-deps

WORKDIR /app

# copy the solution file
COPY ./ActivitySignUp.sln ./ActivitySignUp.sln

COPY ./ActivitySignUpConstants/ActivitySignUpConstants.csproj ./ActivitySignUpConstants/
COPY ./ActivitySignUpModels/ActivitySignUpModels.csproj ./ActivitySignUpModels/
COPY ./ActivitySignUpRepositories/ActivitySignUpRepositories.csproj ./ActivitySignUpRepositories/
COPY ./ActivitySignUpRepositoryTests/ActivitySignUpRepositoryTests.csproj ./ActivitySignUpRepositoryTests/
COPY ./ActivitySignUpServices/ActivitySignUpServices.csproj ./ActivitySignUpServices/
COPY ./ActivitySignUpServiceTests/ActivitySignUpServiceTests.csproj ./ActivitySignUpServiceTests/
COPY ./ActivitySignUpValidation/ActivitySignUpValidation.csproj ./ActivitySignUpValidation/
COPY ./ActivitySignUpValidationTests/ActivitySignUpValidationTests.csproj ./ActivitySignUpValidationTests/
COPY ./ActivitySignUpApi/ActivitySignUpApi.csproj ./ActivitySignUpApi/
# restore nuget packages
RUN dotnet restore

# Constants
COPY ./ActivitySignUpConstants/StoredProcedures.cs ./ActivitySignUpConstants/

# Models
COPY ./ActivitySignUpModels ./ActivitySignUpModels/
COPY ./ActivitySignUpModels/Activity ./ActivitySignUpModels/Activity/
COPY ./ActivitySignUpModels/Comment ./ActivitySignUpModels/Comment/
COPY ./ActivitySignUpModels/Person ./ActivitySignUpModels/Person/

# Repositories
COPY ./ActivitySignUpRepositories ./ActivitySignUpRepositories/
COPY ./ActivitySignUpRepositories/Interfaces ./ActivitySignUpRepositories/Interfaces/

# Repository Tests
COPY ./ActivitySignUpRepositoryTests ./ActivitySignUpRepositoryTests/

# Services
COPY ./ActivitySignUpServices ./ActivitySignUpServices/
COPY ./ActivitySignUpServices/Interfaces ./ActivitySignUpServices/Interfaces/

# Validation
COPY ./ActivitySignUpValidation ./ActivitySignUpValidation/
COPY ./ActivitySignUpValidation/Interfaces ./ActivitySignUpValidation/Interfaces/

# Api Project
COPY ./ActivitySignUpApi/Program.cs ./ActivitySignUpApi/
COPY ./ActivitySignUpApi/Startup.cs ./ActivitySignUpApi/
COPY ./ActivitySignUpApi/appsettings.json ./ActivitySignUpApi/appsettings.json
COPY ./ActivitySignUpApi/appsettings.Production.json ./ActivitySignUpApi/appsettings.Production.json
COPY ./ActivitySignUpApi/Controllers ./ActivitySignUpApi/Controllers/

RUN dotnet publish -c Release -o out

FROM mcr.microsoft.com/dotnet/sdk:3.1 as final-build

WORKDIR /app

COPY --from=build-deps /app/out .

RUN ["mkdir", "/app/Resources"]
RUN ["mkdir", "/app/Resources/Images"]

ENV ASPNETCORE_ENVIRONMENT=Production
ENV ASPNETCORE_URLS=http://+:5000

ENTRYPOINT ["dotnet", "/app/ActivitySignUpApi.dll"]
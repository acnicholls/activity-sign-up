FROM mcr.microsoft.com/dotnet/sdk/aspnet:3.1 as build-deps

WORKDIR /app

# copy the solution file
COPY ./ActivitySignUp.sln ./ActivitySignUp.sln

# restore nuget packages
RUN dotnet restore

# Constants
COPY ./ActivitySignUpConstants/ActivitySignUpConstants.csproj ./ActivitySignUpConstants
COPY ./ActivitySignUpConstants/StoredProcedures.cs ./ActivitySignUpConstants

# Models
COPY ./ActivitySignUpModels/ActivitySignUpModels.csproj ./ActivitySignUpModels
COPY ./ActivitySignUpModels/ServiceError.cs ./ActivitySignUpModels
COPY ./ActivitySignUpModels/ServiceResult.cs ./ActivitySignUpModels
COPY ./ActivitySignUpModels/ValidationError.cs ./ActivitySignUpModels
COPY ./ActivitySignUpModels/ValidationResults.cs ./ActivitySignUpModels
COPY ./ActivitySignUpModels/Activity ./ActivitySignUpModels/Activity
COPY ./ActivitySignUpModels/Comment ./ActivitySignUpModels/Comment
COPY ./ActivitySignUpModels/Person ./ActivitySignUpModels/Person

# Repositories
COPY ./ActivitySignUpRepositories/ActivitySignUpRepositories.csproj ./ActivitySignUpRepositories
COPY ./ActivitySignUpRepositories/BaseRepository.cs ./ActivitySignUpRepositories
COPY ./ActivitySignUpRepositories/ActivityRepository.cs ./ActivitySignUpRepositories
COPY ./ActivitySignUpRepositories/CommentRepository.cs ./ActivitySignUpRepositories
COPY ./ActivitySignUpRepositories/PersonRepository.cs ./ActivitySignUpRepositories
COPY ./ActivitySignUpRepositories/DbConnectionFactory.cs ./ActivitySignUpRepositories
COPY ./ActivitySignUpRepositories/Interfaces ./ActivitySignUpRepositories/Interfaces

# Services
COPY ./ActivitySignUpServices/ActivitySignUpServices.csproj ./ActivitySignUpServices
COPY ./ActivitySignUpServices/BaseService.cs ./ActivitySignUpServices
COPY ./ActivitySignUpServices/PersonService.cs ./ActivitySignUpServices
COPY ./ActivitySignUpServices/CommentService.cs ./ActivitySignUpServices
COPY ./ActivitySignUpServices/ActivityService.cs ./ActivitySignUpServices
COPY ./ActivitySignUpServices/Interfaces ./ActivitySignUpServices/Interfaces

# Validation
COPY ./ActivitySignUpValidation/ActivitySignUpValidation.csproj ./ActivitySignUpValidation
COPY ./ActivitySignUpValidation/ActivityValidation.cs ./ActivitySignUpValidation
COPY ./ActivitySignUpValidation/PersonValidation.cs ./ActivitySignUpValidation
COPY ./ActivitySignUpValidation/CommentValidation.cs ./ActivitySignUpValidation
COPY ./ActivitySignUpValidation/Interfaces ./ActivitySignUpValidation/Interfaces

# Api Project
COPY ./ActivitySignUpValidation/ActivitySignUpApi.csproj ./ActivitySignUpApi
COPY ./ActivitySignUpValidation/Program.cs ./ActivitySignUpApi
COPY ./ActivitySignUpValidation/Startup.cs ./ActivitySignUpApi
COPY ./ActivitySignUpValidation/appsettings.json ./ActivitySignUpApi/appsettings.json
COPY ./ActivitySignUpValidation/Controllers ./ActivitySignUpApi/Controllers

RUN dotnet publish -c Release -o out

FROM mcr.micrsoft.com/dotnet/aspnet:3.1

WORKDIR /app

COPY --from=build-deps /app/out .

ENV ASPNETCORE_ENVIRONMENT=Production

CMD ["dotnet" "ActivtySignUp.dll"]
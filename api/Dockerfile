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
COPY ./ActivitySignUpModels/ServiceError.cs ./ActivitySignUpModels/
COPY ./ActivitySignUpModels/ServiceResult.cs ./ActivitySignUpModels/
COPY ./ActivitySignUpModels/ValidationError.cs ./ActivitySignUpModels/
COPY ./ActivitySignUpModels/ValidationResults.cs ./ActivitySignUpModels/
COPY ./ActivitySignUpModels/Activity ./ActivitySignUpModels/Activity/
COPY ./ActivitySignUpModels/Comment ./ActivitySignUpModels/Comment/
COPY ./ActivitySignUpModels/Person ./ActivitySignUpModels/Person/

# Repositories
COPY ./ActivitySignUpRepositories/BaseRepository.cs ./ActivitySignUpRepositories/
COPY ./ActivitySignUpRepositories/ActivityRepository.cs ./ActivitySignUpRepositories/
COPY ./ActivitySignUpRepositories/CommentRepository.cs ./ActivitySignUpRepositories/
COPY ./ActivitySignUpRepositories/PersonRepository.cs ./ActivitySignUpRepositories/
COPY ./ActivitySignUpRepositories/DbConnectionFactory.cs ./ActivitySignUpRepositories/
COPY ./ActivitySignUpRepositories/Interfaces ./ActivitySignUpRepositories/Interfaces/

# Repository Tests
COPY ./ActivitySignUpRepositoryTests ./ActivitySignUpRepositoryTests/

# Services
COPY ./ActivitySignUpServices/BaseService.cs ./ActivitySignUpServices/
COPY ./ActivitySignUpServices/PersonService.cs ./ActivitySignUpServices/
COPY ./ActivitySignUpServices/CommentService.cs ./ActivitySignUpServices/
COPY ./ActivitySignUpServices/ActivityService.cs ./ActivitySignUpServices/
COPY ./ActivitySignUpServices/Interfaces ./ActivitySignUpServices/Interfaces/

# Validation
COPY ./ActivitySignUpValidation/ActivityValidators.cs ./ActivitySignUpValidation/
COPY ./ActivitySignUpValidation/PersonValidators.cs ./ActivitySignUpValidation/
COPY ./ActivitySignUpValidation/CommentValidators.cs ./ActivitySignUpValidaton/
COPY ./ActivitySignUpValidation/Interfaces ./ActivitySignUpValidation/Interfaces/

# Api Project
COPY ./ActivitySignUpApi/Program.cs ./ActivitySignUpApi/
COPY ./ActivitySignUpApi/Startup.cs ./ActivitySignUpApi/
COPY ./ActivitySignUpApi/appsettings.json ./ActivitySignUpApi/appsettings.json
COPY ./ActivitySignUpApi/Controllers ./ActivitySignUpApi/Controllers/

RUN dotnet publish -c Release -o out

FROM mcr.microsoft.com/dotnet/aspnet:3.1

WORKDIR /app

COPY --from=build-deps /app/out .

ENV ASPNETCORE_ENVIRONMENT=Production

CMD ["dotnet" "ActivtySignUp.dll"]
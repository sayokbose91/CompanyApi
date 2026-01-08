# Build stage
FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src

# Copy solution and project files
COPY ["CompanyApi.sln", "./"]
COPY ["src/CompanyApi.Api/CompanyApi.Api.csproj", "src/CompanyApi.Api/"]
COPY ["src/CompanyApi.Application/CompanyApi.Application.csproj", "src/CompanyApi.Application/"]
COPY ["src/CompanyApi.Domain/CompanyApi.Domain.csproj", "src/CompanyApi.Domain/"]
COPY ["src/CompanyApi.Infrastructure/CompanyApi.Infrastructure.csproj", "src/CompanyApi.Infrastructure/"]

# Restore dependencies
RUN dotnet restore "CompanyApi.sln"

# Copy source code
COPY . .

# Build the application
WORKDIR "/src/src/CompanyApi.Api"
RUN dotnet build "CompanyApi.Api.csproj" -c Release -o /app/build

# Publish stage
FROM build AS publish
RUN dotnet publish "CompanyApi.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

# Runtime stage
FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS final
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "CompanyApi.Api.dll"]

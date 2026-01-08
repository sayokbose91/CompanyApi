# Company API - Clean Architecture with MongoDB

A production-ready .NET 10 Web API implementing Clean Architecture principles with MongoDB integration for managing company data.

## Features

- **Clean Architecture** - Domain, Application, Infrastructure, and API layers
- **CQRS Pattern** - MediatR for command and query separation
- **MongoDB Integration** - Full CRUD operations with MongoDB.Driver
- **Validation** - FluentValidation for request validation
- **OpenAPI Documentation** - Swagger/OpenAPI support
- **Health Checks** - MongoDB connection health monitoring
- **Docker Support** - Containerized deployment with Docker Compose
- **Comprehensive Testing** - Unit and integration tests with xUnit, Moq, and EphemeralMongo

## Technology Stack

- **.NET 10.0** - Latest .NET framework
- **ASP.NET Core Web API** - RESTful API framework
- **MongoDB.Driver 3.5.2** - MongoDB database driver
- **MediatR 14.0.0** - CQRS pattern implementation
- **FluentValidation 12.1.1** - Input validation
- **Riok.Mapperly 4.3.1** - Object mapping
- **xUnit** - Unit testing framework
- **Moq** - Mocking framework
- **FluentAssertions** - Fluent test assertions
- **EphemeralMongo** - In-memory MongoDB for integration testing

## Project Structure

```
CompanyApi/
├── src/
│   ├── CompanyApi.Domain/          # Domain entities and interfaces
│   │   ├── Common/                 # BaseEntity
│   │   ├── Entities/              # Company entity
│   │   └── Interfaces/            # IRepository
│   ├── CompanyApi.Application/     # Business logic layer
│   │   ├── Common/                # ICommand, IQuery interfaces
│   │   └── Companies/
│   │       ├── Commands/          # Create, Update, Delete commands
│   │       ├── Queries/           # Get, Search queries
│   │       └── DTOs/              # Data transfer objects
│   ├── CompanyApi.Infrastructure/  # Data access layer
│   │   ├── Persistence/           # MongoDB repository implementation
│   │   └── Health/                # Health checks
│   └── CompanyApi.Api/            # Web API layer
│       ├── Controllers/           # API controllers
│       └── Program.cs             # Application startup
├── tests/
│   └── CompanyApi.Tests/          # Unit and integration tests
├── Dockerfile                      # Docker image definition
└── docker-compose.yml             # Docker Compose configuration
```

## API Endpoints

### Company Management

| Method | Endpoint | Description |
|--------|----------|-------------|
| `POST` | `/api/companies` | Create a new company |
| `GET` | `/api/companies/{id}` | Get company by ID |
| `GET` | `/api/companies` | Get all companies (paginated) |
| `GET` | `/api/companies/search?query={term}` | Search companies |
| `PUT` | `/api/companies/{id}` | Update company (full) |
| `PATCH` | `/api/companies/{id}` | Update company (partial) |
| `DELETE` | `/api/companies/{id}` | Delete company |

### System

| Method | Endpoint | Description |
|--------|----------|-------------|
| `GET` | `/health` | Health check with MongoDB status |
| `GET` | `/openapi/v1.json` | OpenAPI specification |

## Company Entity

```csharp
{
  "id": "string",
  "name": "string (required, max 200 chars)",
  "description": "string (optional)",
  "industry": "string (optional)",
  "website": "string (optional, URL format)",
  "email": "string (optional, email format, unique)",
  "phone": "string (optional)",
  "address": {
    "street": "string",
    "city": "string",
    "state": "string",
    "zipCode": "string",
    "country": "string"
  },
  "employeeCount": "number (optional, >= 0)",
  "foundedDate": "datetime (optional)",
  "isActive": "boolean (default: true)",
  "tags": ["string"],
  "createdAt": "datetime",
  "updatedAt": "datetime (nullable)"
}
```

## Getting Started

### Prerequisites

- [.NET 10 SDK](https://dotnet.microsoft.com/download)
- [MongoDB](https://www.mongodb.com/try/download/community) (local or remote instance)
- [Docker](https://www.docker.com/get-started) (optional, for containerized deployment)

### Running Locally

1. **Clone the repository**
   ```bash
   git clone https://github.com/sayokbose91/CompanyApi.git
   cd CompanyApi
   ```

2. **Configure MongoDB connection**

   Edit `src/CompanyApi.Api/appsettings.json`:
   ```json
   {
     "MongoDbSettings": {
       "ConnectionString": "mongodb://localhost:27017",
       "DatabaseName": "CompanyDb",
       "CompaniesCollectionName": "companies"
     }
   }
   ```

3. **Restore dependencies**
   ```bash
   dotnet restore
   ```

4. **Build the solution**
   ```bash
   dotnet build
   ```

5. **Run tests**
   ```bash
   dotnet test
   ```

6. **Run the API**
   ```bash
   dotnet run --project src/CompanyApi.Api
   ```

7. **Access the API**
   - API: `https://localhost:5001` or `http://localhost:5000`
   - OpenAPI: `https://localhost:5001/openapi/v1.json`
   - Health Check: `https://localhost:5001/health`

### Running with Docker

1. **Build and run with Docker Compose**
   ```bash
   docker-compose up --build
   ```

2. **Access the API**
   - API: `http://localhost:5000` or `https://localhost:5001`
   - MongoDB: `mongodb://localhost:27017`

3. **Stop the containers**
   ```bash
   docker-compose down
   ```

## MongoDB Indexes

The following indexes are automatically created:

- **Text Index**: Name, Description, Industry (for text search)
- **Unique Index**: Email (sparse, allows multiple nulls)
- **Single Index**: IsActive (for filtering)
- **Compound Index**: Industry + IsActive (for optimized queries)

## Example Usage

### Create a Company

```bash
curl -X POST https://localhost:5001/api/companies \
  -H "Content-Type: application/json" \
  -d '{
    "name": "Acme Corporation",
    "description": "Leading technology company",
    "industry": "Technology",
    "website": "https://acme.com",
    "email": "contact@acme.com",
    "phone": "+1-555-0123",
    "address": {
      "street": "123 Tech Lane",
      "city": "San Francisco",
      "state": "CA",
      "zipCode": "94105",
      "country": "USA"
    },
    "employeeCount": 500,
    "tags": ["tech", "innovation"]
  }'
```

### Get All Companies (Paginated)

```bash
curl "https://localhost:5001/api/companies?pageNumber=1&pageSize=10&sortBy=name&isActive=true"
```

### Search Companies

```bash
curl "https://localhost:5001/api/companies/search?query=technology"
```

## Testing

The project includes comprehensive unit and integration tests:

- **Unit Tests**: Validators, Command/Query handlers
- **Integration Tests**: MongoDB repository operations with EphemeralMongo

Run all tests:
```bash
dotnet test
```

Run with coverage:
```bash
dotnet test --collect:"XPlat Code Coverage"
```

## Configuration

### appsettings.json

```json
{
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  },
  "AllowedHosts": "*",
  "MongoDbSettings": {
    "ConnectionString": "mongodb://localhost:27017",
    "DatabaseName": "CompanyDb",
    "CompaniesCollectionName": "companies"
  }
}
```

### Environment Variables (Docker)

```yaml
- ASPNETCORE_ENVIRONMENT=Development
- MongoDbSettings__ConnectionString=mongodb://mongodb:27017
- MongoDbSettings__DatabaseName=CompanyDb
- MongoDbSettings__CompaniesCollectionName=companies
```

## Architecture Highlights

### Clean Architecture Layers

1. **Domain Layer** - Core business entities and repository interfaces
2. **Application Layer** - Business logic, CQRS commands/queries, validators
3. **Infrastructure Layer** - Data access implementations, external services
4. **API Layer** - Controllers, middleware, startup configuration

### Design Patterns

- **CQRS** - Separate read and write operations
- **Repository Pattern** - Abstract data access
- **Dependency Injection** - Loose coupling between layers
- **Mediator Pattern** - Decoupled request handling

## Contributing

1. Fork the repository
2. Create a feature branch
3. Commit your changes
4. Push to the branch
5. Create a Pull Request

## License

This project is licensed under the MIT License.

## Contact

- **Repository**: https://github.com/sayokbose91/CompanyApi
- **Issues**: https://github.com/sayokbose91/CompanyApi/issues

---

Built with ❤️ using .NET 10 and Clean Architecture principles

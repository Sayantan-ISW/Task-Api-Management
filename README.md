# Task-Api-Management

A Task Management Web API built with .NET 8 following Clean Architecture principles.

## Project Structure

The solution follows Clean Architecture with clear separation of concerns across four layers:

```
src/
├── TaskApi.API/              # Presentation Layer
│   ├── Controllers/          # API Controllers
│   ├── Program.cs           # Application entry point with Swagger configuration
│   └── appsettings.json     # Configuration including SQL Server connection string
│
├── TaskApi.Application/      # Application Layer
│   ├── DTOs/                # Data Transfer Objects
│   ├── Interfaces/          # Application service interfaces
│   └── Services/            # Business logic services
│
├── TaskApi.Domain/          # Domain Layer (Core)
│   ├── Entities/            # Domain entities
│   └── Interfaces/          # Domain contracts (repositories, etc.)
│
└── TaskApi.Infrastructure/   # Infrastructure Layer
    ├── Data/                # Database context and configurations
    └── Repositories/        # Repository implementations
```

## Dependencies

- **API Layer** → depends on Application and Infrastructure
- **Application Layer** → depends on Domain
- **Infrastructure Layer** → depends on Application and Domain
- **Domain Layer** → no dependencies (core)

## Getting Started

### Prerequisites
- .NET 8 SDK or later
- SQL Server (optional, for database functionality)

### Build the Solution
```bash
dotnet build
```

### Run the API
```bash
cd src/TaskApi.API
dotnet run
```

The API will be available at:
- HTTP: `http://localhost:5000`
- HTTPS: `https://localhost:5001`
- Swagger UI: `https://localhost:5001/swagger`

## Configuration

Update the connection string in `src/TaskApi.API/appsettings.json`:
```json
"ConnectionStrings": {
  "DefaultConnection": "Server=YOUR_SERVER_NAME;Database=TaskManagementDB;User Id=YOUR_USERNAME;Password=YOUR_PASSWORD;TrustServerCertificate=True;"
}
```

## Features

- Clean Architecture structure
- Swagger/OpenAPI documentation
- RESTful API design
- Dependency injection ready
- Structured for scalability and maintainability

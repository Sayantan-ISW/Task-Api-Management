# Task-Api-Management

A Task Management Web API built with .NET 10 following Clean Architecture principles.

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
- .NET 10 SDK or later
- SQL Server (optional, for database functionality)

### Build the Solution
```bash
dotnet build
```

### Apply Database Migrations

Before running the API, apply the database migrations to create the schema:

```bash
cd src/TaskApi.API
dotnet ef database update --project ../TaskApi.Infrastructure --startup-project .
```

### Run the API
```bash
cd src/TaskApi.API
dotnet run
```

The API will be available at:
- HTTP: `http://localhost:5219`
- HTTPS: `https://localhost:7067`
- Swagger UI: `https://localhost:7067/swagger`

## Configuration

Update the connection string in `src/TaskApi.API/appsettings.json`:
```json
"ConnectionStrings": {
  "DefaultConnection": "Server=YOUR_SERVER_NAME;Database=TaskManagementDB;User Id=YOUR_USERNAME;Password=YOUR_PASSWORD;TrustServerCertificate=True;"
}
```

## Features

- **Clean Architecture** - Proper separation of concerns with Domain, Application, Infrastructure, and API layers
- **RESTful API** - Complete CRUD operations for task management
- **Entity Framework Core** - Database access with Code-First approach
- **SQL Server** - Relational database support
- **Swagger/OpenAPI** - Interactive API documentation
- **Dependency Injection** - Built-in .NET DI container
- **Validation** - Model validation using Data Annotations
- **Structured for scalability** - Easy to extend and maintain

## API Endpoints

| Method | Endpoint | Description |
|--------|----------|-------------|
| GET | `/api/tasks` | Get all tasks |
| GET | `/api/tasks/{id}` | Get a specific task |
| GET | `/api/tasks/completed` | Get all completed tasks |
| GET | `/api/tasks/pending` | Get all pending tasks |
| POST | `/api/tasks` | Create a new task |
| PUT | `/api/tasks/{id}` | Update a task |
| DELETE | `/api/tasks/{id}` | Delete a task |

## Database Schema

**Tasks Table:**
- `Id` - int (Primary Key, Auto-increment)
- `Title` - nvarchar(200) (Required)
- `Description` - nvarchar(1000) (Optional)
- `IsCompleted` - bit (Default: false)
- `CreatedAt` - datetime2 (Default: GETUTCDATE())
- `UpdatedAt` - datetime2 (Optional)

## Testing

See [TESTING.md](TESTING.md) for detailed testing instructions and API examples.

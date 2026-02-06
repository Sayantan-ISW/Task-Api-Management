# Testing the Task Management API

This document provides instructions for testing the Task Management API.

## Prerequisites

- .NET 8 SDK installed
- SQL Server (or SQL Server Express) installed and running
- Valid SQL Server connection string

## Configuration

1. Update the connection string in `src/TaskApi.API/appsettings.json`:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=localhost;Database=TaskManagementDB;Integrated Security=true;TrustServerCertificate=True;"
  }
}
```

For SQL Server with username/password:
```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=YOUR_SERVER;Database=TaskManagementDB;User Id=YOUR_USER;Password=YOUR_PASSWORD;TrustServerCertificate=True;"
  }
}
```

## Database Setup

1. Navigate to the API project:
```bash
cd src/TaskApi.API
```

2. Apply the database migrations:
```bash
dotnet ef database update --project ../TaskApi.Infrastructure --startup-project .
```

This will create the TaskManagementDB database and the Tasks table.

## Running the Application

1. From the API project directory:
```bash
dotnet run
```

2. The API will be available at:
   - HTTP: `http://localhost:5219`
   - HTTPS: `https://localhost:7067`
   - Swagger UI: `https://localhost:7067/swagger`

## Testing API Endpoints

### Using Swagger UI (Recommended)

1. Navigate to `https://localhost:7067/swagger`
2. Use the interactive UI to test all endpoints

### Using cURL

#### Get All Tasks
```bash
curl -X GET "https://localhost:7067/api/tasks" -k
```

#### Get Task by ID
```bash
curl -X GET "https://localhost:7067/api/tasks/1" -k
```

#### Get Completed Tasks
```bash
curl -X GET "https://localhost:7067/api/tasks/completed" -k
```

#### Get Pending Tasks
```bash
curl -X GET "https://localhost:7067/api/tasks/pending" -k
```

#### Create a New Task
```bash
curl -X POST "https://localhost:7067/api/tasks" \
  -H "Content-Type: application/json" \
  -d '{"title":"My First Task","description":"This is a test task"}' \
  -k
```

#### Update a Task
```bash
curl -X PUT "https://localhost:7067/api/tasks/1" \
  -H "Content-Type: application/json" \
  -d '{"title":"Updated Task","isCompleted":true}' \
  -k
```

#### Delete a Task
```bash
curl -X DELETE "https://localhost:7067/api/tasks/1" -k
```

## Expected Results

1. **Build**: Should complete with 0 errors and 0 warnings
2. **Database Migration**: Should create the Tasks table successfully
3. **API Startup**: Should start without errors and display listening ports
4. **GET /api/tasks**: Returns an empty array initially `[]`
5. **POST /api/tasks**: Returns the created task with auto-generated ID
6. **GET /api/tasks/{id}**: Returns the specific task or 404 if not found
7. **PUT /api/tasks/{id}**: Updates and returns the task or 404 if not found
8. **DELETE /api/tasks/{id}**: Returns 204 No Content on success or 404 if not found

## Troubleshooting

### Database Connection Issues

If you see SQL Server connection errors:
1. Verify SQL Server is running
2. Check the connection string in `appsettings.json`
3. Ensure the database user has appropriate permissions
4. For Integrated Security, ensure your Windows user has access

### Migration Issues

If migrations fail:
```bash
# Remove the last migration
dotnet ef migrations remove --project ../TaskApi.Infrastructure --startup-project .

# Recreate the migration
dotnet ef migrations add InitialCreate --project ../TaskApi.Infrastructure --startup-project .

# Apply the migration
dotnet ef database update --project ../TaskApi.Infrastructure --startup-project .
```

## Verification Checklist

- [ ] Solution builds successfully
- [ ] Database migrations run without errors
- [ ] Application starts and listens on expected ports
- [ ] Swagger UI is accessible
- [ ] Can create a new task
- [ ] Can retrieve all tasks
- [ ] Can retrieve a specific task by ID
- [ ] Can update a task
- [ ] Can delete a task
- [ ] Can filter completed tasks
- [ ] Can filter pending tasks

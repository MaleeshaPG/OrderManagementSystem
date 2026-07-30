# Order Management System

A .NET 8 ASP.NET Core Web API for managing departments, employees, items, suppliers, stores, and orders.

## Features

- JWT-based authentication and authorization
- Role-based access control for Admin, Manager, Employee, and User
- CRUD operations for core business entities
- Swagger/OpenAPI documentation
- SQLite database support by default
- Seeded admin account and roles

## Project Structure

- Controllers/: API endpoints
- Services/: business logic
- Repositories/: data access logic
- Models/: entity models
- DTOs/: request/response DTOs
- Data/: database context and seed initialization
- Middleware/: custom middleware
- OrderManagementSystem.Tests/: unit tests

## Prerequisites

- .NET 8 SDK
- Visual Studio 2022 or VS Code with C# support

## Getting Started

1. Clone the repository.
2. Restore dependencies:
   ```bash
   dotnet restore
   ```
3. Run the project:
   ```bash
   dotnet run --project OrderManagementSystem.csproj
   ```
4. Open Swagger UI at:
   ```text
   https://localhost:5001/swagger
   ```
   or, depending on your launch settings, the URL shown in the terminal.

## Database

The project uses SQLite by default through the connection string in appsettings.json.

On first run, the application will create the database files and seed:
- roles: Admin, Manager, Employee, User
- an initial admin account

Default admin credentials:
- Email: maleeshagunasekera@gmail.com
- Password: Admin@123456!

## Authentication

The API uses JWT bearer tokens.

### Login

Send a POST request to:
```http
POST /api/Auth/login
```

Example body:
```json
{
  "email": "maleeshagunasekera99@gmail.com",
  "password": "Admin@123456!"
}
```

Use the returned token in the Authorization header:
```http
Authorization: Bearer <token>
```

## Running Tests

Run the full test suite with:
```bash
dotnet test OrderManagementSystem.sln
```

If you want to run a filtered test, use:
```bash
dotnet test OrderManagementSystem.sln --filter "FullyQualifiedName~DepartmentServiceTests"
```

>If Visual Studio or the app is still running, the filtered test command may fail because the build output file is locked. Stop the running app or close the debugger and try again.

## Notes

- The app uses HTTPS redirection and CORS middleware.
- Security headers are enabled by default.
- You can change the JWT settings and database connection string in appsettings.json.

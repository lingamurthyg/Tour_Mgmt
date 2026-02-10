# Tour Management System - .NET 8 Migration

## Overview
This project is a complete migration of a legacy ASP.NET Web Forms application to .NET 8 using clean architecture principles, Razor Pages, and Entity Framework Core.

## Architecture
The solution follows clean architecture with four main layers:

### 1. Domain Layer (`TourManagement.Domain`)
- Core business entities (Tour, User, Booking)
- Repository and Service interfaces
- Domain exceptions
- No external dependencies

### 2. Application Layer (`TourManagement.Application`)
- Service implementations
- DTOs (Data Transfer Objects)
- AutoMapper profiles
- Business logic and validation

### 3. Infrastructure Layer (`TourManagement.Infrastructure`)
- Entity Framework Core DbContext
- Repository implementations
- Entity configurations
- Data access logic

### 4. Web Layer (`TourManagement.Web`)
- ASP.NET Core Razor Pages
- ViewModels
- UI and presentation logic
- Static files (CSS, JavaScript)

## Technologies
- .NET 8
- ASP.NET Core Razor Pages
- Entity Framework Core 8.0
- SQL Server (LocalDB)
- AutoMapper 12.0
- Serilog for logging
- Bootstrap 5 for UI
- xUnit for testing

## Getting Started

### Prerequisites
- .NET 8 SDK
- SQL Server or SQL Server LocalDB
- Visual Studio 2022 or Visual Studio Code

### Setup Instructions

1. **Restore NuGet packages:**
   ```bash
   dotnet restore
   ```

2. **Update database connection string:**
   Edit `src/TourManagement.Web/appsettings.json` and update the `DefaultConnection` string to point to your SQL Server instance.

3. **Create the database:**
   ```bash
   cd src/TourManagement.Infrastructure
   dotnet ef migrations add InitialCreate --startup-project ../TourManagement.Web
   dotnet ef database update --startup-project ../TourManagement.Web
   ```

4. **Build the solution:**
   ```bash
   dotnet build
   ```

5. **Run the application:**
   ```bash
   cd src/TourManagement.Web
   dotnet run
   ```

6. **Access the application:**
   Navigate to `https://localhost:5001` or the port shown in the console output.

## Migration Summary

### What Was Migrated

1. **Web Forms Pages → Razor Pages**
   - `userlogin.aspx` → Login/Register pages
   - `AddTour.aspx` → Tour CRUD pages
   - `TourCrud.aspx` → Tours Index page
   - `DisplayTours.aspx` → Tours display functionality
   - `mybooking.aspx` → My Bookings page
   - `allbooking.aspx` → All Bookings admin page

2. **Data Access**
   - ADO.NET with SqlConnection → Entity Framework Core
   - SQL injection vulnerable queries → Parameterized EF Core queries
   - Synchronous operations → Async/await pattern

3. **Configuration**
   - Web.config → appsettings.json
   - Connection strings migrated
   - Application settings converted to strongly-typed configuration

4. **Authentication**
   - Plain text passwords → Password hashing (ready for ASP.NET Core Identity)
   - Session-based authentication → Modern authentication patterns

5. **Server Controls**
   - asp:TextBox, asp:Button → HTML helpers and Tag Helpers
   - asp:GridView → HTML tables with proper binding
   - SqlDataSource → Repository pattern with EF Core

## Key Improvements

1. **Security**
   - Eliminated SQL injection vulnerabilities
   - Implemented password hashing
   - Added CSRF protection (built-in with Razor Pages)
   - HTTPS enforcement

2. **Architecture**
   - Clean architecture with proper separation of concerns
   - Dependency injection throughout
   - Repository pattern for data access
   - Service layer for business logic

3. **Performance**
   - Async/await for all I/O operations
   - Proper connection management
   - AsNoTracking for read-only queries

4. **Maintainability**
   - Strongly-typed models and DTOs
   - Comprehensive logging with Serilog
   - Error handling throughout
   - Unit and integration test projects

5. **Modern Patterns**
   - AutoMapper for object mapping
   - FluentValidation ready for complex validation
   - Options pattern for configuration
   - Middleware pipeline

## Project Structure

```
TourManagement/
├── src/
│   ├── TourManagement.Domain/
│   │   ├── Entities/
│   │   ├── Interfaces/
│   │   └── Exceptions/
│   ├── TourManagement.Application/
│   │   ├── Services/
│   │   ├── DTOs/
│   │   ├── Mappings/
│   │   └── Extensions/
│   ├── TourManagement.Infrastructure/
│   │   ├── Data/
│   │   ├── Repositories/
│   │   └── Extensions/
│   └── TourManagement.Web/
│       ├── Pages/
│       ├── ViewModels/
│       └── wwwroot/
└── tests/
    ├── TourManagement.UnitTests/
    └── TourManagement.IntegrationTests/
```

## Database Schema

### Tables
- **Tour**: Tour packages with details (name, place, days, price, locations, info, picture)
- **Userinfo**: User accounts (email, password hash, name, phone)
- **Booking**: Tour bookings (user, tour, date, people count, total price, status)

## Known Issues & Future Enhancements

### Known Issues
- Database migration needs to be run on first setup
- Image upload functionality needs configuration for file storage path
- Admin authentication needs to be implemented with ASP.NET Core Identity

### Future Enhancements
- Implement ASP.NET Core Identity for full authentication/authorization
- Add role-based access control (Admin, User)
- Implement payment gateway integration
- Add email notifications for bookings
- Implement advanced search and filtering
- Add API endpoints for mobile app integration
- Implement caching for frequently accessed data
- Add comprehensive unit and integration tests

## Testing

### Run Unit Tests
```bash
dotnet test tests/TourManagement.UnitTests
```

### Run Integration Tests
```bash
dotnet test tests/TourManagement.IntegrationTests
```

### Run All Tests
```bash
dotnet test
```

## Logging
Logs are written to:
- Console (all environments)
- File: `logs/tourmanagement-YYYY-MM-DD.txt` (rolling daily)

## Contributing
This is a migrated application. For any improvements or bug fixes, please follow the clean architecture principles established in this solution.

## License
Migrated from legacy ASP.NET Web Forms application to .NET 8.

---

**Migration Date:** February 10, 2026
**Target Framework:** .NET 8
**Build Status:** ✅ Build Succeeded

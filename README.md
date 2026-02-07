# NeuroLingo WebAPI

A .NET 10 Web API for language learning with flashcards, quizzes, and spaced repetition.

## Features

- **Authentication**: User registration, login, and logout using ASP.NET Core Identity
- **Flashcards**: Create and manage flashcard sets for language learning
- **Quizzes**: Test knowledge with quiz functionality  
- **Spaced Repetition**: Schedule reviews using spaced repetition algorithms
- **OpenAPI/Swagger**: Full API documentation via OpenAPI

## Tech Stack

- **.NET 10.0**: Latest .NET framework
- **ASP.NET Core Web API**: RESTful API architecture
- **Entity Framework Core**: ORM for database operations
- **SQLite**: Lightweight database
- **ASP.NET Core Identity**: Authentication and authorization
- **OpenAPI**: API documentation standard

## Getting Started

### Prerequisites

- [.NET 10 SDK](https://dotnet.microsoft.com/download/dotnet/10.0)

### Installation

1. Clone the repository:
```bash
git clone https://github.com/MikolajKocik/NeuroLingo.git
cd NeuroLingo
```

2. Restore dependencies:
```bash
dotnet restore
```

3. Build the project:
```bash
dotnet build
```

4. Run the API:
```bash
dotnet run --project NeuroLingo/NeuroLingo.csproj
```

The API will start on `http://localhost:5063` (HTTP) or `https://localhost:7090` (HTTPS).

## API Documentation

Once the application is running, access the OpenAPI specification at:
- `http://localhost:5063/openapi/v1.json`

### Available Endpoints

#### Authentication
- `POST /api/Auth/register` - Register a new user
  ```json
  {
    "email": "user@example.com",
    "password": "Password123"
  }
  ```

- `POST /api/Auth/login` - Login
  ```json
  {
    "email": "user@example.com",
    "password": "Password123"
  }
  ```

- `POST /api/Auth/logout` - Logout current user

## Project Structure

```
NeuroLingo/
├── Features/              # Feature-based organization
│   ├── Auth/             # Authentication
│   │   ├── Controllers/  # API controllers
│   │   ├── Dtos/         # Data transfer objects
│   │   ├── Models/       # Domain models
│   │   └── Services/     # Business logic
│   ├── Flashcards/       # Flashcard management
│   ├── Quizzes/          # Quiz functionality
│   └── RepeatSchedules/  # Spaced repetition
├── Persistence/          # Database context
│   └── Data/
├── Services/             # Shared services
│   └── EmailNotifications/
├── Exceptions/           # Custom exceptions
└── Program.cs            # Application entry point
```

## Configuration

### Database

The application uses SQLite by default. The connection string can be configured in `appsettings.json`:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Data Source=neurolingo.db"
  }
}
```

### Identity Settings

Password requirements and other identity settings are configured in `Program.cs`:
- Minimum length: 8 characters
- Requires digit: Yes
- Requires lowercase: Yes
- Requires uppercase: Yes  
- Requires special character: No

## Development

### Adding New Features

1. Create a new folder under `Features/`
2. Add Controllers, DTOs, Models, and Services as needed
3. Register services in `Program.cs`

### Database Migrations

When modifying models, create and apply migrations:

```bash
# Install EF Core tools (if not already installed)
dotnet tool install --global dotnet-ef

# Create a migration
dotnet ef migrations add YourMigrationName --project NeuroLingo

# Apply migrations
dotnet ef database update --project NeuroLingo
```

## License

This project is in progress.

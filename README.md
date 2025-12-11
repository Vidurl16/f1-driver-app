# F1 Driver Management Application

## Overview

This is a full-stack Formula 1 driver management application built with C# (.NET 8) for the backend and React with TypeScript for the frontend. The application provides complete CRUD functionality for managing F1 drivers with clean architecture principles.

## Quick Start (Docker)

The fastest way to run the application:

```bash
# Clone and navigate to the project
cd f1-driver-app

# Start the application
docker-compose up -d
```

Then open your browser to:
- **Frontend**: http://localhost
- **API**: http://localhost:5001
- **Swagger UI**: http://localhost:5001/swagger

To stop the application:
```bash
docker-compose down
```

## Technology Stack

### Backend
- ASP.NET Core Web API (.NET 8)
- Entity Framework Core 8.0
- SQLite Database
- Repository Pattern
- Dependency Injection

### Frontend
- React 18
- TypeScript 5
- Vite
- Fetch API for HTTP requests

## Prerequisites

### For Docker (Recommended)
- [Docker Desktop](https://www.docker.com/products/docker-desktop/)
- Docker Compose (included with Docker Desktop)

### For Local Development (Optional)
- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- [Node.js](https://nodejs.org/) (v18 or higher)
- [npm](https://www.npmjs.com/) (comes with Node.js)

## Project Structure

```
F1DriverApp/
├── F1DriverApp.Api/              # Backend API
│   ├── Controllers/              # API Controllers
│   ├── Data/                     # DbContext and seeding
│   ├── DTOs/                     # Data Transfer Objects
│   ├── Models/                   # Domain models
│   ├── Repositories/             # Data access layer
│   ├── Services/                 # Business logic layer
│   └── Program.cs                # Application entry point
└── f1-driver-frontend/           # Frontend React app
    ├── src/
    │   ├── components/           # React components
    │   ├── api.ts                # API service
    │   ├── types.ts              # TypeScript interfaces
    │   └── App.tsx               # Main application
    └── package.json
```

## Backend Setup and Run

### Step 1: Navigate to Backend Directory

```bash
cd F1DriverApp/F1DriverApp.Api
```

### Step 2: Restore Dependencies

```bash
dotnet restore
```

### Step 3: Run the Application

```bash
dotnet run --urls "http://localhost:5001"
```

**Note**: We use port 5001 because port 5000 is commonly used by macOS Control Center (AirPlay Receiver).

The API will start on:
- **HTTP**: http://localhost:5001
- **Swagger UI**: http://localhost:5001/swagger

The database will be automatically created and seeded with sample data on first run. The application uses Entity Framework's `EnsureCreated()` method to initialize the database schema.

## Frontend Setup and Run

### Step 1: Navigate to Frontend Directory

```bash
cd f1-driver-frontend
```

### Step 2: Install Dependencies

```bash
npm install
```

### Step 3: Run the Development Server

```bash
npm run dev
```

The React application will start on:
- **URL**: http://localhost:5173

**Note**: When running locally (not in Docker), the frontend will connect to the API at http://localhost:5001/api. When running in Docker, it uses the nginx proxy at /api.

## API Endpoints

The following REST endpoints are available:

| Method | Endpoint | Description |
|--------|----------|-------------|
| GET | `/api/drivers` | Get all drivers |
| GET | `/api/drivers/{id}` | Get driver by ID |
| POST | `/api/drivers` | Create new driver |
| PUT | `/api/drivers/{id}` | Update existing driver |
| DELETE | `/api/drivers/{id}` | Delete driver |

### Example Request Bodies

**Create Driver (POST /api/drivers)**
```json
{
  "name": "Charles Leclerc",
  "team": "Ferrari",
  "country": "Monaco",
  "championshipPoints": 308
}
```

**Update Driver (PUT /api/drivers/1)**
```json
{
  "name": "Max Verstappen",
  "team": "Red Bull Racing",
  "country": "Netherlands",
  "championshipPoints": 575
}
```

## Features

### Backend Features
- Clean architecture with separation of concerns
- Repository pattern for data access
- Service layer for business logic
- DTOs for API contracts
- Input validation with data annotations
- Asynchronous operations throughout
- Entity Framework Core with SQLite
- Automatic database initialization with `EnsureCreated()`
- Database seeding with sample data
- CORS enabled for frontend integration
- Proper HTTP status codes
- Swagger/OpenAPI documentation

### Frontend Features
- List all drivers in a table
- Add new drivers via form
- Edit existing drivers
- Delete drivers with confirmation
- Client-side validation
- Loading states
- Error handling
- Responsive layout
- TypeScript for type safety

## Database Schema

### Drivers Table
- `DriverId` (int, primary key)
- `Name` (string, required, max 100 chars)
- `Team` (string, required, max 100 chars)
- `Country` (string, required, max 100 chars)
- `ChampionshipPoints` (int, required)
- `TeamId` (int, nullable, foreign key)

### Teams Table
- `TeamId` (int, primary key)
- `Name` (string, required, max 100 chars)

The Teams table demonstrates relational modeling. Drivers can optionally reference a team entity.

## Architecture Notes

### Backend Architecture

The backend follows clean architecture principles with clear separation of concerns:

1. **Controllers Layer**: Handles HTTP requests/responses and routes to services
2. **Services Layer**: Contains business logic and orchestrates operations
3. **Repository Layer**: Manages data access and database operations
4. **Data Layer**: Contains DbContext and entity configurations
5. **Models/DTOs**: Define data structures for domain and API contracts

### Dependency Injection

All services and repositories are registered in `Program.cs` and injected via constructors, promoting loose coupling and testability.

### Validation

- Model validation uses data annotations in DTOs
- Server-side validation occurs automatically via `[ApiController]` attribute
- Client-side validation in React forms before API calls

## Sample Data

The application seeds the following sample drivers on first run:

1. **Max Verstappen**
   - Team: Red Bull Racing
   - Country: Netherlands
   - Points: 575

2. **Lewis Hamilton**
   - Team: Mercedes
   - Country: United Kingdom
   - Points: 234

## Development Notes

### Running Both Applications Locally

1. Open two terminal windows
2. In terminal 1: Start the backend (runs on port 5001)
   ```bash
   cd F1DriverApp.Api
   dotnet run --urls "http://localhost:5001"
   ```
3. In terminal 2: Start the frontend (runs on port 5173)
   ```bash
   cd f1-driver-frontend
   npm run dev
   ```
4. Access the application at http://localhost:5173

### CORS Configuration

The backend is configured to accept requests from:
- http://localhost:5173 (Vite default)
- http://localhost:3000 (Create React App default)

### Database Location

The SQLite database file `f1drivers.db` is created in the API project root directory.

### TypeScript Configuration

The frontend uses strict TypeScript mode with:
- Type checking enabled
- Unused variable detection
- ES2020 target
- React JSX transformation

## Troubleshooting

### Docker Issues

**Issue**: Port 5000 already in use (macOS)
- **Cause**: macOS Control Center uses port 5000 for AirPlay Receiver
- **Solution**: The docker-compose.yml is already configured to use port 5001. No action needed.

**Issue**: Database errors or "no such table"
```bash
# Solution: Remove volumes and restart
docker-compose down -v
docker-compose up --build
```

**Issue**: Containers not starting
```bash
# Check container logs
docker-compose logs api
docker-compose logs frontend
```

### Backend Issues (Local Development)

**Issue**: Port 5000 already in use
```bash
# Solution: Use port 5001 or another available port
dotnet run --urls "http://localhost:5001"
```

**Issue**: Database file locked or corrupted
```bash
# Solution: Delete the database file and restart
rm f1drivers.db
dotnet run
```

### Frontend Issues

**Issue**: API connection refused
- **Docker**: Ensure containers are running with `docker-compose ps`
- **Local**: Ensure backend is running on http://localhost:5001
- Check CORS configuration in backend Program.cs

**Issue**: TypeScript errors
```bash
# Solution: Reinstall dependencies
rm -rf node_modules package-lock.json
npm install
```

## Docker Deployment (Recommended)

The easiest way to run the entire application is using Docker.

### Prerequisites
- [Docker Desktop](https://www.docker.com/products/docker-desktop/)
- Docker Compose (included with Docker Desktop)

### Run with Docker Compose

```bash
# From the project root - first time or after code changes
docker-compose up --build

# Or run in detached mode (background)
docker-compose up -d
```

**Access Points:**
- **Frontend**: http://localhost (port 80)
- **API**: http://localhost:5001
- **Swagger UI**: http://localhost:5001/swagger

### Docker Architecture

The application uses a multi-container setup:

- **API Container**: .NET 8 runtime with SQLite database (exposed on port 5001)
- **Frontend Container**: Nginx serving built React app (exposed on port 80)
- **Network**: Bridge network for inter-container communication
- **Volume**: Persistent storage for database

The frontend uses Nginx as a reverse proxy, forwarding `/api` requests to the backend container.

### Important Notes

- **Port 5001**: Used instead of 5000 to avoid conflicts with macOS AirPlay Receiver
- **Database**: Automatically created and seeded on first run
- **API URL**: Frontend automatically detects if running in Docker or locally

## Future Enhancements

Potential improvements for the application:

- Authentication and authorization
- Search and filter functionality
- Pagination for driver list
- Sorting by column
- Team management CRUD
- Driver statistics and analytics
- Unit and integration tests
- CI/CD pipeline
- Kubernetes deploymentown
```

### View Logs

```bash
# All services
docker-compose logs -f

# Just API
docker-compose logs -f api

# Just frontend
docker-compose logs -f frontend
```

## Production Build (Without Docker)

### Backend
```bash
cd F1DriverApp.Api
dotnet publish -c Release -o ./publish
```

### Frontend
```bash
cd f1-driver-frontend
npm run build
# Output will be in /dist folder
```

## Future Enhancements

Potential improvements for the application:

- Authentication and authorization (JWT)
- Search and filter functionality
- Pagination for driver list
- Sorting by column
- Team management CRUD operations
- Driver statistics and analytics dashboard
- Unit and integration tests
- CI/CD pipeline with GitHub Actions
- Kubernetes deployment
- Redis caching layer
- API rate limiting

---

**Last Updated**: December 11, 2025

# Property Search Application

A full-stack property search application built with React frontend, .NET Web API backend, and SQL Server database. Users can search, view, and add properties with detailed space information.

### Core Functionality
- **Property Search**: Filter by type, price range, and location
- **Property Details**: View full property information with spaces
- **Add Properties**: Create new properties with multiple spaces
- **Responsive Design**: Mobile-first UI with Tailwind CSS

### Advanced Features
- **Real-time Filtering**: Instant search results as you type
- **Space Management**: Add/edit/remove spaces within properties
- **Form Validation**: Client-side validation with Formik + Yup
- **State Management**: React hooks for efficient state handling

### Frontend
- **React 18** - Modern React with hooks
- **React Router DOM** - Client-side routing
- **Tailwind CSS** - Utility-first CSS framework
- **Formik + Yup** - Form handling and validation
- **Axios** - HTTP client for API calls
- **Vite** - Fast build tool and dev server

### Backend
- **.NET 6 Web API** - RESTful API framework
- **Entity Framework Core** - ORM for database operations
- **Fluent Validation** - Server-side validation
- **AutoMapper** - Object mapping
- **Swagger/OpenAPI** - API documentation

### Database
- **SQL Server** - Relational database
- **Indexes** - Optimized for search queries
- **Stored Procedures** - Complex queries and aggregations

### Frontend Setup
```bash
# Navigate to frontend directory
cd Frontend

# Install dependencies
npm install

# Create environment file
cp .env.example .env

# Start development server
npm run dev
```

### Backend Setup
```bash
# Navigate to backend directory
cd Backend

# Restore NuGet packages
dotnet restore

# Update database connection string in appsettings.json
# Run database migrations
dotnet ef database update

# Start the API
dotnet run
```

### Database Setup
```sql
-- Run the database creation script
-- Seed with sample data
EXEC SeedSampleData;
```

## 🔧 Configuration

### Frontend Environment Variables
```bash
# .env file
VITE_API_URL= # Your backend url
```

### Backend Configuration
```json
// appsettings.json
{
  "ConnectionStrings": {
    "sqlcon": "Server=localhost;Database=PropertySearch;Trusted_Connection=true;"
  },
  "Cors": {
    "AllowedOrigins": ["http://localhost:5173", "http://localhost:3000"]
  }
}
```

 

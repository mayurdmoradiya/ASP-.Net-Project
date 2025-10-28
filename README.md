# Project Management System

A comprehensive Task/Project Management web application built with ASP.NET Core MVC.

## Features

### 📊 Dashboard
- Overview of all projects and tasks
- Statistics cards showing:
  - Total projects
  - Active projects
  - Total tasks
  - Overdue tasks
- Task status breakdown (Completed, In Progress, Pending)
- Upcoming tasks list
- Recent projects display

### 📁 Project Management
- Create, Read, Update, Delete (CRUD) operations for projects
- Project details with:
  - Name and description
  - Start and end dates
  - Status tracking (Planning, In Progress, On Hold, Completed, Cancelled)
  - Progress visualization
  - Task list
  - Team members

### ✅ Task Management
- Full CRUD operations for tasks
- Task properties:
  - Title and description
  - Priority levels (Low, Medium, High, Critical)
  - Status tracking (Todo, In Progress, Review, Completed, Blocked)
  - Due dates
  - Assignment to team members
- Filter tasks by status
- Visual priority and status badges

### 👥 Team Management
- Add team members to projects
- Assign roles (Project Manager, Developer, Designer, Tester, Analyst)
- Track member information (name, email, role)

## Technology Stack

- **Framework**: ASP.NET Core 9.0 MVC
- **Database**: SQLite (via Entity Framework Core)
- **UI Framework**: Bootstrap 5
- **Icons**: Font Awesome 6
- **ORM**: Entity Framework Core 9.0

## Project Structure

```
Project/
├── Controllers/          # MVC Controllers
│   ├── DashboardController.cs
│   ├── ProjectsController.cs
│   ├── TasksController.cs
│   ├── TeamMembersController.cs
│   └── HomeController.cs
├── Models/              # Data models
│   ├── Project.cs
│   ├── ProjectTask.cs
│   ├── TeamMember.cs
│   └── DashboardViewModel.cs
├── Views/               # Razor views
│   ├── Dashboard/
│   ├── Projects/
│   ├── Tasks/
│   ├── TeamMembers/
│   └── Shared/
├── Data/                # Database context
│   └── ApplicationDbContext.cs
└── wwwroot/             # Static files (CSS, JS, images)
```

## Getting Started

### Prerequisites

- .NET 9.0 SDK or later
- Visual Studio 2022 / VS Code / Rider (optional)

### Installation

1. **Clone or navigate to the project directory**
   ```bash
   cd "d:\AMTICS\Sem 5\ASP.NET\Project"
   ```

2. **Restore dependencies**
   ```bash
   dotnet restore
   ```

3. **Build the project**
   ```bash
   dotnet build
   ```

4. **Run the application**
   ```bash
   dotnet run
   ```

5. **Open your browser**
   - Navigate to: `https://localhost:5001` or `http://localhost:5000`
   - The application will redirect to the Dashboard

### Database

The application uses SQLite database (`ProjectManagement.db`) which is created automatically on first run with sample data:
- 1 sample project
- 2 sample tasks

## Usage

### Creating a New Project

1. Navigate to **Projects** from the navigation bar
2. Click **New Project** button
3. Fill in the project details:
   - Name (required)
   - Description
   - Start date (required)
   - End date (optional)
   - Status
4. Click **Create Project**

### Adding Tasks

1. Go to a project's detail page
2. Click **Add Task** button
3. Fill in task information:
   - Title (required)
   - Description
   - Project selection
   - Priority level
   - Status
   - Due date
   - Assigned to (optional)
4. Click **Create Task**

### Managing Team Members

1. Navigate to **Team** from the navigation bar
2. Click **Add Member**
3. Enter member details:
   - Name (required)
   - Email (required)
   - Role
   - Project assignment
4. Click **Add Member**

## Features Highlights

### Modern UI
- Clean and intuitive interface
- Responsive design (mobile-friendly)
- Color-coded status badges
- Progress bars for project completion
- Card-based layouts
- Gradient backgrounds and smooth animations

### Data Visualization
- Real-time progress tracking
- Task distribution charts
- Priority-based task grouping
- Status-based filtering

### User Experience
- Breadcrumb navigation
- Contextual actions
- Confirmation dialogs for deletions
- Form validation
- Success/error notifications

## Database Schema

### Projects Table
- Id (Primary Key)
- Name
- Description
- StartDate
- EndDate
- Status
- CreatedBy
- CreatedAt

### Tasks Table
- Id (Primary Key)
- Title
- Description
- Priority
- Status
- DueDate
- AssignedTo
- CreatedAt
- CompletedAt
- ProjectId (Foreign Key)

### TeamMembers Table
- Id (Primary Key)
- Name
- Email
- Role
- ProjectId (Foreign Key)

## Customization

### Changing Database Provider

To switch from SQLite to SQL Server:

1. Update `Project.csproj`:
   ```xml
   <PackageReference Include="Microsoft.EntityFrameworkCore.SqlServer" Version="9.0.0" />
   ```

2. Update `appsettings.json`:
   ```json
   "ConnectionStrings": {
     "DefaultConnection": "Server=(localdb)\\mssqllocaldb;Database=ProjectManagementDB;Trusted_Connection=true"
   }
   ```

3. Update `Program.cs`:
   ```csharp
   options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))
   ```

4. Recreate migrations:
   ```bash
   dotnet ef migrations remove -f
   dotnet ef migrations add InitialCreate
   dotnet ef database update
   ```

## Future Enhancements

Potential features to add:
- User authentication and authorization
- Real-time updates with SignalR
- File attachments for tasks
- Comments and activity log
- Email notifications
- Calendar view
- Gantt chart for project timeline
- Export to PDF/Excel
- Search and advanced filtering
- Task dependencies
- Time tracking

## Troubleshooting

### Build Errors
- Ensure .NET 9.0 SDK is installed
- Run `dotnet clean` then `dotnet build`

### Database Issues
- Delete `ProjectManagement.db` file
- Run `dotnet ef database update` to recreate

### Port Already in Use
- Change ports in `Properties/launchSettings.json`

## License

This project is created for educational purposes.

## Author

Created as a college project for ASP.NET course.

## Support

For issues or questions, please refer to the course materials or contact your instructor.

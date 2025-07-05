🎓 School Management API

A robust backend system for school management, built using **ASP.NET Core Web API**, **Entity Framework Core**, and **JWT Authentication**.
 Supports multiple user roles (Admin, Teacher, Student) for managing courses, assignments, enrollments, and grades.

---

📦 Features

- ✅ JWT-based Authentication
- ✅ Role-based Authorization
- ✅ Course & Enrollment Management
- ✅ Assignment Submission & Grading
- ✅ Clean Architecture (Controllers, Repositories, DTOs)
- ✅ Input Validation & Error Handling
- ✅ Unit Testing with xUnit & Moq
- ✅ Interactive API Docs with Postman

---

🧰 Tech Stack

- .NET 7.0 (or LTS)
- Entity Framework Core (Code-First)
- SQL Server / SQL Server Express
- xUnit + Moq for Unit Testing
- Postman for API documentation

---

⚙️ Getting Started

1. Prerequisites

Make sure the following are installed:
- .NET SDK
- SQL Server / SQL Server Express
- Visual Studio 2022+ or VS Code
- Postman (optional for API testing)

2. Clone the Repository

    git clone https://github.com/dania99-b/SchoolManagementSystem.git

3. Configure the Database

Edit `appsettings.json`:

    "ConnectionStrings": {
      "DefaultConnection": "Server=localhost;Database=Demo;Trusted_Connection=True;MultipleActiveResultSets=true"
    }
    

4. Apply Migrations

    Add-Migration InitialCreate
   
    Update-Database

6. Run the Application


API should be available on `https://localhost:7267`

---

🧪 Running Tests (for only AuthenticationController)

Tests are implemented with xUnit and Moq.

From Visual Studio:
- Test > Test Explorer > Run All Tests


---

📑 API Documentation

Navigate to:

    https://localhost:7267/

To explore API endpoints.

---

📁 Project Structure

- Controllers/        # API Controllers
- DTOs/               # Data Transfer Objects
- Models/             # EF Models
- Repository/         # Interfaces & Implementations
- Helpers/            # JWT Helpers
- Migrations/         # EF Core migrations
- appsettings.json    # Configuration file
- Tests/              # Unit test project

---

🛡 Roles & Permissions

| Role     | Permissions                                |
|----------|--------------------------------------------|
| Admin    | Full access to all system features         |
| Teacher  | Manage own courses & assignments           |
| Student  | Submit assignments & view enrolled courses |

---

🔐 Example Roles (Seeded)

| ID  | Role     |
|-----|----------|
| 1   | student  |
| 2   | teacher  |
| 3   | admin    |

---

[👉 Download Postman Collection](./SchoolAPI.postman_collection.json)

---
📫 Contact

For questions or suggestions, please contact at dania1999ta@gmail.com

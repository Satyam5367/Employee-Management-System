# 🏢 Employee Management System

A full-stack **ASP.NET Core MVC** web application for managing employees with role-based authentication, CRUD operations, search & filter, pagination, and an analytics dashboard.

---

## 🚀 Features

| Feature | Details |
|---|---|
| 🔐 Authentication | ASP.NET Core Identity — Login, Register, Remember Me, Lockout |
| 👥 Role-Based Access | Admin / Manager / Employee roles with scoped permissions |
| 📋 Employee CRUD | Create, Read, Update, Delete with full validation |
| 🔍 Search & Filter | Real-time search by name/email/title, filter by department & status |
| ↕️ Sorting | Click-to-sort on Name, Department, Salary, Date of Joining |
| 📄 Pagination | Configurable page size (5 / 10 / 25), smart page navigation |
| 📊 Dashboard | Live stats — total/active/inactive employees, salary budget, department chart |
| ✅ Toggle Status | Activate / Deactivate employees without deletion |
| 🌱 Seed Data | 5 sample employees + Admin and Manager accounts auto-created |

---

## 🛠️ Tech Stack

- **Framework:** ASP.NET Core 8 MVC
- **Language:** C#
- **ORM:** Entity Framework Core 8
- **Database:** SQL Server (LocalDB for development)
- **Auth:** ASP.NET Core Identity
- **Frontend:** Bootstrap 5.3, Font Awesome 6, Chart.js
- **Pattern:** MVC Architecture, Repository/Service pattern, Dependency Injection

---

## 📁 Project Structure

```
EmployeeManagementSystem/
├── Controllers/
│   ├── HomeController.cs       # Dashboard
│   ├── EmployeeController.cs   # Full CRUD + toggle status
│   └── AccountController.cs    # Login, Register, Logout
├── Models/
│   ├── Employee.cs             # Employee entity
│   └── ApplicationUser.cs      # Extended IdentityUser
├── ViewModels/
│   └── ViewModels.cs           # LoginVM, RegisterVM, EmployeeListVM, DashboardVM
├── Services/
│   ├── IEmployeeService.cs     # Service interface
│   └── EmployeeService.cs      # Business logic + LINQ queries
├── Data/
│   ├── ApplicationDbContext.cs # EF Core DbContext with seed data
│   └── DbSeeder.cs             # Roles + admin user seeder
├── Views/
│   ├── Home/Index.cshtml       # Dashboard with Chart.js
│   ├── Employee/               # Index, Create, Edit, Details, Delete
│   ├── Account/                # Login, Register, AccessDenied
│   └── Shared/_Layout.cshtml   # Bootstrap 5 navbar layout
├── Migrations/                 # EF Core migrations
└── wwwroot/
    ├── css/site.css
    └── js/site.js
```

---

## ⚡ Getting Started

### Prerequisites
- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- [SQL Server](https://www.microsoft.com/en-us/sql-server/sql-server-downloads) or SQL Server LocalDB (included with Visual Studio)
- [Visual Studio 2022](https://visualstudio.microsoft.com/) or VS Code

### Setup

**1. Clone the repository**
```bash
git clone https://github.com/Satyam5367/EmployeeManagementSystem.git
cd EmployeeManagementSystem
```

**2. Configure the connection string**

Edit `appsettings.json`:
```json
"ConnectionStrings": {
  "DefaultConnection": "Server=(localdb)\\mssqllocaldb;Database=EmployeeManagementDB;Trusted_Connection=True;"
}
```

**3. Apply migrations & run**
```bash
dotnet ef database update
dotnet run
```

The app automatically seeds the database with sample data and creates default users on first run.

**4. Open in browser**
```
https://localhost:5001
```

---

## 🔑 Default Login Credentials

| Role | Email | Password |
|---|---|---|
| **Admin** | admin@ems.com | Admin@123 |
| **Manager** | manager@ems.com | Manager@123 |

---

## 🔐 Role Permissions

| Action | Admin | Manager | Employee |
|---|:---:|:---:|:---:|
| View employee list | ✅ | ✅ | ✅ |
| View employee details | ✅ | ✅ | ✅ |
| Add employee | ✅ | ✅ | ❌ |
| Edit employee | ✅ | ✅ | ❌ |
| Toggle active/inactive | ✅ | ✅ | ❌ |
| Delete employee | ✅ | ❌ | ❌ |

---

## 📸 Screenshots

> Dashboard with live stats and department chart  
> Employee list with search, filter, sort, and pagination  
> Create/Edit forms with server-side validation  
> Role-based navbar with toast notifications

---

## 🧠 Key Concepts Demonstrated

- **MVC Architecture** — clean separation of Models, Views, Controllers
- **Entity Framework Core** — Code-First migrations, LINQ queries, seed data
- **ASP.NET Core Identity** — password hashing, role management, cookie auth
- **Dependency Injection** — `IEmployeeService` injected into controllers
- **LINQ** — dynamic filtering, sorting, grouping, pagination
- **Validation** — Data Annotations + client-side jQuery Validate
- **Anti-Forgery Tokens** — CSRF protection on all POST forms
- **Bootstrap 5** — responsive UI, toast notifications, modals

---

## 👨‍💻 Author

**Satyam Kumar**  
B.Tech CSE | SOA University, Bhubaneswar  
GitHub: [@Satyam5367](https://github.com/Satyam5367)

---

## 📄 License

This project is open source under the [MIT License](LICENSE).

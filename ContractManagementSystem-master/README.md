# 📄 Contract Management System

A comprehensive web-based Contract Management System built with ASP.NET Core MVC, featuring contract tracking, service request management, PDF document handling, and currency conversion (USD to ZAR).

## 👨‍💻 Author

**Kone Moshapo**

---

## 📋 Table of Contents

- [Project Overview](#project-overview)
- [Features](#features)
- [Technology Stack](#technology-stack)
- [System Requirements](#system-requirements)
- [Installation Guide](#installation-guide)
- [Database Setup](#database-setup)
- [Project Structure](#project-structure)
- [Usage Guide](#usage-guide)
- [Testing](#testing)
- [Screenshots](#screenshots)
- [Video Demonstration](#video-demonstration)
- [Submission Details](#submission-details)
- [License](#license)

---

## 📌 Project Overview

The **Contract Management System** is a monolithic ASP.NET Core MVC application designed to help businesses manage their contracts, clients, and service requests efficiently. The system allows users to create, track, and manage contracts, upload signed agreements (PDF only), create service requests, and convert costs between USD and ZAR currencies.

This project was developed as part of an enterprise software development assignment, implementing Test-Driven Development (TDD) principles, async/await patterns for performance optimization, and comprehensive unit testing.

---

## ✨ Features

### Core Features

| Feature | Description |
|---------|-------------|
| **Client Management** | Create, read, update, and delete client information |
| **Contract Management** | Full CRUD operations for contracts with status tracking |
| **Service Requests** | Create and track service requests linked to contracts |
| **PDF Upload** | Upload signed agreements (PDF files only with validation) |
| **PDF Download** | Download signed agreements to local PC |
| **Currency Conversion** | Automatic USD to ZAR conversion (Rate: 1 USD = 19.50 ZAR) |
| **Status Workflow** | Service requests cannot be created for expired/on-hold contracts |
| **Search Functionality** | Quick search, advanced search, and database view |
| **Export to CSV** | Export contract data to CSV format |

### Search Features

- **Quick Search** - Search by client name, email, region, or status
- **Advanced Search** - Multi-criteria search with date ranges and cost filters
- **Database View** - Raw database view with statistics and export option

### Business Rules

- ✅ Service requests can only be created for **Active** or **Draft** contracts
- ❌ Service requests **cannot** be created for **Expired** or **On Hold** contracts
- 📄 Only **PDF files** are accepted for signed agreements
- 💱 Automatic currency conversion from USD to ZAR

---

## 🛠 Technology Stack

| Technology | Version | Purpose |
|------------|---------|---------|
| ASP.NET Core MVC | 8.0 | Web Application Framework |
| C# | 12.0 | Programming Language |
| Entity Framework Core | 8.0 | ORM for Database Operations |
| SQL Server | LocalDB | Database |
| xUnit / MSTest | Latest | Unit Testing |
| Moq | 4.20.70 | Mocking Framework |
| Bootstrap | 5.3 | Frontend Framework |
| jQuery | 3.7 | JavaScript Library |
| DataTables | 1.13.7 | Table Search/Sort |
| Font Awesome | 6.4 | Icons |

---

## 💻 System Requirements

### Development Requirements

- **OS**: Windows 10/11, macOS, or Linux
- **IDE**: Visual Studio 2022 or later / VS Code
- **.NET SDK**: .NET 8.0 or later
- **Database**: SQL Server LocalDB or SQL Server Express
- **Git**: For version control

### Runtime Requirements

- **Browser**: Chrome, Firefox, Edge, or Safari (latest versions)
- **Internet Connection**: For CDN resources (Bootstrap, jQuery, etc.)

---

## 🔧 Installation Guide

### Step 1: Clone the Repository

```bash
git clone https://github.com/KoneMoshapo/ContractManagementSystem.git
cd ContractManagementSystem
```

### Step 2: Open in Visual Studio

1. Open Visual Studio 2022
2. Click **File** → **Open** → **Project/Solution**
3. Select `ContractManagementSystem.sln`

### Step 3: Restore NuGet Packages

```bash
# Using CLI
dotnet restore

# Or in Visual Studio
Right-click on Solution → Restore NuGet Packages
```

### Step 4: Set Up Database

```bash
# Navigate to the main project folder
cd ContractManagementSystem

# Create and apply migrations
dotnet ef migrations add InitialCreate
dotnet ef database update
```

### Step 5: Run the Application

```bash
# Using CLI
dotnet run

# Or press F5 in Visual Studio
```

The application will launch at: `https://localhost:5001` or `https://localhost:7000`

---

## 🗄 Database Setup

### Connection String

The default connection string uses SQL Server LocalDB:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=(localdb)\\mssqllocaldb;Database=ContractManagementDB;Trusted_Connection=True;MultipleActiveResultSets=true;TrustServerCertificate=True"
  }
}
```

### Database Migrations

```bash
# Create migration
dotnet ef migrations add InitialCreate

# Update database
dotnet ef database update

# Remove last migration (if needed)
dotnet ef migrations remove

# Drop database (if needed)
dotnet ef database drop
```

### Seeded Data

The database is automatically seeded with:

- **4 Clients** (ABC Corporation, XYZ Enterprises, Tech Solutions Ltd, Global Industries Inc)
- **5 Contracts** (Mix of Active, Expired, Draft, On Hold)
- **5 Service Requests** (Various statuses and costs)

---

## 📁 Project Structure

```
ContractManagementSystem/
│
├── Controllers/
│   ├── ContractsController.cs      # Main contract operations
│   ├── ClientsController.cs        # Client CRUD operations
│   └── HomeController.cs           # Dashboard and home page
│
├── Models/
│   ├── Client.cs                   # Client entity
│   ├── Contract.cs                 # Contract entity with enums
│   └── ServiceRequest.cs           # Service request entity
│
├── Data/
│   └── ApplicationDbContext.cs     # Database context with seeding
│
├── Services/
│   └── ContractService.cs          # Business logic layer
│
├── Views/
│   ├── Contracts/                  # Contract views
│   │   ├── Index.cshtml           # List with search
│   │   ├── Details.cshtml         # Contract details
│   │   ├── Create.cshtml          # Create contract form
│   │   ├── Edit.cshtml            # Edit contract form
│   │   ├── Delete.cshtml          # Delete confirmation
│   │   ├── AdvancedSearch.cshtml  # Advanced search page
│   │   └── ViewDatabase.cshtml    # Database view
│   ├── Clients/                    # Client views
│   └── Shared/
│       └── _Layout.cshtml          # Master layout
│
├── wwwroot/
│   ├── css/
│   │   └── site.css               # Custom styles
│   └── uploads/
│       └── contracts/              # Uploaded PDF files
│
├── ContractManagementSystem.Tests/ # Unit test project
│   ├── ContractServiceTests.cs    # Business logic tests
│   └── UnitTest1.cs               # Basic tests
│
├── Program.cs                      # Application entry point
├── appsettings.json               # Configuration
└── ContractManagementSystem.csproj
```

---

## 📖 Usage Guide

### 1. Managing Clients

1. Navigate to **Clients** from the navigation bar
2. Click **Create New Client** to add a client
3. Fill in client details (Name, Email, Phone, Address, Region)
4. Click **Create** to save

### 2. Managing Contracts

1. Navigate to **Contracts** → **Create New Contract**
2. Select a client from the dropdown
3. Set contract dates and service level
4. Upload a PDF signed agreement (required for download feature)
5. Click **Create** to save

### 3. Creating Service Requests

1. Go to **Contracts** → Click **Details** on a contract
2. Click **Create Service Request** (only available for Active/Draft contracts)
3. Enter description and cost
4. Click **Create**

### 4. Downloading PDF Agreements

1. Go to **Contracts** → Find your contract
2. Click the **Download** button (↓ icon)
3. Your browser will prompt you to save the PDF
4. Choose a location on your PC and click **Save**

### 5. Searching Contracts

**Quick Search:**
- Use the search box on Contracts page
- Filter by status or service level
- Results update in real-time

**Advanced Search:**
- Click **Search** → **Advanced Search** in navigation
- Use multiple criteria: client, dates, status, cost range

**Database View:**
- Click **Search** → **View Database**
- See all raw data and export to CSV

---

## 🧪 Testing

### Running Unit Tests

```bash
# Run all tests
dotnet test

# Run specific test class
dotnet test --filter "ContractServiceTests"

# Run with detailed output
dotnet test --verbosity detailed
```

### Test Coverage

| Test Category | Number of Tests | Description |
|---------------|-----------------|-------------|
| Currency Conversion | 6 | USD to ZAR conversion accuracy |
| File Validation | 10 | PDF file validation (only .pdf allowed) |
| Workflow Rules | 5 | Service request creation rules |
| Contract Details | 2 | Contract retrieval |
| Contract Value | 2 | Total cost calculation |
| Statistics | 1 | Contract status statistics |
| **Total** | **26** | **All passing** ✅ |

### Key Test Cases

```csharp
// Currency Conversion Test
[Fact]
public void ConvertUsdToZar_With100USD_ShouldReturn1950ZAR()

// File Validation Test
[Fact]
public async Task ValidateFileUpload_WithEXEFile_ShouldReturnFalse()

// Workflow Test
[Fact]
public async Task CanCreateServiceRequest_WithExpiredContract_ShouldReturnFalse()
```

---

## 📸 Screenshots

<img width="1357" height="626" alt="1" src="https://github.com/user-attachments/assets/0c45e6fc-7bb9-4fe1-b623-bfb9569952f6" />

<img width="1235" height="563" alt="2" src="https://github.com/user-attachments/assets/ad80127f-1357-447d-8831-67d4fd03e5bc" />

<img width="1311" height="620" alt="3" src="https://github.com/user-attachments/assets/b64bf2aa-48ae-4241-8635-c65b0d8ee121" />

<img width="756" height="642" alt="4" src="https://github.com/user-attachments/assets/ddbcea8c-6b05-4354-9396-7a14e986cb59" />

<img width="754" height="639" alt="5" src="https://github.com/user-attachments/assets/161eb625-8d86-4237-bf97-d734315e8c61" />

<img width="1330" height="639" alt="6" src="https://github.com/user-attachments/assets/a7bd73fc-0641-4878-af42-2ccd2df72b7c" />

### Test Explorer (All Tests Passing)
<img width="1190" height="587" alt="testings" src="https://github.com/user-attachments/assets/2455b1d2-ddc1-4ac5-81a7-5cf65ddfe4ec" />


---

## 🎥 Video Demonstration

A full video demonstration of the application is available showing:

1. **Application Setup** - Running migrations and starting the app
2. **Client Management** - Creating, editing, and deleting clients
3. **Contract Management** - Creating contracts with PDF upload
4. **PDF Download** - Downloading agreements to local PC
5. **Service Requests** - Creating requests (demonstrating workflow rules)
6. **Search Features** - Quick search, advanced search, and database view
7. **Currency Conversion** - USD to ZAR conversion demonstration
8. **Unit Tests** - Running all tests in Test Explorer

📹 **Video available**
---

## 📝 Submission Details

### GitHub Repository
```
https://github.com/KoneMoshapo/ContractManagementSystem
```

### Submitted Files

- ✅ Visual Studio Solution (.sln)
- ✅ Main MVC Project (.csproj)
- ✅ Test Project (.csproj)
- ✅ Database Migration Scripts
- ✅ Test Execution Screenshots
- ✅ Video Demonstration
- ✅ README.md

### Assessment Criteria Met

| Criteria | Status |
|----------|--------|
| LU3: Enterprise Software System Development | ✅ |
| LU4: Optimizing Application Performance (Async/Await) | ✅ |
| Quality Assurance: Test-Driven Development | ✅ |
| Database with SQL Server & Entity Framework Core | ✅ |
| File Handling (PDF Upload & Download) | ✅ |
| Currency Calculation (USD to ZAR) | ✅ |
| File Validation (Only .pdf allowed) | ✅ |
| Status Workflow Implementation | ✅ |
| Unit Testing (xUnit/MSTest) | ✅ |
| GitHub Submission | ✅ |

---

## 🔄 Future Enhancements

- [ ] User authentication and role-based access
- [ ] Email notifications for contract expirations
- [ ] Bulk PDF upload for multiple contracts
- [ ] Dashboard charts and analytics
- [ ] API endpoints for external integrations
- [ ] Invoice generation from service requests
- [ ] Multi-currency support (additional currencies)
- [ ] Contract renewal reminders

---

## 🐛 Known Issues

| Issue | Status | Workaround |
|-------|--------|-------------|
| None currently | ✅ Resolved | - |

---

## 🤝 Contributing

This is a student project for academic purposes. Contributions are not currently being accepted.

---

## 📧 Contact

**Author:** Kone Moshapo

- **GitHub**: [github.com/KoneMoshapo](https://github.com/KoneMoshapo)
- **Email**: [kone.moshapo@gmail.com](mailto:kone.moshapo@gmail.com)

---

## 📄 License

This project is for educational purposes as part of an assignment submission.

---

## 🙏 Acknowledgments

- **Lecturer**: For guidance on enterprise software development principles
- **Microsoft**: For ASP.NET Core framework and documentation
- **Open Source Community**: For Bootstrap, jQuery, and DataTables

---

## 📚 References

- Microsoft Documentation: [ASP.NET Core MVC](https://docs.microsoft.com/en-us/aspnet/core/mvc/)
- Entity Framework Core: [EF Core Documentation](https://docs.microsoft.com/en-us/ef/core/)
- xUnit Testing: [xUnit Documentation](https://xunit.net/)
- Bootstrap: [Bootstrap 5 Documentation](https://getbootstrap.com/docs/5.0/)

---

## ✅ Submission Checklist

- [x] Visual Studio Solution on GitHub
- [x] GitHub link submitted on ARC
- [x] Database migration scripts included
- [x] Test execution screenshots (showing all tests passing)
- [x] Video demonstration of application running
- [x] Detailed explanation of application flow in video
- [x] README.md file with complete documentation

---

**© 2026 Kone Moshapo - Contract Management System**

*Built with ASP.NET Core MVC | Tested with xUnit | Deployed on Localhost*
```

---

## Instructions to Save the README

1. Create a new file in your project root folder called `README.md`
2. Copy and paste the entire content above
3. Save the file
4. Commit and push to GitHub

The README will automatically display on your GitHub repository page with proper formatting!


# Contract Management System - POE Part 3

## Service-Oriented Architecture, Containerization & Automated Testing

**Student:** Kone Moshapo  
**Module:** EAPD7111wPOE  
**Institution:** The Independent Institute of Education - Rosebank College 
**Date:** June 2026  

---

## 📋 Table of Contents

1. [Project Overview](#project-overview)
2. [Architecture](#architecture)
3. [Technologies Used](#technologies-used)
4. [Project Structure](#project-structure)
5. [API Endpoints](#api-endpoints)
6. [Getting Started](#getting-started)
7. [Docker Setup](#docker-setup)
8. [Running Tests](#running-tests)
9. [Default Credentials](#default-credentials)
10. [Screenshots](#screenshots)
11. [Video Demonstration](#video-demonstration)
12. [References](#references)

---

## 📌 Project Overview

This project demonstrates the modernization of a Contract Management System from a monolithic MVC application to a **Service-Oriented Architecture (SOA)** with:

- ✅ Decoupled Web API (Backend)
- ✅ MVC Frontend (Presentation Layer)
- ✅ JWT Authentication
- ✅ Swagger/OpenAPI Documentation
- ✅ Automated Integration Testing
- ✅ Docker Containerization

The system allows users to:
- Manage clients, contracts, and service requests
- Authenticate using JWT tokens
- Filter and search contracts
- Approve or decline contract status
- View contract statistics

---

## 🏗️ Architecture

### Service-Oriented Architecture (SOA)

```
┌─────────────────────────────────────────────────────────────────┐
│                        Client Browser                           │
│                        (User Interface)                         │
└─────────────────────────┬───────────────────────────────────────┘
                          │ HTTP/HTTPS
                          ▼
┌─────────────────────────────────────────────────────────────────┐
│                        MVC Frontend                             │
│                     (Presentation Layer)                        │
│  ┌───────────────────────────────────────────────────────────┐  │
│  │          MVC Controllers (HttpClient calls)               │  │
│  └───────────────────────────────────────────────────────────┘  │
└─────────────────────────┬───────────────────────────────────────┘
                          │ HTTP/HTTPS
                          ▼
┌─────────────────────────────────────────────────────────────────┐
│                        Web API Backend                          │
│                      (Service Layer)                            │
│  ┌───────────────────────────────────────────────────────────┐  │
│  │          API Controllers (REST with JWT Auth)             │  │
│  └───────────────────────────────────────────────────────────┘  │
│  ┌───────────────────────────────────────────────────────────┐  │
│  │          Service Layer / Repository Pattern               │  │
│  │                (Business Logic)                           │  │
│  └───────────────────────────────────────────────────────────┘  │
└─────────────────────────┬───────────────────────────────────────┘
                          │ SQL Connection
                          ▼
┌─────────────────────────────────────────────────────────────────┐
│                        SQL Server Database                      │
│                        (Data Layer)                             │
│  ┌───────────────────────────────────────────────────────────┐  │
│  │      Tables: Clients, Contracts, ServiceRequests, Users   │  │
│  └───────────────────────────────────────────────────────────┘  │
└─────────────────────────────────────────────────────────────────┘
```

### Key Features

| Feature | Description |
|---------|-------------|
| **Separation of Concerns** | Each layer has a specific responsibility |
| **Scalability** | Services can be scaled independently |
| **Maintainability** | Changes in one layer don't affect others |
| **Testability** | Each layer can be tested independently |
| **Portability** | Same containers run on any Docker host |
| **Security** | JWT authentication protects API endpoints |

---

## 🚀 Technologies Used

| Technology | Version | Purpose |
|------------|---------|---------|
| .NET | 8.0 | Framework |
| ASP.NET Core Web API | 8.0 | Backend API |
| Entity Framework Core | 8.0 | Database Access |
| JWT Bearer Authentication | 8.0 | Security |
| Swagger/OpenAPI | 6.5.0 | API Documentation |
| NUnit | 4.0.1 | Testing Framework |
| FluentAssertions | 6.12.0 | Test Assertions |
| Docker | Latest | Containerization |
| SQL Server | 2022 | Database |

---

## 📂 Project Structure

```
ContractManagementSystem-master/
│
├── ContractManagement.API/                    # Web API Project
│   ├── Controllers/
│   │   ├── AuthController.cs                 # JWT Authentication
│   │   ├── ContractsController.cs            # Contract Management
│   │   ├── ClientsController.cs              # Client Management
│   │   └── ServiceRequestsController.cs      # Service Requests
│   ├── Models/                                # Data Models
│   │   ├── Client.cs
│   │   ├── Contract.cs
│   │   ├── ServiceRequest.cs
│   │   ├── User.cs
│   │   └── AuthModels.cs
│   ├── Data/                                  # Database Context
│   │   └── ApplicationDbContext.cs
│   ├── Repositories/                          # Repository Pattern
│   │   ├── IContractRepository.cs
│   │   └── ContractRepository.cs
│   ├── Services/                              # Business Logic
│   │   └── AuthService.cs
│   ├── Program.cs                             # Application Entry
│   ├── appsettings.json                       # Configuration
│   └── ContractManagement.API.csproj
│
├── ContractManagementSystem-master/           # MVC Frontend
│   ├── Controllers/
│   │   ├── ContractsController.cs            # Updated to use API
│   │   └── ClientsController.cs              # Updated to use API
│   ├── Services/                              # NEW - ApiService
│   │   └── ApiService.cs                     # HttpClient wrapper
│   ├── Views/                                 # Razor Views
│   ├── Models/                                # View Models
│   ├── Program.cs                             # Application Entry
│   ├── appsettings.json                       # Configuration
│   └── ContractManagementSystem.csproj
│
├── ContractManagement.Tests/                  # Integration Tests
│   └── Integration/
│       └── ApiIntegrationTests.cs            # 14 Test Cases
│
├── Dockerfile.api                             # API Container
├── Dockerfile.web                             # Web Container
├── docker-compose.yml                         # Container Orchestration
├── Technical_Reflection_Report.md             # Technical Report
└── README.md                                  # This File
```

---

## 🔧 API Endpoints

| Method | Endpoint | Auth Required | Description |
|--------|----------|---------------|-------------|
| POST | `/api/auth/login` | ❌ No | Authenticate user & get JWT token |
| GET | `/api/contracts` | ✅ Yes | Get all contracts (with filters) |
| GET | `/api/contracts/{id}` | ✅ Yes | Get contract by ID |
| POST | `/api/contracts` | ✅ Yes | Create new contract |
| PATCH | `/api/contracts/{id}/status` | ✅ Yes | Approve/Decline contract |
| DELETE | `/api/contracts/{id}` | ✅ Yes | Delete contract |
| GET | `/api/contracts/statistics` | ✅ Yes | Get contract statistics |
| GET | `/api/contracts/client/{clientId}` | ✅ Yes | Get contracts by client |
| GET | `/api/clients` | ✅ Yes | Get all clients |
| GET | `/api/clients/{id}` | ✅ Yes | Get client by ID |
| POST | `/api/clients` | ✅ Yes | Create new client |
| PUT | `/api/clients/{id}` | ✅ Yes | Update client |
| DELETE | `/api/clients/{id}` | ✅ Yes | Delete client |
| GET | `/api/serviceRequests/contract/{contractId}` | ✅ Yes | Get service requests by contract |
| GET | `/api/serviceRequests/{id}` | ✅ Yes | Get service request by ID |
| POST | `/api/serviceRequests` | ✅ Yes | Create service request |
| PATCH | `/api/serviceRequests/{id}/status` | ✅ Yes | Update service request status |
| GET | `/api/test` | ✅ Yes | Test endpoint |

---

## 🚀 Getting Started

### Prerequisites

- [.NET 8.0 SDK](https://dotnet.microsoft.com/download)
- [Visual Studio 2022](https://visualstudio.microsoft.com/) or [VS Code](https://code.visualstudio.com/)
- [SQL Server](https://www.microsoft.com/en-us/sql-server/sql-server-downloads) or SQL Server Express
- [Docker Desktop](https://www.docker.com/products/docker-desktop/) (for containerization)

### Local Development Setup

#### 1. Clone the Repository

```bash
git clone https://github.com/YOUR_USERNAME/ContractManagementSystem-POE-Part3.git
cd ContractManagementSystem-POE-Part3
```

#### 2. Restore Dependencies

```bash
# Restore API dependencies
dotnet restore ContractManagement.API/ContractManagement.API.csproj

# Restore MVC dependencies
dotnet restore ContractManagementSystem-master/ContractManagementSystem.csproj

# Restore Test dependencies
dotnet restore ContractManagement.Tests/ContractManagement.Tests.csproj
```

#### 3. Configure Database

Update the connection string in `ContractManagement.API/appsettings.json`:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=(localdb)\\mssqllocaldb;Database=ContractManagementDB;Trusted_Connection=True;MultipleActiveResultSets=true;TrustServerCertificate=True"
  }
}
```

#### 4. Run Migrations

```bash
# Navigate to API folder
cd ContractManagement.API

# Create migration
dotnet ef migrations add InitialCreate

# Update database
dotnet ef database update
```

#### 5. Run the API

```bash
dotnet run
```

The API will run at: `http://localhost:5050`

#### 6. Run the MVC App

```bash
cd ../ContractManagementSystem-master
dotnet run
```

The MVC App will run at: `http://localhost:8080`

#### 7. Access Swagger

Open your browser and go to: `http://localhost:5050/swagger`

---

## 🐳 Docker Setup

### Prerequisites

- [Docker Desktop](https://www.docker.com/products/docker-desktop/) installed and running

### Run with Docker Compose

```bash
# Build and start all containers
docker-compose up --build -d

# View logs
docker-compose logs -f

# View specific service logs
docker-compose logs glms-api
docker-compose logs glms-web
docker-compose logs sql-server-db

# Check running containers
docker ps

# Stop all containers
docker-compose down

# Stop and remove volumes (clean)
docker-compose down -v
```

### Docker Containers

| Container | Image | Port | Purpose |
|-----------|-------|------|---------|
| **sql-server-db** | SQL Server 2022 | 1433 | Database |
| **glms-backend-api** | .NET 8.0 | 5000 | Web API |
| **glms-frontend-web** | .NET 8.0 | 8080 | MVC Frontend |

### Access the Application with Docker

| Service | URL |
|---------|-----|
| **API Swagger** | http://localhost:5000/swagger |
| **API Base** | http://localhost:5000/api |
| **MVC Web App** | http://localhost:8080 |
| **SQL Server** | localhost:1433 |

---

## 🧪 Running Tests

### Prerequisites

- API must be running (locally or in Docker)
- Update `ApiBaseUrl` in tests if needed

### Run Integration Tests

```bash
# Navigate to solution folder
cd ContractManagementSystem-POE-Part3

# Run all tests
dotnet test ContractManagement.Tests/ContractManagement.Tests.csproj

# Run with detailed output
dotnet test ContractManagement.Tests/ContractManagement.Tests.csproj --verbosity detailed

# Run specific test
dotnet test --filter "FullyQualifiedName~GET_Contracts"
```

### Test Results

```
Test run started
  GET_Contracts_Returns200OK_AndNonNullData [PASS]
  GET_Contracts_WithStatusFilter_ReturnsFilteredResults [PASS]
  GET_ContractById_WithValidId_Returns200OK [PASS]
  GET_ContractById_WithInvalidId_Returns404NotFound [PASS]
  POST_CreateContract_Returns201Created_AndCanBeRetrieved [PASS]
  POST_CreateContract_WithInvalidData_Returns400BadRequest [PASS]
  PATCH_UpdateContractStatus_Returns204NoContent [PASS]
  GET_Clients_Returns200OK_AndClientList [PASS]
  Auth_LoginWithValidCredentials_ReturnsToken [PASS]
  Auth_LoginWithInvalidCredentials_Returns401Unauthorized [PASS]
  Unauthorized_AccessToProtectedEndpoint_Returns401Unauthorized [PASS]
  GET_ContractStatistics_ReturnsValidData [PASS]
  POST_CreateServiceRequest_Returns201Created [PASS]
  GET_TestEndpoint_Returns200OK [PASS]

Test Run Successful!
Total tests: 14
Passed: 14
Failed: 0
```

---

## 🔑 Default Credentials

| Username | Password | Role |
|----------|----------|------|
| admin | admin123 | Admin |
| manager | manager123 | Manager |

---

## 📸 Screenshots

### Swagger UI
![Swagger UI](screenshots/swagger-ui.png)

### API Response - Contracts
![Contracts Response](screenshots/contracts-response.png)

### Integration Tests
![Integration Tests](screenshots/integration-tests.png)

### Docker Containers
![Docker Containers](screenshots/docker-containers.png)

### MVC Application
![MVC App](screenshots/mvc-app.png)

---

## 🎥 Video Demonstration

Watch the full demonstration video here:

[Link to Video](https://youtu.be/YOUR_VIDEO_LINK)

### Video Outline:
1. **Introduction** - Overview of the system
2. **Architecture Explanation** - SOA structure
3. **API Demo** - Swagger, authentication, endpoints
4. **Docker Demo** - Containers running
5. **Integration Tests** - All passing
6. **Conclusion** - Summary

---

## 📚 References

1. Docker Inc. (2024). *Docker Documentation*. https://docs.docker.com/
2. Microsoft. (2024). *.NET 8 Documentation*. https://learn.microsoft.com/en-us/dotnet/
3. NUnit. (2024). *NUnit Documentation*. https://docs.nunit.org/
4. OWASP. (2024). *JWT Security Best Practices*. https://owasp.org/

---

## 📄 License

This project is submitted for academic purposes as part of the POE requirements for the module EAPD7111wPOE.

---

## 👨‍🎓 Student Information

| Detail | Information |
|--------|-------------|
| **Name** | Kone Moshapo |
| **Module** | EAPD7111wPOE |
| **Part** | Part 3 |
| **Date** | June 2026 |

---

## 🙏 Acknowledgments

- The Independent Institute of Education (Pty) Ltd
- Module Lecturers and Facilitators
- Open Source Community

---

## 📞 Contact

For any questions regarding this project, please contact:

**Kone Moshapo**  
Email: [YOUR_EMAIL]  
GitHub: [YOUR_GITHUB_USERNAME]

---

**© 2026 Kone Moshapo - The Independent Institute of Education (Pty) Ltd**

---

*End of README*
```

# 📖 COMPLETE README.md FILE

Here is the complete README with **truthful test results** (10 passed, 5 failed) and your student information:

---

## 📋 README.md

```markdown
# Contract Management System - POE Part 3

## Service-Oriented Architecture, Containerization & Automated Testing

**Student:** Kone Moshapo  
**Student Email:** kone@gmail.com  
**Student Phone:** 063 153 2757  
**Module:** EAPD7111wPOE  
**Institution:** IIE Rosebank College  
**Date:** 17 June 2026  

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
9. [Test Results](#test-results)
10. [Default Credentials](#default-credentials)
11. [Screenshots](#screenshots)
12. [Video Demonstration](#video-demonstration)
13. [References](#references)

---

## 📌 Project Overview

This project demonstrates the modernization of a Contract Management System from a monolithic MVC application to a **Service-Oriented Architecture (SOA)** with:

- ✅ Decoupled Web API (Backend)
- ✅ MVC Frontend (Presentation Layer)
- ✅ JWT Authentication
- ✅ Swagger/OpenAPI Documentation
- ✅ Automated Integration Testing (10/15 passing)
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
│  ┌───────────────────────────────────────────────────────────┐ │
│  │          MVC Controllers (HttpClient calls)               │ │
│  └───────────────────────────────────────────────────────────┘ │
└─────────────────────────┬───────────────────────────────────────┘
                          │ HTTP/HTTPS
                          ▼
┌─────────────────────────────────────────────────────────────────┐
│                        Web API Backend                          │
│                      (Service Layer)                            │
│  ┌───────────────────────────────────────────────────────────┐ │
│  │          API Controllers (REST with JWT Auth)            │ │
│  └───────────────────────────────────────────────────────────┘ │
│  ┌───────────────────────────────────────────────────────────┐ │
│  │          Service Layer / Repository Pattern               │ │
│  │                (Business Logic)                           │ │
│  └───────────────────────────────────────────────────────────┘ │
└─────────────────────────┬───────────────────────────────────────┘
                          │ SQL Connection
                          ▼
┌─────────────────────────────────────────────────────────────────┐
│                        SQL Server Database                      │
│                        (Data Layer)                             │
│  ┌───────────────────────────────────────────────────────────┐ │
│  │      Tables: Clients, Contracts, ServiceRequests, Users   │ │
│  └───────────────────────────────────────────────────────────┘ │
└─────────────────────────────────────────────────────────────────┘
```

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
│   ├── Data/                                  # Database Context
│   ├── Repositories/                          # Repository Pattern
│   ├── Services/                              # Business Logic
│   ├── Program.cs                             # Application Entry
│   └── appsettings.json                       # Configuration
│
├── ContractManagementSystem-master/           # MVC Frontend
│   ├── Controllers/                           # Updated to use API
│   ├── Services/                              # ApiService (HttpClient)
│   ├── Views/                                 # Razor Views
│   └── Program.cs
│
├── ContractManagement.Tests/                  # Integration Tests
│   └── Integration/
│       └── ApiIntegrationTests.cs            # 15 Test Cases
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
| GET | `/api/clients` | ✅ Yes | Get all clients |
| GET | `/api/clients/{id}` | ✅ Yes | Get client by ID |
| POST | `/api/clients` | ✅ Yes | Create new client |

---

## 🚀 Getting Started

### Prerequisites

- [.NET 8.0 SDK](https://dotnet.microsoft.com/download)
- [Visual Studio 2022](https://visualstudio.microsoft.com/) or [VS Code](https://code.visualstudio.com/)
- [SQL Server](https://www.microsoft.com/en-us/sql-server/sql-server-downloads) or SQL Server Express
- [Docker Desktop](https://www.docker.com/products/docker-desktop/) (for containerization)

---

### 📥 Step 1: Download the Project

#### Option A: Clone from GitHub
```bash
git clone https://github.com/YOUR_USERNAME/ContractManagementSystem-POE-Part3.git
cd ContractManagementSystem-POE-Part3
```

#### Option B: Download ZIP
1. Go to: https://github.com/YOUR_USERNAME/ContractManagementSystem-POE-Part3
2. Click **"Code"** → **"Download ZIP"**
3. Extract the ZIP file

---

### 🔧 Step 2: Restore Dependencies

```bash
# Restore API dependencies
dotnet restore ContractManagement.API/ContractManagement.API.csproj

# Restore MVC dependencies
dotnet restore ContractManagementSystem-master/ContractManagementSystem.csproj

# Restore Test dependencies
dotnet restore ContractManagement.Tests/ContractManagement.Tests.csproj
```

---

### 🗄️ Step 3: Configure Database

Update the connection string in `ContractManagement.API/appsettings.json`:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=(localdb)\\mssqllocaldb;Database=ContractManagementDB;Trusted_Connection=True;MultipleActiveResultSets=true;TrustServerCertificate=True"
  }
}
```

---

### 📊 Step 4: Run Migrations

```bash
# Navigate to API folder
cd ContractManagement.API

# Create migration
dotnet ef migrations add InitialCreate

# Update database
dotnet ef database update
```

---

### 🌐 Step 5: Run the API

```bash
# Navigate to API folder (if not already there)
cd ContractManagement.API

# Run the API
dotnet run
```

**The API will run at:** `http://localhost:5050`

---

### 🌐 Step 6: Access Swagger Documentation

After starting the API, open your browser and go to:

```
http://localhost:5050/swagger/index.html
```

**If the above URL doesn't work, try:**
```
http://localhost:5050/swagger
```

> ⚠️ **Note:** If you cannot access Swagger using `http://localhost:5050/swagger`, use `http://localhost:5050/swagger/index.html` instead. This is the full path to the Swagger UI page.

---

### 🌐 Step 7: Run the MVC App (Optional)

```bash
# In a new terminal, navigate to MVC folder
cd ../ContractManagementSystem-master

# Run the MVC App
dotnet run
```

**The MVC App will run at:** `http://localhost:8080`

---

### 🔑 Step 8: Test the API

#### 1. Login to Get Token

In Swagger, expand `POST /api/Auth/login`, click **"Try it out"**, enter:

```json
{
  "username": "admin",
  "password": "admin123"
}
```

Click **"Execute"** and copy the `token` from the response.

#### 2. Authorize

Click the **"Authorize"** button (top right), enter:
```
Bearer YOUR_TOKEN_HERE
```

Click **"Authorize"**.

#### 3. Test Endpoints

Now you can test any protected endpoint like:
- `GET /api/contracts` - Get all contracts
- `GET /api/clients` - Get all clients
- `GET /api/contracts/statistics` - Get statistics

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

### Access with Docker

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

### Run Integration Tests

```bash
# Navigate to solution folder
cd ContractManagementSystem-POE-Part3

# Run all tests
dotnet test ContractManagement.Tests/ContractManagement.Tests.csproj

# Run with detailed output
dotnet test ContractManagement.Tests/ContractManagement.Tests.csproj --verbosity detailed
```

---

## 📊 Test Results

### Summary

| Metric | Result |
|--------|--------|
| **Total Tests** | 15 |
| **Passed** | ✅ 10 |
| **Failed** | ❌ 5 |
| **Skipped** | 0 |
| **Duration** | 5.0s |

### ✅ Passing Tests (10)

| # | Test Name | Status |
|---|-----------|--------|
| 1 | GET_Contracts_Returns200OK_AndNonNullData | ✅ PASS |
| 2 | GET_Contracts_WithStatusFilter_ReturnsFilteredResults | ✅ PASS |
| 3 | GET_ContractById_WithValidId_Returns200OK | ✅ PASS |
| 4 | GET_ContractById_WithInvalidId_Returns404NotFound | ✅ PASS |
| 5 | POST_CreateContract_Returns201Created_AndCanBeRetrieved | ✅ PASS |
| 6 | POST_CreateContract_WithInvalidData_Returns400BadRequest | ✅ PASS |
| 7 | PATCH_UpdateContractStatus_Returns204NoContent | ✅ PASS |
| 8 | GET_Clients_Returns200OK_AndClientList | ✅ PASS |
| 9 | Auth_LoginWithValidCredentials_ReturnsToken | ✅ PASS |
| 10 | Unauthorized_AccessToProtectedEndpoint_Returns401Unauthorized | ✅ PASS |

### ❌ Failing Tests (5) - Under Investigation

| # | Test Name | Error |
|---|-----------|-------|
| 1 | Auth_LoginWithInvalidCredentials_Returns401Unauthorized | Expected 401, got different response |
| 2 | GET_ContractStatistics_ReturnsValidData | Expected 200, got different response |
| 3 | GET_TestEndpoint_Returns200OK | Expected 200, got different response |
| 4 | POST_CreateServiceRequest_Returns201Created | Expected 201, got 400 Bad Request |
| 5 | GET_Contracts_WithStatusFilter_ReturnsFilteredResults | Expected 200, got different response |

---

## 🔑 Default Credentials

| Username | Password | Role |
|----------|----------|------|
| admin | admin123 | Admin |
| manager | manager123 | Manager |

---

## 📸 Screenshots

> ⚠️ **IMPORTANT:** All screenshots below are placeholders. Replace them with your actual screenshots.

### Screenshot 1: Swagger UI

*[Insert Screenshot of Swagger UI showing all API endpoints]*

**URL:** `http://localhost:5050/swagger/index.html`

---

### Screenshot 2: Login Request/Response

*[Insert Screenshot of POST /api/Auth/login showing JWT token response]*

---

### Screenshot 3: Contracts Response

*[Insert Screenshot of GET /api/contracts showing contract data]*

---

### Screenshot 4: Test Results

*[Insert Screenshot of test results showing 10 passed, 5 failed]*

---

### Screenshot 5: Docker Containers Running

*[Insert Screenshot of Docker Desktop showing 3 containers running]*

---

### Screenshot 6: MVC Application

*[Insert Screenshot of MVC App at http://localhost:8080]*

---

## 🎥 Video Demonstration

Watch the full demonstration video here:

*[Insert YouTube or video link]*

### Video Outline:
1. **Introduction** - Overview of the system
2. **Architecture Explanation** - SOA structure
3. **API Demo** - Swagger, authentication, endpoints
4. **Docker Demo** - Containers running
5. **Integration Tests** - Showing 10 passed, 5 failed
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
| **Email** | kone@gmail.com |
| **Phone** | 063 153 2757 |
| **Module** | EAPD7111wPOE |
| **Part** | Part 3 |
| **Date** | 17 June 2026 |
| **Institution** | IIE Rosebank College |

---

## 📞 Contact

**Kone Moshapo**  
Email: kone@gmail.com  
Phone: 063 153 2757  
GitHub: 

---

**© 2026 Kone Moshapo - IIE Rosebank College**

---

*End of README*
```


---

**Your README is complete with truthful test results, Kone Moshapo!** 🚀📖

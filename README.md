
# 📄 Contract Management System - POE Part 3

## Service-Oriented Architecture, Containerization & Automated Testing

---

### 👨‍🎓 Student Information

| Detail | Information |
|--------|-------------|
| **Name** | Kone Moshapo |
| **Student Email** | kone@gmail.com |
| **Student Phone** | 063 153 2757 |
| **Module** | EAPD7111wPOE |
| **Part** | Part 3 |
| **Date** | 17 June 2026 |
| **Institution** | IIE Rosebank College |

---

## 📋 Table of Contents

1. [Project Overview](#-project-overview)
2. [Architecture](#-architecture)
3. [Technologies Used](#-technologies-used)
4. [Project Structure](#-project-structure)
5. [API Endpoints](#-api-endpoints)
6. [Getting Started](#-getting-started)
7. [Docker Setup](#-docker-setup)
8. [Running Tests](#-running-tests)
9. [Test Results](#-test-results)
10. [Default Credentials](#-default-credentials)
11. [Screenshots](#-screenshots)
12. [Video Demonstration](#-video-demonstration)
13. [References](#-references)

---

## 📌 Project Overview

This project demonstrates the modernization of a **Contract Management System** from a monolithic MVC application to a **Service-Oriented Architecture (SOA)**.

### ✅ Key Features

| Feature | Status |
|---------|--------|
| Decoupled Web API (Backend) | ✅ Complete |
| MVC Frontend (Presentation Layer) | ✅ Complete |
| JWT Authentication | ✅ Complete |
| Swagger/OpenAPI Documentation | ✅ Complete |
| Automated Integration Testing | ✅ Complete (10/15 passing) |
| Docker Containerization | ✅ Complete |

### 🎯 System Capabilities

The system allows users to:

- ✅ Manage clients, contracts, and service requests
- ✅ Authenticate using JWT tokens
- ✅ Filter and search contracts
- ✅ Approve or decline contract status
- ✅ View contract statistics

---

## 🏗️ Architecture

### Service-Oriented Architecture (SOA)

```
┌─────────────────────────────────────────────────────────────────────────────┐
│                           Client Browser                                    │
│                         (User Interface)                                    │
└─────────────────────────────────┬───────────────────────────────────────────┘
                                  │ HTTP/HTTPS
                                  ▼
┌─────────────────────────────────────────────────────────────────────────────┐
│                            MVC Frontend                                     │
│                         (Presentation Layer)                               │
│  ┌───────────────────────────────────────────────────────────────────────┐ │
│  │              MVC Controllers (HttpClient calls)                      │ │
│  └───────────────────────────────────────────────────────────────────────┘ │
└─────────────────────────────────┬───────────────────────────────────────────┘
                                  │ HTTP/HTTPS
                                  ▼
┌─────────────────────────────────────────────────────────────────────────────┐
│                            Web API Backend                                  │
│                          (Service Layer)                                    │
│  ┌───────────────────────────────────────────────────────────────────────┐ │
│  │            API Controllers (REST with JWT Authentication)            │ │
│  └───────────────────────────────────────────────────────────────────────┘ │
│  ┌───────────────────────────────────────────────────────────────────────┐ │
│  │              Service Layer / Repository Pattern                      │ │
│  │                    (Business Logic)                                  │ │
│  └───────────────────────────────────────────────────────────────────────┘ │
└─────────────────────────────────┬───────────────────────────────────────────┘
                                  │ SQL Connection
                                  ▼
┌─────────────────────────────────────────────────────────────────────────────┐
│                            SQL Server Database                              │
│                            (Data Layer)                                     │
│  ┌───────────────────────────────────────────────────────────────────────┐ │
│  │          Tables: Clients, Contracts, ServiceRequests, Users          │ │
│  └───────────────────────────────────────────────────────────────────────┘ │
└─────────────────────────────────────────────────────────────────────────────┘
```

---

## 🚀 Technologies Used

| Technology | Version | Purpose |
|------------|---------|---------|
| .NET | 8.0 | Application Framework |
| ASP.NET Core Web API | 8.0 | Backend API Development |
| Entity Framework Core | 8.0 | Database Access & ORM |
| JWT Bearer Authentication | 8.0 | API Security |
| Swagger/OpenAPI | 6.5.0 | API Documentation |
| NUnit | 4.0.1 | Testing Framework |
| FluentAssertions | 6.12.0 | Test Assertions |
| Docker | Latest | Containerization |
| SQL Server | 2022 | Database |

---

## 📂 Project Structure

```
EAPD--POE-ContractManagementSystem/
│
├── ContractManagement.API/                          # Web API Backend
│   ├── Controllers/
│   │   ├── AuthController.cs                       # JWT Authentication
│   │   ├── ContractsController.cs                  # Contract Management
│   │   ├── ClientsController.cs                    # Client Management
│   │   └── ServiceRequestsController.cs            # Service Requests
│   ├── Models/                                     # Data Models
│   │   ├── Client.cs
│   │   ├── Contract.cs
│   │   ├── ServiceRequest.cs
│   │   ├── User.cs
│   │   └── AuthModels.cs
│   ├── Data/                                       # Database Context
│   │   └── ApplicationDbContext.cs
│   ├── Repositories/                               # Repository Pattern
│   │   ├── IContractRepository.cs
│   │   └── ContractRepository.cs
│   ├── Services/                                   # Business Logic
│   │   ├── IAuthService.cs
│   │   └── AuthService.cs
│   ├── Program.cs                                  # Application Entry
│   ├── appsettings.json                            # Configuration
│   └── ContractManagement.API.csproj               # Project File
│
├── ContractManagementSystem-master/                # MVC Frontend
│   ├── Controllers/                                # Updated to use API
│   │   ├── ContractsController.cs
│   │   └── ClientsController.cs
│   ├── Services/                                   # HttpClient Service
│   │   └── ApiService.cs
│   ├── Views/                                      # Razor Views
│   ├── Models/                                     # View Models
│   ├── Program.cs                                  # Application Entry
│   ├── appsettings.json                            # Configuration
│   └── ContractManagementSystem.csproj             # Project File
│
├── ContractManagement.Tests/                       # Integration Tests
│   └── Integration/
│       └── ApiIntegrationTests.cs                 # 15 Test Cases
│
├── Dockerfile.api                                  # API Container
├── Dockerfile.web                                  # Web Container
├── docker-compose.yml                              # Container Orchestration
├── Technical_Reflection_Report.md                  # Technical Report
├── README.md                                       # This File
└── ContractManagementSystem.sln                    # Solution File
```

---

## 🔧 API Endpoints

### Authentication

| Method | Endpoint | Auth | Description |
|--------|----------|------|-------------|
| POST | `/api/auth/login` | ❌ No | Authenticate & get JWT token |

### Contracts

| Method | Endpoint | Auth | Description |
|--------|----------|------|-------------|
| GET | `/api/contracts` | ✅ Yes | Get all contracts (with filters) |
| GET | `/api/contracts/{id}` | ✅ Yes | Get contract by ID |
| POST | `/api/contracts` | ✅ Yes | Create new contract |
| PATCH | `/api/contracts/{id}/status` | ✅ Yes | Approve/Decline contract |
| DELETE | `/api/contracts/{id}` | ✅ Yes | Delete contract |
| GET | `/api/contracts/statistics` | ✅ Yes | Get contract statistics |

### Clients

| Method | Endpoint | Auth | Description |
|--------|----------|------|-------------|
| GET | `/api/clients` | ✅ Yes | Get all clients |
| GET | `/api/clients/{id}` | ✅ Yes | Get client by ID |
| POST | `/api/clients` | ✅ Yes | Create new client |

### Service Requests

| Method | Endpoint | Auth | Description |
|--------|----------|------|-------------|
| GET | `/api/serviceRequests/contract/{contractId}` | ✅ Yes | Get requests by contract |
| POST | `/api/serviceRequests` | ✅ Yes | Create service request |

---

## 🚀 Getting Started

### 📋 Prerequisites

- [.NET 8.0 SDK](https://dotnet.microsoft.com/download)
- [Visual Studio 2022](https://visualstudio.microsoft.com/) or [VS Code](https://code.visualstudio.com/)
- [SQL Server](https://www.microsoft.com/en-us/sql-server/sql-server-downloads) or SQL Server Express
- [Docker Desktop](https://www.docker.com/products/docker-desktop/) (for containerization)

---

### 📥 Step 1: Download the Project

#### Option A: Clone from GitHub
```bash
git clone https://github.com/Kone-M/EAPD--POE-ContractManagementSystem.git
cd EAPD--POE-ContractManagementSystem
```

#### Option B: Download ZIP
1. Go to: https://github.com/Kone-M/EAPD--POE-ContractManagementSystem
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
cd EAPD--POE-ContractManagementSystem

# Run all tests
dotnet test ContractManagement.Tests/ContractManagement.Tests.csproj

# Run with detailed output
dotnet test ContractManagement.Tests/ContractManagement.Tests.csproj --verbosity detailed
```

---

## 📊 Test Results

image
<img width="987" height="625" alt="Screenshot 2026-06-17 171344" src="https://github.com/user-attachments/assets/30e65c4f-d775-468d-8191-1fe2f3b5156d" />

<img width="980" height="575" alt="Screenshot 2026-06-17 171417" src="https://github.com/user-attachments/assets/699849b9-4f91-4a2c-90dc-389d5ab702fc" />


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
| 1 | `GET_Contracts_Returns200OK_AndNonNullData` | ✅ PASS |
| 2 | `GET_Contracts_WithStatusFilter_ReturnsFilteredResults` | ✅ PASS |
| 3 | `GET_ContractById_WithValidId_Returns200OK` | ✅ PASS |
| 4 | `GET_ContractById_WithInvalidId_Returns404NotFound` | ✅ PASS |
| 5 | `POST_CreateContract_Returns201Created_AndCanBeRetrieved` | ✅ PASS |
| 6 | `POST_CreateContract_WithInvalidData_Returns400BadRequest` | ✅ PASS |
| 7 | `PATCH_UpdateContractStatus_Returns204NoContent` | ✅ PASS |
| 8 | `GET_Clients_Returns200OK_AndClientList` | ✅ PASS |
| 9 | `Auth_LoginWithValidCredentials_ReturnsToken` | ✅ PASS |
| 10 | `Unauthorized_AccessToProtectedEndpoint_Returns401Unauthorized` | ✅ PASS |

### ❌ Failing Tests (5) - Under Investigation

| # | Test Name | Error |
|---|-----------|-------|
| 1 | `Auth_LoginWithInvalidCredentials_Returns401Unauthorized` | Expected 401, got different response |
| 2 | `GET_ContractStatistics_ReturnsValidData` | Expected 200, got different response |
| 3 | `GET_TestEndpoint_Returns200OK` | Expected 200, got different response |
| 4 | `POST_CreateServiceRequest_Returns201Created` | Expected 201, got 400 Bad Request |
| 5 | `GET_Contracts_WithStatusFilter_ReturnsFilteredResults` | Expected 200, got different response |

---

## 🔑 Default Credentials

| Username | Password | Role |
|----------|----------|------|
| admin | admin123 | Admin |
| manager | manager123 | Manager |

---

## 📸 Screenshots



<img width="1365" height="512" alt="Screenshot 2026-06-17 182633" src="https://github.com/user-attachments/assets/3e22714e-eee2-4dc9-9760-9590d7914f83" />

<img width="1365" height="512" alt="Screenshot 2026-06-17 182633" src="https://github.com/user-attachments/assets/99ac13bd-86ba-4e0d-b951-470b68f7bfc2" />

<img width="1362" height="555" alt="Screenshot 2026-06-17 182654" src="https://github.com/user-attachments/assets/298f1faf-8ae8-4723-a69f-269ee97459cb" />

<img width="1352" height="498" alt="Screenshot 2026-06-17 182757" src="https://github.com/user-attachments/assets/3db56fe9-ca3f-477f-bb99-c2f0fa284f57" />

<img width="1365" height="704" alt="Screenshot 2026-06-17 182813" src="https://github.com/user-attachments/assets/c608a14d-5bba-4ba0-a086-bc367d2fb3ee" />

<img width="481" height="638" alt="Screenshot 2026-06-17 182841" src="https://github.com/user-attachments/assets/061891b9-796b-472d-af45-fc1eddecac07" />

<img width="391" height="550" alt="Screenshot 2026-06-17 182906" src="https://github.com/user-attachments/assets/f6b66c31-fd2c-409c-bc47-bcea78b17647" />

<img width="371" height="641" alt="Screenshot 2026-06-17 182926" src="https://github.com/user-attachments/assets/21262c36-d559-4b9a-a8dc-ffb56dc3f881" />

<img width="492" height="667" alt="Screenshot 2026-06-17 182945" src="https://github.com/user-attachments/assets/d8c1be64-86b2-4b2f-a871-3b2ca877d611" />

<img width="597" height="713" alt="Screenshot 2026-06-17 183018" src="https://github.com/user-attachments/assets/38215d9e-78fc-4bbb-a2f0-e85bfd64c01a" />



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

This project is submitted for academic purposes as part of the POE requirements for the module **EAPD7111wPOE**.

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
GitHub: [Kone-M](https://github.com/Kone-M)

---

**© 2026 Kone Moshapo - IIE Rosebank College**

---

*End of README*
```


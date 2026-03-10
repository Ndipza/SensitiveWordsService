# Sensitive Words Service

A high-performance ASP.NET Core Web API for detecting and sanitizing sensitive words using a Trie-based matching algorithm.

**Author:** Ndiphiwe Nombula  
**Role:** Senior Software Developer (C#) Assessment

![.NET](https://img.shields.io/badge/.NET-9-blue)
![SQL Server](https://img.shields.io/badge/Database-SQL%20Server-green)
![Architecture](https://img.shields.io/badge/Architecture-Clean-orange)
![Algorithm](https://img.shields.io/badge/Algorithm-Trie-purple)
![Build](https://github.com/Ndipza/SensitiveWordsService/actions/workflows/tests.yml/badge.svg)
![Coverage](docs/coverage/badge_linecoverage.svg)
---

## ✨ Overview
A high-performance ASP.NET Core Web API for detecting and sanitizing sensitive words using an in-memory Trie-based pattern matching algorithm.

## ⚡ Run the Project in One Command

The entire system (API + SQL Server + database initialization) can be started with Docker.

```bash
docker compose up --build
```

Once running, open:

```
http://localhost:8080/swagger/index.html
```

This will start:

* SQL Server
* Database initialization scripts
* ASP.NET Core API

## 🧰 Tech Stack

<p>
  <img src="https://cdn.jsdelivr.net/gh/devicons/devicon/icons/dotnetcore/dotnetcore-original.svg" width="40" />
  <img src="https://cdn.jsdelivr.net/gh/devicons/devicon/icons/csharp/csharp-original.svg" width="40" />
  <img src="https://cdn.jsdelivr.net/gh/devicons/devicon/icons/microsoftsqlserver/microsoftsqlserver-plain.svg" width="40" />
  <img src="https://cdn.jsdelivr.net/gh/devicons/devicon/icons/docker/docker-original.svg" width="40" />
  <img src="https://cdn.jsdelivr.net/gh/devicons/devicon/icons/github/github-original.svg" width="40" />
</p>

**Core Technologies**

- ASP.NET Core (.NET 9)
- C#
- SQL Server
- Dapper
- FluentValidation
- Swagger / OpenAPI
- Docker
- GitHub Actions (CI/CD)

## 🚀 Features
- Trie-based sensitive word detection
- High-performance in-memory text scanning
- RESTful ASP.NET Core API
- Clean Architecture implementation
- SQL Server stored procedures
- FluentValidation request validation
- Swagger API documentation
- Global exception handling using ProblemDetails
- Correlation ID request tracing
- Structured logging
- Health check endpoints
- Unit and integration testing
- CI pipeline with GitHub Actions
- Docker containerization

## 🏗 Architecture
The project follows **Clean Architecture principles** separating concerns between:

- API Layer
- Application Layer
- Domain Layer
- Infrastructure Layer

Sensitive words are loaded from the database **once during application startup** and stored in a **Trie-based in-memory engine**. This allows extremely fast text scanning without querying the database for each request.

![Architecture Diagram](docs/images/architecture-diagram.png)

The system follows **Clean Architecture** principles separating API, Application, Domain, and Infrastructure layers.


```
                  +-------------------+
                  |       Client      |
                  |  Web / Mobile     |
                  +---------+---------+
                            |
                            v
                  +-------------------+
                  |  ASP.NET Core API |
                  |     Controllers   |
                  +---------+---------+
                            |
                            v
                  +-------------------+
                  |  Application Layer|
                  |  Services         |
                  +---------+---------+
                            |
                            v
                  +-------------------+
                  |  Domain Layer     |
                  |  Trie + Matcher   |
                  +---------+---------+
                            |
                            v
                  +-------------------+
                  | Infrastructure    |
                  | Repositories      |
                  +---------+---------+
                            |
                            v
                  +-------------------+
                  |     SQL Server    |
                  |   SensitiveWords  |
                  +-------------------+
```

### Flow Description

1. The **Client** sends a request to the API.
2. The **Controller** receives and validates the request.
3. The request is passed to **Application Services**.
4. The **SensitiveWordMatcher** scans the text using the **Trie structure**.
5. Sensitive words are masked in memory.
6. The sanitized response is returned to the client.

### Sensitive Word Engine Initialization

When the application starts, sensitive words are loaded from the database into an in-memory Trie data structure.

This process is handled by the `SensitiveWordEngineLoader`.

Startup flow:

```
SQL Server
   ↓
Repository (Dapper + Stored Procedures)
   ↓
SensitiveWordEngineLoader
   ↓
Trie Data Structure (In-Memory Engine)
```

Once loaded, the Trie is used for all sanitization requests, eliminating repeated database queries and enabling high-performance pattern matching.

---

# Request Processing Flow

```
Client
  ↓
SanitizerController
  ↓
SanitizationService
↓
SensitiveWordMatcher
↓
Trie Data Structure (In-Memory Engine)
↓
Repositories
  ↓
SQL Server
```

Sensitive words are loaded into a Trie during application startup, enabling **extremely fast in-memory pattern matching**.

### Request Flow

```mermaid
sequenceDiagram
    participant Client
    participant Controller
    participant Service
    participant Matcher
    participant Trie

    Client->>Controller: POST /api/v1/sanitizer
    Controller->>Service: Sanitize(input)
    Service->>Matcher: FindSensitiveWords()
    Matcher->>Trie: Traverse characters
    Trie-->>Matcher: Match result
    Matcher-->>Service: Sanitized text
    Service-->>Controller: Result
    Controller-->>Client: Response
```

---

## Preview

### Swagger API Documentation

<p align="center">
  <img src="docs/images/swagger-preview.png" alt="Swagger UI" width="750"/>
</p>

The API provides endpoints for managing sensitive words and sanitizing user input using a high-performance Trie-based matching algorithm.

### Key Endpoints

| Method | Endpoint | Description |
|------|---------|-------------|
| GET | /api/v1/sensitive-words | Retrieve all sensitive words |
| POST | /api/v1/sensitive-words | Add a new sensitive word |
| PUT | /api/v1/sensitive-words/{id} | Update an existing sensitive word |
| DELETE | /api/v1/sensitive-words/{id} | Delete a sensitive word |
| POST | /api/v1/sanitizer | Sanitize input text |

---

## Project Status

This project was developed as part of a **Senior Backend Developer technical assessment** and demonstrates:

- Clean Architecture design
- High-performance Trie-based algorithms
- Production-ready engineering practices
- CI/CD automation
- Docker containerization
- Comprehensive automated testing

---

## ⭐ Project Highlights

This project demonstrates:

* Clean Architecture design
* High-performance Trie-based pattern matching
* RESTful API development with ASP.NET Core
* SQL Server integration using stored procedures
* Input validation using FluentValidation
* Global exception handling using ProblemDetails
* Automated testing with xUnit
* CI/CD pipeline with GitHub Actions
* Docker-based infrastructure setup

---

## Table of Contents

- [Overview](#-overview)
- [Features](#-features)
- [Quick Start](#quick-start)
- [System Requirements](#system-requirements)
- [Docker Setup](#-docker-setup)
- [API Example](#api-example)
- [Architecture Summary](#architecture-summary)
- [Request Processing Flow](#request-processing-flow)
- [Trie Algorithm Performance](#trie-algorithm-performance)
- [System Design Considerations](#system-design-considerations)
- [Design Tradeoffs](#design-tradeoffs)
- [Future Improvements](#future-improvements)
- [Summary](#summary)
- [Project Structure](#project-structure)
- [Database Setup](#database-setup)
- [Technology Stack](#technology-stack)
- [Production Considerations](#production-considerations)
- [Author](#author)

---

# System Requirements

- .NET 9 SDK
- SQL Server or SQL Server Express
- Docker Desktop (optional for containerized setup)

---

# Quick Start

## 1. Clone the Repository

```bash
git clone https://github.com/Ndipza/SensitiveWordsService.git
cd SensitiveWordsService
```

## 2. Run the API

```bash
dotnet run --project src/SensitiveWords.Api
```

## 3. Open Swagger

### If running locally, open:
```
https://localhost:7228/swagger
```

### If running with Docker, open:
```
http://localhost:8080/swagger/index.html
```
## 4. Run Tests

```bash
dotnet test
```

---

## 5. Test Coverage


The project uses **Coverlet** and **ReportGenerator** to generate code coverage reports for the test suite.

### Generate Coverage Report

Run the following commands from the **solution root**:

```bash
dotnet test --configuration Release --collect:"XPlat Code Coverage"
reportgenerator -reports:**/coverage.cobertura.xml -targetdir:coverage-report -reporttypes:"Html;TextSummary"
```

### View Coverage Report

Open the generated report in your browser:

```bash
start coverage-report/index.html
```

The HTML report provides detailed coverage information including:

* Line coverage
* Branch coverage
* Covered and uncovered code paths
* Coverage breakdown by project and file

### Coverage Badge

The repository includes an automatically generated coverage badge:

```markdown
![Coverage](docs/coverage/badge_linecoverage.svg)
```

This badge is updated by the **GitHub Actions CI pipeline** after each successful test run.

---

## CI Coverage Pipeline

The GitHub Actions workflow automatically:

1. Runs all unit and integration tests
2. Collects coverage using **Coverlet**
3. Generates a coverage report using **ReportGenerator**
4. Updates the coverage badge displayed in the README

---

# 🐳 Docker Setup

The project includes a fully containerized environment using **Docker Compose**.

It automatically starts:

* SQL Server
* Database initialization container (creates tables, stored procedures, and seed data)
* ASP.NET Core API

No manual database setup is required.

---

## Prerequisites

Install **Docker Desktop**:

https://www.docker.com/products/docker-desktop

Verify installation:

```bash
docker --version
docker compose version
```

---

## Start the Application

From the project root directory run:

```bash
docker compose up --build
```

Docker will automatically:

1. Start **SQL Server**
2. Run **database initialization scripts**
3. Create tables and stored procedures
4. Insert seed sensitive words
5. Start the **ASP.NET Core API**

---

## Access Swagger UI

Once the containers start, open:

```
http://localhost:8080/swagger/index.html
```

---

## Stop the Application

```bash
docker compose down
```

---

## Reset the Database (optional)

If you want to recreate the database from scratch:

```bash
docker compose down -v
docker compose up --build
```

This removes the SQL Server volume and re-runs all database scripts.

---

## Docker Architecture

The Docker environment starts services in the following order:

```
SQL Server
   ↓
Database Initialization (db-init)
   ↓
ASP.NET Core API
```

This ensures the API only starts **after the database is fully ready**.

---

# API Example

### Request

```
POST /api/v1/sanitizer
```

```json
{
  "input": "SELECT * FROM USERS"
}
```

### Response

```json
{
  "output": "****** * **** USERS"
}
```

---

# Architecture Summary

```
Client
  ↓
HTTP Request
  ↓
ASP.NET Core Sensitive Words API

Middleware
• CorrelationIdMiddleware
• ValidationFilter
• ExceptionMiddleware

Controllers
  ↓
Application Services
  ↓
SensitiveWordMatcher
  ↓
Trie Data Structure (In-Memory Engine)
  ↓
Repositories
  ↓
Stored Procedures
  ↓
SQL Server
```

The project follows **Clean Architecture principles**, ensuring clear separation of responsibilities.

| Layer | Responsibility |
|------|----------------|
| API | Controllers, middleware, filters |
| Application | Business logic and services |
| Domain | Core models and algorithms |
| Infrastructure | Database access and repositories |

---

## ⚡ Performance Highlights

Sensitive word detection uses a **Trie (Prefix Tree)** algorithm.

Compared with naive string matching:

| Approach       | Complexity |
| -------------- | ---------- |
| Naive scanning | O(N × M)   |
| Trie matching  | **O(N)**   |

Where:

* **N** = length of input text
* **M** = number of sensitive words

This allows extremely fast in-memory pattern matching even with large dictionaries.

---

# Trie Algorithm Performance

The sensitive word detection engine uses a **Trie (prefix tree)** data structure to efficiently match multiple patterns within text.

Unlike naive string comparison approaches that repeatedly scan the text for each word, the Trie allows scanning the input **only once**.

## Why Trie?

A Trie is ideal for pattern matching when:

- Multiple keywords must be detected
- Fast lookup is required
- Patterns share common prefixes
- Real-time processing is required

This makes it well suited for:

- Content moderation
- Chat filtering
- Input sanitization
- Security filtering

---

## Complexity Analysis

Let:

N = length of input text  
M = number of sensitive words  
K = average word length

### Trie Construction

Sensitive words are loaded into the Trie during application startup.

Time Complexity: O(M × K)
Space Complexity: O(M × K)


This operation occurs **once at startup**, not during every request.

---

### Text Sanitization

During request processing, the input text is scanned character-by-character.


Time Complexity: O(N)
Space Complexity: O(1)


Because the Trie traversal happens in memory, the algorithm avoids repeated string comparisons.

---

## Performance Advantages

Compared with naive approaches:

| Approach | Complexity |
|--------|-------------|
Naive word scanning | O(N × M) |
Trie matching | **O(N)** |

This means performance remains **stable even with large dictionaries of sensitive words**.

---

# System Design Considerations

This service was designed with **production-ready system design principles**.

### Startup Data Loading

```mermaid
flowchart TD

DB[(SQL Server)]
Repo[Repository]
Loader[SensitiveWordEngineLoader]
Trie[Trie Data Structure\n(In-Memory Engine)]

DB --> Repo
Repo --> Loader
Loader --> Trie
```
---

## Stateless API

The API is stateless, meaning it does not store session data.

Benefits:

- Horizontal scalability
- Load balancing across instances
- Cloud-native deployment

---

## In-Memory Trie Engine

Sensitive words are loaded into memory during application startup using the `SensitiveWordEngineLoader`.

Database → Repository → Engine Loader → Trie → Request Processing

Benefits:

- Eliminates repeated database queries
- Extremely fast pattern matching
- Low latency request processing
- Predictable O(N) text scanning performance

---

## Request Processing Pipeline

Client
  ↓
ASP.NET Controller
  ↓
Application Service
  ↓
Trie Matcher
  ↓
Repositories
  ↓
SQL Server


The API layer handles HTTP concerns while the **Application layer manages business logic**.

---

## Database Interaction

Database access is isolated in the **Infrastructure layer**.

The application interacts with SQL Server through:

- Dapper
- Stored procedures
- Repository pattern

Benefits:

- Clear separation of concerns
- Testability
- Replaceable infrastructure

---

# Design Tradeoffs

This system prioritizes **runtime performance and simplicity** for detecting sensitive words in large volumes of text.

## In-Memory Trie vs Database Lookups

Sensitive words are loaded into memory during application startup using the `SensitiveWordEngineLoader`.

### Advantages

- Extremely fast lookup performance
- O(N) text scanning using the Trie structure
- No database queries during request processing
- Predictable latency under load

### Tradeoffs

- Updates to sensitive words require rebuilding the Trie
- Increased memory usage for large dictionaries
- Application restart may be required if dynamic updates are not implemented

This design favors **high-performance request processing**, which is ideal for content filtering systems where sensitive word lists change infrequently.

---
# Future Improvements

Although designed for a technical assessment, the system can be extended for real-world deployments.

### Scalability Improvements

- Redis caching
- Distributed Trie updates
- Kubernetes deployment
- Horizontal API scaling

---

### Security Enhancements

- Authentication and authorization
- API rate limiting
- Request throttling
- Web Application Firewall

---

### Observability

Potential improvements include:

- OpenTelemetry tracing
- Prometheus metrics
- Centralized logging with ELK stack

---

# Summary

This project demonstrates a **production-grade backend service** implementing:

- Clean Architecture
- Trie-based high-performance text filtering
- RESTful API design
- SQL Server integration
- Automated testing
- CI/CD pipeline
- Docker containerization
- Comprehensive documentation

The design emphasizes **performance, maintainability, and scalability**, reflecting best practices expected from a **Senior Software Developer**.

---

# Project Structure

```
SensitiveWordsService
│
├── .github
│   └── workflows
│       └── tests.yml
│
├── database
│   ├── migrations
│   │   └── init.sql
│   │
│   ├── procedures
│   │   └── stored_procedures.sql
│   │
│   └── seeds
│       └── seed_sensitive_words.sql
│
├── docs
│   ├── coverage
│   │   └── badge_linecoverage.svg
│   │
│   └── images
│       ├── architecture-diagram.png
│       └── swagger-preview.png
│
├── src
│   ├── SensitiveWords.Api
│   │
│   │   ├── Configuration
│   │   │   ├── ControllerConfiguration.cs
│   │   │   ├── EndpointConfiguration.cs
│   │   │   ├── HealthChecksConfiguration.cs
│   │   │   ├── MiddlewareConfiguration.cs
│   │   │   ├── RateLimitingConfiguration.cs
│   │   │   ├── SwaggerConfiguration.cs
│   │   │   ├── ValidationConfiguration.cs
│   │   │   └── VersioningConfiguration.cs
│   │
│   │   ├── Controllers
│   │   │   ├── SanitizerController.cs
│   │   │   └── SensitiveWordsController.cs
│   │
│   │   ├── Extensions
│   │   │   ├── HttpContextExtensions.cs
│   │   │   └── ValidationExtensions.cs
│   │
│   │   ├── Filters
│   │   │   └── ValidationFilter.cs
│   │
│   │   ├── Middleware
│   │   │   ├── CorrelationIdMiddleware.cs
│   │   │   ├── ExceptionMiddleware.cs
│   │   │   └── RequestLoggingMiddleware.cs
│   │
│   │   ├── Swagger
│   │   │   └── Examples
│   │   │       ├── BadRequestExample.cs
│   │   │       ├── CreateSensitiveWordExample.cs
│   │   │       ├── DuplicateSensitiveWordExample.cs
│   │   │       ├── InternalServerErrorExample.cs
│   │   │       ├── NotFoundExample.cs
│   │   │       ├── SanitizeRequestExample.cs
│   │   │       └── SanitizeResponseExample.cs
│   │
│   │   ├── appsettings.json
│   │   ├── appsettings.Development.json
│   │   └── Program.cs
│
│   ├── SensitiveWords.Application
│   │
│   │   ├── Algorithms
│   │   │   └── Trie
│   │   │       ├── SensitiveWordTrie.cs
│   │   │       └── TrieNode.cs
│   │   │
│   │   ├── DTOs
│   │   │   ├── Sanitization
│   │   │   │   ├── SanitizeRequest.cs
│   │   │   │   └── SanitizeResponse.cs
│   │   │   │
│   │   │   └── SensitiveWords
│   │   │       ├── CreateSensitiveWordRequest.cs
│   │   │       ├── SensitiveWordResponse.cs
│   │   │       └── UpdateSensitiveWordRequest.cs
│   │
│   │   ├── Exceptions
│   │   │   ├── DuplicateSensitiveWordException.cs
│   │   │   └── NotFoundException.cs
│   │
│   │   ├── HealthChecks
│   │   │   └── TrieHealthCheck.cs
│   │
│   │   ├── Interfaces
│   │   │   ├── IDbConnectionFactory.cs
│   │   │   ├── ISanitizationService.cs
│   │   │   ├── ISensitiveWordEngine.cs
│   │   │   ├── ISensitiveWordRepository.cs
│   │   │   └── ISensitiveWordService.cs
│   │
|   |   |── Common
│   │   │   ├── Policies
│   │   │   │   └── PollyPolicies.cs
│   │   │   │
│   │   ├── Services
│   │   │   ├── Engine
│   │   │   │   ├── SensitiveWordEngine.cs
│   │   │   │   └── SensitiveWordEngineLoader.cs
│   │   │   │
│   │   │   ├── SanitizationService.cs
│   │   │   └── SensitiveWordService.cs
│   │
│   │   ├── Validators
│   │   │   ├── CreateSensitiveWordRequestValidator.cs
│   │   │   └── SanitizeRequestValidator.cs
│
│   ├── SensitiveWords.Domain
│   │   └── Entities
│   │       └── SensitiveWord.cs
│
│   └── SensitiveWords.Infrastructure
│       ├── Database
│       │   ├── DbConnectionFactory.cs
│       │   ├── SqlErrorCodes.cs
│       │   └── StoredProcedures.cs
│       │
│       ├── DependencyInjection
│       │   └── InfrastructureServiceRegistration.cs
│       │
│       └── Repositories
│           └── SensitiveWordRepository.cs
│
├── tests
│   └── SensitiveWords.Tests
│
│       ├── Integration
│       │   └── Controllers
│       │       ├── SanitizerControllerTests.cs
│       │       └── SensitiveWordsControllerTests.cs
│       │
│       ├── TestHelpers
│       │   ├── HttpResponseExtensions.cs
│       │   ├── CustomWebApplicationFactory.cs
│       │   └── IntegrationTestBase.cs
│       │
│       ├── TestUtilities
│       │   ├── InMemorySensitiveWordRepository.cs
│       │   └── SensitiveWordEngineFake.cs
│       │
│       └── Unit
│           ├── Algorithms
│           ├── HealthChecks
│           ├── Middleware
│           ├── Services
│           └── Validators
│
├── docker-compose.yml
├── Dockerfile
│
├── README.md
├── ARCHITECTURE_DIAGRAMS.md
├── DESIGN_RATIONALE.md
├── RUNNING_THE_PROJECT.md
├── TESTING.md
└── API_EXAMPLES.md
```

---

# Database Setup

The application uses **SQL Server** and interacts with the database through **stored procedures using Dapper**.

Database scripts are located in:

```
database/
```

### Setup Steps

1. Run `init.sql` to create tables  
2. Run `stored_procedures.sql` to create stored procedures  
3. Run `seed_sensitive_words.sql` to insert initial sensitive words  

Example connection string:

```json
"ConnectionStrings": {
  "DefaultConnection": "Server=localhost;Database=SensitiveWordsDb;Trusted_Connection=True;TrustServerCertificate=True"
}
```

---

# Technology Stack

### Backend

- ASP.NET Core (.NET 9)
- C#

### Database

- SQL Server
- Stored Procedures
- Dapper

### Libraries

- FluentValidation
- Swashbuckle (Swagger)

### Testing

- xUnit
- Moq
- FluentAssertions
- Coverlet

---

# Project Documentation

This folder contains detailed documentation for the **Sensitive Words Service** project.

## Documentation Index


### Assessment Requirements

The original assessment documents are included in this repository:

- [Assessment Specification](docs/assessment-requirements/Interview-SqlWords.pdf)
- [Sensitive Word List](docs/assessment-requirements/sql_sensitive_list.txt)
### Architecture

- [Architecture Diagrams](ARCHITECTURE_DIAGRAMS.md)
- [Design Rationale](DESIGN_RATIONALE.md)

### Project Setup

- [Running the Project](RUNNING_THE_PROJECT.md)
- [Project Structure](PROJECT_STRUCTURE.md)

### Development

- [Testing Strategy](TESTING.md)
- [API Examples](API_EXAMPLES.md)

### Assets

Images used in documentation are located in:

### Coverage Badge

![Coverage](docs/coverage/badge_linecoverage.svg)

---

# Production Considerations

Although this project was created for a technical assessment, it was designed with **production-ready principles**.

### Scalability

The API is **stateless**, allowing horizontal scaling across multiple instances.

Possible improvements:

- Redis distributed caching
- Background refresh of sensitive words
- Kubernetes deployment

---

### Security

Security practices implemented:

- Parameterized stored procedures
- Input validation using FluentValidation
- Centralized exception handling
- Controlled database access
- Structured logging

Future improvements:

- API authentication and authorization
- Rate limiting
- Web Application Firewall

---

### Observability

The system supports:

- Structured logging
- Correlation ID request tracing
- Health checks

Example endpoints:

```
/health/live
/health/ready
```

Future improvements:

- OpenTelemetry tracing
- Prometheus metrics
- Centralized logging

---

### Reliability

The service ensures reliability by:

- Loading sensitive words during application startup
- Verifying database connectivity with health checks
- Using standardized error responses

---

# Author

**Ndiphiwe Nombula**  
Senior Software Developer (C#)

LinkedIn: https://www.linkedin.com/in/ndiphiwe-nombula-23a88b4b/
GitHub: https://github.com/Ndipza
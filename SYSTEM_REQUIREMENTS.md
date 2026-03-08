# System Requirements

This document outlines the runtime environment, tools, dependencies, and configuration required to run the Sensitive Words Service.

---

# Runtime Requirements

The system requires the following runtime environments:

| Component  | Version                  |
| ---------- | ------------------------ |
| .NET       | .NET 9 SDK               |
| SQL Server | SQL Server 2019 or later |

### Verify .NET Installation

```bash
dotnet --version
```

Expected output:

```
9.x.x
```

---

# Development Tools

The following development tools are recommended:

| Tool               | Purpose                           |
| ------------------ | --------------------------------- |
| Visual Studio 2022 | Full IDE support                  |
| JetBrains Rider    | Lightweight .NET IDE              |
| Docker             | Optional containerized deployment |

---

# Database Requirements

The system requires a SQL Server database named:

```
SensitiveWordsDb
```

### Required Database Objects

* SensitiveWords table
* Stored procedures:

```
spSensitiveWords_GetAll
spSensitiveWords_GetById
spSensitiveWords_Insert
spSensitiveWords_Update
spSensitiveWords_Delete
```

These stored procedures handle all CRUD operations for sensitive words.

---

# Application Dependencies

The API relies on the following NuGet packages:

| Package                | Purpose                             |
| ---------------------- | ----------------------------------- |
| Dapper                 | Lightweight ORM for database access |
| FluentValidation       | Request validation                  |
| Swashbuckle.AspNetCore | Swagger/OpenAPI documentation       |

---

# Network Configuration

The API runs locally on the following endpoints.

### API Base URL

```
https://localhost:5004
```

### Swagger Documentation

```
https://localhost:7228/swagger
```

Swagger provides:

* Interactive API documentation
* Request/response testing
* Schema definitions

---

# Optional Docker Support

The application can optionally run inside Docker.

Example Docker command:

```bash
docker build -t sensitivewords-api .
docker run -p 5004:5004 sensitivewords-api
```

This enables containerized deployments and easier environment replication.

---

# Minimum System Requirements

| Resource | Requirement      |
| -------- | ---------------- |
| RAM      | 4 GB             |
| CPU      | 2 cores          |
| Disk     | 500 MB available |

These requirements are sufficient for development and testing environments.

---

# Recommended Environment

For optimal performance during development:

| Resource | Recommended |
| -------- | ----------- |
| RAM      | 8 GB        |
| CPU      | 4 cores     |
| Disk     | SSD         |

---

# Environment Configuration

The application configuration is stored in:

```
appsettings.json
```

Key configuration sections include:

* Database connection strings
* Logging configuration
* API settings

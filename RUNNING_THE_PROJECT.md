# RUNNING_THE_PROJECT.md

This guide explains how to set up and run the **Sensitive Words Service** locally.

---

# Prerequisites

Ensure the following tools are installed:

- .NET 9 SDK
- SQL Server 2019 or later
- Git
- (Optional) Docker

---

# Step 1 — Clone the Repository

```bash
git clone https://github.com/Ndipza/SensitiveWordsService.git
cd SensitiveWordsService
```

---

# Step 2 — Create the Database

Create a database named:

```
SensitiveWordsDb
```

---

# Step 3 — Initialize Database Schema

Run the initialization script:

```
database/init.sql
```

This script creates:

- `SensitiveWords` table
- Required indexes

### Table Structure

| Column | Type | Description |
|------|------|-------------|
| Id | INT | Primary key |
| Word | NVARCHAR(100) | Sensitive word |
| CreatedAt | DATETIME | Creation timestamp |

A **unique constraint** prevents duplicate entries.

---

# Step 4 — Create Stored Procedures

Run the following script:

```
database/stored_procedures.sql
```

Stored procedures used by the API:

| Procedure | Purpose |
|----------|---------|
| spSensitiveWords_GetAll | Retrieve all sensitive words |
| spSensitiveWords_GetById | Retrieve a word by ID |
| spSensitiveWords_Insert | Insert a new word |
| spSensitiveWords_Update | Update a word |
| spSensitiveWords_Delete | Delete a word |

These procedures enforce:

- Duplicate prevention
- Input normalization
- SQL error handling using `THROW`

---

# Step 5 — Seed Initial Data

Run:

```
database/seed_sensitive_words.sql
```

This inserts example sensitive words:

```
SELECT
DROP
DELETE
TRUNCATE
```

These values are loaded into the **Trie structure during application startup**.

---

# Step 6 — Configure Connection String

Edit the following file:

```
appsettings.json
```

Example configuration:

```json
"ConnectionStrings": {
  "DefaultConnection": "Server=localhost;Database=SensitiveWordsDb;Trusted_Connection=True;TrustServerCertificate=True"
}
```

---

# Step 7 — Run the API

Start the application:

```bash
dotnet run --project src/SensitiveWords.Api
```

---

# API Endpoints

Once the API starts, it will be available at:

```
https://localhost:5004
```

Swagger UI:

```
https://localhost:7228/swagger
```

---

# Application Startup Flow

When the API starts:

1. `SensitiveWordEngineLoader` runs as a hosted service
2. Sensitive words are loaded from the database
3. Words are inserted into the Trie
4. The API becomes ready to process requests

---

# Available API Endpoints

| Method | Endpoint | Description |
|------|---------|-------------|
| GET | /api/v1/sensitive-words | Retrieve all sensitive words |
| POST | /api/v1/sensitive-words | Add a sensitive word |
| PUT | /api/v1/sensitive-words/{id} | Update a sensitive word |
| DELETE | /api/v1/sensitive-words/{id} | Remove a sensitive word |
| POST | /api/v1/sanitizer | Sanitize text input |

---

# Example Request

### Endpoint

```
POST /api/v1/sanitizer
```

### Request Body

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

# Error Response Format

Errors follow the **RFC7807 ProblemDetails** standard.

Example:

```json
{
 "title": "Duplicate sensitive word",
 "status": 409,
 "detail": "Sensitive word 'SELECT' already exists."
}
```

---

# Health Check Endpoints

The service exposes health endpoints:

```
/health/live
/health/ready
```

The readiness check verifies:

- Database connectivity
- Trie initialization

---
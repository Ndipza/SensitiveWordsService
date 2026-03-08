# Architecture Diagrams

## High-Level Architecture

```
Client
   ↓
API Controllers
   ↓
Application Services
   ↓
Domain Models / Algorithms
   ↓
Caching Layer
   ↓
Infrastructure Repositories
   ↓
SQL Server
```

### Responsibilities

**Client**

* Sends HTTP requests to the API.

**API Controllers**

* Handle HTTP requests and responses.
* Use validation filters.
* Call Application Services.

**Application Services**

* Implement business logic.
* Coordinate domain algorithms and repositories.
* Example: `SanitizationService`, `SensitiveWordService`.

**Domain Models / Algorithms**

* Core algorithm logic.
* Example: `Trie`, `SensitiveWordMatcher`.

**Caching Layer**

* Stores sensitive words in memory to avoid repeated database calls.
* Improves sanitization performance.

**Infrastructure Repositories**

* Data access layer.
* Handles CRUD operations for sensitive words.

**SQL Server**

* Persistent storage of sensitive words.

---

# Request Pipeline

```
HTTP Request
   ↓
CorrelationIdMiddleware
   ↓
RequestLoggingMiddleware
   ↓
Global Exception Middleware
   ↓
ValidationFilter
   ↓
Controller
   ↓
Application Service
   ↓
Repository / Cache
   ↓
Database
   ↓
HTTP Response (ProblemDetails or Success)
```

### Pipeline Components

**CorrelationIdMiddleware**

* Assigns a unique request ID for traceability.

**RequestLoggingMiddleware**

* Logs request metadata and execution time.

**Global Exception Middleware**

* Catches unhandled exceptions.
* Returns standardized `ProblemDetails` responses.

**ValidationFilter**

* Validates request DTOs before reaching controllers.

**Controllers**

* Entry point for API endpoints.

**Services**

* Execute business logic and algorithmic processing.

**Repositories**

* Retrieve and store sensitive words.

---

# Sanitization Flow

```
Input Text
   ↓
SanitizationService
   ↓
SensitiveWordEngine
   ↓
SensitiveWordMatcher
   ↓
Trie Data Structure
   ↓
Sensitive Word Detection
   ↓
Masking Logic
   ↓
Sanitized Output
```

### Flow Explanation

**SanitizationService**

* Orchestrates the sanitization process.

**SensitiveWordEngine**

* Loads sensitive words into the Trie.
* Handles initialization and caching.

**SensitiveWordMatcher**

* Performs efficient pattern matching using the Trie structure.

**Trie**

* Enables fast prefix-based search for sensitive words.

**Masking Logic**

* Replaces detected sensitive words with `****`.

---

# Trie Data Structure

Example representation:

```
Root
 └── S
     └── E
         └── L
             └── E
                 └── C
                     └── T
                         └── (end)
```

Supports phrases such as:

```
SELECT
SELECT * FROM
DROP TABLE
DELETE FROM
```

### Why Trie?

The Trie allows:

* **O(n)** scanning of text
* Efficient **multi-word matching**
* Fast detection of **prefix-based patterns**
* Ideal for **sensitive word filtering systems**

---

# Component Diagram

```
+---------------------------+
|        API Layer          |
|  Controllers              |
|  Filters                  |
|  Middleware               |
+------------+--------------+
             |
             ↓
+---------------------------+
|     Application Layer     |
|  SanitizationService      |
|  SensitiveWordService     |
+------------+--------------+
             |
             ↓
+---------------------------+
|        Domain Layer       |
|  Trie                     |
|  SensitiveWordMatcher     |
|  SensitiveWordEngine      |
+------------+--------------+
             |
             ↓
+---------------------------+
|     Infrastructure Layer  |
|  SensitiveWordRepository  |
|  Database Context         |
+------------+--------------+
             |
             ↓
+---------------------------+
|        Data Storage       |
|        SQL Server         |
+---------------------------+
```

### Layered Design Benefits

* Clear **separation of concerns**
* Improved **testability**
* Easier **maintenance and scalability**
* Aligns with **Clean Architecture principles**

---

# Sensitive Word Detection Algorithm

```
Start
  ↓
Receive Input Text
  ↓
Load Trie Structure
  ↓
Iterate Through Text Characters
  ↓
Check Trie Prefix Match
  ↓
Is Sensitive Word Found?
   ↓            ↓
  Yes           No
   ↓             ↓
Mask Word      Continue Scan
   ↓             ↓
Continue Processing
  ↓
Return Sanitized Text
  ↓
End
```

### Algorithm Characteristics

* Linear scanning of the input text
* Efficient prefix matching using Trie
* Supports multi-word sensitive phrases
* Minimizes database calls by using in-memory structures

---

# Performance Considerations

### Trie-Based Matching

* Avoids repeated string comparisons
* Allows scanning the input text only once

### In-Memory Caching

* Sensitive words are loaded once and reused
* Reduces database queries

### Stateless API Design

* Enables horizontal scaling
* Works well with containerized environments

---

# Scalability Considerations

The architecture supports future improvements such as:

* Distributed caching (Redis)
* Horizontal scaling with containers
* Rate limiting and API gateways
* Observability with structured logging and tracing

---

# Future Improvements

Possible future enhancements include:

* Aho–Corasick algorithm for even faster multi-pattern matching
* Redis distributed cache
* Admin dashboard for managing sensitive words
* Metrics and monitoring with OpenTelemetry

---

# Sequence Diagram: Sanitize Request

```
Client
  |
  | POST /sanitize
  ↓
Controller
  |
  | Validate Request
  ↓
SanitizationService
  |
  | Use Engine
  ↓
SensitiveWordEngine
  |
  | Provide Trie
  ↓
SensitiveWordMatcher
  |
  | Scan Input Text
  ↓
Trie
  |
  | Match Sensitive Words
  ↓
SensitiveWordMatcher
  |
  | Apply Masking
  ↓
SanitizationService
  |
  | Return Sanitized Text
  ↓
Controller
  |
  | HTTP 200 Response
  ↓
Client
```

### Sequence Explanation

1. Client sends a **sanitize request**.
2. Controller validates the request.
3. The request is passed to **SanitizationService**.
4. The service retrieves the **Trie structure** from `SensitiveWordEngine`.
5. `SensitiveWordMatcher` scans the input text.
6. Matches are found using the Trie.
7. Sensitive words are masked.
8. The sanitized text is returned to the client.

---

# Database Schema

```
+-------------------------+
|     SensitiveWords      |
+-------------------------+
| Id (PK)                 |
| Word                    |
| CreatedAt               |
| UpdatedAt               |
+-------------------------+
```

### Table Description

**SensitiveWords**

* `Id` – Unique identifier for the sensitive word.
* `Word` – The sensitive word or phrase to be filtered.
* `CreatedAt` – Timestamp when the word was added.
* `UpdatedAt` – Timestamp when the word was last modified.

---

# Entity Relationship Diagram

```
SensitiveWords
     |
     | stored in
     ↓
SQL Server
     |
     | loaded by
     ↓
SensitiveWordRepository
     |
     | used by
     ↓
SensitiveWordEngine
     |
     | powers
     ↓
SensitiveWordMatcher
```

---

# Observability Flow

```
Request
  ↓
CorrelationIdMiddleware
  ↓
Structured Logging
  ↓
Application Services
  ↓
Exception Middleware
  ↓
ProblemDetails Response
```

### Observability Goals

* Track each request with a **CorrelationId**
* Provide **structured logs for debugging**
* Enable easier **production troubleshooting**
* Support integration with tools such as:

  * ELK Stack
  * Seq
  * OpenTelemetry

---

# Deployment Architecture

```
+-------------------+
|      Client       |
|  Web / Mobile     |
+---------+---------+
          |
          | HTTPS
          ↓
+-------------------+
|    ASP.NET API    |
|  Docker Container |
+---------+---------+
          |
          | Dependency Injection
          ↓
+-------------------+
| Application Layer |
| Services          |
+---------+---------+
          |
          ↓
+-------------------+
| Domain Algorithms |
| Trie + Matcher    |
+---------+---------+
          |
          ↓
+-------------------+
| Repository Layer  |
+---------+---------+
          |
          ↓
+-------------------+
|     SQL Server    |
|   SensitiveWords  |
+-------------------+
```

### Deployment Notes

* API can run inside **Docker containers**.
* The application is **stateless**, enabling horizontal scaling.
* SQL Server stores sensitive words persistently.
* Sensitive words are loaded into memory at application startup.

---

# Sensitive Word Initialization Flow

```
Application Start
        ↓
Load Sensitive Words
        ↓
SensitiveWordRepository
        ↓
Retrieve Words from Database
        ↓
SensitiveWordEngine
        ↓
Build Trie Structure
        ↓
Store Trie in Memory
        ↓
Ready for Requests
```

### Initialization Explanation

1. The application starts.
2. Sensitive words are loaded from the database.
3. The `SensitiveWordEngine` builds the Trie.
4. The Trie is stored in memory.
5. The system becomes ready to process sanitization requests.

This ensures:

* Fast runtime performance
* Minimal database access
* Efficient multi-word detection

---

# System Design Summary

The Sensitive Words Service is designed with the following architectural goals:

### Performance

* Trie-based pattern matching
* In-memory sensitive word caching
* Linear scanning of input text

### Maintainability

* Clean architecture separation
* Repository pattern
* Dependency injection

### Scalability

* Stateless API
* Container-ready deployment
* Compatible with distributed caching

### Reliability

* Centralized exception handling
* Standardized `ProblemDetails` responses
* Structured request logging

---

# Architecture Principles Used

The system follows several industry best practices:

* **Clean Architecture**
* **Separation of Concerns**
* **Dependency Injection**
* **Repository Pattern**
* **Middleware Pipeline Pattern**

These practices ensure the system remains **testable, scalable, and maintainable**.

---

# Trade-offs & Design Decisions

Designing the Sensitive Words Service required balancing **performance, simplicity, and maintainability**. Below are the key design choices and their trade-offs.

## Why Trie Instead of Simple String Matching?

**Decision:** Use a Trie-based algorithm for word detection.

**Benefits:**

* Enables **O(n)** scanning of input text.
* Efficient for **multiple pattern searches**.
* Supports **phrases and prefixes** such as `SELECT * FROM`.

**Trade-off:**

* Slightly higher memory usage due to the Trie structure.

**Reasoning:**
Since the service may process many requests and potentially large inputs, Trie provides **better scalability than repeated string comparisons**.

---

## Why Load Sensitive Words Into Memory?

**Decision:** Load sensitive words once during application startup.

**Benefits:**

* Eliminates repeated database queries.
* Significantly improves request latency.
* Enables fast in-memory matching.

**Trade-off:**

* Requires rebuilding the Trie when sensitive words change.

**Reasoning:**
Sanitization is a **read-heavy operation**, making caching an effective optimization.

---

## Why Use the Repository Pattern?

**Decision:** Abstract data access behind repositories.

**Benefits:**

* Improves **testability**.
* Allows database implementations to change without affecting business logic.
* Keeps services focused on domain logic.

**Trade-off:**

* Slightly more abstraction layers.

**Reasoning:**
This approach aligns with **Clean Architecture and SOLID principles**.

---

## Why Use Middleware for Logging and Exceptions?

**Decision:** Centralize cross-cutting concerns in middleware.

**Benefits:**

* Avoids duplicate logging logic in controllers.
* Ensures consistent error responses.
* Simplifies observability.

**Trade-off:**

* Requires understanding the ASP.NET middleware pipeline.

**Reasoning:**
Middleware is the **recommended approach in ASP.NET Core** for handling cross-cutting concerns.

---

## Why Stateless API Design?

**Decision:** Ensure the API remains stateless.

**Benefits:**

* Enables **horizontal scaling**.
* Works well with **containerized deployments**.
* Simplifies load balancing.

**Trade-off:**

* Requires external storage for persistent state.

**Reasoning:**
Stateless services are easier to scale in **cloud-native environments**.

---

## Summary of Key Decisions

| Decision            | Reason                                 |
| ------------------- | -------------------------------------- |
| Trie Algorithm      | Fast multi-word detection              |
| In-Memory Caching   | Reduce database load                   |
| Repository Pattern  | Maintain clean architecture            |
| Middleware Pipeline | Centralized logging and error handling |
| Stateless API       | Enables horizontal scaling             |

These design decisions ensure the system is **performant, maintainable, and scalable**, while keeping the implementation straightforward and production-ready.

---

# Security Considerations

The Sensitive Words Service includes several security practices to ensure the API and database remain protected and resilient.

## Input Validation

* All incoming requests are validated using DTO validation filters.
* Prevents malformed or malicious input from reaching the service layer.

## SQL Injection Protection

* The system uses **parameterized stored procedures** instead of dynamic SQL.
* Parameters such as `@Word` and `@Id` are strongly typed.

This prevents SQL injection attacks.

## Error Handling

* Database errors use controlled `THROW` statements with custom error codes.
* The API converts these into standardized **ProblemDetails** responses.

Example:

* `50001` – Sensitive word already exists
* `50002` – Sensitive word not found

## Data Normalization

Words are normalized before storage:

```
SET @Word = UPPER(LTRIM(RTRIM(@Word)))
```

Benefits:

* Prevents duplicates caused by casing differences.
* Ensures consistent comparison during sanitization.

## Logging and Traceability

* Requests include **Correlation IDs**.
* Logs help trace issues in production environments.

---

# Stored Procedure Design

The database layer uses **stored procedures for all CRUD operations** on sensitive words. This approach provides:

* Better security
* Controlled database access
* Consistent validation rules
* Improved maintainability

Each procedure follows a consistent structure.

---

## Design Principles

### 1. Idempotent Deployment

Each procedure begins with:

```
IF OBJECT_ID('ProcedureName', 'P') IS NOT NULL
DROP PROCEDURE ProcedureName
```

This ensures the script can be executed multiple times during deployments.

---

### 2. SET NOCOUNT ON

All procedures include:

```
SET NOCOUNT ON
```

Benefits:

* Prevents extra row count messages.
* Reduces network traffic between SQL Server and the API.
* Improves performance for high-frequency operations.

---

### 3. Input Normalization

Before inserting or updating values, the word is normalized.

```
SET @Word = UPPER(LTRIM(RTRIM(@Word)))
```

This ensures:

* Case-insensitive consistency
* Removal of accidental whitespace

---

### 4. Duplicate Prevention

Before inserting or updating records, the procedure checks if the word already exists.

Example:

```
IF EXISTS (
    SELECT 1
    FROM SensitiveWords
    WHERE Word = @Word
)
```

This prevents duplicate sensitive words from being stored.

---

### 5. Explicit Error Handling

Errors are raised using SQL Server's `THROW` statement.

Example:

```
THROW 50001, 'Sensitive word already exists.', 1;
```

Benefits:

* Provides clear error codes
* Allows the API layer to translate errors into user-friendly responses

---

# Stored Procedure Overview

## Get All Sensitive Words

Purpose:

Retrieve all sensitive words for loading into the Trie structure.

```
spSensitiveWords_GetAll
```

Usage in application:

* Called during **Trie initialization**
* Used to populate the in-memory sensitive word engine

---

## Get Sensitive Word By Id

Purpose:

Retrieve a specific sensitive word.

```
spSensitiveWords_GetById
```

Used by:

* Admin operations
* Validation checks

---

## Insert Sensitive Word

Purpose:

Add a new sensitive word to the database.

Key features:

* Input normalization
* Duplicate prevention
* Returns the new record ID

```
spSensitiveWords_Insert
```

---

## Update Sensitive Word

Purpose:

Update an existing sensitive word.

Validation performed:

* Record existence check
* Duplicate prevention

```
spSensitiveWords_Update
```

---

## Delete Sensitive Word

Purpose:

Remove a sensitive word from the database.

Validation:

* Ensures the record exists before deletion

```
spSensitiveWords_Delete
```

---

# Database Design Benefits

Using stored procedures provides several advantages:

### Security

* Prevents direct table manipulation
* Allows controlled database access

### Performance

* Query plans are cached by SQL Server
* Reduced network traffic

### Maintainability

* Business rules centralized in the database layer
* Easier to audit and manage changes

### Consistency

* Ensures validation logic is applied consistently across all operations

---

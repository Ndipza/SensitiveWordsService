# Project Documentation

This document explains the project structure, how to run the project, testing strategy, and API usage examples for the Sensitive Words Service.

---

# PROJECT_STRUCTURE.md

## Solution Structure

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

## Folder Responsibilities

### SensitiveWords.API

Contains the ASP.NET Core Web API entry point.

Responsibilities:

* Controllers
* Middleware
* Request pipeline configuration

---

### SensitiveWords.Application

Contains the business logic of the system.

Responsibilities:

* Application services
* Request/response DTOs
* Input validation

---

### SensitiveWords.Domain

Contains core domain logic and algorithms.

Responsibilities:

* Trie data structure
* Sensitive word matching
* Domain models

---

### SensitiveWords.Infrastructure

Handles external dependencies such as databases.

Responsibilities:

* Repository implementations
* Database access via Dapper

---

### SensitiveWords.Tests

Contains automated tests.

Responsibilities:

* Unit tests
* Integration tests

---

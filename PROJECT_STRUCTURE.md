# Project Documentation

This document explains the project structure, how to run the project, testing strategy, and API usage examples for the Sensitive Words Service.

---

# PROJECT_STRUCTURE.md

## Solution Structure

```
SensitiveWordsService
│
├── SensitiveWords.API
│   ├── Controllers
│   ├── Middleware
│   ├── Filters
│   └── Program.cs
│
├── SensitiveWords.Application
│   ├── Services
│   ├── DTOs
│   └── Validators
│
├── SensitiveWords.Domain
│   ├── Algorithms
│   │   ├── Trie
│   │   └── SensitiveWordMatcher
│   └── Models
│
├── SensitiveWords.Infrastructure
│   ├── Repositories
│   └── Database
│
├── SensitiveWords.Tests
│   ├── Unit
│   └── Integration
│
└── docs
    └── architecture
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

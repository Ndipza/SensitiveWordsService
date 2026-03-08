# Design Rationale

## Clean Architecture

The system separates responsibilities into four layers:

API
Application
Domain
Infrastructure

Benefits:

* maintainability
* testability
* clear separation of concerns

## Trie Algorithm

Sensitive words are stored in a Trie structure to allow fast prefix matching.

Advantages:

* O(n) text scanning
* efficient prefix matching
* avoids repeated database calls

## Stored Procedures

Stored procedures were used to:

* encapsulate database logic
* enforce constraints
* improve query performance

## Exception Handling

All exceptions are handled through middleware to ensure consistent responses.

## Logging Strategy

Structured logging with correlation IDs enables request tracing and easier debugging.

## Validation

FluentValidation ensures request models are validated before reaching business logic.

## Abstraction

The SanitizationService depends on an abstraction (ISensitiveWordEngine) rather than a concrete implementation, enabling easier unit testing and adhering to the Dependency Inversion Principle.
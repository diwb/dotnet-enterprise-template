# Patterns

## Clean Architecture

Dependencies point inward. The API can change without forcing domain changes.

## DDD

`Customer` and `Order` are aggregate roots. `Address` is a value object. Order invariants are enforced inside the aggregate.

## CQRS

Application use cases are represented by MediatR requests:

- `CreateCustomerCommand`
- `GetCustomersQuery`
- `CreateOrderCommand`
- `LoginCommand`

## Result Pattern

Application handlers return `Result<T>` for expected failures, keeping validation and business errors out of exception flow.

## Validation

FluentValidation validates command shape before persistence.

## Soft Delete and Auditing

Entities inherit `AuditableEntity`, and `ApplicationDbContext.SaveChangesAsync` stamps audit fields.

## Middleware

The API includes correlation IDs and security headers as global middleware.

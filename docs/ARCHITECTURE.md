# Architecture

This template uses Clean Architecture with a strict dependency direction:

```text
Presentation -> Application -> Domain
Presentation -> Infrastructure -> Application
Presentation -> Persistence -> Application
Persistence -> Domain
```

## Layers

`Domain` contains entities, aggregates, value objects, enums and domain events. It has no framework dependency.

`Application` contains CQRS requests, handlers, validators, DTOs and contracts. It references EF Core abstractions for pragmatic query composition.

`Persistence` owns EF Core, SQL Server mappings, migrations, auditing and seed data.

`Infrastructure` owns cross-cutting adapters such as JWT token creation, password hashing and current-user resolution.

`Presentation` is the composition root and HTTP boundary. It configures authentication, authorization, CORS, rate limiting, health checks, Serilog and middleware.

## Domain

The sample domain represents B2B commerce:

- Customers with billing addresses
- Orders with items
- Payments
- Application users with roles and refresh tokens

The domain supports soft delete and audit metadata through `AuditableEntity`.

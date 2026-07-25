# Testing

The repository uses xUnit with separate unit and integration test projects.

## Test Projects

| Project | Purpose |
| --- | --- |
| `tests/UnitTests` | Fast tests for domain rules and infrastructure services |
| `tests/IntegrationTests` | API host bootstrapping and HTTP behavior |

## Current Coverage

- Order invariants.
- Payment capture behavior.
- Result pattern guard rails.
- Password hashing verification.
- Health endpoint and security/correlation headers.

## Running Tests

```powershell
dotnet test DotNetEnterpriseTemplate.slnx
```

With coverage:

```powershell
dotnet test DotNetEnterpriseTemplate.slnx --collect "XPlat Code Coverage" --results-directory TestResults
```

## Strategy

- Keep domain tests fast and deterministic.
- Use integration tests for middleware and HTTP behavior.
- Add SQL Server-backed tests when validating migrations, indexes or provider-specific queries.

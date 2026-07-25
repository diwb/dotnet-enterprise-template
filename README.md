# dotnet-enterprise-template

[![ci](https://github.com/your-org/dotnet-enterprise-template/actions/workflows/ci.yml/badge.svg)](https://github.com/your-org/dotnet-enterprise-template/actions/workflows/ci.yml)
[![.NET](https://img.shields.io/badge/.NET-10.0-512bd4)](https://dotnet.microsoft.com/)
[![License: MIT](https://img.shields.io/badge/License-MIT-green.svg)](LICENSE)

Enterprise-ready ASP.NET Core template using Clean Architecture, DDD, CQRS, EF Core, SQL Server, Docker and GitHub Actions.

## Overview

This repository is a production-oriented template for modern ASP.NET Core Web APIs. It models a B2B commerce domain with customers, orders, items, payments, users, refresh tokens, auditing, soft delete, pagination, filtering, sorting, validation, security and observability.

## Architecture

The solution follows Clean Architecture:

```text
src/
  Domain          Enterprise rules, aggregates, entities and domain events
  Application     CQRS requests, DTOs, validation and application contracts
  Infrastructure  JWT, password hashing and current-user services
  Persistence     EF Core DbContext, mappings, migrations and seed
  Presentation    ASP.NET Core API, controllers, middleware and composition root
  Shared          Result pattern and pagination primitives
tests/
  UnitTests
  IntegrationTests
```

## Technologies

- .NET 10
- ASP.NET Core Web API
- EF Core and SQL Server
- MediatR
- FluentValidation
- Serilog
- JWT bearer authentication
- Rate limiting, CORS and security headers
- xUnit
- Docker and Docker Compose
- GitHub Actions

## Run Locally

```powershell
dotnet restore DotNetEnterpriseTemplate.slnx
dotnet build DotNetEnterpriseTemplate.slnx
docker compose up -d sqlserver
dotnet ef database update --project src\Persistence\Persistence.csproj --startup-project src\Presentation\Presentation.csproj
dotnet run --project src\Presentation\Presentation.csproj
```

API defaults:

- HTTP: `http://localhost:5077`
- HTTPS: `https://localhost:7168`
- Health: `/health`
- Swagger UI: `/swagger`

Seeded administrator:

- Email: `admin@enterprise.local`
- Password: `Admin123!`

## Docker

```powershell
docker compose up --build
```

The compose file starts SQL Server and the API on port `8080`.

## Database

The default connection string targets local SQL Server:

```text
Server=localhost,1433;Database=DotNetEnterpriseTemplate;User Id=sa;Password=Your_strong_password123;TrustServerCertificate=True
```

Migrations live in `src/Persistence/Migrations`.

## Tests

```powershell
dotnet test DotNetEnterpriseTemplate.slnx
```

The integration test uses the `Testing` environment and avoids external SQL Server dependencies.

## Screenshots

Screenshots are intentionally placeholders until the first hosted release:

- `docs/assets/swagger-placeholder.png`
- `docs/assets/health-placeholder.png`

## Roadmap

See [docs/ROADMAP.md](docs/ROADMAP.md).

## Contributing

See [docs/CONTRIBUTING.md](docs/CONTRIBUTING.md).

## License

MIT. See [LICENSE](LICENSE).

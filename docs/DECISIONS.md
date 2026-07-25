# Decisions

## ADR-001: Use .NET 10

The local SDK is `10.0.110`, so the repository targets `net10.0` and GitHub Actions uses `10.0.x`.

## ADR-002: Use SQL Server as the default relational store

SQL Server is configured in Docker Compose and EF Core mappings use SQL Server-specific migration output.

## ADR-003: Keep Application pragmatic

The Application layer references EF Core abstractions to support efficient CQRS reads with `IQueryable`. This keeps handlers simple while preserving persistence implementation ownership in the Persistence layer.

## ADR-004: Seed an admin user

The template seeds `admin@enterprise.local` for local development. Production deployments must override credentials and JWT settings.

## ADR-005: Integration tests avoid external infrastructure

The `Testing` environment disables database health checks so CI can verify API bootstrapping without requiring SQL Server.

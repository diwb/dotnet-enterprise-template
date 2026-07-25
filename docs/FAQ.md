# FAQ

## Why is the sample domain commerce instead of a to-do list?

The goal is to demonstrate enterprise boundaries, invariants and workflows. Customers, orders and payments create better architectural pressure than trivial CRUD.

## Why does Application reference EF Core abstractions?

The template favors pragmatic CQRS handlers. Persistence implementation remains in `Persistence`, while Application can compose efficient read queries.

## Does this include a real refresh-token endpoint?

The domain and login flow persist refresh tokens. Rotation and revocation endpoints are listed in the roadmap.

## Can I run tests without SQL Server?

Yes. Integration tests use the `Testing` environment and avoid external database dependencies.

## Is this ready for production as-is?

It is a strong template baseline. Production use still requires secret management, environment-specific hardening, monitoring integration and deployment review.

## Why is there a seeded admin user?

The seeded user improves local onboarding. It must be changed or disabled in production deployments.

## Why are some roadmap items not implemented?

The repository is a template, not a full commerce platform. It intentionally demonstrates architectural patterns while leaving product-specific expansion to consuming teams.

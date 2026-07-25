# Security

## Supported Versions

Security fixes target the current `main` branch and the latest release.

## Reporting a Vulnerability

Open a private security advisory or contact the maintainers directly. Do not disclose exploitable issues publicly before a fix is available.

## Baseline Controls

- JWT bearer authentication
- Role-based authorization policies
- PBKDF2 password hashing
- Refresh-token persistence
- CORS allowlist
- Rate limiting
- Security response headers
- Correlation IDs for incident tracing
- Global exception handling with ProblemDetails
- Package vulnerability checks in CI

## Production Notes

Replace all development secrets, rotate seeded credentials, use managed secret storage and enable HTTPS at the edge.

## Secret Management

Do not commit production connection strings, JWT signing keys or credentials. Use environment variables, managed identity or a cloud secret manager.

## Logging Guidance

Avoid logging passwords, refresh tokens, access tokens, authorization headers, payment details or personal documents. Use correlation IDs to diagnose incidents without exposing sensitive values.

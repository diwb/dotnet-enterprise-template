# Deployment

This template is deployable as a containerized ASP.NET Core API.

## Required Configuration

Set these values per environment:

- `ConnectionStrings__DefaultConnection`
- `Jwt__Issuer`
- `Jwt__Audience`
- `Jwt__SigningKey`
- `Jwt__AccessTokenMinutes`
- `Cors__AllowedOrigins__0`

## Production Checklist

- Replace development secrets.
- Rotate seeded credentials or disable seed in production.
- Apply EF Core migrations through a controlled release process.
- Enforce HTTPS at the edge.
- Configure centralized logging.
- Configure health probes for `/health`.
- Run vulnerability checks before release.

## Container

The `Dockerfile` builds and publishes `src/Presentation`.

```powershell
docker build -t dotnet-enterprise-template .
docker run -p 8080:8080 dotnet-enterprise-template
```

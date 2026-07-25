# Troubleshooting

## NuGet restore fails

Check network access to `https://api.nuget.org/v3/index.json` and any corporate proxy configuration.

```powershell
dotnet restore DotNetEnterpriseTemplate.slnx
```

## EF Core CLI cannot create migrations

Install or update the EF Core tool:

```powershell
dotnet tool update --global dotnet-ef
```

Then run:

```powershell
dotnet ef migrations add MigrationName --project src\Persistence\Persistence.csproj --startup-project src\Presentation\Presentation.csproj --output-dir Migrations
```

## SQL Server container is not ready

SQL Server can take time to accept connections after the container starts. Check logs:

```powershell
docker compose logs sqlserver
```

## HTTPS certificate warnings

Trust the local development certificate:

```powershell
dotnet dev-certs https --trust
```

## Login fails locally

Confirm migrations were applied and seed ran. The local seeded account is:

- `admin@enterprise.local`
- `Admin123!`

## Docker config warning on Windows

If Docker reports that it cannot read `C:\Users\<user>\.docker\config.json`, fix local file permissions or restart Docker Desktop. This warning is host-specific and does not necessarily mean the compose file is invalid.

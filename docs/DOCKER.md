# Docker

Docker Compose provides a local SQL Server and API runtime.

## Start Everything

```powershell
docker compose up --build
```

## Start Only SQL Server

```powershell
docker compose up -d sqlserver
```

Then run the API locally:

```powershell
dotnet run --project src\Presentation\Presentation.csproj
```

## Services

| Service | Purpose | Port |
| --- | --- | --- |
| `api` | ASP.NET Core Web API | `8080` |
| `sqlserver` | SQL Server 2022 | `1433` |

## Data

SQL Server data is persisted in the `sqlserver-data` Docker volume.

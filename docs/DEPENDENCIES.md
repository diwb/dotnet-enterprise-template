# Dependencies

The template intentionally uses mainstream .NET ecosystem packages.

## Runtime Dependencies

| Package | Purpose |
| --- | --- |
| MediatR | In-process request dispatch |
| FluentValidation | Command validation |
| EF Core SQL Server | Relational persistence |
| Serilog.AspNetCore | Structured logging |
| Microsoft.AspNetCore.Authentication.JwtBearer | JWT authentication |
| Swashbuckle.AspNetCore | Swagger UI |

## Test Dependencies

| Package | Purpose |
| --- | --- |
| xUnit | Test framework |
| Microsoft.NET.Test.Sdk | Test execution |
| Microsoft.AspNetCore.Mvc.Testing | API integration tests |
| coverlet.collector | Coverage collection |

## Maintenance

Dependabot monitors NuGet and GitHub Actions. CI also runs `dotnet list package --vulnerable --include-transitive`.

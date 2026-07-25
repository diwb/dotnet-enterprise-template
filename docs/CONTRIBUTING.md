# Contributing

## Workflow

1. Create a branch with a semantic name.
2. Keep changes scoped.
3. Run `dotnet test DotNetEnterpriseTemplate.slnx`.
4. Update docs when behavior or architecture changes.
5. Open a pull request with a clear summary and test evidence.

## Commit Style

Use semantic commits:

- `feat:`
- `fix:`
- `refactor:`
- `docs:`
- `test:`
- `chore:`
- `build:`

## Quality Bar

Changes should compile without warnings, include tests appropriate to their risk and avoid leaking infrastructure concerns into the domain.

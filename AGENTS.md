# AGENTS.md — Parrillaguille

## Project Type

.NET 9 C# solution — restaurant sales system ("Parrillaguille").
Four-layer architecture: Entity → Data → Logic → Presentation.
Database: SQL Server (see `database.sql` for schema and seed data).

## Solution Structure

```
Parrillaguille.slnx          ← solution entry point
global.json                   ← pins .NET SDK 9.0.100
Capa_Entidad/                 ← POCO entities, no dependencies
Capa_de_datos/                ← repositories, SQL Server via Microsoft.Data.SqlClient
Capa_Logica/                  ← services (thin validation, delegates to repos)
Capa_Presentacion/            ← WinForms UI (net9.0-windows, Windows-only)
Capa_Presentacion_Avalonia/   ← experimental Avalonia UI (NOT in solution, no .csproj)
```

Dependency flow: `Capa_Entidad ← Capa_de_datos ← Capa_Logica ← Capa_Presentacion`.

## Key Commands

```bash
dotnet restore
dotnet build
dotnet run --project Capa_Presentacion   # WinForms entry point (Windows only)
```

No test projects exist. No CI/CD configuration.

## Database

- Engine: SQL Server
- Default connection string: `Server=localhost;Database=ParrillaguilleDB;Trusted_Connection=True;TrustServerCertificate=True;`
- Hardcoded in `Capa_de_datos/ConexionSQL.cs:16` — no config file, no environment variable.
- Schema + seed data: run `database.sql` against local SQL Server before first run.

## Important Gotchas

1. **Windows-only WinForms**: `Capa_Presentacion` targets `net9.0-windows`. It will NOT build or run on Linux/macOS. The Avalonia layer (`Capa_Presentacion_Avalonia`) exists as an alternative but is not wired into the solution and has no `.csproj` — treat it as incomplete/experimental.

2. **No DI container**: Services and repositories are manually instantiated with `new ConexionSQL()` everywhere. If you add features, follow the same pattern or introduce a proper DI container.

3. **No config system**: Connection strings are hardcoded. No `appsettings.json`, no `IConfiguration`. Changes to DB connection require editing `ConexionSQL.cs` directly.

4. **No tests**: There are zero test projects. If you add tests, there is no existing test infrastructure to follow.

5. **Solution file uses `.slnx` format** (XML-based), not the traditional `.sln`. Your editor/tooling must support `.slnx`.

## Code Conventions

- Spanish naming for entities, services, repositories, and UI (classes, methods, properties, form controls).
- English-only in code comments and XML doc comments.
- Repository pattern: each entity has a `*Repository` class in `Capa_de_datos`.
- Service pattern: each entity has a `*Service` class in `Capa_Logica` that wraps the repository.
- Entities are plain POCOs in `Capa_Entidad` with public get/set properties.
- SQL is inline in repositories (no ORM, no Dapper).

## Adding Features

1. Define entity in `Capa_Entidad/`.
2. Create repository in `Capa_de_datos/` (follow `ProductoRepository` as reference).
3. Create service in `Capa_Logica/` with validation (follow `ProductoService`).
4. Add UI in `Capa_Presentacion/` (WinForms) or `Capa_Presentacion_Avalonia/` (if cross-platform is needed).

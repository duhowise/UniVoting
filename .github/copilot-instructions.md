# Copilot Instructions for UniVoting

## Project Overview

UniVoting is a Windows desktop application suite for managing institutional (university) elections. It consists of multiple WPF applications and supporting libraries built with C# and .NET Framework.

## Solution Structure

```
UniVoting.sln
└── Src/
    ├── UniVoting.Model       – Domain entities (Candidate, Voter, Vote, Position, Rank, User, etc.)
    ├── UniVoting.Data        – Data access layer using Dapper + SimpleCRUD; repository pattern
    ├── UniVoting.Services    – Business logic (VotingService, ElectionConfigurationService, LiveViewService, SystemEventLoggerService)
    ├── UniVoting.Core        – Shared utilities; uses Entity Framework Core
    ├── UniVoting.Client      – WPF voter-facing application (casts votes, uses SQLite)
    ├── UniVoting.Admin       – WPF administrator application (election setup, voter management, reporting)
    ├── UniVoting.LiveView    – WPF real-time results display application
    ├── UniVoting.Report      – Telerik reporting definitions
    └── UniVoting.Reporting   – Report generation and management logic
```

## Tech Stack

- **Language:** C# targeting .NET Framework 4.5.2–4.8
- **UI Framework:** WPF with MahApps.Metro v1.6 and MaterialDesignThemes v2.3
- **ORM:** Dapper v1.50 + SimpleCRUD v1.13 (primary), Entity Framework 6 and EF Core 2.1 (in Core project)
- **Database:** SQLite via System.Data.SQLite v1.0.108
- **IoC Container:** Autofac v4.6
- **Reporting:** Telerik Reporting v10.2 with ReportViewer.Wpf
- **Reactive Extensions:** System.Reactive v4.0 preview, Akavache v6.0 (Client caching)
- **Other:** Newtonsoft.Json v11, ExcelDataReader v3.4 (Admin import), ControlzEx v3.0

## Building

Open `UniVoting.sln` in Visual Studio (2015 or later) and build with MSBuild. NuGet packages must be restored first:

```
nuget restore UniVoting.sln
msbuild UniVoting.sln /p:Configuration=Debug
```

Target platforms include AnyCPU, x86, and x64. The solution has no GitHub Actions workflows; AppVeyor is the CI platform.

## Code Conventions

- **Namespaces/Classes/Methods/Properties:** PascalCase (e.g., `VotingService`, `CandidateName`, `GetVotes()`)
- **Interfaces:** Prefixed with `I` (e.g., `IRepository`, `IVotingService`)
- **Concrete implementations:** Placed in `Implementations/` subfolders
- **Interface definitions:** Placed in `Interfaces/` subfolders
- **WPF code-behind:** Paired XAML + XAML.cs files; views organized in feature subfolders (e.g., `Administrators/`, `Clients/`)
- No dedicated test projects exist in the solution

## Key Patterns

- **Repository pattern:** `Repository.cs` base class in `UniVoting.Data/Implementations/`; specialised repositories (e.g., `CandidateRepository`, `VoterRepository`) extend it
- **Service layer:** Services in `UniVoting.Services` orchestrate repositories and encapsulate business logic
- **Dependency injection:** Autofac is used for IoC wiring in WPF applications
- **Database initialization:** `DbManager.cs` in `UniVoting.Data` handles SQLite connection setup and schema migrations

## Important Notes

- All applications target Windows only (WPF); cross-platform changes are not applicable
- Telerik controls require a valid Telerik license; avoid replacing or removing Telerik dependencies
- Package versions are pinned in `packages.config` files; prefer keeping existing versions unless a security fix requires an upgrade
- When adding new models, follow the existing Dapper/SimpleCRUD attribute pattern seen in `UniVoting.Model`
- When adding new repository operations, extend the existing `Repository<T>` base class

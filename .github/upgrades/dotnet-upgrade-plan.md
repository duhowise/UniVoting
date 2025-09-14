# .NET 8.0 Upgrade Plan

## Execution Steps

Execute steps below sequentially one by one in the order they are listed.

1. Validate that a .NET 8.0 SDK required for this upgrade is installed on the machine and if not, help to get it installed.
2. Ensure that the SDK version specified in global.json files is compatible with the .NET 8.0 upgrade.
3. Upgrade Src\UniVoting.Model\UniVoting.Model.csproj
4. Upgrade Src\UniVoting.Data\UniVoting.Data.csproj
5. Upgrade Src\UniVoting.Services\UniVoting.Services.csproj
6. Upgrade Src\Univoting.Reporting\Univoting.Reporting.csproj
7. Upgrade Src\UniVoting.Admin\UniVoting.Admin.csproj
8. Upgrade Src\UniVoting.Client\UniVoting.Client.csproj
9. Upgrade Src\UniVoting.LiveView\UniVoting.LiveView.csproj

## Settings

This section contains settings and data used by execution steps.

### Excluded projects

No projects are excluded from this upgrade.

### Aggregate NuGet packages modifications across all projects

NuGet packages used across all selected projects or their dependencies that need version update in projects that reference them.

| Package Name                                     | Current Version           | New Version | Description                         |
|:-------------------------------------------------|:-------------------------:|:-----------:|:------------------------------------|
| ControlzEx                                       | 3.0.2.4                   | 6.0.0       | Recommended for .NET 8.0           |
| Dapper.SimpleCRUD                                | 1.13.0                    | 2.3.0       | Recommended for .NET 8.0           |
| Google.Protobuf                                  | 3.3.0                     | 3.32.1      | Security vulnerability              |
| MahApps.Metro                                    | 1.6.0-alpha0184           | 2.4.11      | Recommended for .NET 8.0           |
| MahApps.Metro.Resources                          | 0.6.1.0                   | 0.6.1       | Deprecated package replacement      |
| MahApps.Metro.SimpleChildWindow                  | 1.4.1                     | 2.2.1       | Recommended for .NET 8.0           |
| MaterialDesignColors                             | 1.1.3                     | 5.2.1       | Recommended for .NET 8.0           |
| MaterialDesignThemes                             | 2.3.2-ci1036              | 5.2.1       | Recommended for .NET 8.0           |
| MaterialDesignThemes.MahApps                     | 0.0.11                    | 5.2.1       | Recommended for .NET 8.0           |
| Microsoft.NETCore.Platforms                     | 2.0.1                     | 8.0.0       | Recommended for .NET 8.0           |
| Newtonsoft.Json                                  | 11.0.1-beta3              | 13.0.3      | Recommended for .NET 8.0           |
| SharpZipLib                                      | 0.86.0                    | 1.4.2       | Security vulnerability              |
| System.Data.SQLite                               | 1.0.108.0                 | 2.0.2       | Recommended for .NET 8.0           |
| System.Data.SQLite.Core                          | 1.0.108.0                 | 1.0.119     | Recommended for .NET 8.0           |
| System.Diagnostics.DiagnosticSource              | 4.4.1                     | 8.0.1       | Recommended for .NET 8.0           |
| System.Net.Http                                  | 4.3.3                     | 4.3.4       | Security vulnerability              |
| System.Text.RegularExpressions                   | 4.3.0                     | 4.3.1       | Security vulnerability              |

### Project upgrade details

This section contains details about each project upgrade and modifications that need to be done in the project.

#### UniVoting.Model modifications

Project properties changes:
  - Target framework should be changed from `.NETFramework,Version=v4.6.2` to `net8.0`
  - Convert project to SDK-style

NuGet packages changes:
  - Dapper.SimpleCRUD should be updated from `1.13.0` to `2.3.0` (*recommended for .NET 8.0*)

#### UniVoting.Data modifications

Project properties changes:
  - Target framework should be changed from `.NETFramework,Version=v4.6.2` to `net8.0`
  - Convert project to SDK-style

NuGet packages changes:
  - Dapper.SimpleCRUD should be updated from `1.13.0` to `2.3.0` (*recommended for .NET 8.0*)
  - Google.Protobuf should be updated from `3.3.0` to `3.32.1` (*security vulnerability*)

#### UniVoting.Services modifications

Project properties changes:
  - Target framework should be changed from `.NETFramework,Version=v4.6.2` to `net8.0-windows`
  - Convert project to SDK-style

NuGet packages changes:
  - ControlzEx should be updated from `3.0.2.4` to `6.0.0` (*recommended for .NET 8.0*)
  - Dapper.SimpleCRUD should be updated from `1.13.0` to `2.3.0` (*recommended for .NET 8.0*)
  - MahApps.Metro should be updated from `1.6.0-alpha0184` to `2.4.11` (*recommended for .NET 8.0*)

#### Univoting.Reporting modifications

Project properties changes:
  - Target framework should be changed from `.NETFramework,Version=v4.6.2` to `net8.0-windows`
  - Convert project to SDK-style

NuGet packages changes:
  - Google.Protobuf should be updated from `3.3.0` to `3.32.1` (*security vulnerability*)

#### UniVoting.Admin modifications

Project properties changes:
  - Target framework should be changed from `.NETFramework,Version=v4.6.2` to `net8.0-windows`
  - Convert project to SDK-style

NuGet packages changes:
  - ControlzEx should be updated from `3.0.2.4` to `6.0.0` (*recommended for .NET 8.0*)
  - Dapper.SimpleCRUD should be updated from `1.13.0` to `2.3.0` (*recommended for .NET 8.0*)
  - Google.Protobuf should be updated from `3.3.0` to `3.32.1` (*security vulnerability*)
  - MahApps.Metro should be updated from `1.6.0-alpha0184` to `2.4.11` (*recommended for .NET 8.0*)
  - MahApps.Metro.Resources should be replaced from `0.6.1.0` to `0.6.1` (*deprecated package replacement*)
  - MaterialDesignColors should be updated from `1.1.3` to `5.2.1` (*recommended for .NET 8.0*)
  - MaterialDesignThemes should be updated from `2.3.2-ci1036` to `5.2.1` (*recommended for .NET 8.0*)
  - MaterialDesignThemes.MahApps should be updated from `0.0.11` to `5.2.1` (*recommended for .NET 8.0*)
  - SharpZipLib should be updated from `0.86.0` to `1.4.2` (*security vulnerability*)

#### UniVoting.Client modifications

Project properties changes:
  - Target framework should be changed from `.NETFramework,Version=v4.6.2` to `net8.0-windows`
  - Convert project to SDK-style

NuGet packages changes:
  - ControlzEx should be updated from `3.0.2.4` to `6.0.0` (*recommended for .NET 8.0*)
  - Google.Protobuf should be updated from `3.3.0` to `3.32.1` (*security vulnerability*)
  - MahApps.Metro should be updated from `1.6.0-alpha0184` to `2.4.11` (*recommended for .NET 8.0*)
  - MahApps.Metro.Resources should be replaced from `0.6.1.0` to `0.6.1` (*deprecated package replacement*)
  - MahApps.Metro.SimpleChildWindow should be updated from `1.4.1` to `2.2.1` (*recommended for .NET 8.0*)
  - MaterialDesignColors should be updated from `1.1.3` to `5.2.1` (*recommended for .NET 8.0*)
  - MaterialDesignThemes should be updated from `2.3.2-ci1036` to `5.2.1` (*recommended for .NET 8.0*)
  - MaterialDesignThemes.MahApps should be updated from `0.0.11` to `5.2.1` (*recommended for .NET 8.0*)
  - Microsoft.NETCore.Platforms should be updated from `2.0.1` to `8.0.0` (*recommended for .NET 8.0*)
  - Newtonsoft.Json should be updated from `11.0.1-beta3` to `13.0.3` (*recommended for .NET 8.0*)
  - System.Data.SQLite should be updated from `1.0.108.0` to `2.0.2` (*recommended for .NET 8.0*)
  - System.Data.SQLite.Core should be updated from `1.0.108.0` to `1.0.119` (*recommended for .NET 8.0*)
  - System.Diagnostics.DiagnosticSource should be updated from `4.4.1` to `8.0.1` (*recommended for .NET 8.0*)
  - System.Net.Http should be updated from `4.3.3` to `4.3.4` (*security vulnerability*)
  - System.Text.RegularExpressions should be updated from `4.3.0` to `4.3.1` (*security vulnerability*)

Other changes:
  - Remove packages that are now included with framework reference (Microsoft.Win32.Primitives, NETStandard.Library, System.AppContext, and many others)
  - Handle incompatible packages: SQLitePCLRaw.provider.e_sqlite3.net45 and System.Data.SQLite.Linq have no supported versions and may need alternatives

#### UniVoting.LiveView modifications

Project properties changes:
  - Target framework should be changed from `.NETFramework,Version=v4.6.2` to `net8.0-windows`
  - Convert project to SDK-style

NuGet packages changes:
  - ControlzEx should be updated from `3.0.2.4` to `6.0.0` (*recommended for .NET 8.0*)
  - Google.Protobuf should be updated from `3.3.0` to `3.32.1` (*security vulnerability*)
  - MahApps.Metro should be updated from `1.6.0-alpha0184` to `2.4.11` (*recommended for .NET 8.0*)
  - MahApps.Metro.Resources should be replaced from `0.6.1.0` to `0.6.1` (*deprecated package replacement*)
  - MaterialDesignColors should be updated from `1.1.3` to `5.2.1` (*recommended for .NET 8.0*)
  - MaterialDesignThemes should be updated from `2.3.2-ci1036` to `5.2.1` (*recommended for .NET 8.0*)
  - MaterialDesignThemes.MahApps should be updated from `0.0.11` to `5.2.1` (*recommended for .NET 8.0*)
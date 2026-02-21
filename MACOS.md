# Running UniVoting on macOS

This document describes the current macOS compatibility status of each component and the steps needed to run or further migrate the application.

## Current Status

| Project | Target | macOS Compatible? |
|---|---|---|
| `UniVoting.Model` | `net9.0` | ✅ Yes |
| `UniVoting.Data` | `net9.0` | ✅ Yes (uses `Microsoft.Data.SqlClient`) |
| `UniVoting.Services` | `net9.0` | ✅ Yes |
| `UniVoting.Core` | `net9.0` | ✅ Yes |
| `UniVoting.Admin` | `net9.0-windows` (WPF) | ❌ UI requires migration |
| `UniVoting.Client` | `net9.0-windows` (WPF) | ❌ UI requires migration |
| `UniVoting.LiveView` | `net9.0-windows` (WPF) | ❌ UI requires migration |

---

## Prerequisites

### 1. .NET 9 SDK

Download and install from <https://dotnet.microsoft.com/download/dotnet/9>:

```bash
brew install --cask dotnet
```

### 2. SQL Server via Docker

The application requires SQL Server. On macOS, run it in Docker:

```bash
# Pull and start SQL Server 2022
docker run \
  -e 'ACCEPT_EULA=Y' \
  -e 'SA_PASSWORD=<YourStrongPassword>' \
  -p 1433:1433 \
  --name univoting-sql \
  -d mcr.microsoft.com/mssql/server:2022-latest
```

> **Note:** The SA password must meet SQL Server complexity requirements (upper, lower, digit, symbol, ≥8 chars). Do not commit real passwords to source control.

### 3. Connection String Configuration

Update each `App.config` to point to your local SQL Server instance.
The `App.config` files already use SQL Server authentication format (cross-platform).
Replace `YourPassword123!` with your actual SA password:

```xml
<add connectionString="Data Source=localhost,1433;Initial Catalog=VotingSystemV2;User ID=sa;Password=CHANGE_ME;TrustServerCertificate=True;"
     name="VotingSystem"
     providerName="Microsoft.Data.SqlClient"/>
```

Files to update:
- `Src/UniVoting.Data/App.config`
- `Src/UniVoting.Admin/App.config`
- `Src/UniVoting.Client/App.config`
- `Src/UniVoting.LiveView/App.config`

---

## Building the Backend Libraries on macOS

The backend projects (`Model`, `Data`, `Services`, `Core`) build and run on macOS natively:

```bash
dotnet build Src/UniVoting.Model/UniVoting.Model.csproj
dotnet build Src/UniVoting.Data/UniVoting.Data.csproj
dotnet build Src/UniVoting.Services/UniVoting.Services.csproj
dotnet build Src/UniVoting.Core/UniVoting.Core.csproj
```

The WPF UI projects can be **compiled** on macOS (with the `EnableWindowsTargeting` flag) but cannot be **executed** on macOS because WPF is a Windows-only UI framework.

---

## UI Migration Required: WPF → Avalonia UI

The three WPF application projects (`Admin`, `Client`, `LiveView`) use **Windows Presentation Foundation (WPF)**, which is a Windows-only UI technology and **cannot run on macOS**.

To run the UI on macOS, these projects must be migrated to a cross-platform UI framework. The recommended option is **[Avalonia UI](https://avaloniaui.net/)**, which:

- Supports Windows, macOS, and Linux
- Has very similar XAML syntax to WPF
- Has an active ecosystem and good WPF migration guides

### Migration Steps (Avalonia UI)

1. **Install Avalonia templates:**
   ```bash
   dotnet new install Avalonia.Templates
   ```

2. **Create new Avalonia projects** for each WPF app:
   ```bash
   dotnet new avalonia.app -o Src/UniVoting.Admin.Avalonia
   dotnet new avalonia.app -o Src/UniVoting.Client.Avalonia
   dotnet new avalonia.app -o Src/UniVoting.LiveView.Avalonia
   ```

3. **Reference the existing backend** (`Model`, `Data`, `Services`) from the new Avalonia projects — no changes needed in those libraries.

4. **Migrate XAML windows/pages** — Avalonia XAML is very close to WPF XAML; main differences:
   - `Window` stays `Window` (no `MetroWindow` equivalent needed — Avalonia has native-feeling windows on each OS)
   - `ui:SymbolIcon` (WPF-UI) → use `PathIcon` with geometry data, or the [Fluent Icons](https://avaloniaui.net/blog/2023/02/25/introducing-fluent-icons) built into Avalonia's Fluent theme
   - `ui:ThemesDictionary` / `ui:ControlsDictionary` → `<StyleInclude Source="avares://Avalonia.Themes.Fluent/FluentTheme.xaml"/>`
   - `System.Windows.*` namespaces → `Avalonia.*` namespaces
   - `code-behind` events use the same pattern (`Loaded`, `Click`, etc.)

5. **Replace WPF-specific packages:**
   - `WPF-UI` → remove (Avalonia has built-in Fluent styling)
   - `System.Drawing.Common` → use [SkiaSharp](https://github.com/mono/SkiaSharp) or [SixLabors.ImageSharp](https://github.com/SixLabors/ImageSharp) for image manipulation

### Avalonia Migration Resources

- [Avalonia WPF Compat docs](https://docs.avaloniaui.net/docs/next/get-started/wpf/)
- [WPF to Avalonia migration guide](https://docs.avaloniaui.net/docs/next/stay-up-to-date/wpf-migration-guide)
- [Avalonia samples](https://github.com/AvaloniaUI/Avalonia/tree/master/samples)

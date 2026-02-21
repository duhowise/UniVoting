# Running UniVoting on macOS

This document describes the current macOS compatibility status of each component and the steps needed to run or further migrate the application.

## Current Status

| Project | Target | macOS Compatible? |
|---|---|---|
| `UniVoting.Model` | `net9.0` | ✅ Yes |
| `UniVoting.Data` | `net9.0` | ✅ Yes (uses `Microsoft.Data.SqlClient`) |
| `UniVoting.Services` | `net9.0` | ✅ Yes |
| `UniVoting.Core` | `net9.0` | ✅ Yes |
| `UniVoting.Admin` | `net9.0` (Avalonia UI) | ✅ Yes |
| `UniVoting.Client` | `net9.0` (Avalonia UI) | ✅ Yes |
| `UniVoting.LiveView` | `net9.0` (Avalonia UI) | ✅ Yes |

---

## Prerequisites

### 1. .NET 9 SDK

```bash
brew install --cask dotnet
```

Or download from <https://dotnet.microsoft.com/download/dotnet/9>.

### 2. SQL Server via Docker

```bash
docker run \
  -e 'ACCEPT_EULA=Y' \
  -e 'SA_PASSWORD=<YourStrongPassword>' \
  -p 1433:1433 \
  --name univoting-sql \
  -d mcr.microsoft.com/mssql/server:2022-latest
```

> The SA password must meet SQL Server complexity requirements (upper, lower, digit, special char, ≥8 chars).

### 3. Connection String Configuration

Every `App.config` uses SQL Server authentication (no Integrated Security). Replace `CHANGE_ME` with your actual SA password in:

- `Src/UniVoting.Data/App.config`
- `Src/UniVoting.Admin/App.config`
- `Src/UniVoting.Client/App.config`
- `Src/UniVoting.LiveView/App.config`
- `Src/UniVoting.Model/App.config`

The connection string format in each file:

```xml
<add connectionString="Data Source=localhost,1433;Initial Catalog=VotingSystemV2;User ID=sa;Password=CHANGE_ME;TrustServerCertificate=True;"
     name="VotingSystem"
     providerName="Microsoft.Data.SqlClient"/>
```

> **Do not commit real credentials.** Use a local override file or your OS keychain.

---

## Building All Projects on macOS

All projects (including the UI apps) now build and run on macOS natively:

```bash
# Backend libraries
dotnet build Src/UniVoting.Model/UniVoting.Model.csproj
dotnet build Src/UniVoting.Data/UniVoting.Data.csproj
dotnet build Src/UniVoting.Services/UniVoting.Services.csproj
dotnet build Src/UniVoting.Core/UniVoting.Core.csproj

# UI apps (now Avalonia, runs on macOS)
dotnet build Src/UniVoting.Admin/UniVoting.Admin.csproj
dotnet build Src/UniVoting.Client/UniVoting.Client.csproj
dotnet build Src/UniVoting.LiveView/UniVoting.LiveView.csproj

# Or build everything at once:
dotnet build UniVoting.sln
```

To run an app:

```bash
dotnet run --project Src/UniVoting.Admin/UniVoting.Admin.csproj
dotnet run --project Src/UniVoting.Client/UniVoting.Client.csproj
dotnet run --project Src/UniVoting.LiveView/UniVoting.LiveView.csproj
```

---

## UI Migration: WPF → Avalonia UI (Completed)

The three app projects (`Admin`, `Client`, `LiveView`) have been migrated from WPF to **[Avalonia UI](https://avaloniaui.net/)** 11.3.12. All three projects now target `net9.0` (cross-platform) and build/run on Windows, macOS, and Linux.

The migration steps below are kept as reference documentation.

---

### Step 1 — Install Avalonia templates

```bash
dotnet new install Avalonia.Templates
```

---

### Step 2 — Replace each WPF .csproj with an Avalonia one

**Before** (`UniVoting.Admin.csproj`):

```xml
<Project Sdk="Microsoft.NET.Sdk">
  <PropertyGroup>
    <TargetFramework>net9.0-windows</TargetFramework>
    <OutputType>WinExe</OutputType>
    <UseWPF>true</UseWPF>
    <EnableWindowsTargeting>true</EnableWindowsTargeting>
  </PropertyGroup>
  <ItemGroup>
    <PackageReference Include="WPF-UI" Version="4.0.0" />
    <PackageReference Include="System.Drawing.Common" Version="9.0.0" />
  </ItemGroup>
</Project>
```

**After** (`UniVoting.Admin.csproj`):

```xml
<Project Sdk="Microsoft.NET.Sdk">
  <PropertyGroup>
    <TargetFramework>net9.0</TargetFramework>
    <OutputType>WinExe</OutputType>
    <Nullable>enable</Nullable>
    <BuiltInComInteropSupport>true</BuiltInComInteropSupport>
    <ApplicationManifest>app.manifest</ApplicationManifest>
  </PropertyGroup>
  <ItemGroup>
    <PackageReference Include="Avalonia" Version="11.2.5" />
    <PackageReference Include="Avalonia.Desktop" Version="11.2.5" />
    <PackageReference Include="Avalonia.Themes.Fluent" Version="11.2.5" />
    <PackageReference Include="Avalonia.Fonts.Inter" Version="11.2.5" />
    <!-- Image processing: replace System.Drawing.Common -->
    <PackageReference Include="SixLabors.ImageSharp" Version="3.1.6" />
    <!-- Keep existing backend references unchanged -->
    <PackageReference Include="Autofac" Version="8.2.0" />
    <PackageReference Include="Dapper" Version="2.1.35" />
    <PackageReference Include="Dapper.SimpleCRUD" Version="2.3.0" />
    <PackageReference Include="ExcelDataReader" Version="3.7.0" />
    <PackageReference Include="ExcelDataReader.DataSet" Version="3.7.0" />
  </ItemGroup>
  <ItemGroup>
    <ProjectReference Include="..\UniVoting.Data\UniVoting.Data.csproj" />
    <ProjectReference Include="..\UniVoting.Model\UniVoting.Model.csproj" />
    <ProjectReference Include="..\UniVoting.Services\UniVoting.Services.csproj" />
  </ItemGroup>
</Project>
```

---

### Step 3 — Migrate `App.xaml` → `App.axaml`

Rename `App.xaml` to `App.axaml` and replace the WPF-UI theme dictionaries with Avalonia's Fluent theme.

**Before** (`App.xaml`):

```xml
<Application x:Class="UniVoting.Admin.App"
             xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
             xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
             xmlns:ui="http://schemas.lepo.co/wpfui/2022/xaml">
  <Application.Resources>
    <ResourceDictionary>
      <ResourceDictionary.MergedDictionaries>
        <ui:ThemesDictionary Theme="Light" />
        <ui:ControlsDictionary />
      </ResourceDictionary.MergedDictionaries>
    </ResourceDictionary>
  </Application.Resources>
</Application>
```

**After** (`App.axaml`):

```xml
<Application x:Class="UniVoting.Admin.App"
             xmlns="https://github.com/avaloniaui"
             xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml">
  <Application.Styles>
    <FluentTheme />
  </Application.Styles>
</Application>
```

**Before** (`App.xaml.cs`):

```csharp
using System.Windows;
using Wpf.Ui.Appearance;

public partial class App : Application
{
    protected override void OnStartup(StartupEventArgs e)
    {
        ApplicationThemeManager.Apply(ApplicationTheme.Light);
        MainWindow = new AdminLoginWindow();
        MainWindow.Show();
        base.OnStartup(e);
    }
}
```

**After** (`App.axaml.cs`):

```csharp
using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;

public partial class App : Application
{
    public override void Initialize() => AvaloniaXamlLoader.Load(this);

    public override void OnFrameworkInitializationCompleted()
    {
        if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
            desktop.MainWindow = new AdminLoginWindow();
        base.OnFrameworkInitializationCompleted();
    }
}
```

Also add a `Program.cs` entry point (Avalonia requires one):

```csharp
using Avalonia;

class Program
{
    [STAThread]
    static void Main(string[] args) =>
        BuildAvaloniaApp().StartWithClassicDesktopLifetime(args);

    static AppBuilder BuildAvaloniaApp() =>
        AppBuilder.Configure<App>()
            .UsePlatformDetect()
            .WithInterFont()
            .LogToTrace();
}
```

---

### Step 4 — Migrate windows from `.xaml` → `.axaml`

Rename each `.xaml` file to `.axaml`. The XAML content changes minimally.

**Before** (`AdminLoginWindow.xaml`):

```xml
<Window x:Class="UniVoting.Admin.Administrators.AdminLoginWindow"
        xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
        xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
        xmlns:ui="http://schemas.lepo.co/wpfui/2022/xaml"
        Title="UNIVOTING" WindowState="Maximized">
    <Grid>
        <StackPanel Grid.Row="0">
            <Label Foreground="{DynamicResource SystemAccentColorBrush}" FontSize="42">UNIVOTING</Label>
        </StackPanel>
        <StackPanel Grid.Row="1" HorizontalAlignment="Center">
            <Grid>
                <!-- WPF-UI icon -->
                <ui:SymbolIcon Symbol="Person24" Width="50" Height="50"
                               Foreground="{DynamicResource SystemAccentColorBrush}" />
                <TextBox Name="Username" FontSize="24" Margin="10" />
                <ui:SymbolIcon Symbol="Key24" Width="50" Height="50"
                               Foreground="{DynamicResource SystemAccentColorBrush}" />
                <PasswordBox Name="Password" FontSize="24" Margin="10" />
                <Button Name="BtnLogin" Content="Login" />
            </Grid>
        </StackPanel>
    </Grid>
</Window>
```

**After** (`AdminLoginWindow.axaml`):

```xml
<Window x:Class="UniVoting.Admin.Administrators.AdminLoginWindow"
        xmlns="https://github.com/avaloniaui"
        xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
        Title="UNIVOTING" WindowState="Maximized">
    <Grid>
        <StackPanel Grid.Row="0">
            <!-- Label → TextBlock (Avalonia's Label is simpler; TextBlock is idiomatic) -->
            <TextBlock Foreground="{DynamicResource SystemAccentColor}" FontSize="42">UNIVOTING</TextBlock>
        </StackPanel>
        <StackPanel Grid.Row="1" HorizontalAlignment="Center">
            <Grid>
                <!-- ui:SymbolIcon → PathIcon or use Avalonia.UI.FluentIcons package -->
                <PathIcon Data="{StaticResource PersonRegular}" Width="50" Height="50"
                          Foreground="{DynamicResource SystemAccentColor}" />
                <TextBox Name="Username" FontSize="24" Margin="10" />
                <PathIcon Data="{StaticResource KeyRegular}" Width="50" Height="50"
                          Foreground="{DynamicResource SystemAccentColor}" />
                <!-- PasswordBox → TextBox with PasswordChar -->
                <TextBox Name="Password" PasswordChar="●" FontSize="24" Margin="10" />
                <Button Name="BtnLogin" Content="Login" />
            </Grid>
        </StackPanel>
    </Grid>
</Window>
```

> **Note:** For full Fluent icons, add the [Avalonia.UI.FluentIcons](https://github.com/davidortinau/FluentIcons.Avalonia) package:
> ```bash
> dotnet add package FluentIcons.Avalonia
> ```
> Then use `<fi:SymbolIcon Symbol="Person" />` (very similar to WPF-UI).

---

### Step 5 — Migrate code-behind files

**Namespace changes** (applies to all code-behind files):

| WPF / WPF-UI | Avalonia |
|---|---|
| `using System.Windows;` | `using Avalonia;` |
| `using System.Windows.Controls;` | `using Avalonia.Controls;` |
| `using System.Windows.Media;` | `using Avalonia.Media;` |
| `using System.Windows.Media.Imaging;` | `using Avalonia.Media.Imaging;` |
| `using System.Windows.Input;` | `using Avalonia.Input;` |
| `using Wpf.Ui.Appearance;` | *(remove — Avalonia themes are set in App.axaml)* |

**Class base** — stays `Window`, `Page`, or `UserControl` (same names in Avalonia).

**Before** (`AdminLoginWindow.xaml.cs`):

```csharp
using System.Windows;
using MahApps.Metro.Controls.Dialogs; // already removed in this repo
using UniVoting.Model;
using UniVoting.Services;

public partial class AdminLoginWindow : Window
{
    public AdminLoginWindow()
    {
        InitializeComponent();
        BtnLogin.IsDefault = true;   // <-- WPF-only
        Username.Focus();
        BtnLogin.Click += BtnLogin_Click;
    }

    private async void BtnLogin_Click(object sender, RoutedEventArgs e)
    {
        var admin = await ElectionConfigurationService.Login(...);
        if (admin != null) { new MainWindow(admin).Show(); Close(); }
        else MessageBox.Show("Wrong username or password.", "Login Error");
    }
}
```

**After** (`AdminLoginWindow.axaml.cs`):

```csharp
using Avalonia.Controls;
using Avalonia.Interactivity;
using UniVoting.Model;
using UniVoting.Services;

public partial class AdminLoginWindow : Window
{
    public AdminLoginWindow()
    {
        InitializeComponent();
        // IsDefault → handled by KeyBinding in AXAML or explicitly:
        // BtnLogin.IsDefault = true; // not available in Avalonia; bind Enter key instead
        Username.Focus();
        BtnLogin.Click += BtnLogin_Click;
    }

    private async void BtnLogin_Click(object sender, RoutedEventArgs e)
    {
        var admin = await ElectionConfigurationService.Login(...);
        if (admin != null) { new MainWindow(admin).Show(); Close(); }
        else await MessageBox.ShowAsync("Wrong username or password.", "Login Error"); // use a dialog library
    }
}
```

> For `MessageBox.Show`, use the [MessageBox.Avalonia](https://github.com/AvaloniaCommunity/MessageBox.Avalonia) package:
> ```bash
> dotnet add package MessageBox.Avalonia
> ```

---

### Step 6 — Replace `System.Drawing.Common` image helpers

`System.Drawing.Common` (used in `Admin/Util.cs` for `ResizeImage`, `ConvertImage`, `BitmapToImageSource`) is Windows-only. Replace with [SixLabors.ImageSharp](https://github.com/SixLabors/ImageSharp):

**Before** (`Util.cs`):

```csharp
using System.Drawing;
using System.Drawing.Imaging;

public static Bitmap ResizeImage(System.Drawing.Image image, int width, int height)
{
    var destImage = new Bitmap(width, height);
    using var graphics = Graphics.FromImage(destImage);
    graphics.DrawImage(image, 0, 0, width, height);
    return destImage;
}
```

**After** (`Util.cs` with ImageSharp):

```csharp
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;

public static byte[] ResizeImageBytes(byte[] imageBytes, int width, int height)
{
    using var image = Image.Load(imageBytes);
    image.Mutate(x => x.Resize(width, height));
    using var ms = new MemoryStream();
    image.SaveAsPng(ms);
    return ms.ToArray();
}
```

---

### Summary of package replacements

| WPF package (remove) | Avalonia replacement (add) |
|---|---|
| `WPF-UI` | `Avalonia`, `Avalonia.Desktop`, `Avalonia.Themes.Fluent`, `Avalonia.Fonts.Inter` |
| `System.Drawing.Common` | `SixLabors.ImageSharp` |
| *(WPF-UI icons)* | `FluentIcons.Avalonia` (optional) |
| `MessageBox.Show` | `MessageBox.Avalonia` |

---

### Avalonia Migration Resources

- [Avalonia WPF compatibility docs](https://docs.avaloniaui.net/docs/next/get-started/wpf/)
- [WPF to Avalonia migration guide](https://docs.avaloniaui.net/docs/next/stay-up-to-date/wpf-migration-guide)
- [Avalonia samples](https://github.com/AvaloniaUI/Avalonia/tree/master/samples)
- [FluentIcons.Avalonia](https://github.com/davidortinau/FluentIcons.Avalonia)
- [MessageBox.Avalonia](https://github.com/AvaloniaCommunity/MessageBox.Avalonia)

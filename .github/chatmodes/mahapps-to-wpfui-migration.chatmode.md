# MahApps.Metro to WPF UI Migration Chatmode

## Overview
This chatmode provides comprehensive guidance for migrating WPF applications from MahApps.Metro to WPF UI (lepoco/wpfui), focusing on the UniVoting electronic voting system codebase patterns and requirements.

## Migration Context

### Source Framework: MahApps.Metro
- **Current Version**: 2.4.11
- **Package**: MahApps.Metro + MaterialDesignThemes.MahApps
- **Design**: Metro/Modern UI design language
- **Controls**: MetroWindow, MetroDialog, TransitioningContentControl, etc.
- **Theming**: Accent colors, light/dark themes, resource dictionaries

### Target Framework: WPF UI
- **Latest Version**: 4.0.3
- **Package**: Wpf.Ui
- **Design**: Windows 11 Fluent Design System
- **Controls**: FluentWindow, NavigationView, Card, etc.
- **Theming**: Modern Windows 11 styling, Mica effects, system themes

## Migration Phases

### Phase 1: Package and References Migration
**Duration**: 1-2 days

#### Remove MahApps.Metro Packages
```xml
<!-- Remove these packages -->
<PackageReference Include="MahApps.Metro" Version="2.4.11" />
<PackageReference Include="MahApps.Metro.Resources" Version="0.6.1" />
<PackageReference Include="MahApps.Metro.SimpleChildWindow" Version="1.4.1" />
<PackageReference Include="MaterialDesignColors" Version="5.2.1" />
<PackageReference Include="MaterialDesignThemes" Version="5.2.1" />
<PackageReference Include="MaterialDesignThemes.MahApps" Version="5.2.1" />
<PackageReference Include="ControlzEx" Version="4.4.0" />
```

#### Add WPF UI Package
```xml
<!-- Add this package -->
<PackageReference Include="Wpf.Ui" Version="4.0.3" />
```

#### Update Using Statements
**Before (MahApps.Metro)**:
```csharp
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
using MahApps.Metro.Theming;
using MaterialDesignThemes.Wpf;
```

**After (WPF UI)**:
```csharp
using Wpf.Ui;
using Wpf.Ui.Controls;
using Wpf.Ui.Appearance;
using Wpf.Ui.Extensions;
```

### Phase 2: App.xaml Resource Dictionary Migration
**Duration**: 1 day

#### Remove MahApps.Metro Resources
**Before**:
```xml
<Application.Resources>
    <ResourceDictionary>
        <ResourceDictionary.MergedDictionaries>
            <!-- MahApps.Metro resources -->
            <ResourceDictionary Source="pack://application:,,,/MahApps.Metro;component/Styles/Controls.xaml" />
            <ResourceDictionary Source="pack://application:,,,/MahApps.Metro;component/Styles/Fonts.xaml" />
            <ResourceDictionary Source="pack://application:,,,/MahApps.Metro;component/Styles/Colors.xaml" />
            <ResourceDictionary Source="pack://application:,,,/MahApps.Metro;component/Styles/Accents/Blue.xaml" />
            <ResourceDictionary Source="pack://application:,,,/MahApps.Metro;component/Styles/Accents/BaseLight.xaml" />
            <!-- MaterialDesign resources -->
            <ResourceDictionary Source="pack://application:,,,/MaterialDesignThemes.Wpf;component/Themes/MaterialDesignTheme.Light.xaml"/>
            <ResourceDictionary Source="pack://application:,,,/MaterialDesignThemes.Wpf;component/Themes/MaterialDesignTheme.Defaults.xaml"/>
            <ResourceDictionary Source="pack://application:,,,/MaterialDesignThemes.MahApps;component/Themes/MaterialDesignTheme.MahApps.Fonts.xaml" />
        </ResourceDictionary.MergedDictionaries>
    </ResourceDictionary>
</Application.Resources>
```

#### Add WPF UI Resources
**After**:
```xml
<Application 
    xmlns:ui="http://schemas.lepo.co/wpfui/2022/xaml">
    <Application.Resources>
        <ResourceDictionary>
            <ResourceDictionary.MergedDictionaries>
                <ui:ThemesDictionary Theme="Dark" />
                <ui:ControlsDictionary />
            </ResourceDictionary.MergedDictionaries>
        </ResourceDictionary>
    </Application.Resources>
</Application>
```

### Phase 3: Window Base Class Migration
**Duration**: 2-3 days

#### MetroWindow → FluentWindow Migration

**Before (MahApps.Metro)**:
```xml
<Controls:MetroWindow x:Class="UniVoting.Client.ClientsLoginWindow"
                      xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
                      xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
                      xmlns:Controls="http://metro.mahapps.com/winfx/xaml/controls"
                      xmlns:materialDesign="http://materialdesigninxaml.net/winfx/xaml/themes"
                      WindowState="Maximized"
                      Background="{DynamicResource WhiteColorBrush}"
                      Title="UNIVOTING">
```

**After (WPF UI)**:
```xml
<ui:FluentWindow x:Class="UniVoting.Client.ClientsLoginWindow"
                 xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
                 xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
                 xmlns:ui="http://schemas.lepo.co/wpfui/2022/xaml"
                 WindowState="Maximized"
                 WindowBackdropType="Mica"
                 Title="UNIVOTING">
```

#### Code-Behind Migration
**Before**:
```csharp
public partial class ClientsLoginWindow : MetroWindow
{
    public ClientsLoginWindow()
    {
        InitializeComponent();
        // MahApps initialization
    }
}
```

**After**:
```csharp
public partial class ClientsLoginWindow : FluentWindow
{
    public ClientsLoginWindow()
    {
        InitializeComponent();
        
        // Apply WPF UI theme
        Wpf.Ui.Appearance.ApplicationThemeManager.Apply(this);
    }
}
```

### Phase 4: Dialog System Migration
**Duration**: 2-3 days

#### MetroDialog → ContentDialog Migration

**Before (MahApps.Metro)**:
```csharp
private async void ShowConfirmationDialog()
{
    var metroWindow = Window.GetWindow(this) as MetroWindow;
    var result = await metroWindow.ShowMessageAsync(
        "Confirm Vote", 
        "Are you sure you want to cast this vote?",
        MessageDialogStyle.AffirmativeAndNegative);
        
    if (result == MessageDialogResult.Affirmative)
    {
        // Process vote
    }
}
```

**After (WPF UI)**:
```csharp
private async void ShowConfirmationDialog()
{
    var dialog = new ContentDialog
    {
        Title = "Confirm Vote",
        Content = "Are you sure you want to cast this vote?",
        PrimaryButtonText = "Yes",
        SecondaryButtonText = "No"
    };

    var result = await dialog.ShowAsync(ContentDialogPlacement.Popup);
    
    if (result == ContentDialogResult.Primary)
    {
        // Process vote
    }
}
```

### Phase 5: Control Migration
**Duration**: 3-4 days

#### Control Mapping Reference

| MahApps.Metro Control | WPF UI Equivalent | Migration Notes |
|----------------------|-------------------|-----------------|
| `MetroWindow` | `FluentWindow` | Base window class |
| `TransitioningContentControl` | `ContentPresenter` with animations | Custom transitions needed |
| `Tile` | `Card` | Similar card-based layout |
| `ToggleSwitch` | `ToggleSwitch` | Direct equivalent |
| `NumericUpDown` | `NumberBox` | Enhanced number input |
| `DatePicker` | `DatePicker` | Enhanced date picker |
| `TimePicker` | `TimePicker` | Enhanced time picker |
| `ColorPicker` | `ColorPicker` | Modern color selection |
| `ProgressRing` | `ProgressRing` | Direct equivalent |
| `Flyout` | `Flyout` | Context flyout menus |

#### MaterialDesign Cards → WPF UI Cards
**Before**:
```xml
<materialDesign:Card Margin="2" UniformCornerRadius="1" 
                     Background="DodgerBlue" Padding="2">
    <StackPanel>
        <TextBlock Text="{Binding PositionName}" />
        <TextBlock Text="{Binding VoteCount}" />
    </StackPanel>
</materialDesign:Card>
```

**After**:
```xml
<ui:Card Margin="2" Padding="12">
    <StackPanel>
        <TextBlock Text="{Binding PositionName}" 
                   Style="{StaticResource SubtitleTextBlockStyle}" />
        <TextBlock Text="{Binding VoteCount}" 
                   Style="{StaticResource BodyTextBlockStyle}" />
    </StackPanel>
</ui:Card>
```

### Phase 6: Theme and Styling Migration
**Duration**: 2-3 days

#### Theme Manager Migration
**Before (MahApps.Metro)**:
```csharp
public static class ThemeManagerHelper
{
    public static void CreateAppStyleBy(Color color, bool isDarkTheme = false)
    {
        var theme = isDarkTheme ? BaseTheme.Dark : BaseTheme.Light;
        ThemeManager.Current.ChangeTheme(Application.Current, 
            ThemeManager.Current.AddTheme(RuntimeThemeGenerator.Current.GenerateRuntimeTheme(theme, color)));
    }
}
```

**After (WPF UI)**:
```csharp
public static class ThemeManagerHelper
{
    public static void ApplyTheme(ApplicationTheme theme, Color accentColor)
    {
        ApplicationThemeManager.Apply(theme);
        ApplicationAccentColorManager.Apply(accentColor);
    }
    
    public static void ApplySystemTheme()
    {
        ApplicationThemeManager.Apply(ApplicationTheme.Auto);
    }
}
```

#### Custom Accent Colors
**Before**:
```csharp
// Complex ResourceDictionary manipulation
var resourceDictionary = new ResourceDictionary();
resourceDictionary.Add("AccentColor", color);
// Multiple brush definitions...
```

**After**:
```csharp
// Simple accent color application
ApplicationAccentColorManager.Apply(Colors.DodgerBlue);
```

### Phase 7: Navigation Migration
**Duration**: 2-3 days

#### NavigationView Integration
**New WPF UI Pattern**:
```xml
<ui:FluentWindow>
    <ui:NavigationView x:Name="NavigationView" 
                       IsBackButtonVisible="Auto"
                       PaneDisplayMode="Left">
        <ui:NavigationView.MenuItems>
            <ui:NavigationViewItem Content="Dashboard" 
                                   Icon="{ui:SymbolIcon Home24}" />
            <ui:NavigationViewItem Content="Voting" 
                                   Icon="{ui:SymbolIcon Vote24}" />
            <ui:NavigationViewItem Content="Results" 
                                   Icon="{ui:SymbolIcon ChartMultiple24}" />
        </ui:NavigationView.MenuItems>
        
        <ui:NavigationView.FooterMenuItems>
            <ui:NavigationViewItem Content="Settings" 
                                   Icon="{ui:SymbolIcon Settings24}" />
        </ui:NavigationView.FooterMenuItems>
        
        <Frame x:Name="ContentFrame" />
    </ui:NavigationView>
</ui:FluentWindow>
```

### Phase 8: Icon and Asset Migration
**Duration**: 1-2 days

#### Icon System Migration
**Before (MaterialDesign)**:
```xml
<materialDesign:PackIcon Kind="Vote" Width="24" Height="24" />
```

**After (WPF UI)**:
```xml
<ui:SymbolIcon Symbol="Vote24" />
```

#### Icon Mapping Reference
| MaterialDesign Icon | WPF UI Symbol | Notes |
|-------------------|---------------|-------|
| `Vote` | `Vote24` | Voting icon |
| `AccountCircle` | `Person24` | User profile |
| `Settings` | `Settings24` | Settings gear |
| `Home` | `Home24` | Home/dashboard |
| `Chart` | `ChartMultiple24` | Charts/analytics |
| `Lock` | `Lock24` | Security/login |

### Phase 9: Animation and Transitions
**Duration**: 2-3 days

#### TransitioningContentControl Migration
**Before (MahApps.Metro)**:
```xml
<controls:TransitioningContentControl>
    <Grid>
        <!-- Content -->
    </Grid>
</controls:TransitioningContentControl>
```

**After (WPF UI with custom animations)**:
```xml
<ContentPresenter x:Name="ContentPresenter">
    <ContentPresenter.Triggers>
        <EventTrigger RoutedEvent="ContentPresenter.Loaded">
            <BeginStoryboard>
                <Storyboard>
                    <DoubleAnimation Storyboard.TargetProperty="Opacity" 
                                     From="0" To="1" Duration="0:0:0.3" />
                </Storyboard>
            </BeginStoryboard>
        </EventTrigger>
    </ContentPresenter.Triggers>
</ContentPresenter>
```

### Phase 10: Testing and Validation
**Duration**: 2-3 days

#### Validation Checklist
- [ ] All windows display correctly with FluentWindow
- [ ] Theme switching works properly
- [ ] Dialogs show and behave correctly
- [ ] Navigation flows are preserved
- [ ] Custom controls render properly
- [ ] Performance is maintained or improved
- [ ] Accessibility features work
- [ ] Touch interface compatibility
- [ ] High DPI support maintained

## Migration Patterns for UniVoting Codebase

### Identified Migration Points

#### 1. Window Classes
- `UniVoting.Admin.MainWindow` (MetroWindow → FluentWindow)
- `UniVoting.Client.ClientsLoginWindow` (MetroWindow → FluentWindow)
- `UniVoting.Client.MainWindow` (MetroWindow → FluentWindow)
- `UniVoting.LiveView.MainWindow` (MetroWindow → FluentWindow)

#### 2. Dialog Usage
- `CandidateControl.xaml.cs` - MetroDialog usage
- `ClientVotingPage.xaml.cs` - MetroDialog usage
- `AdminDispensePasswordWindow.xaml.cs` - MetroDialog usage

#### 3. Theme Management
- `ThemeManagerHelper.cs` classes in Admin, Client, and LiveView projects
- Custom accent color generation
- Resource dictionary management

#### 4. Control Usage
- MaterialDesign Cards in TileControl components
- TransitioningContentControl in LiveView
- Custom accent brushes and styles

### Automated Migration Script Template

```powershell
# PowerShell script for bulk text replacements
param(
    [string]$ProjectPath
)

# Replace using statements
$files = Get-ChildItem -Path $ProjectPath -Filter "*.cs" -Recurse
foreach ($file in $files) {
    $content = Get-Content $file.FullName
    $content = $content -replace "using MahApps\.Metro\.Controls;", "using Wpf.Ui.Controls;"
    $content = $content -replace "using MahApps\.Metro\.Controls\.Dialogs;", "using Wpf.Ui.Controls;"
    $content = $content -replace ": MetroWindow", ": FluentWindow"
    Set-Content -Path $file.FullName -Value $content
}

# Replace XAML namespaces
$xamlFiles = Get-ChildItem -Path $ProjectPath -Filter "*.xaml" -Recurse
foreach ($file in $xamlFiles) {
    $content = Get-Content $file.FullName
    $content = $content -replace "xmlns:Controls=""http://metro\.mahapps\.com/winfx/xaml/controls""", "xmlns:ui=""http://schemas.lepo.co/wpfui/2022/xaml"""
    $content = $content -replace "Controls:MetroWindow", "ui:FluentWindow"
    $content = $content -replace "materialDesign:Card", "ui:Card"
    Set-Content -Path $file.FullName -Value $content
}

Write-Host "Automated migration completed. Manual review and adjustments required."
```

## Benefits of Migration

### Technical Advantages
1. **Modern Design**: Windows 11 Fluent Design System
2. **Better Performance**: Optimized for modern WPF
3. **Native Feel**: Matches Windows 11 system apps
4. **Improved Accessibility**: Better screen reader support
5. **Mica Effects**: Modern backdrop materials
6. **Enhanced Icons**: Fluent System Icons
7. **Touch Support**: Better touch interface support

### Development Benefits
1. **Active Development**: WPF UI is actively maintained
2. **Better Documentation**: Comprehensive docs and samples
3. **Community Support**: Growing community and ecosystem
4. **Future-Proof**: Aligned with Microsoft's design direction
5. **Less Dependencies**: Fewer package dependencies
6. **Better Theming**: Simplified theme management

## Risks and Mitigation

### Potential Issues
1. **Breaking Changes**: Control APIs may differ
2. **Custom Styling**: Existing styles need rework
3. **Animation Loss**: Some transitions may need recreation
4. **Third-party Integration**: MaterialDesign dependencies
5. **Learning Curve**: Team needs to learn new patterns

### Mitigation Strategies
1. **Phased Migration**: Migrate one project at a time
2. **Feature Parity Check**: Ensure all features work
3. **UI Testing**: Comprehensive visual testing
4. **Team Training**: Provide WPF UI training sessions
5. **Rollback Plan**: Keep MahApps.Metro version in branches

## Timeline and Resource Allocation

### Estimated Timeline: 3-4 weeks
- **Week 1**: Packages, resources, and base window migration
- **Week 2**: Control and dialog migration
- **Week 3**: Theme and styling migration
- **Week 4**: Testing, validation, and polish

### Required Resources
- **1 Senior Developer**: Lead migration effort
- **1 UI/UX Designer**: Validate visual consistency
- **1 QA Tester**: Comprehensive testing
- **Team Training**: 2-3 training sessions

## Success Criteria

### Functional Requirements
- [ ] All existing functionality preserved
- [ ] UI remains visually consistent
- [ ] Performance maintained or improved
- [ ] Accessibility compliance maintained
- [ ] Touch interface support preserved

### Technical Requirements
- [ ] Successful compilation with WPF UI
- [ ] All unit tests pass
- [ ] No memory leaks introduced
- [ ] Theme switching works correctly
- [ ] High DPI support maintained

### User Experience Requirements
- [ ] Familiar workflow preserved
- [ ] Modern Windows 11 appearance
- [ ] Smooth animations and transitions
- [ ] Consistent navigation patterns
- [ ] Responsive design maintained

## Post-Migration Enhancements

### Potential Improvements
1. **Navigation Enhancement**: Implement NavigationView for better UX
2. **Mica Effects**: Add modern backdrop materials
3. **Enhanced Icons**: Upgrade to Fluent System Icons
4. **Touch Optimization**: Improve touch interface elements
5. **Dark Mode**: Better dark theme implementation
6. **Accessibility**: Enhanced screen reader support

This chatmode provides comprehensive guidance for migrating the UniVoting system from MahApps.Metro to WPF UI while maintaining functionality and improving the user experience with modern Windows 11 design patterns.
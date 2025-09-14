# UniVoting .NET 8 Migration Project

## Project Overview
UniVoting is an electronic voting system undergoing migration from .NET Framework 4.8 to .NET 8. This is a comprehensive modernization project involving UI framework replacement, reporting system updates, and dependency modernization.

## Project Structure
- `Src/UniVoting.Admin/`: Administrative interface components
- `UniVoting/`: Core voting system application
- `packages/`: Legacy NuGet packages (to be converted to PackageReference)
- `migration.prompt.md`: Complete migration specification and requirements

## Migration Phases
1. **Framework Migration**: .NET Framework 4.8 → .NET 8 (net8.0-windows)
2. **Dependency Injection**: Autofac → Microsoft.Extensions.DependencyInjection
3. **UI Migration**: MahApps.Metro + MaterialDesign → WPF UI 4.0.3
4. **Reporting**: Telerik Reporting → FastReport.NET 2025.2.0
5. **Data Access**: Entity Framework 6 → EF Core 8
6. **Testing & Validation**: Comprehensive system validation

## Development Guidelines

### Code Standards
- Maintain existing security model and role-based access control
- Preserve all existing functionality during migration
- Follow Windows 10/11 compatibility requirements
- Support touch interfaces and high DPI displays
- Maintain database schema compatibility

### Migration Priorities
1. **Critical**: Preserve security and data integrity
2. **High**: Maintain existing user workflows
3. **Medium**: Improve performance and modernize UI
4. **Low**: Add new features or enhancements

### Technology Stack (Target)
- **Framework**: .NET 8 (net8.0-windows)
- **UI**: WPF UI 4.0.3
- **Dependency Injection**: Microsoft.Extensions.DependencyInjection
- **Data Access**: Entity Framework Core 8, Dapper (latest)
- **Reporting**: FastReport.NET 2025.2.0
- **Caching**: Microsoft.Extensions.Caching

### Migration Approach
- Convert projects to SDK-style format
- Use PackageReference instead of packages.config
- Implement gradual migration with continuous validation
- Maintain backward compatibility during transition
- Create comprehensive test coverage

### Quality Requirements
- All existing unit tests must pass
- No functionality regression allowed
- Performance must equal or exceed current system
- Security model must remain intact
- Accessibility compliance required

### Documentation Standards
- Update all technical documentation
- Create migration guides for developers
- Document breaking changes and workarounds
- Maintain deployment documentation
- Provide user migration guides

## Key Constraints
- Windows desktop application (no web conversion)
- Must support existing database without schema changes
- Preserve all existing security mechanisms
- Maintain performance under load
- Support offline operation capabilities

## Success Metrics
- Successful compilation with .NET 8
- All existing functionality operational
- Improved application startup time
- Modern, accessible user interface
- Successful report generation
- Positive user acceptance testing results

This migration is critical for maintaining system supportability and security compliance. Approach with methodical planning and continuous validation.
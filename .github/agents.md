# UniVoting .NET 8 Migration - Agent Instructions

## Goals
Execute the complete migration of UniVoting from .NET Framework 4.8 to .NET 8 while maintaining system integrity, security, and all existing functionality.

## Migration Context
This is a comprehensive modernization project involving:
- Framework upgrade (.NET Framework 4.8 → .NET 8)
- UI modernization (MahApps.Metro → WPF UI 4.0.3)
- Reporting system replacement (Telerik → FastReport.NET 2025.2.0)
- Dependency injection modernization (Autofac → Microsoft DI)
- Data access updates (EF6 → EF Core 8)

## Tools and Technology Preferences

### Preferred Frameworks and Libraries
- **.NET**: Target `net8.0-windows` for all projects
- **UI Framework**: WPF UI 4.0.3 (replace MahApps.Metro and MaterialDesign)
- **Dependency Injection**: Microsoft.Extensions.DependencyInjection
- **Data Access**: Entity Framework Core 8, Dapper (latest version)
- **Reporting**: FastReport.NET 2025.2.0
- **Caching**: Microsoft.Extensions.Caching (replace Akavache)
- **Package Management**: PackageReference format (no packages.config)

### Avoid
- Legacy .NET Framework patterns and APIs
- packages.config format
- Deprecated NuGet packages
- Non-standard dependency injection containers
- Outdated UI frameworks

## Change Management

### Migration Strategy
- **Incremental Approach**: Migrate one component/project at a time
- **Continuous Validation**: Build and test after each significant change
- **Preserve Functionality**: Never break existing user workflows
- **Backward Compatibility**: Maintain during transition period
- **Risk Mitigation**: Create backups before major changes

### File Conversion Priority
1. **Project Files**: Convert `.csproj` to SDK-style format first
2. **Package References**: Migrate packages.config to PackageReference
3. **Core Libraries**: Update shared components and utilities
4. **UI Components**: Convert windows and controls systematically
5. **Reports**: Migrate report definitions and viewers
6. **Tests**: Update and validate test suites

### Code Quality Standards
- Maintain existing code patterns and conventions
- Preserve all security mechanisms and validations
- Keep performance characteristics or improve them
- Ensure accessibility compliance
- Follow SOLID principles and clean code practices

## Testing & Continuous Integration

### Testing Strategy
- **Unit Tests**: All existing tests must pass after migration
- **Integration Tests**: Validate system components work together
- **UI Tests**: Ensure all user workflows function correctly
- **Performance Tests**: Verify no degradation in system performance
- **Security Tests**: Validate all security measures remain effective

### Validation Commands
- Build all projects: `dotnet build`
- Run unit tests: `dotnet test`
- Package applications: `dotnet publish`
- Analyze code: Use built-in analyzers and linters

### Quality Gates
- Zero compilation errors or warnings
- All tests passing
- No security vulnerabilities introduced
- Performance benchmarks met
- Accessibility standards maintained

## Security & Compliance

### Security Requirements
- **Preserve Authentication**: Maintain existing user authentication mechanisms
- **Role-Based Access**: Keep all role-based access control (RBAC) functionality
- **Data Protection**: Ensure all data encryption and protection remains intact
- **Audit Trails**: Preserve all logging and audit functionality
- **Secure Communications**: Maintain any encrypted communication channels

### Compliance Considerations
- Election system security standards
- Data privacy requirements
- Accessibility compliance (WCAG)
- Windows security model integration

## Communication & Documentation

### Progress Reporting
- Document all changes made during migration
- Track milestones and completion status
- Report any blockers or issues immediately
- Provide regular status updates on migration progress

### Documentation Requirements
- Update technical documentation for new frameworks
- Create migration guides for future reference
- Document breaking changes and required updates
- Maintain deployment and configuration guides

### Change Descriptions
- Explain rationale for all non-trivial changes
- Reference migration phase and objectives
- Include validation steps taken
- Note any potential impact on users or operations

## Migration Phases & Milestones

### Phase 1: Framework Migration (Priority: Critical)
- Convert all projects to SDK-style format
- Update target framework to net8.0-windows
- Migrate package references
- Fix compilation errors

### Phase 2: Dependency Injection (Priority: High)
- Replace Autofac with Microsoft.Extensions.DependencyInjection
- Update service registrations and lifetimes
- Modify application startup sequences

### Phase 3: UI Migration (Priority: High)
- Install and configure WPF UI 4.0.3
- Convert MahApps.Metro windows and controls
- Update themes and styling
- Ensure accessibility compliance

### Phase 4: Reporting Migration (Priority: Medium)
- Install FastReport.NET 2025.2.0
- Convert Telerik report definitions
- Update report viewers and generation logic
- Validate all report outputs

### Phase 5: Data Access Migration (Priority: High)
- Migrate to Entity Framework Core 8
- Update DbContext configurations
- Modernize Dapper usage patterns
- Replace Akavache with Microsoft.Extensions.Caching

### Phase 6: Testing & Validation (Priority: Critical)
- Comprehensive system testing
- Performance validation
- Security verification
- User acceptance testing preparation

## Emergency Protocols

### Critical Issues
- **Build Failures**: Stop migration, fix immediately, validate fix
- **Security Vulnerabilities**: Address before proceeding with migration
- **Data Loss Risk**: Implement additional safeguards and validation
- **Performance Regression**: Investigate and optimize before continuing

### Rollback Procedures
- Maintain ability to revert to previous working state
- Document rollback steps for each migration phase
- Keep original .NET Framework version available until validation complete

## Success Criteria
- Application compiles and runs successfully on .NET 8
- All existing functionality preserved and working
- Modern UI with improved user experience
- All reports generate correctly
- Performance equals or exceeds original system
- Security model fully intact
- Comprehensive documentation updated

Execute this migration with methodical precision, continuous validation, and unwavering focus on maintaining system integrity and user functionality.
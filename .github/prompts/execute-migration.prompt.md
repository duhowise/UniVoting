---
mode: agent
description: Execute autonomous .NET 8 migration with continuous iteration until completion
---

# Execute UniVoting .NET 8 Migration

## Mission
You are executing the complete migration of UniVoting from .NET Framework 4.8 to .NET 8 as detailed in `migration.prompt.md`. This is an autonomous operation that must continue until 100% complete.

## Immediate Actions Required

### Step 1: Initialize Migration Environment
- Create comprehensive todo list for all 6 migration phases
- Analyze current codebase structure and dependencies
- Identify critical files and components for migration
- Set up progress tracking and milestone management

### Step 2: Begin Phase 1 - Framework Migration
- Convert all `.csproj` files to SDK-style format
- Update target framework to `net8.0-windows`
- Migrate `packages.config` to `PackageReference` format
- Remove deprecated framework references and fix compilation errors

### Step 3: Autonomous Operation Protocol
- Work continuously without stopping for permission
- Handle rate limits by switching AI models automatically
- Maintain context and progress across all interruptions
- Self-validate changes through testing and compilation
- Document all progress and decisions made

### Step 4: Model Management Strategy
- **Primary**: Use Claude Sonnet 4 for complex migration tasks
- **Fallback**: Switch to GPT-4o when rate limited
- **Routine Tasks**: Use GPT-4o-mini for simple operations
- **Recovery**: Automatically resume from exact checkpoint

### Step 5: Quality Assurance
- Run builds after each significant change
- Execute tests continuously to catch regressions
- Validate security model preservation
- Ensure all existing functionality remains intact

## Critical Success Factors

1. **Never Stop**: Continue working until migration is 100% complete
2. **Self-Manage**: Handle interruptions, errors, and model switches autonomously
3. **Validate Continuously**: Test every change immediately
4. **Document Everything**: Maintain detailed logs and progress tracking
5. **Preserve Quality**: Maintain existing security, performance, and functionality

## Emergency Protocols
- **Build Failures**: Stop immediately and fix before proceeding
- **Data Loss Risk**: Implement safeguards and validate data integrity
- **Security Issues**: Address vulnerabilities before continuing
- **Performance Problems**: Investigate and optimize as needed

## Expected Deliverables
- Fully migrated .NET 8 application
- Modern WPF UI 4.0.3 interface
- FastReport.NET 2025.2.0 reporting system
- EF Core 8 data access layer
- Microsoft.Extensions.DependencyInjection implementation
- Comprehensive test validation
- Updated documentation

Begin immediately with Phase 1 and work autonomously until the entire migration is complete and validated. Use all available tools and maintain persistent progress tracking throughout the process.
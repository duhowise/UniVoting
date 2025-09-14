# UniVoting .NET 8 Migration Instructions

This repository contains custom GitHub Copilot chat mode configurations for the UniVoting migration project.

## Available Chat Modes

### 🚀 NET8 Migration Agent (`net8-migration.chatmode.md`)

An autonomous migration agent designed to execute the complete .NET Framework 4.8 to .NET 8 migration with:

- **Autonomous Operation**: Continuously works until migration is complete
- **Model Switching**: Automatically switches between AI models when rate limits are reached
- **Progress Tracking**: Uses comprehensive todo management for milestone tracking
- **Error Recovery**: Self-diagnoses and fixes issues during migration
- **Validation**: Runs tests and validation after each major change

## How to Use

1. **Activate the Chat Mode**:
   - Open VS Code in the UniVoting workspace
   - Open Copilot Chat (Ctrl+Alt+I)
   - Select "NET8 Migration Agent" from the chat mode dropdown

2. **Start the Migration**:
   Simply type: "Begin the .NET 8 migration process"
   
   The agent will:
   - Analyze the current codebase
   - Create comprehensive migration todos
   - Begin Phase 1 (Framework Migration)
   - Continue autonomously through all phases

3. **Monitor Progress**:
   - The agent maintains detailed todo lists
   - Milestones are tracked and reported
   - You can ask for status updates at any time

4. **Handle Interruptions**:
   - If rate limited, the agent automatically switches AI models
   - Context and progress are preserved across interruptions
   - Simply resume by asking "Continue the migration"

## Migration Phases

The agent follows a structured 6-phase approach:

1. **Framework Migration** (2-3 weeks) - Convert to .NET 8 SDK-style projects
2. **Dependency Injection** (1-2 weeks) - Replace Autofac with Microsoft DI
3. **UI Migration** (4-5 weeks) - Replace MahApps.Metro with WPF UI 4.0.3
4. **Reporting System** (3-4 weeks) - Convert Telerik to FastReport.NET
5. **Data Access** (1-2 weeks) - Migrate EF6 to EF Core 8
6. **Testing & Validation** (3-4 weeks) - Comprehensive validation

## Emergency Commands

- `"Stop migration and assess current state"` - Pause and get status report
- `"Fix critical error: [description]"` - Address urgent issues
- `"Rollback last change"` - Revert recent modifications
- `"Switch to validation mode"` - Focus on testing and validation
- `"Generate migration report"` - Create comprehensive progress report

## Model Switching Strategy

The chat mode automatically manages AI model usage:

- **Claude Sonnet 4**: Primary model for complex migration tasks
- **GPT-4o**: Fallback for complex reasoning when Claude is unavailable
- **GPT-4o-mini**: For routine tasks and validation
- **Claude Haiku**: For simple file operations

## Key Features

### Autonomous Operation
- Never stops until migration is complete
- Self-manages interruptions and resumptions
- Automatically troubleshoots issues

### Progress Tracking
- Granular todo management
- Phase-level milestone tracking
- Continuous validation and testing

### Quality Assurance
- Maintains existing security model
- Preserves all functionality
- Ensures performance standards
- Validates accessibility compliance

### Error Recovery
- Automatic build error detection and fixing
- Test failure analysis and resolution
- Runtime error debugging
- Compatibility issue workarounds

## Best Practices

1. **Regular Commits**: The agent will create frequent commits to track progress
2. **Testing**: Validation happens after each major change
3. **Documentation**: All changes are documented automatically
4. **Backup**: Ensure you have backups before starting migration
5. **Monitoring**: Check progress regularly and provide feedback

## Support

If you encounter issues:

1. Ask the agent for a status report
2. Review the todo list for current progress
3. Check recent commits for changes made
4. Use emergency commands if needed
5. Restart the chat mode if necessary

The migration agent is designed to handle complex scenarios autonomously while keeping you informed of progress and any critical decisions.
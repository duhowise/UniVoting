# UniVoting .NET 8 Migration - Custom Chat Mode Usage Guide

## Overview

This guide explains how to use the custom GitHub Copilot chat mode created specifically for the UniVoting .NET Framework 4.8 to .NET 8 migration project. The chat mode provides autonomous migration capabilities with model switching and continuous iteration until completion.

## Setup Instructions

### 1. Verify Chat Mode Installation

The chat mode files should be located in:
- `.github/chatmodes/net8-migration.chatmode.md` - Main migration agent
- `.github/chatmodes/README.md` - Documentation
- `.github/prompts/execute-migration.prompt.md` - Migration prompt
- `.github/copilot-instructions.md` - Repository instructions
- `.github/agents.md` - Agent-specific instructions

### 2. Activate the Chat Mode

1. Open VS Code in the UniVoting workspace
2. Ensure you have GitHub Copilot extension installed and enabled
3. Open Copilot Chat panel (Ctrl+Alt+I)
4. Click the dropdown menu at the bottom of the chat panel
5. Select "🚀 UniVoting .NET 8 Migration Agent" from the list

### 3. Verify Setup

To confirm the chat mode is working correctly, type:
```
"Test mode - provide migration overview"
```

The agent should respond with details about the 6-phase migration plan and its autonomous capabilities.

## Using the Migration Agent

### Starting the Migration

To begin the complete migration process:
```
"Begin the .NET 8 migration process"
```

The agent will:
1. Analyze the current codebase structure
2. Create comprehensive todo lists for all phases
3. Begin Phase 1 (Framework Migration)
4. Continue autonomously through all phases

### Key Commands

#### Status and Progress
- `"Provide migration status report"` - Get current progress overview
- `"Show todo list"` - Display current tasks and milestones
- `"What phase are we in?"` - Get current phase information
- `"Generate progress report"` - Create detailed migration report

#### Phase-Specific Commands
- `"Begin Phase 1 - Framework Migration"` - Start framework conversion
- `"Begin Phase 2 - Dependency Injection"` - Start DI migration
- `"Begin Phase 3 - UI Migration"` - Start UI framework replacement
- `"Begin Phase 4 - Reporting Migration"` - Start report system conversion
- `"Begin Phase 5 - Data Access Migration"` - Start EF Core migration
- `"Begin Phase 6 - Testing & Validation"` - Start comprehensive testing

#### Control Commands
- `"Pause migration"` - Stop current work and provide status
- `"Resume migration"` - Continue from where left off
- `"Switch to validation mode"` - Focus on testing and validation
- `"Emergency stop"` - Halt all migration work immediately

#### Error Handling
- `"Fix compilation errors"` - Address build failures
- `"Resolve test failures"` - Debug and fix failing tests
- `"Rollback last change"` - Revert recent modifications
- `"Diagnose issue: [description]"` - Investigate specific problems

#### Prompt Execution
- `"/execute-migration"` - Run the migration prompt file
- `"/execute-migration --phase=1"` - Run specific phase only

## Model Switching Features

### Automatic Model Management

The chat mode automatically handles:
- **Rate Limit Detection**: Recognizes when current model hits limits
- **Model Switching**: Automatically switches to available models
- **Context Preservation**: Maintains conversation context across switches
- **Progress Continuity**: Resumes exactly where it left off

### Model Hierarchy

1. **Claude Sonnet 4** (Primary) - Complex migration tasks
2. **GPT-4o** (Fallback) - Complex reasoning when Claude unavailable  
3. **GPT-4o-mini** (Secondary) - Routine tasks and validation
4. **Claude Haiku** (Tertiary) - Simple file operations

### Manual Model Requests

You can request specific models:
- `"Switch to Claude Sonnet for this complex task"`
- `"Use GPT-4o-mini for this simple operation"`
- `"Upgrade to highest available model"`

## Autonomous Operation Features

### Continuous Working
- Never stops until migration is complete
- Works through rate limits by switching models
- Self-manages interruptions and resumptions
- Maintains progress across sessions

### Self-Validation
- Runs builds after each significant change
- Executes tests continuously
- Validates functionality preservation
- Checks security model integrity

### Error Recovery
- Automatically detects and fixes compilation errors
- Debugs and resolves test failures
- Handles runtime errors and exceptions
- Implements workarounds for compatibility issues

### Progress Tracking
- Maintains detailed todo lists for each phase
- Tracks file-level changes and conversions
- Records milestones and achievements
- Documents decisions and rationale

## Monitoring and Troubleshooting

### Progress Monitoring

Check progress regularly with:
```
"Show detailed progress across all phases"
"What files have been migrated so far?"
"Any blockers or issues encountered?"
```

### Common Issues and Solutions

#### Chat Mode Not Available
- Restart VS Code
- Check that files are in correct `.github/chatmodes/` directory
- Verify file extensions are `.chatmode.md`

#### Agent Not Responding Correctly
- Try: `"Reset to migration agent mode"`
- Verify you're in the correct chat mode
- Check if rate limits are affecting responses

#### Build Failures During Migration
- Agent should auto-fix, but you can also use: `"Stop and fix all compilation errors"`
- Request status: `"What build errors are present?"`

#### Progress Lost or Unclear
- Request: `"Assess current migration state and continue"`
- Check: `"What todos are in progress?"`

### Emergency Procedures

If migration encounters critical issues:

1. **Stop Migration**: `"Emergency stop - critical issue"`
2. **Assess Damage**: `"Evaluate current system state"`
3. **Rollback if Needed**: `"Rollback to last stable state"`
4. **Report Issue**: `"Generate incident report"`
5. **Plan Recovery**: `"Create recovery plan"`

## Best Practices

### Before Starting
1. Create full backup of current codebase
2. Ensure all current tests pass
3. Document any known issues or technical debt
4. Verify development environment is ready

### During Migration
1. Monitor progress regularly
2. Review changes being made
3. Test functionality in phases
4. Communicate any concerns to the agent

### After Completion
1. Run comprehensive testing
2. Validate all functionality works
3. Update documentation
4. Plan deployment strategy

## Expected Timeline

The agent will work continuously, but expect these timeframes:
- **Phase 1**: Framework Migration (2-3 weeks)
- **Phase 2**: Dependency Injection (1-2 weeks)  
- **Phase 3**: UI Migration (4-5 weeks)
- **Phase 4**: Reporting Migration (3-4 weeks)
- **Phase 5**: Data Access Migration (1-2 weeks)
- **Phase 6**: Testing & Validation (3-4 weeks)

**Total**: 18-26 weeks (can be accelerated through automation)

## Success Indicators

The migration is successful when:
- ✅ Application compiles without errors on .NET 8
- ✅ All existing functionality works
- ✅ UI appears modern and accessible
- ✅ All reports generate correctly  
- ✅ Performance equals or exceeds original
- ✅ All tests pass
- ✅ Security model remains intact

## Support and Assistance

If you need help:
1. Ask the agent for guidance: `"I need help with [specific issue]"`
2. Request documentation: `"Generate guide for [specific topic]"`
3. Get recommendations: `"What should I do about [situation]?"`

The migration agent is designed to be autonomous and helpful. Don't hesitate to ask questions or request clarification on any aspect of the migration process.
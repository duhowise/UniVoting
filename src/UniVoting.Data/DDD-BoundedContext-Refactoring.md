# DDD Bounded Context Refactoring for UniVoting

## Overview

The `VotingDbContext` has been refactored to follow Domain-Driven Design (DDD) principles using **Bounded Contexts** as outlined in the Microsoft article "Shrink EF Models with DDD Bounded Contexts". This refactoring creates smaller, more focused contexts that are easier to maintain and reason about.

## Original Problem

The original `VotingDbContext` contained all entities in a single large context:
- `Voter`, `Vote`, `SkippedVotes`, `Position`, `Candidate`, `Rank`, `Commissioner`, `Setting`
- All entity configurations in one `OnModelCreating` method
- Single responsibility for all voting-related operations

## DDD Bounded Context Solution

### Three Bounded Contexts Identified

#### 1. Election Management Context
**Purpose**: Handles election setup, position management, candidate registration, and commissioner activities

**Entities**:
- `ElectionPosition` - Enhanced with approval status, creation tracking
- `ElectionCandidate` - Enhanced with registration status, approval workflow
- `Rank`, `Commissioner`, `Setting` - Administrative entities

**Key Features**:
- Focused on pre-election setup and administration
- Enhanced domain models with management-specific properties
- Separate configuration for election management concerns

#### 2. Voting Process Context
**Purpose**: Handles voter authentication and the actual voting process

**Entities**:
- `VotingVoter` - Enhanced with voting status, authentication tracking
- `VotingPosition` - Read-only view optimized for voting UI
- `VotingCandidate` - Read-only view optimized for ballot display
- `SkippedVotes` - Tracking abstentions

**Key Features**:
- Focused on real-time voting operations
- Optimized domain models for voting UI
- Read-only access to reference data (positions, candidates)

#### 3. Results Processing Context
**Purpose**: Handles vote recording, tallying, and results analysis

**Entities**:
- `ResultsVote` - Enhanced with validation and processing metadata
- `ResultsCandidate` - Enhanced with vote counts, percentages, rankings
- `ResultsPosition` - Enhanced with participation rates, winner summaries
- `ResultsVoter` - Minimal audit trail information

**Key Features**:
- Focused on post-voting analysis and reporting
- Enhanced domain models with calculated properties
- Audit trail and validation capabilities

## Benefits of This Approach

### 1. **Separation of Concerns**
- Each context has a clear, single responsibility
- Reduces coupling between different aspects of the voting system
- Easier to understand and modify specific functionality

### 2. **Performance Improvements**
- Smaller models load faster into memory
- Targeted queries for specific operations
- Reduced metadata overhead

### 3. **Team Development**
- Different teams can work on different contexts independently
- Clear boundaries reduce merge conflicts
- Specialized domain knowledge can be applied to each context

### 4. **Maintainability**
- Changes to election management don't affect voting process
- Easier to test individual contexts in isolation
- Clear data access patterns for each business capability

### 5. **Domain-Specific Optimizations**
- Each context has entities optimized for its specific needs
- Computed properties relevant to each domain
- Reduced over-fetching of unnecessary data

## Implementation Details

### Base Context Pattern
```csharp
public abstract class BaseContext<TContext> : DbContext where TContext : DbContext
```
- Provides common configuration for all bounded contexts
- Handles database connection and initialization concerns
- Follows the pattern recommended in the Microsoft article

### Entity Configuration Separation
- Each context applies only relevant entity configurations
- Uses `modelBuilder.Ignore<T>()` to exclude irrelevant entities
- Maintains clean, focused `OnModelCreating` methods

### Specialized Domain Classes
- Each context has its own domain classes optimized for its operations
- Properties and computed fields relevant to specific use cases
- Navigation properties configured for context-specific needs

## Usage Examples

### Election Management
```csharp
using var context = new ElectionManagementContext(options);
var pendingCandidates = context.Candidates
    .Where(c => c.RegistrationStatus == "Pending")
    .Include(c => c.Position)
    .ToList();
```

### Voting Process
```csharp
using var context = new VotingProcessContext(options);
var availablePositions = context.Positions
    .Where(p => p.IsActive)
    .Include(p => p.Candidates)
    .OrderBy(p => p.DisplayOrder)
    .ToList();
```

### Results Processing
```csharp
using var context = new ResultsProcessingContext(options);
var results = context.Positions
    .Include(p => p.Candidates)
    .ThenInclude(c => c.Votes)
    .Where(p => p.ResultsFinalized)
    .ToList();
```

## Migration Considerations

### Database Schema
- All contexts map to the same physical database
- Specialized classes map to existing tables using `[Table]` attributes
- No breaking changes to existing database schema

### Application Code Updates
- Services should use the appropriate context for their operations
- Dependency injection can provide specific contexts to specific services
- Original `VotingDbContext` can remain for database migrations and full-model operations

### Testing Benefits
- Each context can be tested independently
- Smaller, focused integration tests
- Easier to mock specific bounded contexts

## Conclusion

This DDD Bounded Context refactoring transforms a monolithic `DbContext` into three focused, specialized contexts. Each context provides:

1. **Clear boundaries** around business capabilities
2. **Optimized domain models** for specific operations
3. **Improved maintainability** and team development
4. **Better performance** through targeted data access
5. **Enhanced testability** through focused contexts

The refactoring follows Microsoft's recommended patterns while preserving existing functionality and database compatibility.
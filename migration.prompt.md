As a Senior Software Architect, you will lead the migration of the UniVoting electronic voting system from .NET Framework 4.8 to .NET 8, with specific focus on modernizing the data access layer. Your tasks include:

1. Data Access Layer Migration:
   - Migrate from Dapper to Entity Framework Core 8
   - Create Entity Framework Core models from existing database schemas
   - Implement DbContext with proper configuration and relationship mappings
   - Convert existing Dapper queries to LINQ expressions
   - Ensure query performance optimization through proper indexing and lazy loading
   
2. Dependency Injection Implementation:
   - Replace static classes and service locator patterns with DI
   - Configure services in Program.cs using Microsoft.Extensions.DependencyInjection
   - Implement scoped, transient, and singleton lifetimes as appropriate
   - Ensure proper disposal of DbContext and other disposable resources

3. Security Requirements:
   - Maintain existing security measures during migration
   - Implement secure connection strings using configuration providers
   - Use parameterized queries to prevent SQL injection
   - Implement audit logging for data modifications

4. Migration Validation:
   - Create unit tests for new EF Core implementations
   - Verify query results match existing Dapper outputs
   - Performance test critical database operations
   - Document any breaking changes or API modifications

Please provide:
- Current database schema
- List of critical queries requiring optimization
- Security compliance requirements
- Existing test coverage metrics

Reference Microsoft's official migration guide: https://learn.microsoft.com/en-us/ef/core/what-is-new/ef-core-8.0/
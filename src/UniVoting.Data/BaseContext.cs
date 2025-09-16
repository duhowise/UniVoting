using Microsoft.EntityFrameworkCore;

namespace UniVoting.Data
{
    /// <summary>
    /// Base context class that provides common configuration for all bounded contexts
    /// </summary>
    /// <typeparam name="TContext">The specific context type</typeparam>
    public abstract class BaseContext<TContext> : DbContext where TContext : DbContext
    {
        static BaseContext()
        {
            // Disable database initialization for bounded contexts
            // The main context will handle database creation/migration
        }

        protected BaseContext(DbContextOptions<TContext> options) : base(options)
        {
        }

        protected BaseContext(string connectionString) : base()
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                // This will be configured by dependency injection in the real application
                optionsBuilder.UseSqlServer("VotingDatabase");
            }
        }
    }
}
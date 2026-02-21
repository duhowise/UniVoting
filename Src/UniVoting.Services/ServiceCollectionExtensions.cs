using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using UniVoting.Data;
using UniVoting.Data.Implementations;
using UniVoting.Data.Interfaces;
using UniVoting.Model;

namespace UniVoting.Services;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddUniVotingServices(this IServiceCollection services, string connectionString)
    {
        // EF Core context factory — creates a fresh DbContext per operation (safe for singleton repos)
        services.AddDbContextFactory<ElectionDbContext>(options =>
            options.UseSqlServer(connectionString));

        // Data repositories — MSDI resolves IDbContextFactory<ElectionDbContext> automatically
        services.AddTransient<ICandidateRepository, CandidateRepository>();
        services.AddTransient<IVoterRepository, VoterRepository>();
        services.AddTransient<IPositionRepository, PositionRepository>();
        services.AddTransient<IComissionerRepository, ComissionerRepository>();
        services.AddTransient<IService, ElectionService>();

        // Application services
        services.AddSingleton<ILogger, SystemEventLoggerService>();
        services.AddTransient<IElectionConfigurationService, ElectionConfigurationService>();
        services.AddTransient<IVotingService, VotingService>();
        services.AddTransient<ILiveViewService, LiveViewService>();

        return services;
    }
}

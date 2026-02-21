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
        services.AddSingleton<ICandidateRepository, CandidateRepository>();
        services.AddSingleton<IVoterRepository, VoterRepository>();
        services.AddSingleton<IPositionRepository, PositionRepository>();
        services.AddSingleton<IComissionerRepository, ComissionerRepository>();
        services.AddSingleton<IService, ElectionService>();

        // Application services
        services.AddSingleton<ILogger, SystemEventLoggerService>();
        services.AddSingleton<IElectionConfigurationService, ElectionConfigurationService>();
        services.AddSingleton<IVotingService, VotingService>();
        services.AddSingleton<ILiveViewService, LiveViewService>();

        return services;
    }
}

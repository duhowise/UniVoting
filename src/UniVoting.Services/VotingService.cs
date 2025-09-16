using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Concurrent;
using System.Linq;
using System.Threading.Tasks;
using UniVoting.Data;
using UniVoting.Model;

namespace UniVoting.Services
{
    public class VotingService
    {
        private readonly ILogger<VotingService> _logger;
        private readonly VotingDbContext _dbContext;

        public VotingService(VotingDbContext dbContext, ILogger<VotingService> logger)
        {
            _dbContext = dbContext;
            _logger = logger;
        }

        public async Task SkipVote(SkippedVotes skipped)
        {
            try
            {
                _dbContext.SkippedVotes.Add(skipped);
                await _dbContext.SaveChangesAsync();
            }
            catch (Exception exception)
            {
                _logger.LogError(exception, "Failed to skip vote for voter {VoterId} and position {PositionId}", skipped.VoterId, skipped.Positionid);
                throw;
            }
        }

        public async Task CastVote(ConcurrentBag<Vote> votes, Voter voter, ConcurrentBag<SkippedVotes> skippedVotes)
        {
            try
            {
                using var transaction = await _dbContext.Database.BeginTransactionAsync();
                
                // Add votes
                _dbContext.Votes.AddRange(votes);
                
                // Add skipped votes
                _dbContext.SkippedVotes.AddRange(skippedVotes);
                
                // Update voter status
                voter.Voted = true;
                voter.VoteInProgress = false;
                _dbContext.Voters.Update(voter);
                
                await _dbContext.SaveChangesAsync();
                await transaction.CommitAsync();
            }
            catch (Exception exception)
            {
                _logger.LogError(exception, "Failed to cast votes for voter {VoterId}", voter.Id);
                throw;
            }
        }

        public async Task UpdateVoter(Voter voter)
        {
            try
            {
                _dbContext.Voters.Update(voter);
                await _dbContext.SaveChangesAsync();
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Failed to update voter {VoterId}", voter.Id);
                throw;
            }
        }

        public async Task ResetVoter(Voter voter)
        {
            try
            {
                // Reset voter status
                voter.Voted = false;
                voter.VoteInProgress = false;
                
                // Remove existing votes for this voter
                var existingVotes = _dbContext.Votes.Where(v => v.VoterId == voter.Id);
                _dbContext.Votes.RemoveRange(existingVotes);
                
                // Remove existing skipped votes for this voter
                var existingSkippedVotes = _dbContext.SkippedVotes.Where(sv => sv.VoterId == voter.Id);
                _dbContext.SkippedVotes.RemoveRange(existingSkippedVotes);
                
                _dbContext.Voters.Update(voter);
                await _dbContext.SaveChangesAsync();
            }
            catch (Exception exception)
            {
                _logger.LogError(exception, "Failed to reset voter {VoterId}", voter.Id);
                throw;
            }
        }

        public async Task<Voter> GetVoterPass(Voter voter)
        {
            try
            {
                return await _dbContext.Voters
                    .FirstOrDefaultAsync(v => v.IndexNumber == voter.IndexNumber);
            }
            catch (Exception exception)
            {
                _logger.LogError(exception, "Failed to get voter pass for voter with index number {IndexNumber}", voter.IndexNumber);
                throw;
            }
        }
    }

    // Cache service interface and implementation
    public interface ICacheService
    {
        Task<T> GetObjectAsync<T>(string key) where T : class;
        Task InsertObjectAsync<T>(string key, T value, TimeSpan? expiration = null) where T : class;
        Task InvalidateAllAsync();
        Task InvalidateAsync(string key);
    }

    public class MemoryCacheService : ICacheService
    {
        private readonly IMemoryCache _memoryCache;

        public MemoryCacheService(IMemoryCache memoryCache)
        {
            _memoryCache = memoryCache;
        }

        public Task<T> GetObjectAsync<T>(string key) where T : class
        {
            try
            {
                if (_memoryCache.TryGetValue(key, out var cachedValue))
                {
                    if (cachedValue is string jsonString)
                    {
                        var result = JsonConvert.DeserializeObject<T>(jsonString);
                        return Task.FromResult(result);
                    }
                    return Task.FromResult(cachedValue as T);
                }
                return Task.FromResult<T>(null);
            }
            catch (Exception)
            {
                return Task.FromResult<T>(null);
            }
        }

        public Task InsertObjectAsync<T>(string key, T value, TimeSpan? expiration = null) where T : class
        {
            try
            {
                var options = new MemoryCacheEntryOptions();
                if (expiration.HasValue)
                {
                    options.AbsoluteExpirationRelativeToNow = expiration.Value;
                }
                else
                {
                    options.AbsoluteExpirationRelativeToNow = TimeSpan.FromHours(1);
                }

                var jsonString = JsonConvert.SerializeObject(value);
                _memoryCache.Set(key, jsonString, options);
                return Task.CompletedTask;
            }
            catch (Exception)
            {
                return Task.CompletedTask;
            }
        }

        public Task InvalidateAllAsync()
        {
            try
            {
                if (_memoryCache is MemoryCache mc)
                {
                    mc.Compact(1.0); // Remove all entries
                }
                return Task.CompletedTask;
            }
            catch (Exception)
            {
                return Task.CompletedTask;
            }
        }

        public Task InvalidateAsync(string key)
        {
            try
            {
                _memoryCache.Remove(key);
                return Task.CompletedTask;
            }
            catch (Exception)
            {
                return Task.CompletedTask;
            }
        }
    }
}
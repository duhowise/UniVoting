using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Concurrent;
using System.Threading.Tasks;
using UniVoting.Data.Interfaces;
using UniVoting.Model;

namespace UniVoting.Services
{
    public class VotingService
    {
        private readonly ILogger<VotingService> _logger;
        private readonly IService _electionservice;

        public VotingService(IService electionService, ILogger<VotingService> logger)
        {
            _electionservice = electionService;
            _logger = logger;
        }

        public async Task SkipVote(SkippedVotes skipped)
        {
            try
            {
                await _electionservice.Voters.InsertSkippedVotes(skipped);
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
                await _electionservice.Voters.InsertBulkVotes(votes, voter, skippedVotes);
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
                await _electionservice.Voters.UpdateAsync(voter);
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
                await _electionservice.Voters.ResetVoter(voter);
            }
            catch (Exception exception)
            {
                _logger.LogError(exception, "Failed to reset voter {VoterId}", voter.Id);
            }
        }

        public async Task<Voter> GetVoterPass(Voter voter)
        {
            try
            {
                return await _electionservice.Voters.GetVoterPass(voter);
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
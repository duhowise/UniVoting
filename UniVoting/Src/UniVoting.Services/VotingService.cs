﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UniVoting.Data.Implementations;
using UniVoting.Data.Interfaces;
using UniVoting.Model;

namespace UniVoting.Services
{
    public class VotingService
    {
        private static readonly IService _electionservice = new ElectionService();

        public static async Task SkipVote(SkippedVotes skipped)
        {
            try
            {
                await _electionservice.Voters.InsertSkippedVotes(skipped);
            }
            catch (Exception exception)
            {
                SystemEventLoggerService.Log(exception.StackTrace);
            }
        }

        public static async Task CastVote(List<Vote> votes, Voter voter, List<SkippedVotes> skippedVotes)
        {
            try
            {
                await _electionservice.Voters.InsertBulkVotes(votes, voter, skippedVotes);
            }
            catch (Exception exception)
            {
                SystemEventLoggerService.Log(exception.StackTrace);
            }
        }

        public static async Task UpdateVoter(Voter voter)
        {
            try
            {
                await _electionservice.Voters.UpdateAsync(voter);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public static async Task ResetVoter(Voter voter)
        {
            try
            {
                await _electionservice.Voters.ResetVoter(voter);
            }
            catch (Exception exception)
            {
                SystemEventLoggerService.Log(exception.StackTrace);
            }
        }

        public static async Task<Voter> GetVoterPass(Voter voter)
        {
            try
            {
                return await _electionservice.Voters.GetVoterPass(voter);
            }
            catch (Exception exception)
            {
                SystemEventLoggerService.Log(exception.StackTrace);
            }
            return null;
        }
    }
}
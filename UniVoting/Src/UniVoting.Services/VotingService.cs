using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UniVoting.Data.Implementations;
using UniVoting.Model;

namespace UniVoting.Services
{
	public class VotingService
	{
		public static async Task SkipVote(SkippedVotes skipped)
		{
			try
			{
				await new ElectionBaseRepository().Insert(skipped);

			}
			catch (Exception e)
			{
				Console.WriteLine(e);
				throw;
			}
		}
		public static async Task CastVote(List<Vote> votes,Voter voter,List<SkippedVotes> skippedVotes)
		{
			try
			{
				await new ElectionBaseRepository().InsertBulkVotes(votes, voter,skippedVotes);

			}
			catch (Exception e)
			{
				Console.WriteLine(e);
				throw;
			}
		}
		public static async Task UpdateVoter(Voter voter)
		{
			try
			{
				await new ElectionBaseRepository().Update(voter);

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
				await new ElectionBaseRepository().ResetVoter(voter);

			}
			catch (Exception e)
			{
				Console.WriteLine(e);
				throw;
			}
		}
		public static async Task<Voter> GetVoterPass(Voter voter)
		{
			try
			{
				return	await new ElectionBaseRepository().GetVoterPass(voter);

			}
			catch (Exception e)
			{
				Console.WriteLine(e);
				throw;
			}
		}
	}
}
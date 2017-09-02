using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UniVoting.Data.Implementations;
using UniVoting.Data.Interfaces;
using UniVoting.Model;

namespace UniVoting.Services
{
	public class LiveViewService
	{
		private static readonly IService _electionservice=new ElectionService();
		
		public static Task<int> VoteCountAsync(string position)
		{
			try
			{
				return _electionservice.Voters.VoteCount(new Position { PositionName = position });

			}
			catch (Exception e)
			{
				Console.WriteLine(e);
				throw;
			}
		}
		public static Task<int> VotesSkipppedCountAsync(string position)
		{
			try
			{
				return _electionservice.Voters.VoteSkipCount(new Position { PositionName = position });

			}
			catch (Exception e)
			{
				Console.WriteLine(e);
				throw;
			}
		}

		public static Task<IEnumerable<Position>> Positions()
		{
			try
			{
				return _electionservice.Positions.GetAllAsync();

			}
			catch (Exception e)
			{
				Console.WriteLine(e);
				throw;
			}
		}

	}
}
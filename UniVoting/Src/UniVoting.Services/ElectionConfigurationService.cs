using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using UniVoting.Core;
using UniVoting.Data;

namespace UniVoting.Services
{
    public class ElectionConfigurationService : IElectionConfigurationService
	{
		private readonly ElectionDbContext _context;

		public ElectionConfigurationService(ElectionDbContext context)
		{
			_context = context;
		}

		
		#region Voters
		public async Task<List<Voter>> AddVotersAsync(List<Voter> voters)
		{
			try
			{
				await _context.Voters.AddRangeAsync(voters);
				await _context.SaveChangesAsync();
				return voters;
			}
			catch (Exception e)
			{
				throw new Exception("Something went wrong. we could not add Users",e);
			}
		}
		public async Task<IEnumerable<Voter>> GetAllVotersAsync()
		{
			try
			{
				return await _context.Voters.ToListAsync();

			}
			catch (Exception e)
			{
				throw new Exception("Something went wrong. we could not retrieve Users", e);

			}
		}
		public  async Task<Voter> LoginVoter(Voter voter)
		{
			if (voter == null) throw new ArgumentNullException(nameof(voter));
			try
			{
				return await _context.Voters.FirstOrDefaultAsync(v => v.VoterCode.Equals(voter.VoterCode, StringComparison.InvariantCultureIgnoreCase));

			}
			catch (Exception e)
			{
				throw new Exception("Something went wrong. we could not login User", e);

			}


		}
		#endregion

		#region Election
		//public static  Task<ElectionConfiguration> ConfigureElection(int id)
		//{
		//	try
		//	{
		//		return   new ElectionBaseRepository().GetByIdAsync<ElectionConfiguration>(id);

		//	}
		//	catch (Exception e)
		//	{
		//		Console.WriteLine(e);
		//		throw;
		//	}
		//}
		public  async Task<ElectionConfiguration> ConfigureElection()
		{
			try
			{
				return await _context.Settings.FirstOrDefaultAsync();
			}
			catch (Exception e)
			{
				throw new Exception("Something went wrong. we could not retrieve settings", e);

			}
		}
		public  async Task<ElectionConfiguration> AddSettings(ElectionConfiguration electionConfiguration)
		{
			if (electionConfiguration == null) throw new ArgumentNullException(nameof(electionConfiguration));
			try
			{
				//await _electionservice.Comissioners.AddNewConfiguration(electionConfiguration);
				var data= await _context.Settings.FirstOrDefaultAsync();
				if (data == null) throw new ArgumentNullException(nameof(data));
				_context.Remove(data);
				 await  _context.Settings.AddAsync(electionConfiguration);
				await _context.SaveChangesAsync();
				return electionConfiguration;


			}
			catch (Exception e)
			{
				throw new Exception("Something went wrong. we could not add settings", e);

			}
		}
		#endregion
		#region Candidate
		public async Task<Candidate> AddCandidate(Candidate candidate)
		{
			if (candidate == null) throw new ArgumentNullException(nameof(candidate));
			try
			{
				await _context.Candidates.AddAsync(candidate);
				await _context.SaveChangesAsync();
				return candidate;
			}
			catch (Exception e)
			{
				throw new Exception("Something went wrong. we could not add candidate", e);

			}
		}
		public async Task<Commissioner> SaveComissioner(Commissioner commissioner)
		{
			if (commissioner == null) throw new ArgumentNullException(nameof(commissioner));
			if (commissioner.Id==0)
			{
				try
				{
					await _context.Comissioners.AddAsync(commissioner);
					await _context.SaveChangesAsync();
						return commissioner;
				}
				catch (Exception e)
				{
					throw new Exception("Something went wrong. we could not add commisioner", e);

				}
			}
			else
			{
				try
				{
					 _context.Comissioners.Update(commissioner);
					await _context.SaveChangesAsync();
					return commissioner;

				}
				catch (Exception e)
				{
					throw new Exception("Something went wrong. we could not update commisioner", e);

				}
			}
		}
	   
		public async Task<IEnumerable<Candidate>> GetAllCandidates()
		{
			try
			{
				return await _context.Candidates.ToListAsync();
				
			}
			catch (Exception e)
			{
				throw new Exception("Something went wrong. we could not update commisioner", e);

			}
		}
		
		public  async Task<Candidate> SaveCandidate(Candidate candidate)
		{
			try
			{
				if (candidate.Id == 0)
				{
					 await _context.Candidates.AddAsync(candidate);
					
				}
				else
				{
					 _context.Candidates.Update(candidate);

				}

				await	_context.SaveChangesAsync();
				return candidate;
			}
			catch (Exception e)
			{
				throw new Exception("Something went wrong. we could not add or update candidate", e);

			}

		}
		public async Task<IEnumerable<Candidate>> GetCandidateWithDetails()
		{
			try
			{
				return await _context.Candidates.Include(c => c.Position).ToListAsync();

			}
			catch (Exception e)
			{
				throw new Exception("Something went wrong. could not retrieve candidates with details ", e);

			}

		}
		public void RemoveCandidate(Candidate candidate)
		{
			try
			{
				_context.Candidates.Remove(candidate);

			}
			catch (Exception e)
			{
				throw new Exception("Something went wrong. we could not delete candidate", e);

			}
		}
		#endregion
		#region Position
		public async Task<Position> AddPosition(Position position)
		{
			try
			{
				 await _context.Positions.AddAsync(position);
				await _context.SaveChangesAsync();
				return position;
			}
			catch (Exception e)
			{
				throw new Exception("Something went wrong. we could not add position", e);

			}

		}
		public  async Task<Position> GetPosition(int positionid)
		{
			try
			{
				return await _context.Positions.FirstOrDefaultAsync(x=>x.Id==positionid);

			}
			catch (Exception e)
			{
				throw new Exception("Something went wrong. we could not retrieve position", e);

			}

		}
		public async Task<List<Position>> GetAllPositionsAsync()
		{
			try
			{
				return await _context.Positions.Include(p=>p.Candidates).ToListAsync();

			}
			catch (Exception e)
			{
				throw new Exception("Something went wrong. we could not retrieve position with candidates", e);

			}
		}
		
		public  async Task<Position> UpdatePosition(Position position)
		{
			try
			{
				 _context.Positions.Update(position);
			await	_context.SaveChangesAsync();
				return position;
			}
			catch (Exception e)
			{
				throw new Exception("Something went wrong. we could not update position", e);

			}
		}
	   
		public  async Task RemovePosition(Position position)
		{
			try
			{
				_context.Positions.Remove(position);
				await _context.SaveChangesAsync();
				
			}
			catch (Exception e)
			{
				throw new Exception("Something went wrong. we could not delete position", e);

			}
		}
		#endregion   
		#region User
		public  async Task<Commissioner> Login(Commissioner commissioner)
		{
			if (commissioner.IsChairman)
			{
				try
				{ return await _context.Comissioners.FirstOrDefaultAsync(x=>x.IsChairman && x.UserName.Equals(commissioner.UserName) && x.Password.Equals(commissioner.Password));}
				catch (Exception e)
				{
					throw new Exception("Something went wrong. could not log in as chairman", e);

				}
			}
			else if (commissioner.IsPresident)
			{
				try
				{return await _context.Comissioners.FirstOrDefaultAsync(x => x.IsPresident && x.UserName.Equals(commissioner.UserName) && x.Password.Equals(commissioner.Password)); }
				catch (Exception e)
				{
					throw new Exception("Something went wrong. could not log in as president", e);

				}
			}
			else if (commissioner.IsAdmin)
			{
				try
				{return await _context.Comissioners.FirstOrDefaultAsync(x => x.IsAdmin && x.UserName.Equals(commissioner.UserName) && x.Password.Equals(commissioner.Password)); }
				catch (Exception e)
				{
					throw new Exception("Something went wrong. could not log in as admin", e);

				}
			}
			else if (commissioner.IsPresident)
			{
				try
				{ return await _context.Comissioners.FirstOrDefaultAsync(x => x.IsPresident && x.UserName.Equals(commissioner.UserName) && x.Password.Equals(commissioner.Password)); }
				catch (Exception e)
				{
					throw new Exception("Something went wrong. could not log in as president", e);

				}
			}
			else
			{
				try
				{ return await _context.Comissioners.FirstOrDefaultAsync(x => x.UserName.Equals(commissioner.UserName) && x.Password.Equals(commissioner.Password)); }
				catch (Exception e)
				{
					throw new Exception("Something went wrong. could not log in ", e);

				}
			}
			
			
		}
		#endregion

	
	}
}
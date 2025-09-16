using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using UniVoting.Data;
using UniVoting.Model;

namespace UniVoting.Services
{
	public class ElectionConfigurationService
	{
		private readonly VotingDbContext _dbContext;

		public ElectionConfigurationService(VotingDbContext dbContext)
		{
			_dbContext = dbContext;
		}

		
		#region Voters
		public async Task<int> AddVotersAsync(List<Voter> voters)
		{
			try
			{
				_dbContext.Voters.AddRange(voters);
				return await _dbContext.SaveChangesAsync();
			}
			catch (Exception e)
			{
				Console.WriteLine(e);
				throw;
			}
		}
		
		public async Task<IEnumerable<Voter>> GetAllVotersAsync()
		{
			try
			{
				return await _dbContext.Voters.ToListAsync();
			}
			catch (Exception e)
			{
				Console.WriteLine(e);
				throw;
			}
		}
		
		public async Task<Voter> LoginVoter(Voter voter)
		{
			try
			{
				return await _dbContext.Voters
					.FirstOrDefaultAsync(v => v.VoterCode == voter.VoterCode && v.IndexNumber == voter.IndexNumber);
			}
			catch (Exception e)
			{
				Console.WriteLine(e);
				throw;
			}
		}
		#endregion

		#region Election
		public Setting ConfigureElection()
		{
			try
			{
				return _dbContext.Settings.FirstOrDefault();
			}
			catch (Exception e)
			{
				Console.WriteLine(e);
				throw;
			}
		}
		
		public async Task NewElection(Setting setting)
		{
			try
			{
				_dbContext.Settings.Add(setting);
				await _dbContext.SaveChangesAsync();
			}
			catch (Exception e)
			{
				Console.WriteLine(e);
				throw;
			}
		}
		#endregion
		
		#region Candidate
		public async Task AddCandidate(Candidate candidate)
		{
			try
			{
				_dbContext.Candidates.Add(candidate);
				await _dbContext.SaveChangesAsync();
			}
			catch (Exception e)
			{
				Console.WriteLine(e);
				throw;
			}
		}
		
		public async Task SaveComissioner(Commissioner comissioner)
		{
			try
			{
				if (comissioner.Id == 0)
				{
					_dbContext.Commissioners.Add(comissioner);
				}
				else
				{
					_dbContext.Commissioners.Update(comissioner);
				}
				await _dbContext.SaveChangesAsync();
			}
			catch (Exception e)
			{
				Console.WriteLine(e);
				throw;
			}
		}
	   
		public async Task<IEnumerable<Candidate>> GetAllCandidates()
		{
			try
			{
				return await _dbContext.Candidates.ToListAsync();
			}
			catch (Exception e)
			{
				Console.WriteLine(e);
				throw;
			}
		}
		
		public async Task<Candidate> SaveCandidate(Candidate candidate)
		{
			try
			{
				if (candidate.Id == 0)
				{
					_dbContext.Candidates.Add(candidate);
				}
				else
				{
					_dbContext.Candidates.Update(candidate);
				}
				await _dbContext.SaveChangesAsync();
				return candidate;
			}
			catch (Exception e)
			{
				Console.WriteLine(e);
				throw;
			}
		}
	   
		public async Task RemoveCandidate(Candidate candidate)
		{
			try
			{
				_dbContext.Candidates.Remove(candidate);
				await _dbContext.SaveChangesAsync();
			}
			catch (Exception e)
			{
				Console.WriteLine(e);
				throw;
			}
		}
		#endregion
		
		#region Position
		public async Task<Position> AddPosition(Position position)
		{
			try
			{
				_dbContext.Positions.Add(position);
				await _dbContext.SaveChangesAsync();
				return position;
			}
			catch (Exception e)
			{
				Console.WriteLine(e);
				throw;
			}
		}
		
		public async Task<Position> GetPosition(int positionid)
		{
			try
			{
				return await _dbContext.Positions.FindAsync(positionid);
			}
			catch (Exception e)
			{
				Console.WriteLine(e);
				throw;
			}
		}
		
		public IEnumerable<Position> GetAllPositions()
		{
			try
			{
				return _dbContext.Positions
					.Include(p => p.Candidates)
					.ToList();
			}
			catch (Exception e)
			{
				Console.WriteLine(e);
				throw;
			}
		}
		
		public IEnumerable<Position> GetPositionsSlim()
		{
			try
			{
				return _dbContext.Positions.ToList();
			}
			catch (Exception e)
			{
				Console.WriteLine(e);
				throw;
			}
		}
		
		public async Task<IEnumerable<Position>> GetAllPositionsAsync()
		{
			try
			{
				return await _dbContext.Positions
					.Include(p => p.Candidates)
					.ToListAsync();
			}
			catch (Exception e)
			{
				Console.WriteLine(e);
				throw;
			}
		}
		
		public async Task UpdatePosition(Position position)
		{
			try
			{
				_dbContext.Positions.Update(position);
				await _dbContext.SaveChangesAsync();
			}
			catch (Exception e)
			{
				Console.WriteLine(e);
				throw;
			}
		}
	   
		public async Task RemovePosition(Position position)
		{
			try
			{
				_dbContext.Positions.Remove(position);
				await _dbContext.SaveChangesAsync();
			}
			catch (Exception e)
			{
				Console.WriteLine(e);
				throw;
			}
		}
		#endregion   
		
		#region User
		public async Task<Commissioner> Login(Commissioner comissioner)
		{
			try
			{
				var query = _dbContext.Commissioners.AsQueryable();
				
				if (comissioner.IsChairman)
				{
					query = query.Where(c => c.IsChairman && c.UserName == comissioner.UserName && c.Password == comissioner.Password);
				}
				else if (comissioner.IsPresident)
				{
					query = query.Where(c => c.IsPresident && c.UserName == comissioner.UserName && c.Password == comissioner.Password);
				}
				else if (comissioner.IsAdmin)
				{
					query = query.Where(c => c.IsAdmin && c.UserName == comissioner.UserName && c.Password == comissioner.Password);
				}
				else
				{
					query = query.Where(c => c.UserName == comissioner.UserName && c.Password == comissioner.Password);
				}
				
				return await query.FirstOrDefaultAsync();
			}
			catch (Exception e)
			{
				Console.WriteLine(e);
				throw;
			}
		}
		#endregion

		public async Task<IEnumerable<Candidate>> GetCandidateWithDetails()
		{
			try
			{
				return await _dbContext.Candidates
					.Include(c => c.Position)
					.Include(c => c.Rank)
					.ToListAsync();
			}
			catch (Exception e)
			{
				Console.WriteLine(e);
				throw;
			}
		}
	}
}
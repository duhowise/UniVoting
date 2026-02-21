using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UniVoting.Data.Interfaces;
using UniVoting.Model;

namespace UniVoting.Services
{
	public class ElectionConfigurationService : IElectionConfigurationService
	{
		private readonly IService _electionservice;

		public ElectionConfigurationService(IService electionService)
		{
			_electionservice = electionService;
		}

		#region Voters
		public Task<int> AddVotersAsync(List<Voter> voters)
		{
			try
			{
				return _electionservice.Voters.InsertBulkVoters(voters);

			}
			catch (Exception e)
			{
				Console.WriteLine(e);
				throw;
			}
		}
		public Task<IEnumerable<Voter>> GetAllVotersAsync()
		{
			try
			{
				return _electionservice.Voters.GetAllAsync();

			}
			catch (Exception e)
			{
				Console.WriteLine(e);
				throw;
			}
		}
		public Task<Voter> LoginVoter(Voter voter)
		{
			try
			{
				return _electionservice.Voters.Login(voter);

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
				return _electionservice.Comissioners.ConfigureElection();

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
				await _electionservice.Comissioners.AddNewConfiguration(setting);

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
				await _electionservice.Candidates.InsertAsync(candidate);

			}
			catch (Exception e)
			{
				Console.WriteLine(e);
				throw;
			}
		}
		public async Task SaveComissioner(Comissioner comissioner)
		{
			comissioner.Password = BCrypt.Net.BCrypt.HashPassword(comissioner.Password);
			if (comissioner.Id==0)
			{
				try
				{
					await _electionservice.Comissioners.InsertAsync(comissioner);

				}
				catch (Exception e)
				{
					Console.WriteLine(e);
					throw;
				}
			}
			else
			{
				try
				{
					await _electionservice.Comissioners.UpdateAsync(comissioner);

				}
				catch (Exception e)
				{
					Console.WriteLine(e);
					throw;
				}
			}
			
		}
	   
		public Task<IEnumerable<Candidate>> GetAllCandidates()
		{
			try
			{
				return _electionservice.Candidates.GetAllAsync();
				
			}
			catch (Exception e)
			{
				Console.WriteLine(e);
				throw;
			}
		}
		
		public Task<Candidate> SaveCandidate(Candidate candidate)
		{
			try
			{
				if (candidate.Id == 0)
				{
					return _electionservice.Candidates.InsertAsync(candidate);
				}
				else
				{
					return _electionservice.Candidates.UpdateAsync(candidate);

				}
			}
			catch (Exception e)
			{
				Console.WriteLine(e);
				throw;
			}
			
		}
	   
		public void RemoveCandidate(Candidate candidate)
		{
			try
			{
				_electionservice.Candidates.Delete(candidate);

			}
			catch (Exception e)
			{
				Console.WriteLine(e);
				throw;
			}
		}
		#endregion
		#region Position
		public Task<Position> AddPosition(Position position)
		{
			try
			{
				var data = _electionservice.Positions.InsertAsync(position);
				return data;
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
				return await _electionservice.Positions.GetByIdAsync(positionid);

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
				return _electionservice.Positions.GetPositionsWithDetails();

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
				return _electionservice.Positions.GetAll();

			}
			catch (Exception e)
			{
				Console.WriteLine(e);
				throw;
			}
		}
		public Task<IEnumerable<Position>> GetAllPositionsAsync()
		{
			try
			{
				return _electionservice.Positions.GetPositionsWithDetailsAsync();

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
				await _electionservice.Positions.UpdateAsync(position);

			}
			catch (Exception e)
			{
				Console.WriteLine(e);
				throw;
			}
		}
	   
		public void RemovePosition(Position position)
		{
			try
			{
				_electionservice.Positions.Delete(position);

			}
			catch (Exception e)
			{
				Console.WriteLine(e);
				throw;
			}
		}
		#endregion   
		#region User
		public async Task<Comissioner> Login(Comissioner comissioner)
		{
			Comissioner user=new Comissioner();
			if (comissioner.IsChairman)
			{
				try
				{ user= await _electionservice.Comissioners.LoginChairman(comissioner);}
				catch (Exception e)
				{
					Console.WriteLine(e);
					throw;
				}
			}
			else if (comissioner.IsPresident)
			{
				try
				{ user= await _electionservice.Comissioners.LoginPresident(comissioner);}
				catch (Exception e)
				{
					Console.WriteLine(e);
					throw;
				}
			}else if (comissioner.IsAdmin)
			{
				try
				{ user= await _electionservice.Comissioners.LoginAdmin(comissioner);}
				catch (Exception e)
				{
					Console.WriteLine(e);
					throw;
				}
			}
			else
			{
				try
				{  user= await _electionservice.Comissioners.Login(comissioner);}
				catch (Exception e)
				{
					Console.WriteLine(e);
					throw;
				}
			}
			
			return user;
		}
		#endregion

		public async Task<IEnumerable<Candidate>> GetCandidateWithDetails()
		{
			var data = await _electionservice.Candidates.GetAllAsync();

			var candidateWithDetails = data as IList<Candidate> ?? data.ToList();
			foreach (var candidate in candidateWithDetails)
			{
				candidate.Position = _electionservice.Positions.GetById(Convert.ToInt32(candidate.PositionId));
			}
			return candidateWithDetails;
		}
	}
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Univoting.Data;
using UniVoting.Core;
using UniVoting.Services;

namespace Univoting.Services
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
                throw new Exception("Something went wrong. we could not add Users", e);
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
        public async Task<Voter> LoginVoterAsync(Voter voter)
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
        //public static  Task<ElectionConfiguration> ConfigureElectionAsync(int id)
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
        public async Task<ElectionConfiguration> ConfigureElectionAsync()
        {
            try
            {
                return await _context.ElectionConfigurations.FirstOrDefaultAsync();
            }
            catch (Exception e)
            {
                throw new Exception("Something went wrong. we could not retrieve ElectionConfigurations", e);

            }
        }
        public async Task<ElectionConfiguration> AddElectionConfigurationsAsync(ElectionConfiguration electionConfiguration)
        {
            if (electionConfiguration == null) throw new ArgumentNullException(nameof(electionConfiguration));
            try
            {
                var data = await _context.ElectionConfigurations.FirstOrDefaultAsync();
                if (data == null)
                {
                    return await SaveElectionConfiguration(electionConfiguration);

                }
                _context.Remove(data);
                return await SaveElectionConfiguration(electionConfiguration);
            }
            catch (Exception e)
            {
                throw new Exception("Something went wrong. we could not add ElectionConfigurations", e);

            }
        }

        private async Task<ElectionConfiguration> SaveElectionConfiguration(ElectionConfiguration electionConfiguration)
        {
            await _context.ElectionConfigurations.AddAsync(electionConfiguration);
            await _context.SaveChangesAsync();
            return electionConfiguration;
        }

        #endregion
        #region Candidate
        public async Task<Candidate> AddCandidateAsync(Candidate candidate)
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
        public async Task<Commissioner> SaveCommissionerAsync(Commissioner commissioner)
        {
            if (commissioner == null) throw new ArgumentNullException(nameof(commissioner));
            if (commissioner.Id == 0)
            {
                try
                {
                    await _context.Commissioners.AddAsync(commissioner);
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
                    _context.Commissioners.Update(commissioner);
                    await _context.SaveChangesAsync();
                    return commissioner;

                }
                catch (Exception e)
                {
                    throw new Exception("Something went wrong. we could not update commisioner", e);

                }
            }
        }

        public async Task<List<Faculty>> GetFacultiesAsync()
        {
            try
            {
                return await _context.Faculties.ToListAsync();
            }
            catch (Exception e)
            {
                throw new Exception("Something went wrong. we could not get Faculties", e);

            }
        }

        public async Task<IList<Rank>> GetAllRanks()
        {
            try
            {
                return await _context.Ranks.AsNoTracking().ToListAsync();
            }
            catch (Exception e)
            {
                throw new Exception("Something went wrong. we could not get ranks", e);

            }
        }

        public async Task<IEnumerable<Candidate>> GetAllCandidatesAsync()
        {
            try
            {
                return await _context.Candidates.AsNoTracking().ToListAsync();

            }
            catch (Exception e)
            {
                throw new Exception("Something went wrong. we could not update commissioner", e);

            }
        }

        public async Task<IEnumerable<Rank>> GetAllRanksAsync()
        {
            try
            {
                return await _context.Ranks.AsNoTracking().ToListAsync();
            }
            catch (Exception e)
            {
                throw new Exception("Something went wrong. we could retrieve ranks", e);

            }
        }

        public async Task<Candidate> SaveCandidateAsync(Candidate candidate)
        {
            try
            {
                if (candidate.Id == 0)
                {
                    await _context.Candidates.AddAsync(candidate);

                }
                else
                {

                    var dbCandidate = await _context.Candidates.FirstOrDefaultAsync(x => x.Id == candidate.Id);

                    if (dbCandidate!=null)
                    {
                        dbCandidate.PositionId = candidate.PositionId;
                        dbCandidate.CandidateName = candidate.CandidateName;
                        dbCandidate.CandidatePicture = candidate.CandidatePicture;
                        dbCandidate.RankId = candidate.RankId;
                       
                        _context.Candidates.Update(dbCandidate);
                    }

                }

                await _context.SaveChangesAsync();
                return candidate;
            }
            catch (Exception e)
            {
                throw new Exception("Something went wrong. we could not add or update candidate", e);

            }

        }
        public async Task<IEnumerable<Candidate>> GetCandidateWithDetailsAsync()
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
        public void RemoveCandidateAsync(Candidate candidate)
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
        public async Task<Position> AddPositionAsync(Position position)
        {
            try
            {
                if (position.Id==default(int))
                {
                await _context.Positions.AddAsync(position);

                }
                else
                {
                    var pos = _context.Positions.Find(position.Id);
                    pos.PositionName = position.PositionName;
                    pos.FacultyId = position.FacultyId;
                     _context.Positions.Update(pos);

                }

                await _context.SaveChangesAsync();
                return position;
            }
            catch (Exception e)
            {
                throw new Exception("Something went wrong. we could not add position", e);

            }

        }
        public async Task<Position> GetPositionAsync(int positionId)
        {
            try
            {
                return await _context.Positions.FirstOrDefaultAsync(x => x.Id == positionId);

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
                return await _context.Positions.Include(p => p.Candidates).OrderBy(x=>x.RankId).ToListAsync();

            }
            catch (Exception e)
            {
                throw new Exception("Something went wrong. we could not retrieve position with candidates", e);

            }
        }
        public async Task<List<Position>> GetAllPositionsAsync(bool includeFaculty=false)
        {


            try
            {
                if (includeFaculty)
                {
                return await _context.Positions.Include(x=>x.Faculty).OrderBy(x => x.RankId).ToListAsync();

                }
                return await _context.Positions.Include(p => p.Candidates).ToListAsync();

            }
            catch (Exception e)
            {
                throw new Exception("Something went wrong. we could not retrieve position with candidates", e);

            }
        }

        public async Task<Position> UpdatePositionAsync(Position position)
        {
            try
            {
                _context.Positions.Update(position);
                await _context.SaveChangesAsync();
                return position;
            }
            catch (Exception e)
            {
                throw new Exception("Something went wrong. we could not update position", e);

            }
        }

        public async Task RemovePositionAsync(Position position)
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
        public async Task<Commissioner> LoginAsync(Commissioner commissioner)
        {
            if (commissioner == null) throw new ArgumentNullException(nameof(commissioner));
            if (commissioner.IsChairman)
            {
                try
                { return await _context.Commissioners.FirstOrDefaultAsync(x => x.IsChairman && x.UserName.Equals(commissioner.UserName) && x.Password.Equals(commissioner.Password)); }
                catch (Exception e)
                {
                    throw new Exception("Something went wrong. could not log in as chairman", e);

                }
            }
            else if (commissioner.IsPresident)
            {
                try
                { return await _context.Commissioners.FirstOrDefaultAsync(x => x.IsPresident && x.UserName.Equals(commissioner.UserName) && x.Password.Equals(commissioner.Password)); }
                catch (Exception e)
                {
                    throw new Exception("Something went wrong. could not log in as president", e);

                }
            }
            else if (commissioner.IsAdmin)
            {
                try
                { return await _context.Commissioners.FirstOrDefaultAsync(x => x.IsAdmin && x.UserName.Equals(commissioner.UserName) && x.Password.Equals(commissioner.Password)); }
                catch (Exception e)
                {
                    throw new Exception("Something went wrong. could not log in as admin", e);

                }
            }
            else if (commissioner.IsPresident)
            {
                try
                { return await _context.Commissioners.FirstOrDefaultAsync(x => x.IsPresident && x.UserName.Equals(commissioner.UserName) && x.Password.Equals(commissioner.Password)); }
                catch (Exception e)
                {
                    throw new Exception("Something went wrong. could not log in as president", e);

                }
            }
            else
            {
                try
                {
                    var result = await _context.Commissioners.FirstOrDefaultAsync(x => x.UserName.Equals(commissioner.UserName, StringComparison.InvariantCultureIgnoreCase)
                                                                                     && x.Password.Equals(commissioner.Password));
                    return result;
                }
                catch (Exception e)
                {
                    throw new Exception("Something went wrong. could not log in ", e);

                }
            }
        }
        #endregion


    }
}
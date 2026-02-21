using System.Collections.Generic;
using System.Threading.Tasks;
using UniVoting.Model;

namespace UniVoting.Data.Interfaces;

public interface ICandidateRepository
{
    Task<IEnumerable<Candidate>> GetAllAsync();
    Task<Candidate> InsertAsync(Candidate candidate);
    Task<Candidate> UpdateAsync(Candidate candidate);
    void Delete(Candidate candidate);
}

namespace UniVoting.Data.Interfaces;

public interface IService
{
    ICandidateRepository Candidates { get; }
    IVoterRepository Voters { get; }
    IPositionRepository Positions { get; }
    IComissionerRepository Comissioners { get; }
}
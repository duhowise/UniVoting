using System.Collections.Concurrent;
using System.Collections.Generic;
using UniVoting.Model;

namespace UniVoting.Client;

public interface IClientSessionService
{
    Voter? CurrentVoter { get; set; }
    Stack<Position> Positions { get; set; }
    ConcurrentBag<Vote> Votes { get; set; }
    ConcurrentBag<SkippedVotes> SkippedVotes { get; set; }
    Position? CurrentPosition { get; set; }
    Candidate? CurrentCandidate { get; set; }
}

public class ClientSessionService : IClientSessionService
{
    public Voter? CurrentVoter { get; set; }
    public Stack<Position> Positions { get; set; } = new();
    public ConcurrentBag<Vote> Votes { get; set; } = new();
    public ConcurrentBag<SkippedVotes> SkippedVotes { get; set; } = new();
    public Position? CurrentPosition { get; set; }
    public Candidate? CurrentCandidate { get; set; }
}

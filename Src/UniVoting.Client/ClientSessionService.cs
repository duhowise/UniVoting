using System.Collections.Concurrent;
using System.Collections.Generic;
using UniVoting.Model;
using UniVoting.Services;

namespace UniVoting.Client;

public class ClientSessionService : IClientSessionService
{
    public Voter? CurrentVoter { get; set; }
    public Stack<Position> Positions { get; set; } = new();
    public ConcurrentBag<Vote> Votes { get; set; } = new();
    public ConcurrentBag<SkippedVotes> SkippedVotes { get; set; } = new();
    public Position? CurrentPosition { get; set; }
    public Candidate? CurrentCandidate { get; set; }
}
